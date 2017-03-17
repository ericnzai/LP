using System.Threading.Tasks;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface IVAConversionToolCommands
    {
        Task<VAConversionToolDetailsResponseContract> GetVAConversionTool(string culture, UserDetails userDetails, string path);
        Task<VAConversionToolResponseContract> SaveVAConversionTool(string culture, string fileName, string fileDownloadPath, string comments);
        Task<VAConversionToolDownloadPdfResponseContract> GetConversionToolTranslationFilePath(string culture, string path);
    }
}
