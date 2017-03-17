using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.ServiceHost.DataContracts.Request.Content
{
    public class GlossaryPdfRequestContract
    {
        public List<TranslatedItem> TranslatedItems { get; set; }
    }
}
