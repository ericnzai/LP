namespace LP.Model.ViewModels.Dashboards
{
    public class PerformanceGroupTypeViewModel
    {
        public int GroupTypeId { get; set; }
        public string GroupTypeName { get; set; }
        
        private string _trainingTranslatedItem;
        public string TrainingTranslatedItem
        {
            get
            {
                return string.IsNullOrEmpty(_trainingTranslatedItem) ? "Training" : _trainingTranslatedItem;
            }
            set
            {
                _trainingTranslatedItem = value;
            }
        }
    }
}
