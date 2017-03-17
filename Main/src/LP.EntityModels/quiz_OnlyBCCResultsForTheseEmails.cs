namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_OnlyBCCResultsForTheseEmails
    {
        [Key]
        public int EmailID { get; set; }

        public string EmailAddress { get; set; }
    }
}
