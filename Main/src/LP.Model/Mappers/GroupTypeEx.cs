using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.Model.ViewModels.Group;

namespace LP.Model.Mappers
{
    public static class GroupTypeEx
    {
        public static GroupTypeViewModel ToViewModel(this ltl_GroupType groupType)
        {
            if (groupType == null) return null;

            return new GroupTypeViewModel
            {
                GroupTypeId = groupType.ID,
                SortOrder = groupType.SortOrder,
                Description = groupType.Description,
                Name = groupType.Name
            };
        }

        public static List<GroupTypeViewModel> ToViewModels(this IEnumerable <ltl_GroupType> groupTypes)
        {
            if (groupTypes == null) return null;
            var groupTypeViewModels = new List<GroupTypeViewModel>();

            foreach (var groupType in groupTypes)
            {
                groupTypeViewModels.Add(new GroupTypeViewModel
            {
                GroupTypeId = groupType.ID,
                SortOrder = groupType.SortOrder,
                Description = groupType.Description,
                Name = groupType.Name
            });
            }


            return groupTypeViewModels;
        }
    }
}
