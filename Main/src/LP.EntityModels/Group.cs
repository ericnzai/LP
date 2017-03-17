namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ltl_Groups")]
    public class Group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Group()
        {
            ltl_GroupPermissions = new HashSet<GroupPermission>();
            ltl_Sections = new HashSet<ltl_Sections>();
        }

        [Key]
        public int GroupID { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string NewsgroupName { get; set; }

        public int SortOrder { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int? StatusBankID { get; set; }

        public int? TrainingAreaID { get; set; }

        public string FriendlyUrl { get; set; }

        public string LearningObjectives { get; set; }

        [StringLength(100)]
        public string GroupImage { get; set; }

        public int? GroupTypeID { get; set; }

        [StringLength(15)]
        public string Culture { get; set; }

        public DateTime LastUpdated { get; set; }

        public int LastUpdatedByUserID { get; set; }

        public int ParentGroupID { get; set; }

        public string CoverImage { get; set; }

        public string FooterText { get; set; }

        public bool IsPartiallyLive { get; set; }

        public int? LinkID { get; set; }

        public int? GroupCategoryID { get; set; }

        public int LayoutType { get; set; }

        public virtual ltl_GroupCategory ltl_GroupCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupPermission> ltl_GroupPermissions { get; set; }

        public virtual ltl_GroupType ltl_GroupType { get; set; }

        public virtual ltl_StatusBank ltl_StatusBank { get; set; }

        public virtual TrainingArea TrainingArea { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_Sections> ltl_Sections { get; set; }
    }
}
