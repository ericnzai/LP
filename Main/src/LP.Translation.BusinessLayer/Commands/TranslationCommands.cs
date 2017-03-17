using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Translation;
using LP.Api.Shared.Interfaces.Data;

using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Translation;

namespace LP.Translation.BusinessLayer.Commands
{
    public class TranslationCommands : ITranslationCommands
    {
        private readonly IBaseCommands _baseCommands;

        public TranslationCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        private IQueryable<ResourceLocalization> _resourceLocalizations;

        public async Task<TranslationResponseContract> GetTranslatedItems(TranslationRequestContract translationRequestContract)
        {
            _resourceLocalizations = await _baseCommands.GetAllAsync<ResourceLocalization>();

            var translatedItems = new List<TranslatedItem>();

            foreach (var translationRequest in translationRequestContract.TranslationRequests)
            {
                translatedItems.Add(GetTranslatedItem(translationRequest, translationRequestContract.Culture));
            }

            var translationResponseContract = new TranslationResponseContract
            {
                TranslatedItems = translatedItems
            };

            return translationResponseContract;
        }

        private TranslatedItem GetTranslatedItem(TranslationRequest translationRequest, string culture)
        {
            var resourceLocalizations =
                _resourceLocalizations.Where(
                    a =>
                        a.ResourceId == translationRequest.ResourceId && a.ResourceSet == translationRequest.ResourceSet);

            var translatedItem = new TranslatedItem
            {
                ResourceId = translationRequest.ResourceId,
                ResourceSet = translationRequest.ResourceSet
            };

            var translatedLocalization = resourceLocalizations.FirstOrDefault(a => a.LocaleId == culture);

            if (translatedLocalization != null)
            {
                translatedItem.TranslatedValue = translatedLocalization.Value;

                return translatedItem;
            }
            
            var globalLocalization = resourceLocalizations.FirstOrDefault(a => a.LocaleId == string.Empty);

            if (globalLocalization != null)
            {
                translatedItem.TranslatedValue = globalLocalization.Value;

                return translatedItem;
            }

            translatedItem.TranslatedValue = translationRequest.ResourceId;

            return translatedItem;
        }
    }
}
