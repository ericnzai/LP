using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LP.Api.Shared.Providers;
using LP.Model.ViewModels.Shared;
using LP.Model.ViewModels.VAConversionTool;
using LP.Model.ViewModels.VAConversionToolTranslation;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class VAConversionToolTranslationController : BaseController
    {
        // GET: Eylea/VAConversionToolTranslation
        public async Task<PartialViewResult> ConversionToolTranslationView()
        {
            var requestVAConversionTool = new VAConversionToolPathRequestContract
            {
                PermPath =
                    Server.MapPath(string.Format("{0}/perm/{1}/",
                        ConfigurationManager.AppSettings["UploadConversionToolPath"], ConstantProvider.GlobalCulture))
            };

            var vAConversionToolResponseContract =
                await
                    PostRequestToService<VAConversionToolPathRequestContract, VAConversionToolDetailsResponseContract>(
                        "api/content/conversion-tool", requestVAConversionTool);

            var conversionToolTranslationViewModel = new VAConversionToolTranslationViewModel()
            {
                NewContentViewModel = new VAConversionToolTranslationNewViewModel()
                {
                    Culture = ConstantProvider.GlobalCulture,
                    SaveUrl = string.Format("{0}/Eylea/VAConversionToolTranslation/UploadFile", ConfigurationManager.AppSettings["BaseUrl"]),
                    LastUpdated = vAConversionToolResponseContract.LastUpdated,
                    CreatedByUser = vAConversionToolResponseContract.CreatedByUser,
                    IsTranslationCompleted = vAConversionToolResponseContract.IsTranslationCompleted
                },
                OriginalContentViewModel = new VAConversionToolTranslationOriginalViewModel()
                {
                    FileName = vAConversionToolResponseContract.FileName,
                    Culture = vAConversionToolResponseContract.Culture,
                    CultureDisplayName = vAConversionToolResponseContract.CultureDisplayName,
                    Comment = vAConversionToolResponseContract.Comments,
                    Content = vAConversionToolResponseContract.Content
                },
                LanguageSelectorViewModel = new LanguageSelectorViewModel()
                {
                    Languages = vAConversionToolResponseContract.Languages.Select(l=> new LanguageViewModel()
                    {
                        Culture = l.Culture,
                        CultureDisplayName = l.CultureDisplayName
                    }).ToList()
                },
                ConversionToolHistoryViewModel = new ConversionToolHistoryViewModel()
                {
                    HistoryViewModel = vAConversionToolResponseContract.VAConversionTools != null ? new List<HistoryViewModel>(
                        vAConversionToolResponseContract.VAConversionTools.Select(c => new HistoryViewModel()
                        {
                            CreatedByUser = c.CreatedByUser,
                            DateModified = c.DateCreated,
                            Text = c.FileName
                        }).ToList()) : new List<HistoryViewModel>()
                } 
            };

            return PartialView("~/Areas/Eylea/Views/VAConversionToolTranslation/_ConversionToolTranslationView.cshtml", conversionToolTranslationViewModel);
        }

        // POST: Eylea/VAConversionToolTranslation
        public async Task<PartialViewResult> GetConversionToolNewTranslationWithHistoryView(string culture)
        {
            var requestVAConversionToolTranslation = new VAConversionToolTranslationRequestContract()
            {
                Culture = culture,
                Path = Server.MapPath(string.Format("{0}/perm/{1}/", ConfigurationManager.AppSettings["UploadConversionToolPath"], culture))
            };
            var vAConversionToolTranslationResponseContract =
                await
                    PostRequestToService<VAConversionToolTranslationRequestContract, VAConversionToolTranslationDetailsResponseContract>(
                        "api/content/conversion-tool-translation", requestVAConversionToolTranslation);

            var requestVAConversionTool = new VAConversionToolPathRequestContract
            {
                PermPath =
                    Server.MapPath(string.Format("{0}/perm/{1}/",
                        ConfigurationManager.AppSettings["UploadConversionToolPath"], ConstantProvider.GlobalCulture))
            };

            var vAConversionToolResponseContract =
                await
                    PostRequestToService<VAConversionToolPathRequestContract, VAConversionToolDetailsResponseContract>(
                        "api/content/conversion-tool", requestVAConversionTool);

            var conversionToolTranslationViewModel = new VAConversionToolTranslationViewModel()
            {
                NewContentViewModel = new VAConversionToolTranslationNewViewModel()
                {
                    FileName = vAConversionToolTranslationResponseContract.FileName,
                    Culture = culture,
                    CultureDisplayName = vAConversionToolTranslationResponseContract.CultureDisplayName,
                    Content = vAConversionToolTranslationResponseContract.Content,
                    SaveUrl = string.Format("{0}/Eylea/VAConversionToolTranslation/UploadFile", ConfigurationManager.AppSettings["BaseUrl"]),
                    LastUpdated = vAConversionToolTranslationResponseContract.LastUpdated,
                    CreatedByUser = vAConversionToolTranslationResponseContract.CreatedByUser,
                    IsTranslationCompleted = vAConversionToolTranslationResponseContract.IsTranslationCompleted
                },
                OriginalContentViewModel = new VAConversionToolTranslationOriginalViewModel()
                {
                    FileName = vAConversionToolResponseContract.FileName,
                    Culture = vAConversionToolResponseContract.Culture,
                    CultureDisplayName = vAConversionToolResponseContract.CultureDisplayName,
                    Comment = vAConversionToolResponseContract.Comments,
                    Content = vAConversionToolResponseContract.Content
                },
                ConversionToolHistoryViewModel = new ConversionToolHistoryViewModel()
                {
                    HistoryViewModel = vAConversionToolTranslationResponseContract.VAConversionToolTranslations != null ? new List<HistoryViewModel>(
                        vAConversionToolTranslationResponseContract.VAConversionToolTranslations.Select(c => new HistoryViewModel()
                        {
                            CreatedByUser = c.CreatedByUser,
                            DateModified = c.DateCreated,
                            Text = c.FileName
                        }).ToList()) : new List<HistoryViewModel>()
                }
            };

            return PartialView("~/Areas/Eylea/Views/VAConversionToolTranslation/Partial/_ConversionToolNewTranslationWithHistory.cshtml",
                conversionToolTranslationViewModel);
        }

        //POST:Eylea/VAConvertionTool
        public async Task<JsonResult> UploadFile(string culture)
        {
            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
            var path = Server.MapPath(string.Format("{0}/temp/{1}/", ConfigurationManager.AppSettings["UploadConversionToolPath"], culture));
            bool folder = Directory.Exists(path);
            if (!folder)
            {
                Directory.CreateDirectory(path);
            }
            var di = new DirectoryInfo(path);
            foreach (FileInfo fileInfo in di.GetFiles())
            {
                fileInfo.Delete();
            }

            if (file != null)
            {
                file.SaveAs(string.Format("{0}{1}", path, file.FileName));
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Eylea/VAConversionTool
        public async Task<JsonResult> SaveConversionToolFile(string fileName, string comment, string culture, bool IsTranslationCompleted)
        {
            var tempPath = Server.MapPath(string.Format("{0}/temp/{1}/", ConfigurationManager.AppSettings["UploadConversionToolPath"], culture));
            var permPath = Server.MapPath(string.Format("{0}/perm/{1}/", ConfigurationManager.AppSettings["UploadConversionToolPath"], culture));

            var requestVAConversionToolTranslation = new VAConversionToolTranslationSaveRequestContract()
            {
                Culture = culture,
                FileName = fileName,
                PermPath = permPath,
                TempPath = tempPath,
                Comments = comment,
                IsTranslationCompleted = IsTranslationCompleted
            };
            var conversionToolTranslationResponseContract =
                await
                    PostRequestToService<VAConversionToolTranslationSaveRequestContract, VAConversionToolTranslationDetailsResponseContract>(
                        "api/content/conversion-tool-translation/add", requestVAConversionToolTranslation);

            if (conversionToolTranslationResponseContract.VAConversionToolTranslations != null)
            {
                var vAConversionToolViewModel = new ConversionToolHistoryViewModel()
                {
                    HistoryViewModel = new List<HistoryViewModel>(
                        conversionToolTranslationResponseContract.VAConversionToolTranslations.Select(
                            c => new HistoryViewModel()
                            {
                                CreatedByUser = c.CreatedByUser,
                                DateModified = c.DateCreated,
                                Text = c.FileName
                            }).ToList())
                };

                conversionToolTranslationResponseContract.History =
                    RenderRazorViewToString("~/Areas/Eylea/Views/Shared/Partial/_HistoryListItems.cshtml",
                        vAConversionToolViewModel);
            }

            return Json(conversionToolTranslationResponseContract, JsonRequestBehavior.AllowGet);
        }
    }
}