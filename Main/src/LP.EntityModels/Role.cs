namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("askCore_Roles")]
    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            askCore_Country_CulturePermissions = new HashSet<askCore_Country_CulturePermissions>();
            askCore_UsersRoles = new HashSet<UserRole>();
            ltl_AutomaticRoleAssignment = new HashSet<AutomaticRoleAssignment>();
            askCore_Roles1 = new HashSet<Role>();
            askCore_Roles2 = new HashSet<Role>();
        }

        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        public int? RoleGroupID { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<askCore_Country_CulturePermissions> askCore_Country_CulturePermissions { get; set; }

        public virtual askCore_RoleGroup askCore_RoleGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> askCore_UsersRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutomaticRoleAssignment> ltl_AutomaticRoleAssignment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role> askCore_Roles1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role> askCore_Roles2 { get; set; }
    }
}
