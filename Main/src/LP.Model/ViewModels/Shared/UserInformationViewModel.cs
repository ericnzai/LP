using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.Model.ViewModels.Shared
{
    public class UserInformationViewModel
    {
        public TranslatedItem FieldOfEmploymenTranslatedItem { get; set; }
        public string FieldOfEmployment { get; set; }
        public string UserDisplayName { get; set; }
        public string UserCountry { get; set; }
      
    }
}
