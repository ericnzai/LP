using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.ServiceHost.DataContracts.Request.Content
{
    public class GlossaryFilteredPdfRequestContract
    {
        public List<TranslatedItem> TranslatedItems { get; set; }
        public string Filters { get; set; }
        public string Sort { get; set; }
    }
}
