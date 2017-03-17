using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.Model.Mappers
{
    public static class ResourceLocalizationEx
    {
        public static TranslatedItem ToTranslatedItem(this ResourceLocalization resourceLocalization)
        {
            if (resourceLocalization == null) return null;

            var translatedItem = new TranslatedItem
            {
                ResourceId = resourceLocalization.ResourceId,
                ResourceSet = resourceLocalization.ResourceSet,
                TranslatedValue = resourceLocalization.Value
            };

            return translatedItem;
        }
    }
}
