namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_Sections
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_Sections()
        {
            ltl_Posts = new HashSet<ltl_Posts>();
            ltl_PostTranslationMapping = new HashSet<ltl_PostTranslationMapping>();
            ltl_SectionPermissions = new HashSet<ltl_SectionPermissions>();
            ltl_SectionTranslationNotifications = new HashSet<ltl_SectionTranslationNotifications>();
            ltl_SectionTranslationMapping = new HashSet<ltl_SectionTranslationMapping>();
            ltl_SectionTranslations = new HashSet<ltl_SectionTranslations>();
        }

        [Key]
        public int SectionID { get; set; }

        public int SettingsID { get; set; }

        public short IsActive { get; set; }

        public int ParentID { get; set; }

        public int GroupID { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime? DateCreated { get; set; }

        public int SortOrder { get; set; }

        public int? TotalPosts { get; set; }

        public string FriendlyUrl { get; set; }

        public bool? IsHidden { get; set; }

        public bool? IsRequired { get; set; }

        public bool? AlwaysAvailable { get; set; }

        public bool? ContainsSelfAssesments { get; set; }

        public bool? DownloadSection { get; set; }

        public DateTime? ContentLastUpdated { get; set; }

        public int? Status { get; set; }

        public int ContentLastUpdatedByUserID { get; set; }

        public int LastUpdatedByUserID { get; set; }

        public virtual Group ltl_Groups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_Posts> ltl_Posts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_PostTranslationMapping> ltl_PostTranslationMapping { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_SectionPermissions> ltl_SectionPermissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_SectionTranslationNotifications> ltl_SectionTranslationNotifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_SectionTranslationMapping> ltl_SectionTranslationMapping { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_SectionTranslations> ltl_SectionTranslations { get; set; }
    }
}
