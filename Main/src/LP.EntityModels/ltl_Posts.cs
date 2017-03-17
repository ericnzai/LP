namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ltl_Posts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_Posts()
        {
            ltl_ClientAppPostVisiblity = new HashSet<ltl_ClientAppPostVisiblity>();
            ltl_Favourites = new HashSet<ltl_Favourites>();
            ltl_FeatureAttachment = new HashSet<ltl_FeatureAttachment>();
            ltl_PostTranslationMapping = new HashSet<ltl_PostTranslationMapping>();
            quiz_PostExams = new HashSet<quiz_PostExams>();
            ltl_PostTranslations = new HashSet<ltl_PostTranslations>();
            ltl_StickyNotes = new HashSet<ltl_StickyNotes>();

            ltl_Topics = new HashSet<Topic>();
        }

        [Key]
        public int PostID { get; set; }

        public int? UserID { get; set; }

        public int SectionID { get; set; }

        public int SortOrder { get; set; }

        [StringLength(256)]
        public string Subject { get; set; }

        public DateTime? PostDate { get; set; }

        public int? TotalViews { get; set; }

        [Column(TypeName = "ntext")]
        public string Body { get; set; }

        [Column(TypeName = "ntext")]
        public string FormattedBody { get; set; }

        [StringLength(256)]
        public string PostName { get; set; }

        public int? PostStatus { get; set; }

        public bool? ShowDisclaimer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_ClientAppPostVisiblity> ltl_ClientAppPostVisiblity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_Favourites> ltl_Favourites { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_FeatureAttachment> ltl_FeatureAttachment { get; set; }

        public virtual ltl_ScormPackage ltl_ScormPackage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_PostTranslationMapping> ltl_PostTranslationMapping { get; set; }

        public virtual ltl_Sections ltl_Sections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_PostExams> quiz_PostExams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_PostTranslations> ltl_PostTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_StickyNotes> ltl_StickyNotes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Topic> ltl_Topics { get; set; }
    }
}
