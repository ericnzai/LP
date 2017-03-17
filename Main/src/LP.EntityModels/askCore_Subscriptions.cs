namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class askCore_Subscriptions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public askCore_Subscriptions()
        {
            askCore_SubscriptionExtraInfo = new HashSet<askCore_SubscriptionExtraInfo>();
        }

        [Key]
        public int SubscriptionID { get; set; }

        public int UserID { get; set; }

        public int SiteID { get; set; }

        public int? NumberOfLogins { get; set; }

        public DateTime? LastLoginDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<askCore_SubscriptionExtraInfo> askCore_SubscriptionExtraInfo { get; set; }
    }
}
