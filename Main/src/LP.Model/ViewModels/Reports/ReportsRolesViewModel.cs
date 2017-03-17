using  System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams;

namespace LP.Model.ViewModels.Reports
{
    public class ReportsRolesViewModel
    {
        public ReportsRolesViewModel()
        {
            ReportRoles = new List<Role>();
        }

        public List<Role> ReportRoles { get; set; }
    }
}
