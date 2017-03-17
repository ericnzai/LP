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
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class VAConversionToolController : BaseController
    {
        // POST: Eylea/VAConversionTool
        public async Task<JsonResult> SaveConversionToolFile(string fileName, string comment)
        {

            var temp_Path = Server.MapPath(string.Format("{0}/temp/{1}/", ConfigurationManager.AppSettings["UploadConversionToolPath"], ConstantProvider.GlobalCulture));
            var perm_Path = Server.MapPath(string.Format("{0}/perm/{1}/", ConfigurationManager.AppSettings["UploadConversionToolPath"], ConstantProvider.GlobalCulture));
            
            // delete old conversion tool file
            var di = new DirectoryInfo(perm_Path);
            if (di.Exists)
            {
                foreach (FileInfo fileInfo in di.GetFiles())
                {
                    fileInfo.Delete();
                }
            }
            else
            {
                di.Create();
            }

            di = new DirectoryInfo(temp_Path);
            if (di.Exists)
            {
                foreach (FileInfo fileInfo in di.GetFiles())
                {
                    fileInfo.CopyTo(perm_Path + fileInfo.Name);
                    fileInfo.Delete();
                }
            }

            var requestVAConversionTool = new VAConversionToolRequestContract()
            {
                FileName = fileName, FileDownloadPath = perm_Path + fileName, Comments = comment
            };
            var conversionToolResponseContract =
                await
                    PostRequestToService<VAConversionToolRequestContract, VAConversionToolResponseContract>(
                        "api/content/conversion-tool/add", requestVAConversionTool);


            var vAConversionToolViewModel = new ConversionToolHistoryViewModel()
            {
                HistoryViewModel = new List<HistoryViewModel>(
                    conversionToolResponseContract.ConversionToolHistory.Select(c => new HistoryViewModel()
                    {
                        CreatedByUser = c.CreatedByUser,
                        DateModified = c.DateCreated,
                        Text = c.FileName
                    }).ToList())
            };

            conversionToolResponseContract.History =
                RenderRazorViewToString("~/Areas/Eylea/Views/Shared/Partial/_HistoryListItems.cshtml", vAConversionToolViewModel);

            return Json(conversionToolResponseContract, JsonRequestBehavior.AllowGet);
        }

        //POST:Eylea/VAConvertionTool
        public async Task<JsonResult> UploadFile()
        {
            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
            var path = Server.MapPath(string.Format("{0}/temp/{1}/", ConfigurationManager.AppSettings["UploadConversionToolPath"], ConstantProvider.GlobalCulture));
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

  
        // GET: Eylea/VAConversionTool
        public async Task<PartialViewResult> AddConversionToolPdfFileView()
        {
            var requestVAConversionTool = new VAConversionToolPathRequestContract()
            {
                PermPath = Server.MapPath(string.Format("{0}/perm/{1}/", ConfigurationManager.AppSettings["UploadConversionToolPath"], ConstantProvider.GlobalCulture))
            };

            var vAConversionToolResponseContract =
                await
                    PostRequestToService<VAConversionToolPathRequestContract, VAConversionToolDetailsResponseContract>(
                        "api/content/conversion-tool", requestVAConversionTool);
            
            // check if there is a pdf conversion toll added for this culture
            // if so populate the view model accordingly

            var vAConversionToolViewModel = new VAConversionToolViewModel()
            {
                Culture = vAConversionToolResponseContract.Culture,
                CultureDisplayName = vAConversionToolResponseContract.CultureDisplayName,
                FileName = vAConversionToolResponseContract.FileName,
                DownloadFilePath = vAConversionToolResponseContract.DownloadFilePath,
                Content = vAConversionToolResponseContract.Content,
                Comment = vAConversionToolResponseContract.Comments,
                SaveUrl = string.Format("{0}/Eylea/VAConversionTool/UploadFile", ConfigurationManager.AppSettings["BaseUrl"]),
                IsNew = string.IsNullOrEmpty(vAConversionToolResponseContract.FileName),
                ConversionToolHistoryViewModel = new ConversionToolHistoryViewModel()
                {
                    HistoryViewModel = vAConversionToolResponseContract.VAConversionTools != null ? new List<HistoryViewModel>(
                        vAConversionToolResponseContract.VAConversionTools.Select(c=>new HistoryViewModel()
                        {
                            CreatedByUser = c.CreatedByUser,
                            DateModified = c.DateCreated,
                            Text = c.FileName
                        }).ToList()) : new List<HistoryViewModel>()
                } 
            };

            return PartialView("~/Areas/Eylea/Views/VAConversionTool/_AddConversionToolPdfFile.cshtml", vAConversionToolViewModel);
        }

        // GET: Eylea/VAConversionTool
        public async Task<JsonResult> DownloadPdf()
        {
            var path = Server.MapPath(string.Format("{0}/perm/", ConfigurationManager.AppSettings["UploadConversionToolPath"]));
            var requestVAConversionToolDownload = new VAConversionToolDownloadPdfRequestContract() {Path = path};

            var vAConversionToolDownloadResponseContract =
                await
                    PostRequestToService<VAConversionToolDownloadPdfRequestContract, VAConversionToolDownloadPdfResponseContract>(
                        "api/content/conversion-tool/download", requestVAConversionToolDownload);

            return Json(vAConversionToolDownloadResponseContract, JsonRequestBehavior.AllowGet);
        }
    }
}