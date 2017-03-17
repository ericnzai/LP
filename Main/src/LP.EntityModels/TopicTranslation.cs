using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.EntityModels
{
    [Table("ltl_TopicTranslations")]
    public class TopicTranslation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicTranslationId { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TopicId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(13)]
        public string Culture { get; set; }

        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }

        public Status Status { get; set; }

        public DateTime LastUpdated { get; set; }

        public int LastUpdatedByUserId { get; set; }

        public string Name { get; set; }

        [ForeignKey("LastUpdatedByUserId")]
        public virtual User User { get; set; }
    }
}
