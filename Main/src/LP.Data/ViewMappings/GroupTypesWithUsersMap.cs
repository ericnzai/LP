using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.EntityModels.Views;

namespace LP.Data.ViewMappings
{
    public class GroupTypesWithUsersMap : EntityTypeConfiguration<GroupTypesWithUsers>
    {
        public GroupTypesWithUsersMap()
        {
            ToTable("GroupTypesWithUsers");

            HasKey(t => new { t.GroupTypeId, t.userId});
        }
    }
}
