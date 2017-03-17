using System.Web.Mvc;

namespace LP.PresentationLayer.Areas.Eylea
{
    public class EyleaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Eylea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Eylea_default",
                "Eylea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Eylea_GlobalDashboardFilterRoute",
                "Eylea/CertificationOverview/ProgressFiltered/{regionId}/{countryId}/{jobRoleId}",
                new { controller = "CertificationOverview", action = "ProgressFiltered", regionId = @"\d+", countryId = @"\d+", jobRoleId = @"\d+" }
            );

            context.MapRoute(
                "Eylea_CountryDashboardFilterRoute",
                "Eylea/CertificationOverview/ProgressFilteredForCountry/{countryId}/{jobRoleId}/{trainerId}",
                new { controller="CertificationOverview", action = "ProgressFilteredForCountry",  countryId = @"\d+", jobRoleId = @"\d+", trainerId = @"\d+" }
            );
        }
    }
}