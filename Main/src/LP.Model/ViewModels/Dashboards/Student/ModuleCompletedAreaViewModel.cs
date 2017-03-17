using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.Model.ViewModels.Dashboards.Student
{
    public class ModuleCompletedAreaViewModel
    {
        public TranslatedItem ModulesCompletedTranslatedItem { get; set; }
        public List<ModuleComplete> ModulesComplete { get; set; } 
    }
}
