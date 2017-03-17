using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.Model.ViewModels.Dashboards
{
    public class CertificationOverviewViewModel
    {
        public CertificationOverviewViewModel()
        {
            DashboardBarChartViewModels = new List<DashboardBarChartViewModel>();
        }

        public TranslatedItem NumberOfUsersRegisteredTranslatedItem { get; set; }
        public List<DashboardBarChartViewModel> DashboardBarChartViewModels { get; set; }
        public int NumberOfUsersRegistered { get; set; }
    }
}
