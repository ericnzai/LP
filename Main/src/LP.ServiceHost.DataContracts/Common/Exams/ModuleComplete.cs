using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.ServiceHost.DataContracts.Common.Exams
{
    public class ModuleComplete
    {
        public int NumberOfModulesComplete { get; set; }
        public int TotalNumberOfModules { get; set; }
        public TranslatedItem TranslatedItem { get; set; }
    }
}
