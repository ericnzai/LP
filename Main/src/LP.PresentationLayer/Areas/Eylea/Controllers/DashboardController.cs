using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Api.Shared.Providers;
using LP.Model.Mappers;
using LP.Model.ViewModels.Dashboards.Student;
using LP.PresentationLayer.Controllers;
using LP.PresentationLayer.Filters;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    [JwtAuthorize]
    public class DashboardController : BaseController
    {
        private TrainingAreasCompleteResponseContract _trainingAreasCompleteResponseContract;

        public async Task<PartialViewResult> ModulesCompleted()
        {
            const string resourceSet = TranslatedItemResourceSetProvider.OldStudentDashboard;

            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Student.FoundationTraining, ResourceSet = resourceSet},
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Student.ProductTraining, ResourceSet = resourceSet},
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Student.FurtherTraining, ResourceSet = resourceSet},
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Student.ModulesCompleted, ResourceSet = resourceSet}
            });

            _trainingAreasCompleteResponseContract = await
                    GetResponseFromService<TrainingAreasCompleteResponseContract>(UriProvider.Exams.TrainingAreaCompletion);

            var foundationTrainingAreaCompletion = GetDefaultTrainingAreaCompletionIfNull(1);

            var productTrainingAreaCompletion = GetDefaultTrainingAreaCompletionIfNull(2);

            var furtherTrainingAreaCompletion = GetDefaultTrainingAreaCompletionIfNull(3);

            var moduleCompletedAreaViewModel = new ModuleCompletedAreaViewModel
            {
                ModulesComplete = new List<ModuleComplete>
                {
                    new ModuleComplete {NumberOfModulesComplete = foundationTrainingAreaCompletion.NumberOfModulesComplete, TotalNumberOfModules = foundationTrainingAreaCompletion.TotalNumberOfModules, TranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Student.FoundationTraining, translatedItems)},
                    new ModuleComplete {NumberOfModulesComplete = productTrainingAreaCompletion.NumberOfModulesComplete, TotalNumberOfModules = productTrainingAreaCompletion.TotalNumberOfModules, TranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Student.ProductTraining, translatedItems)},
                    new ModuleComplete {NumberOfModulesComplete = furtherTrainingAreaCompletion.NumberOfModulesComplete, TotalNumberOfModules = furtherTrainingAreaCompletion.TotalNumberOfModules, TranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Student.FurtherTraining, translatedItems)}
                },
                ModulesCompletedTranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Student.ModulesCompleted, translatedItems)
            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/Student/_ModuleCompletedArea.cshtml", moduleCompletedAreaViewModel);
        }

        private TrainingAreaCompletion GetDefaultTrainingAreaCompletionIfNull(int trainingAreaId)
        {
            var trainingAreaCompletion =
               _trainingAreasCompleteResponseContract.TrainingAreaCompletions.FirstOrDefault(x => x.TrainingAreaId == trainingAreaId);

            return trainingAreaCompletion ?? new TrainingAreaCompletion();
        }

        public PartialViewResult MyTrainingTitle()
        {
            var translatedItem = new TranslatedItem { TranslatedValue = "MY TRAINING" };

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/Student/_MyTrainingTitle.cshtml", translatedItem);
        }

        public async Task<PartialViewResult> TrainingAreaProgress(int id)
        {
            var url = string.Format(UriProvider.Exams.TrainingAreaProgress, id);

            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Student.StartTrainingButtonText, ResourceSet = TranslatedItemResourceSetProvider.OldStudentDashboard},
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Student.ContinueTrainingButtonText, ResourceSet = TranslatedItemResourceSetProvider.OldStudentDashboard},
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Student.ReviewTrainingButtonText, ResourceSet = TranslatedItemResourceSetProvider.OldStudentDashboard}
            });

            var trainingAreaProgressResponseContract = await GetResponseFromService<TrainingAreaProgressResponseContract>(url);

            var trainingAreaProgressViewModel = trainingAreaProgressResponseContract.ToViewModel(GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Student.StartTrainingButtonText, translatedItems), GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Student.ContinueTrainingButtonText, translatedItems), GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Student.ReviewTrainingButtonText, translatedItems));

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/Student/_TrainingAreaProgress.cshtml", trainingAreaProgressViewModel);
        }

        public async Task<PartialViewResult> GlobalCertificationOverviewTitle()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Titles.CertificationOverview, ResourceSet = TranslatedItemResourceSetProvider.GlobalDashboard}
            });

            var translatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.CertificationOverview,
                translatedItems);

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_DashboardSectionTitleWithInfoIcon.cshtml", translatedItem);
        }

        public async Task<PartialViewResult> GlobalCountryPerformanceTitle()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Titles.CountryPerformance, ResourceSet = TranslatedItemResourceSetProvider.GlobalDashboard}
            });

            var translatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.CountryPerformance,
                translatedItems);

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_DashboardSectionTitleWithInfoIcon.cshtml", translatedItem);
        }

        public async Task<PartialViewResult> RegionalCertificationOverviewTitle()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Titles.CertificationOverview, ResourceSet = TranslatedItemResourceSetProvider.RegionalDashboard}
            });

            var translatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.CertificationOverview,
                translatedItems);

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_DashboardSectionTitleWithInfoIcon.cshtml", translatedItem);
        }

        public async Task<PartialViewResult> RegionalCountryPerformanceTitle()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Titles.CountryPerformance, ResourceSet = TranslatedItemResourceSetProvider.RegionalDashboard}
            });

            var translatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.CountryPerformance,
                translatedItems);

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_DashboardSectionTitleWithInfoIcon.cshtml", translatedItem);
        }
    }
}