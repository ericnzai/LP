namespace LP.EntityModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ltl_AutomaticRoleAssignment")]
    public partial class AutomaticRoleAssignment
    {
        [Key]
        public int AutomaticRoleAssignmentId { get; set; }

        public int AutomaticRoleAssignmentTypeId { get; set; }

        [Required]
        public string AutomaticRoleAssignmentValue { get; set; }

        public int RoleId { get; set; }

        public bool? Complete { get; set; }

        public string Description { get; set; }

        public virtual Role askCore_Roles { get; set; }

        public virtual ltl_AutomaticRoleAssignmentType ltl_AutomaticRoleAssignmentType { get; set; }
    }
}
