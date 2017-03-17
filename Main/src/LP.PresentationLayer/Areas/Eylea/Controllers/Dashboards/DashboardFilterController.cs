using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Api.Shared.Providers;
using LP.Model.Mappers;
using LP.Model.ViewModels.Shared;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.PresentationLayer.Areas.Eylea.Controllers.Dashboards
{
    public class DashboardFilterController : BaseController
    {
        private const string CountryDropDownClientId = "country-drop-down";
        private const string RegionDropDownClientId = "region-drop-down";
        private const string TrainerDropDownClientId = "trainer-drop-down";
        private const string JobRoleDropDownClientId = "job-role-drop-down";

        public async Task<PartialViewResult> Country()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest{ResourceId = TranslationItemIdProvider.Dashboard.Global.CountryDropDownAll, ResourceSet = TranslatedItemResourceSetProvider.GlobalDashboard }
            });

            var dashboardFilterDropdownResponseContract =
                await
                    GetResponseFromService<DashboardFilterDropdownResponseContract>(
                        UriProvider.Content.DashboardCountryDropdown);

            var defaultTextTranslatedValue =
                GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Global.CountryDropDownAll,
                    translatedItems);

            return GetDropdownPartial(CountryDropDownClientId, dashboardFilterDropdownResponseContract, DashboardType.GlobalDashboard, defaultTextTranslatedValue);
        }

        public async Task<PartialViewResult> CountriesForRegion(int id)
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest{ResourceId = TranslationItemIdProvider.Dashboard.Global.CountryDropDownAll, ResourceSet = TranslatedItemResourceSetProvider.RegionalDashboard }
            });

            var dashboardFilterDropdownResponseContract =
                await
                    GetResponseFromService<DashboardFilterDropdownResponseContract>(
                        string.Format(UriProvider.Content.DashboardCountryDropdownWithId, id));

            var defaultTextTranslatedValue =
              GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Global.CountryDropDownAll,
                  translatedItems);

            return GetDropdownPartial(CountryDropDownClientId, dashboardFilterDropdownResponseContract, DashboardType.RegionalDashboard, defaultTextTranslatedValue);
        }

        public async Task<PartialViewResult> FilteredCountriesForRegion(int id)
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest{ResourceId = TranslationItemIdProvider.Dashboard.Global.CountryDropDownAll, ResourceSet = TranslatedItemResourceSetProvider.GlobalDashboard }
            });

            var dashboardFilterDropdownResponseContract =
                await
                    GetResponseFromService<DashboardFilterDropdownResponseContract>(
                        string.Format(UriProvider.Content.DashboardCountryDropdownWithId, id));

            var defaultTextTranslatedValue =
              GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Global.CountryDropDownAll,
                  translatedItems);

            return GetDropdownPartial(CountryDropDownClientId, dashboardFilterDropdownResponseContract, DashboardType.GlobalDashboard, defaultTextTranslatedValue);
        }

        public async Task<PartialViewResult> Region()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest{ResourceId = TranslationItemIdProvider.Dashboard.Global.RegionDropDownAll, ResourceSet = TranslatedItemResourceSetProvider.GlobalDashboard }
            });

            var dashboardFilterDropdownResponseContract =
             await GetResponseFromService<DashboardFilterDropdownResponseContract>(
                     string.Format(UriProvider.Content.DashboardRegionDropdown));

            var defaultTextTranslatedValue =
                GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Global.RegionDropDownAll,
                    translatedItems);

            return GetDropdownPartial(RegionDropDownClientId, dashboardFilterDropdownResponseContract, DashboardType.GlobalDashboard, defaultTextTranslatedValue);
        }

        public async Task<PartialViewResult> Trainer(int id)
        {
            var dashboardFilterDropdownResponseContract =
              await
                  GetResponseFromService<DashboardFilterDropdownResponseContract>(
                      string.Format(UriProvider.Content.DashboardTrainerDropdownWithId, id));
 
            return GetDropdownPartial(TrainerDropDownClientId, dashboardFilterDropdownResponseContract, DashboardType.TrainerDashboard);
        }

        public async Task<PartialViewResult> JobRoleGlobal()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest{ResourceId = TranslationItemIdProvider.Dashboard.Global.FunctionDropDownAll, ResourceSet = TranslatedItemResourceSetProvider.GlobalDashboard }
            });

            var defaultTextTranslatedValue =
                GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Global.RegionDropDownAll,
                    translatedItems);

            return await GetJobRoleDropDown(defaultTextTranslatedValue, DashboardType.GlobalDashboard);
        }

        public async Task<PartialViewResult> JobRoleRegional()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest{ResourceId = TranslationItemIdProvider.Dashboard.Regional.FunctionDropDownAll, ResourceSet = TranslatedItemResourceSetProvider.RegionalDashboard }
            });

            var defaultTextTranslatedValue =
                GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Global.RegionDropDownAll,
                    translatedItems);

            return await GetJobRoleDropDown(defaultTextTranslatedValue, DashboardType.RegionalDashboard);
        }

        public async Task<PartialViewResult> JobRoleCountry()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest{ResourceId = TranslationItemIdProvider.Dashboard.Regional.FunctionDropDownAll, ResourceSet = TranslatedItemResourceSetProvider.CountryDashboard }
            });

            var defaultTextTranslatedValue =
                GetTranslatedValueByResourceId(TranslationItemIdProvider.Dashboard.Global.CountryDropDownAll,
                    translatedItems);

            return await GetJobRoleDropDown(defaultTextTranslatedValue, DashboardType.CountryDashboard);
        }

        private async Task<PartialViewResult> GetJobRoleDropDown(string defaultTextTranslatedValue, DashboardType dashboardType)
        {
            var dashboardFilterDropdownResponseContract =
              await
                  GetResponseFromService<DashboardFilterDropdownResponseContract>(
                      string.Format(UriProvider.Content.DashboardJobRoleDropdown));

            return GetDropdownPartial(JobRoleDropDownClientId, dashboardFilterDropdownResponseContract, dashboardType, defaultTextTranslatedValue);
        }

        private PartialViewResult GetDropdownPartial(string clientId, DashboardFilterDropdownResponseContract dashboardFilterDropdownResponseContract, DashboardType dashboardType, string defaultTranslatedText = "")
        {

            var dropdownItemViewModels = dashboardFilterDropdownResponseContract.ToViewModels(defaultTranslatedText);

            var dropdownViewModel = new DropdownViewModel
            {
                ClientId = clientId,
                DropdownItems = dropdownItemViewModels
            };

            if (dashboardType == DashboardType.GlobalDashboard)
            {
                return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_DashboardFilterDropDownThreeSections.cshtml", dropdownViewModel);
            }

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_DashboardFilterDropDownTwoSections.cshtml", dropdownViewModel);
        }
    }
}