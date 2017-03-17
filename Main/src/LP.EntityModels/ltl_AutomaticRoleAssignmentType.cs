namespace LP.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ltl_AutomaticRoleAssignmentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_AutomaticRoleAssignmentType()
        {
            ltl_AutomaticRoleAssignment = new HashSet<AutomaticRoleAssignment>();
        }

        [Key]
        public int AutomaticRoleAssignmentTypeId { get; set; }

        [Required]
        [StringLength(80)]
        public string AutomaticRoleAssignmentType { get; set; }

        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutomaticRoleAssignment> ltl_AutomaticRoleAssignment { get; set; }
    }
}
