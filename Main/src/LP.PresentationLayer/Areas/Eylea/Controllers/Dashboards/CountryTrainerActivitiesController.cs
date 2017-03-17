using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Api.Shared.Providers;
using LP.Model.Extensions;
using LP.Model.Mappers;
using LP.Model.ViewModels.Dashboards;
using LP.Model.ViewModels.Dashboards.Country;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Country;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Trainer;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.PresentationLayer.Areas.Eylea.Controllers.Dashboards
{
    public class CountryTrainerActivitiesController : BaseController
    {
        private TrainerActivitiesContract _trainerActivitiesContract;
        private CountryActivitiesContract _countryActivitiesContract;

        public async Task<PartialViewResult> TrainerActivities()
        {

            _trainerActivitiesContract = await
                    GetResponseFromService<TrainerActivitiesContract>(UriProvider.Exams.TrainerActivities);

            var groupTypeHeadersResponseContract =
              await GetResponseFromService<GroupTypeHeadersResponseContract>(
                    UriProvider.Exams.GroupTypeHeaders);


            var trainerActivitiesViewModel = _trainerActivitiesContract.ToViewModel();

            trainerActivitiesViewModel.PerformanceGroupTypeViewModels = groupTypeHeadersResponseContract.ToViewModels();

            return PartialView("~/Areas/Eylea/Views/Dashboard/_TrainerActivitiesOverview.cshtml", trainerActivitiesViewModel);
        }

        public async Task<PartialViewResult> TrainerActivitiesFiltered(string jobRoleIds)
        {
            var trainerActivitiesUri = string.Format(UriProvider.Exams.TrainerActivitiesFiltered, jobRoleIds);

            _trainerActivitiesContract = await
                   GetResponseFromService<TrainerActivitiesContract>(trainerActivitiesUri);

            var groupTypeHeadersResponseContract =
              await GetResponseFromService<GroupTypeHeadersResponseContract>(
                    UriProvider.Exams.GroupTypeHeaders);


            var trainerActivitiesViewModel = _trainerActivitiesContract.ToViewModel();

            trainerActivitiesViewModel.PerformanceGroupTypeViewModels = groupTypeHeadersResponseContract.ToViewModels();

            return PartialView("~/Areas/Eylea/Views/Dashboard/_TrainerActivitiesOverview.cshtml", trainerActivitiesViewModel);
        }
        public async Task<PartialViewResult> CountryActivities(int id)
        {
            var countryActivitiesUri = string.Format(UriProvider.Exams.CountryActivities, id);

            _countryActivitiesContract = await
                    GetResponseFromService<CountryActivitiesContract>(countryActivitiesUri);

            var groupTypeHeadersResponseContract =
                    await GetResponseFromService<GroupTypeHeadersResponseContract>(UriProvider.Exams.GroupTypeHeaders);

            var countryActivitiesViewModel = _countryActivitiesContract.ToViewModel();

            countryActivitiesViewModel.PerformanceGroupTypeViewModel = groupTypeHeadersResponseContract.ToViewModels();

            return PartialView("~/Areas/Eylea/Views/Dashboard/_CountryActivitiesOverview.cshtml", countryActivitiesViewModel);
        }

        public async Task<PartialViewResult> CountryActivitiesFiltered(string jobRoleIds, int id)
        {
            var countryActivitiesUri = string.Format(UriProvider.Exams.CountryActivitiesFiltered, id, jobRoleIds);

            _countryActivitiesContract = await
                    GetResponseFromService<CountryActivitiesContract>(countryActivitiesUri);

            var groupTypeHeadersResponseContract =
                    await GetResponseFromService<GroupTypeHeadersResponseContract>(UriProvider.Exams.GroupTypeHeaders);

            var countryActivitiesViewModel = _countryActivitiesContract.ToViewModel();

            countryActivitiesViewModel.PerformanceGroupTypeViewModel = groupTypeHeadersResponseContract.ToViewModels();

            return PartialView("~/Areas/Eylea/Views/Dashboard/_CountryActivitiesOverview.cshtml", countryActivitiesViewModel);
        }
        public async Task<PartialViewResult> GetActivitiesStatusLegendTrainer()
        {
            const string resourceSet = TranslatedItemResourceSetProvider.TrainerDashboard;

            return await GetPartialViewForLegend(resourceSet);
        }
        
        public async Task<PartialViewResult> GetActivitiesStatusLegendCountry()
        {
            const string resourceSet = TranslatedItemResourceSetProvider.CountryDashboard;

            return await GetPartialViewForLegend(resourceSet);
        }

        private async Task<PartialViewResult> GetPartialViewForLegend(string resourceSet)
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest
                {
                    ResourceId = TranslationItemIdProvider.Dashboard.Legend.Registered,
                    ResourceSet = resourceSet
                },
                new TranslationRequest
                {
                    ResourceId = TranslationItemIdProvider.Dashboard.Legend.Started,
                    ResourceSet = resourceSet
                },
                new TranslationRequest
                {
                    ResourceId = TranslationItemIdProvider.Dashboard.Legend.InProgress,
                    ResourceSet = resourceSet
                },
                new TranslationRequest
                {
                    ResourceId = TranslationItemIdProvider.Dashboard.Legend.Certified,
                    ResourceSet = resourceSet
                }
            });

            var countryTrainerActivitiesLegendViewModel = new CountryTrainerActivitiesLegendViewModel
            {
                Registered =
                    GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Legend.Registered,
                        translatedItems),
                Started =
                    GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Legend.Started, translatedItems),
                InProgress =
                    GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Legend.InProgress,
                        translatedItems),
                Certified =
                    GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Legend.Certified, translatedItems)
            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/_ActivitiesStatusLegend.cshtml", countryTrainerActivitiesLegendViewModel);
        }

        public async Task<PartialViewResult> GetStatisticsTitleCountry()
        {
            const string resourceSet = TranslatedItemResourceSetProvider.CountryDashboard;

            return await GetPartialViewStatisticsTitle(resourceSet);
        }

        public async Task<PartialViewResult> GetStatisticsTitleTrainer()
        {
            const string resourceSet = TranslatedItemResourceSetProvider.TrainerDashboard;

            return await GetPartialViewStatisticsTitle(resourceSet);
        }

        private async Task<PartialViewResult> GetPartialViewStatisticsTitle(string resourceSet)
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Titles.Statistics, ResourceSet = resourceSet}
            });

            var translatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.Statistics,
                translatedItems);

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_CountryTrainerActivitiesTitleWithInfoIcon.cshtml", translatedItem);
        }

        public async Task<PartialViewResult> GetActivitiesTitleCountry()
        {
            const string resourceSet = TranslatedItemResourceSetProvider.CountryDashboard;

            return await GetPartialViewActivitiesTitle(resourceSet);
        }

        public async Task<PartialViewResult> GetActivitiesTitleTrainer()
        {
            const string resourceSet = TranslatedItemResourceSetProvider.TrainerDashboard;

            return await GetPartialViewActivitiesTitle(resourceSet);
        }

        private async Task<PartialViewResult> GetPartialViewActivitiesTitle(string resourceSet)
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Titles.Activities, ResourceSet = resourceSet}
            });

            var translatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.Activities,
                translatedItems);

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_CountryTrainerActivitiesTitleWithInfoIcon.cshtml", translatedItem);
        }

        public async Task<PartialViewResult> GetReportLinkTitleCountry()
        {
            const string resourceSet = TranslatedItemResourceSetProvider.CountryDashboard;

            return await GetPartialViewReportLinkTitle(resourceSet);
        }

        public async Task<PartialViewResult> GetReportLinkTitleTrainer()
        {
            const string resourceSet = TranslatedItemResourceSetProvider.TrainerDashboard;

            return await GetPartialViewReportLinkTitle(resourceSet);
        }

        public async Task<PartialViewResult> GetPartialViewReportLinkTitle(string resourceSet)
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Titles.ViewFullReportTitle, ResourceSet = resourceSet},
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.Titles.ViewFullReportTooltip, ResourceSet = resourceSet},
            });

            var countryTrainerViewReportLinkViewModel = new CountryTrainerViewReportLinkViewModel
            {
                Title = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.ViewFullReportTitle,
                translatedItems).TranslatedValue,
                Tooltip = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.ViewFullReportTooltip,
                translatedItems).TranslatedValue
            };


            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_CountryTrainerViewReportLink.cshtml", countryTrainerViewReportLinkViewModel);
        }
    }
}