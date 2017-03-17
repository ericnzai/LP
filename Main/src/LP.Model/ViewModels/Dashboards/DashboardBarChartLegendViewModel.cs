using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.Model.ViewModels.Dashboards
{
    public class DashboardBarChartLegendViewModel
    {
        public TranslatedItem RegisteredTranslatedItem { get; set; }
        public TranslatedItem StartedTranslatedItem { get; set; }
        public TranslatedItem InProgressTranslatedItem { get; set; }
        public TranslatedItem CertifiedTranslatedItem { get; set; }
    }
}