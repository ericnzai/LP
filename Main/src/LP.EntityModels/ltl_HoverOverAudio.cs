namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_HoverOverAudio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_HoverOverAudio()
        {
            ltl_HoverOver = new HashSet<ltl_HoverOver>();
        }

        [Key]
        public int HoverOverAudioID { get; set; }

        [Required]
        public byte[] SourceFile { get; set; }

        [Required]
        [StringLength(100)]
        public string FileName { get; set; }

        public bool IsEnabled { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_HoverOver> ltl_HoverOver { get; set; }
    }
}
