using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;
using LP.Model.ViewModels.Dashboards.Country;
using LP.Model.ViewModels.Dashboards.Global;
using LP.Model.ViewModels.Dashboards.Regional;
using LP.Model.ViewModels.Dashboards.Trainer;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Country;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Trainer;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface IDashboardActivitiesCommands
    {
        Task<TrainerActivitiesContract> GetTrainerActivities(int trainerId);
        Task<TrainerActivitiesContract> GetTrainerActivities(int trainerId, string jobRoleIds);
        Task<CountryActivitiesContract> GetCountryActivities(int countryId);
        Task<CountryActivitiesContract> GetCountryActivities(int countryId, string jobRoleIds);
    }
}
