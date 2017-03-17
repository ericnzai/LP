namespace LP.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("askCore_Countries")]
    public class Country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Country()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int CountryID { get; set; }

        [StringLength(10)]
        public string CountryCode { get; set; }

        [StringLength(100)]
        public string CountryName { get; set; }

        [StringLength(100)]
        public string Region { get; set; }

        public int? Ranking { get; set; }

        public int? StatusID { get; set; }

        public bool IsFakeCountry { get; set; }

        public int? RegionId { get; set; }

        public virtual Region askCore_Regions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
