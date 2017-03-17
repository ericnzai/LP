using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.EntityModels.Views;

namespace LP.Data.ViewMappings
{
    public class TrainersWithStudentsCountriesMap : EntityTypeConfiguration<TrainersWithStudentsCountries>
    {
        public TrainersWithStudentsCountriesMap()
        {
            ToTable("TrainersWithStudentsCountries");

            HasKey(t => t.UserID);
        }
    }
}
