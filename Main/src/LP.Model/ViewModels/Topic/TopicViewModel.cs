using LP.EntityModels;
using LP.Model.ViewModels.Shared;

namespace LP.Model.ViewModels.Topic
{
    public class TopicViewModel
    {
        public TopicViewModel()
        {
            TopicCategories = new DropdownViewModel();
            
        }

        public string TopicName { get; set; }
        public string TopicCategory { get; set; }
        public int TopicId { get; set; }
        public int CategoryId { get; set; }

        public TopicCategoryViewModel Category { get; set; }
        
        public DropdownViewModel TopicCategories { get; set; }
    }
}
