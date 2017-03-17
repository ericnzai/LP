using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Common.Content
{
    public class DataPdf
    {
        public DataPdf()
        {
            GlossaryItems = new List<GlossaryItem>();
        }

        public string GlossaryTitleStr { get; set; }
        public string MsgSorryNoResultsStr { get; set; }
        public string FilterMessage { get; set; }
        public List<GlossaryItem> GlossaryItems { get; set; }
    }
}
