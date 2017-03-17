using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Model.ViewModels.Common
{
    public class TrainingAreaViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StatusId { get; set; }
        public IEnumerable<GroupMainInfoViewModel> GroupsInfoInMainCulture { get; set; }
        public int Id { get; set; }   
    }
}
