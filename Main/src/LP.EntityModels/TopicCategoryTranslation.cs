using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.EntityModels
{
    [Table("ltl_TopicCategoryTranslations")]
    public class TopicCategoryTranslation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicCategoryTranslationId { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TopicCategoryId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(13)]
        public string Culture { get; set; }
        
        [ForeignKey("TopicCategoryId")]
        public virtual TopicCategory TopicCategory{ get; set; }

        public Status Status { get; set; }

        public DateTime LastUpdated { get; set; }

        public int LastUpdatedByUserId { get; set; }

        public string Name { get; set; }

        [ForeignKey("LastUpdatedByUserId")]
        public virtual User User { get; set; }
    }
}
