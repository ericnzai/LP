using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface IVAConversionToolTranslationCommands
    {
        Task<VAConversionToolTranslationDetailsResponseContract> GetVAConversionToolTranslation(string culture, string path);

        Task<VAConversionToolTranslationDetailsResponseContract> SaveVAConversionToolTranslation(string culture, string fileName,
            string permPath, string tempPath, bool isTranslationCompleted);
    }
}
