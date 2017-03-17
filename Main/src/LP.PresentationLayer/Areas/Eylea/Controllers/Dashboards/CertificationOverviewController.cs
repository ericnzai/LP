using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Api.Shared.Providers;
using LP.Model.Mappers;
using LP.Model.ViewModels.Dashboards;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.PresentationLayer.Areas.Eylea.Controllers.Dashboards
{
    public class CertificationOverviewController : BaseController
    {
        public async Task<PartialViewResult> Progress()
        {
            var numberOfUsersRegistered = await GetResponseFromService<int>(UriProvider.Authentication.NumberOfRegisteredUsers);

            var overviewGroupTypeProgressResponseContract = await GetResponseFromService<OverviewGroupTypeProgressResponseContract>(UriProvider.Exams.OverviewGroupTypeProgress);

            return await GetProgressViews(numberOfUsersRegistered, overviewGroupTypeProgressResponseContract);
        }

        public async Task<PartialViewResult> ProgressFiltered(int regionId, int countryId, int jobRoleId)
        {
            var numberOfRegisteredUsersFilteredUri = string.Format(UriProvider.Authentication.NumberOfRegisteredUsersFiltered, regionId, countryId, jobRoleId);

            var numberOfUsersRegistered = await GetResponseFromService<int>(numberOfRegisteredUsersFilteredUri);

            var overviewGroupTypeProgressFilteredUri = string.Format(UriProvider.Exams.OverviewGroupTypeProgressFiltered, regionId, countryId, jobRoleId);

            var overviewGroupTypeProgressResponseContract = await GetResponseFromService<OverviewGroupTypeProgressResponseContract>(overviewGroupTypeProgressFilteredUri);

            return await GetProgressViews(numberOfUsersRegistered, overviewGroupTypeProgressResponseContract);
        }

        public async Task<PartialViewResult> ProgressFilteredForCountry(int countryId, int jobRoleId, int trainerId)
        {
            var numberOfRegisteredUsersFilteredUri =
                string.Format(UriProvider.Authentication.NumberOfRegisteredUsersFilteredForCountry, countryId, jobRoleId,
                    trainerId);

            var numberOfUsersRegistered = await GetResponseFromService<int>(numberOfRegisteredUsersFilteredUri);

            var overviewGroupTypeProgressFilteredUri =
                string.Format(UriProvider.Exams.OverviewGroupTypeProgressFilteredForCountry, countryId, jobRoleId, trainerId);

            var overviewGroupTypeProgressResponseContract = await GetResponseFromService<OverviewGroupTypeProgressResponseContract>(overviewGroupTypeProgressFilteredUri);

            return await GetProgressViews(numberOfUsersRegistered, overviewGroupTypeProgressResponseContract);
        }

        private async Task<PartialViewResult> GetProgressViews(int numberOfUsersRegistered, OverviewGroupTypeProgressResponseContract overviewGroupTypeProgressResponseContract)
        {
            var dashboardBarChartViewModels = overviewGroupTypeProgressResponseContract.ToViewModels();


            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId =  TranslationItemIdProvider.Dashboard.Titles.CertificationOverview, ResourceSet = GetDashboardTypeResourceSet(DashboardType.GlobalDashboard)}
            });

            var certificationOverviewViewModel = new CertificationOverviewViewModel
            {
                NumberOfUsersRegistered = numberOfUsersRegistered,
                DashboardBarChartViewModels = dashboardBarChartViewModels,
                NumberOfUsersRegisteredTranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.CertificationOverview, translatedItems)
            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/_CertficationOverview.cshtml", certificationOverviewViewModel);
        }

        public async Task<PartialViewResult> ProgressRegional(int id)
        {
            var numberUrl = string.Format(UriProvider.Authentication.NumberOfRegisteredUsersForRegion, id);
            var numberOfUsersRegistered = await GetResponseFromService<int>(numberUrl, null);
            var url = string.Format(UriProvider.Exams.OverviewGroupTypeRegionProgress, id);
            var overviewGroupProgressResponseContract = await GetResponseFromService<OverviewGroupTypeProgressResponseContract>(url, null);
            var dashboardBarChartViewModels = overviewGroupProgressResponseContract.ToViewModels();


            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId =  TranslationItemIdProvider.Dashboard.Titles.CertificationOverview, ResourceSet = GetDashboardTypeResourceSet(DashboardType.GlobalDashboard)}
            });

            var certificationOverviewViewModel = new CertificationOverviewViewModel
            {
                NumberOfUsersRegistered = numberOfUsersRegistered,
                DashboardBarChartViewModels = dashboardBarChartViewModels,
                NumberOfUsersRegisteredTranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.CertificationOverview, translatedItems)
            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/_CertficationOverview.cshtml", certificationOverviewViewModel);
        }

        public async Task<PartialViewResult> ProgressTrainer()
        {
            var numberOfUsersRegistered = await GetResponseFromService<int>(UriProvider.Authentication.NumberOfRegisteredUsersForTrainer);

            var overviewGroupTypeProgressResponseContract = await GetResponseFromService<OverviewGroupTypeProgressResponseContract>(UriProvider.Exams.OverviewGroupTypeTrainerProgress);

            var dashboardBarChartViewModels = overviewGroupTypeProgressResponseContract.ToViewModels();


            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId =  TranslationItemIdProvider.Dashboard.Titles.NumberOfUserRegistered, ResourceSet = GetDashboardTypeResourceSet(DashboardType.TrainerDashboard)}
            });

            var certificationOverviewViewModel = new CertificationOverviewViewModel
            {
                NumberOfUsersRegistered = numberOfUsersRegistered,
                DashboardBarChartViewModels = dashboardBarChartViewModels,
                NumberOfUsersRegisteredTranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.NumberOfUserRegistered, translatedItems)
            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/_CertficationOverview.cshtml", certificationOverviewViewModel);
        }

        public async Task<PartialViewResult> ProgressCountry(int id)
        {
            var numberOfUsersRegistered = await GetResponseFromService<int>(string.Format(UriProvider.Authentication.NumberOfRegisteredUsersForCountry, id));

            var overviewGroupTypeProgressResponseContract = await GetResponseFromService<OverviewGroupTypeProgressResponseContract>(string.Format(UriProvider.Exams.OverviewGroupTypeCountryProgress, id));

            var dashboardBarChartViewModels = overviewGroupTypeProgressResponseContract.ToViewModels();


            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId =  TranslationItemIdProvider.Dashboard.Titles.NumberOfUserRegistered, ResourceSet = GetDashboardTypeResourceSet(DashboardType.CountryDashboard)}
            });

            var certificationOverviewViewModel = new CertificationOverviewViewModel
            {
                NumberOfUsersRegistered = numberOfUsersRegistered,
                DashboardBarChartViewModels = dashboardBarChartViewModels,
                NumberOfUsersRegisteredTranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.Titles.NumberOfUserRegistered, translatedItems)
            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/_CertficationOverview.cshtml", certificationOverviewViewModel);
        }

        public async Task<PartialViewResult> BarChartLegend(int id)
        {
            var resourceSet = GetDashboardTypeResourceSet(id);

            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.BarChart.Registered, ResourceSet = resourceSet},
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.BarChart.Started, ResourceSet = resourceSet},
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.BarChart.InProgress, ResourceSet = resourceSet},
                new TranslationRequest {ResourceId = TranslationItemIdProvider.Dashboard.BarChart.Certified, ResourceSet = resourceSet}
            });

            var dashboardBarChartLegendViewModel = new DashboardBarChartLegendViewModel
            {
                CertifiedTranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.BarChart.Certified, translatedItems),
                InProgressTranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.BarChart.InProgress, translatedItems),
                RegisteredTranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.BarChart.Registered, translatedItems),
                StartedTranslatedItem = GetTranslatedItemByResourceId(TranslationItemIdProvider.Dashboard.BarChart.Started, translatedItems)
            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_DashboardBarChartLegend.cshtml", dashboardBarChartLegendViewModel);
        }

        public async Task<PartialViewResult> ProgressGlobalCountry()
        {
            var overviewCountryProgressResponseContract =
                await
                    GetResponseFromService<OverviewCountryProgressResponseContract>(
                        UriProvider.Exams.OverviewCountryGlobalProgress, null);
            var groupTypeHeadersResponseContract =
              await  GetResponseFromService<GroupTypeHeadersResponseContract>(
                    UriProvider.Exams.GroupTypeHeaders, null);

            var countryPerformanceViewModel = new CountryPerformanceViewModel
            {
                PerformanceGroupTypeViewModel = groupTypeHeadersResponseContract.ToViewModels(),
                IndividualCountryPerformanceViewModels = overviewCountryProgressResponseContract.ToViewModels()

            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/_CountryPerformanceOverview.cshtml", countryPerformanceViewModel);
        }

        public async Task<PartialViewResult> ProgressGlobalCountryFiltered(string jobRoleIds)
        {
            var url = string.Format(UriProvider.Exams.OverviewCountryGlobalProgressFiltered, jobRoleIds);
            var overviewCountryProgressResponseContract =
                await
                    GetResponseFromService<OverviewCountryProgressResponseContract>(
                       url,null );

            var groupTypeHeadersResponseContract =
           await GetResponseFromService<GroupTypeHeadersResponseContract>(
                 UriProvider.Exams.GroupTypeHeaders, null);

            var countryPerformanceViewModel = new CountryPerformanceViewModel
            {
                PerformanceGroupTypeViewModel = groupTypeHeadersResponseContract.ToViewModels(),
                IndividualCountryPerformanceViewModels = overviewCountryProgressResponseContract.ToViewModels()

            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/_CountryPerformanceOverview.cshtml", countryPerformanceViewModel);
        }
        public async Task<PartialViewResult> ProgressRegionalCountry(int id)
        {
            var url = string.Format(UriProvider.Exams.OverviewCountryRegionProgress, id);
            var overviewCountryProgressResponseContract =
                await
                    GetResponseFromService<OverviewCountryProgressResponseContract>(
                        url, null);
            var groupTypeHeadersResponseContract =
              await GetResponseFromService<GroupTypeHeadersResponseContract>(
                    UriProvider.Exams.GroupTypeHeaders, null);

            var countryPerformanceViewModel = new CountryPerformanceViewModel
            {
                PerformanceGroupTypeViewModel = groupTypeHeadersResponseContract.ToViewModels(),
                IndividualCountryPerformanceViewModels = overviewCountryProgressResponseContract.ToViewModels()

            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/_CountryPerformanceOverview.cshtml", countryPerformanceViewModel);
        }

        public async Task<PartialViewResult> ProgressRegionalCountryFiltered(string jobRoleIds, int regionId)
        {
            var url = string.Format(UriProvider.Exams.OverviewCountryRegionProgressFiltered, jobRoleIds, regionId);

            var overviewCountryProgressResponseContract =
                await
                    GetResponseFromService<OverviewCountryProgressResponseContract>(
                        url, null);
            var groupTypeHeadersResponseContract =
              await GetResponseFromService<GroupTypeHeadersResponseContract>(
                    UriProvider.Exams.GroupTypeHeaders, null);

            var countryPerformanceViewModel = new CountryPerformanceViewModel
            {
                PerformanceGroupTypeViewModel = groupTypeHeadersResponseContract.ToViewModels(),
                IndividualCountryPerformanceViewModels = overviewCountryProgressResponseContract.ToViewModels()

            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/_CountryPerformanceOverview.cshtml", countryPerformanceViewModel);
        }
    }
}