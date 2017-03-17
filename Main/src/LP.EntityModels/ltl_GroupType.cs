namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_GroupType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_GroupType()
        {
            ltl_Groups = new HashSet<Group>();
            ltl_GroupTypeTranslations = new HashSet<ltl_GroupTypeTranslations>();
            ltl_Link = new HashSet<ltl_Link>();
            ltl_UsersFavouriteGroup = new HashSet<ltl_UsersFavouriteGroup>();
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int SortOrder { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public string FriendlyUrl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> ltl_Groups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_GroupTypeTranslations> ltl_GroupTypeTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_Link> ltl_Link { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_UsersFavouriteGroup> ltl_UsersFavouriteGroup { get; set; }
    }
}
