using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.EntityModels
{
    [Table("ltl_VAConversionTools")]
    public class VAConversionTool
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VAConversionToolId { get; set; }

        public DateTime DateCreated { get; set; }

        public int CreatedByUserId { get; set; }

        [ForeignKey("CreatedByUserId")]
        public virtual User CreatedByUser { get; set; }

        public string FileName { get; set; }

        public Status Status { get; set; }

        [StringLength(13)]
        public string Culture { get; set; }
 

        public string Comments { get; set; }
    }
}
