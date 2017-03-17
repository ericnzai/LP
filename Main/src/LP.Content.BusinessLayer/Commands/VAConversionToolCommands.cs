using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Interfaces.Wrappers;
using LP.Api.Shared.Providers;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.BusinessLayer.Commands
{
    public class VAConversionToolCommands : IVAConversionToolCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IEncryptionHandler _encryptionHandler;
        private readonly ICultureProvider _cultureProvider;
        private readonly IServerWrapper _serverWrapper;
        private readonly IPdfContent _pdfContent;
        private readonly ICultureMenuCommands _cultureMenuCommands;
        public VAConversionToolCommands(IBaseCommands baseCommands, IEncryptionHandler encryptionHandler, ICultureProvider cultureProvider, IServerWrapper serverWrapper, IPdfContent pdfContent, ICultureMenuCommands cultureMenuCommands)
        {
            _baseCommands = baseCommands;
            _encryptionHandler = encryptionHandler;
            _cultureProvider = cultureProvider;
            _serverWrapper = serverWrapper;
            _pdfContent = pdfContent;
            _cultureMenuCommands = cultureMenuCommands;
        }

        public async Task<VAConversionToolDetailsResponseContract> GetVAConversionTool(string culture, UserDetails userDetails, string permPath)
        {
            var vAConversionTools = await _baseCommands.GetWithIncludesAsync<VAConversionTool>(c=>c.CreatedByUser);

            var vaConversionToolsByCulture =
                vAConversionTools.Where(c => c.Culture == culture && (c.Status == Status.Live || c.Status == Status.TranslationInProgress));
            
           
            var result = new VAConversionToolDetailsResponseContract();
            result.Culture = culture;
            result.CultureDisplayName = await _cultureProvider.GetCultureDisplayName(ConstantProvider.GlobalCulture);

            var availableCultures = await _cultureMenuCommands.GetAvailableCulturesExceptEnglishGlobal(userDetails);
            result.Languages = availableCultures.AvailableCultures.Select(c => new LanguageContract()
            {
                Culture = c.Key,
                CultureDisplayName = c.Value
            }).ToList();
           
            var vAConversionTool = vaConversionToolsByCulture.OrderByDescending(ct => ct.DateCreated).FirstOrDefault();
            if (vAConversionTool != null)
            {
                result.FileName = vAConversionTool.FileName;
                result.Content = _pdfContent.GetPdfFileContent(string.Format("{0}{1}", permPath , vAConversionTool.FileName));
                result.Comments = vAConversionTool.Comments;
                result.LastUpdated = vAConversionTool.DateCreated;
                result.CreatedByUser = _encryptionHandler.DecryptString(vAConversionTool.CreatedByUser.DisplayName);
                result.IsTranslationCompleted = vAConversionTool.Status == Status.Live ? true : false;
                result.VAConversionTools = new List<VAConversionToolContract>();
                var conversionTools = vaConversionToolsByCulture.Where(c => c.VAConversionToolId != vAConversionTool.VAConversionToolId)
                            .OrderByDescending(ct => ct.DateCreated).AsEnumerable();
                if (conversionTools != null && conversionTools.Any())
                {
                    result.VAConversionTools = conversionTools.ToList().Select(ct => new VAConversionToolContract()
                    {
                        CreatedByUser = _encryptionHandler.DecryptString(ct.CreatedByUser.DisplayName),
                        DateCreated = ct.DateCreated,
                        FileName = ct.FileName
                    }).ToList();
                }
            }

            return result;
        }

        public async Task<VAConversionToolResponseContract> SaveVAConversionTool(string culture, string fileName, string fileDownloadPath, string comments)
        {
            var conversionTool = new VAConversionTool() { FileName = fileName, Culture = culture, Status = Status.Live, Comments = comments, DateCreated = DateTime.UtcNow, CreatedByUserId = 1 };

            _baseCommands.Add(conversionTool);
            _baseCommands.SaveChanges();

            // return updated history
            var conversionTools = await _baseCommands.GetWithIncludesAsync<VAConversionTool>(c=>c.CreatedByUser);
            var vaConversionToolsByCulture =
                conversionTools.Where(c => c.Culture == culture && (c.Status == Status.Live || c.Status == Status.TranslationInProgress));
            var conversionToolsHistory = vaConversionToolsByCulture.Where(c => c.VAConversionToolId != conversionTool.VAConversionToolId)
                .OrderByDescending(c => c.DateCreated).AsEnumerable();

            return new VAConversionToolResponseContract()
            {
                Content = _pdfContent.GetPdfFileContent(fileDownloadPath),
                FileName = fileName,
                ConversionToolHistory = conversionToolsHistory.Select(c => new VAConversionToolContract()
            {
                CreatedByUser = _encryptionHandler.DecryptString(c.CreatedByUser.DisplayName),
                DateCreated = c.DateCreated,
                FileName = c.FileName
            }).ToList() };
        }

        public async Task<VAConversionToolDownloadPdfResponseContract> GetConversionToolTranslationFilePath(string culture, string path)
        {
            var conversionToolDownloadPdfResponseContract = new VAConversionToolDownloadPdfResponseContract();
            var conversionToolTranslations = _baseCommands.GetAllAsync<VAConversionTool>();

            var conversionToolTranslation = conversionToolTranslations.Result.Where(c => (c.Status == Status.Live || c.Status == Status.TranslationInProgress)).OrderByDescending(c => c.DateCreated)
                .FirstOrDefault(c => c.Culture == culture);

            if (conversionToolTranslation != null)
            {
                conversionToolDownloadPdfResponseContract.FileName = conversionToolTranslation.FileName;
                conversionToolDownloadPdfResponseContract.Content = _pdfContent.GetPdfFileContent(string.Format("{0}/{1}/{2}", path, culture, conversionToolTranslation.FileName));
            }

            return conversionToolDownloadPdfResponseContract;
        }
    }
}
