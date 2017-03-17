using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Request.Translation
{
    public class TranslationRequestContract
    {
        public TranslationRequestContract()
        {
            TranslationRequests = new List<TranslationRequest>();
        }

        public List<TranslationRequest> TranslationRequests { get; set; }
        public string Culture { get; set; }
    }
}
