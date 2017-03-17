namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards
{
    public class PerformanceGroupTypeContract
    {
        public int GroupTypeId { get; set; }
        public string GroupTypeName { get; set; }

        //private string _trainingTranslation;
        //public string TrainingTranslation
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(_trainingTranslation) ? "Training" : _trainingTranslation;
        //    }
        //    set
        //    {
        //        _trainingTranslation = value;
        //    }
        //}
    }
}
