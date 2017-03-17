using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Model.ViewModels.Dashboards
{
    public class IndividualCountryPerformanceViewModel
    {
        public string CountryName { get; set; }
        public int TotalNumberOfUsers { get; set; }
        public int CountryId { get; set; }
        public List<CountryPerformanceCultureViewModel> CountryPerformanceCultureViewModels { get; set; }
    }
}
