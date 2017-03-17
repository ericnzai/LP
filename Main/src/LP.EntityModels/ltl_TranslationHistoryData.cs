namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_TranslationHistoryData
    {
        [Key]
        public int TranslationHistoryDataID { get; set; }

        public int TranslationHistoryID { get; set; }

        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        public virtual ltl_TranslationHistory ltl_TranslationHistory { get; set; }
    }
}
