namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("ltl_TrainingArea")]
    public partial class TrainingArea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrainingArea()
        {
            ltl_Groups = new HashSet<Group>();
        }

        [Key]
        public int TrainingAreaID { get; set; }

        public string Name { get; set; }

        public string FriendlyUrl { get; set; }

        public int? StatusBankID { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ltl_TrainingAreaPermissions> TrainingAreaPermissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> ltl_Groups { get; set; }
    }
}
