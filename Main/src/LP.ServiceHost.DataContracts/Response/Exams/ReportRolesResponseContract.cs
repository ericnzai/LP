using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams;


namespace LP.ServiceHost.DataContracts.Response.Exams
{
    public class ReportRolesResponseContract
    {
        public ReportRolesResponseContract()
        {
            ReportRoles = new List<Role>();
        }
        public  List<Role> ReportRoles { get; set; }
    }
}
