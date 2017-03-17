namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_FeatureAttachment_CustomField
    {
        [Key]
        public int CustomFieldID { get; set; }

        public int? CustomFieldDefinitionID { get; set; }

        public string Value { get; set; }

        public int? FeatureAttachmentID { get; set; }

        public virtual ltl_FeatureAttachment ltl_FeatureAttachment { get; set; }

        public virtual ltl_FeatureAttachment_CustomFieldDefinition ltl_FeatureAttachment_CustomFieldDefinition { get; set; }
    }
}
