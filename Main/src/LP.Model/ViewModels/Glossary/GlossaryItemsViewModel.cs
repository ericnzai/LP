using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.Model.ViewModels.Glossary
{
    public class GlossaryItemsViewModel
    {
        public GlossaryItemsViewModel()
        {
            GlossaryItems = new List<GlossaryItem>();
        }

        public List<GlossaryItem> GlossaryItems { get; set; }
    }
}
