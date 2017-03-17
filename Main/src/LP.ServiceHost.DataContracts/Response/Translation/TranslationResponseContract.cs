using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.ServiceHost.DataContracts.Response.Translation
{
    public class TranslationResponseContract
    {
        public TranslationResponseContract()
        {
            TranslatedItems = new List<TranslatedItem>();
        }

        public List<TranslatedItem> TranslatedItems { get; set; } 
    }
}
