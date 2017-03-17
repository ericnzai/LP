namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("askCore_Users")]
    public class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            askCore_UserExtraInfo = new HashSet<askCore_UserExtraInfo>();
            ltl_HoverOver = new HashSet<ltl_HoverOver>();
            ltl_NavigationCulture = new HashSet<ltl_NavigationCulture>();
            Resource_Localization = new HashSet<ResourceLocalization>();
            askCore_Users1 = new HashSet<User>();
            askQuickCMS = new HashSet<askQuickCM>();
            askCore_UsersRoles = new HashSet<UserRole>();
            ltl_UsersFavouriteGroup = new HashSet<ltl_UsersFavouriteGroup>();
        }

        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? CountryID { get; set; }

        public int? AuthenticationTypeID { get; set; }

        public int? FailedLoginAttempts { get; set; }

        public int? UserStatusID { get; set; }

        public bool? LockedOut { get; set; }

        [StringLength(15)]
        public string Culture { get; set; }

        public int? ParentID { get; set; }

        [StringLength(150)]
        public string DisplayName { get; set; }

        public bool DeActivated { get; set; }

        public virtual askCore_AuthenticationType askCore_AuthenticationType { get; set; }

        public virtual Country askCore_Countries { get; set; }

        public virtual askCore_UserDetails askCore_UserDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<askCore_UserExtraInfo> askCore_UserExtraInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_HoverOver> ltl_HoverOver { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_NavigationCulture> ltl_NavigationCulture { get; set; }

        public virtual ltl_UserHistory ltl_UserHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResourceLocalization> Resource_Localization { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> askCore_Users1 { get; set; }

        public virtual User askCore_Users2 { get; set; }

        public virtual askCore_UserStatus askCore_UserStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<askQuickCM> askQuickCMS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> askCore_UsersRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_UsersFavouriteGroup> ltl_UsersFavouriteGroup { get; set; }
    }
}
