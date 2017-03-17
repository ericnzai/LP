using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.Model.ViewModels.Topic
{
    public class TopicsViewModel
    {
        public TopicsViewModel()
        {
            Topics = new List<TopicViewModel>();
            Categories = new List<TopicCategory>();
        }

        public  List<TopicViewModel> Topics { get; set; }
        public List<TopicCategory> Categories { get; set; }
    }
}
