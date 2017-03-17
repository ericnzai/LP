namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class askCore_SubscriptionExtraInfo
    {
        [Key]
        public int SubscriptionExtraInfoID { get; set; }

        public int SubscriptionID { get; set; }

        public int FieldDefinitionID { get; set; }

        public string Value { get; set; }

        public virtual askCore_FieldDefinition askCore_FieldDefinition { get; set; }

        public virtual askCore_Subscriptions askCore_Subscriptions { get; set; }
    }
}
