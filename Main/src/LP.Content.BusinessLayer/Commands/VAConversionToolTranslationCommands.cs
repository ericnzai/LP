using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Interfaces.Wrappers;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.BusinessLayer.Commands
{

    public class VAConversionToolTranslationCommands : IVAConversionToolTranslationCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IEncryptionHandler _encryptionHandler;
        private readonly ICultureProvider _cultureProvider;
        private readonly IPdfContent _pdfContent;
        private readonly IFactoryDirectoryInfoWrapper _factoryDirectoryInfoWrapper;
        public VAConversionToolTranslationCommands(IBaseCommands baseCommands, IEncryptionHandler encryptionHandler, ICultureProvider cultureProvider, IPdfContent pdfContent, IFactoryDirectoryInfoWrapper factoryDirectoryInfoWrapper)
        {
            _baseCommands = baseCommands;
            _encryptionHandler = encryptionHandler;
            _cultureProvider = cultureProvider;
            _pdfContent = pdfContent;
            _factoryDirectoryInfoWrapper = factoryDirectoryInfoWrapper;
        }

        public async Task<VAConversionToolTranslationDetailsResponseContract> GetVAConversionToolTranslation(string culture, string path)
        {
            var vAConversionToolTranslations = await _baseCommands.GetWithIncludesAsync<VAConversionTool>(c => c.CreatedByUser);

            var vaConversionToolsByCulture =
                vAConversionToolTranslations.Where(c => c.Culture == culture && (c.Status == Status.Live || c.Status == Status.TranslationInProgress));
            var vAConversionToolTranslation = vaConversionToolsByCulture.OrderByDescending(ct => ct.DateCreated).FirstOrDefault();

            var result = new VAConversionToolTranslationDetailsResponseContract();
            result.Culture = culture;
            result.CultureDisplayName = await _cultureProvider.GetCultureDisplayName(culture);

            if (vAConversionToolTranslation != null)
            {
                result.FileName = vAConversionToolTranslation.FileName;
                result.Content = _pdfContent.GetPdfFileContent(string.Format("{0}{1}", path, vAConversionToolTranslation.FileName));
                result.VAConversionToolTranslations = new List<VAConversionToolContract>();
                result.CreatedByUser =
                    _encryptionHandler.DecryptString(vAConversionToolTranslation.CreatedByUser.DisplayName);
                result.LastUpdated = vAConversionToolTranslation.DateCreated;
                result.IsTranslationCompleted = vAConversionToolTranslation.Status == Status.Live ? true : false;
                var conversionTools = vaConversionToolsByCulture.Where(c => c.VAConversionToolId != vAConversionToolTranslation.VAConversionToolId)
                            .OrderByDescending(ct => ct.DateCreated).AsEnumerable();
                if (conversionTools != null && conversionTools.Any())
                {
                    result.VAConversionToolTranslations = conversionTools.ToList().Select(ct => new VAConversionToolContract()
                    {
                        CreatedByUser = _encryptionHandler.DecryptString(ct.CreatedByUser.DisplayName),
                        DateCreated = ct.DateCreated,
                        FileName = ct.FileName
                    }).ToList();
                }
            }

            return result;
        }

        public async Task<VAConversionToolTranslationDetailsResponseContract> SaveVAConversionToolTranslation(string culture, string fileName, string permPath, string tempPath, bool isTranslationCompleted)
        {
            var conversionToolTranslation = AddConversionToolTranslation(fileName, culture, isTranslationCompleted);

            DeleteOldConversionToolFromPermanentPath(permPath);

            DeleteExistingTemporaryFileAndCopyToPermanent(tempPath, permPath);

            //CreateNotification();

            return await CreateConversionToolResponseContract(culture, conversionToolTranslation, permPath, fileName);
        }

        private void DeleteOldConversionToolFromPermanentPath(string permPath)
        {
            var permanentPathDirectoryInfo = _factoryDirectoryInfoWrapper.CreateIfNotExists(permPath);
            
            foreach (var fileInfo in permanentPathDirectoryInfo.GetFiles())
            {
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
            }
        }

        private void DeleteExistingTemporaryFileAndCopyToPermanent(string tempPath, string permPath)
        {
            var temporaryPathDirectoryInfo = _factoryDirectoryInfoWrapper.CreateIfNotExists(tempPath);

            foreach (var fileInfo in temporaryPathDirectoryInfo.GetFiles())
            {
                if (!fileInfo.Exists) continue;

                fileInfo.CopyTo(permPath + fileInfo.Name, true);
                fileInfo.Delete();
            }
        }

        private void CreateNotification()
        {
            //// push notification
            //var conversionToolTranslationNotification = new VAConversionToolNotification()
            //{
            //    CreatedByUserId = 1,
            //    DateCreated = DateTime.UtcNow,
            //    VAConversionToolId = conversionTool.VAConversionToolId
            //};
            //_baseCommands.Add(conversionToolTranslationNotification);
            //_baseCommands.SaveChanges();

        }

        public VAConversionTool AddConversionToolTranslation(string fileName, string culture, bool IsTranslationCompleted, int createdByUserId = 1)
        {
            var conversionToolTranslation = new VAConversionTool
            {
                FileName = fileName,
                Status = IsTranslationCompleted ? Status.Live : Status.TranslationInProgress,
                DateCreated = DateTime.UtcNow,
                Culture = culture,
                CreatedByUserId = createdByUserId
            };

            _baseCommands.Add(conversionToolTranslation);
            _baseCommands.SaveChanges();

            conversionToolTranslation.CreatedByUser = _baseCommands.GetById<User>(createdByUserId);

            return conversionToolTranslation;
        }

        private async Task<VAConversionToolTranslationDetailsResponseContract> CreateConversionToolResponseContract(string culture, VAConversionTool conversionToolTranslation, string permPath, string fileName)
        {
            var conversionTools = await _baseCommands.GetWithIncludesAsync<VAConversionTool>(c => c.CreatedByUser);
            var vaConversionToolsByCulture =
                conversionTools.Where(c => c.Culture == culture && (c.Status == Status.Live || c.Status == Status.TranslationInProgress));

            var conversionToolsHistory =
                vaConversionToolsByCulture.Where(c => c.VAConversionToolId != conversionToolTranslation.VAConversionToolId);
            var fileDownloadPath = string.Format("{0}{1}", permPath, fileName);

            var pdfFileContent = _pdfContent.GetPdfFileContent(fileDownloadPath);

            return new VAConversionToolTranslationDetailsResponseContract
            {
                LastUpdated = conversionToolTranslation.DateCreated,
                LastUpdatedDateString = conversionToolTranslation.DateCreated.ToShortDateString(),
                LastUpdatedTimeString = conversionToolTranslation.DateCreated.ToShortTimeString(),
                CreatedByUser =  _encryptionHandler.DecryptString(conversionToolTranslation.CreatedByUser.DisplayName),
                Content = pdfFileContent,
                FileName = fileName,
                VAConversionToolTranslations = conversionToolsHistory.OrderByDescending(c=>c.DateCreated).ToList().Select(c => new VAConversionToolContract()
                {
                    CreatedByUser = _encryptionHandler.DecryptString(c.CreatedByUser.DisplayName),
                    DateCreated = c.DateCreated,
                    FileName = c.FileName
                }).ToList()
            };
        }
    }
}
