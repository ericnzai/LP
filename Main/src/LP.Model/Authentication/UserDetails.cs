using System.Collections.Generic;

namespace LP.Model.Authentication
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; }
        public List<int> CultureRoleIds { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsTranslator { get; set; }
        public List<int> AvailableStatuses { get; set; } 
        public string RoleIdsString
        {
            get { return string.Join(",", RoleIds); }
        }

        public string CultureRoleIdsString
        {
            get { return string.Join(",", CultureRoleIds); }
        }

        public string AvailableStatusesString
        {
            get { return string.Join(",", AvailableStatuses); }
        }
        public string CurrentCulture { get; set; }
    }
}
