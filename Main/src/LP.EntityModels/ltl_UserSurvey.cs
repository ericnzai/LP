namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_UserSurvey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        public int SurveyDecision { get; set; }

        public int NumberOfSiteVisits { get; set; }

        public virtual ltl_UserSurveyOptions ltl_UserSurveyOptions { get; set; }
    }
}
