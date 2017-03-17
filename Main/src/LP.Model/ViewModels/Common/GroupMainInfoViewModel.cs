using System.Collections.Generic;
using LP.EntityModels;

namespace LP.Model.ViewModels.Common
{
    public class GroupMainInfoViewModel
    {
        public int GroupTypeId { get; set; }
        public string GroupTypeName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Culture { get; set; }
        public int? StatusId { get; set; }
        public string ImageUrl { get; set; }
        public int GroupId { get; set; }
        public string FriendlyUrl { get; set; }
        public IEnumerable<EntityModels.Group> AvailableGroups { get; set; }
        public EntityModels.Group MainGroup { get; set; }
    }
}
