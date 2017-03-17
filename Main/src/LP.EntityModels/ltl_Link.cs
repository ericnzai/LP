namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_Link
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_Link()
        {
            ltl_Link_Role = new HashSet<ltl_Link_Role>();
        }

        public int ID { get; set; }

        public int LinkTypeID { get; set; }

        public int LinkFormatType { get; set; }

        public string Url { get; set; }

        public string LinkTitle { get; set; }

        public string ImageUrl { get; set; }

        [StringLength(50)]
        public string CultureVersion { get; set; }

        public int? IndicationID { get; set; }

        public int? SortOrder { get; set; }

        public int? GroupTypeID { get; set; }

        public string LinkDescription { get; set; }

        public int? StatusID { get; set; }

        public virtual ltl_GroupType ltl_GroupType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_Link_Role> ltl_Link_Role { get; set; }
    }
}
