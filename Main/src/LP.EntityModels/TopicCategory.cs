using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.EntityModels
{
    [Table("ltl_TopicCategories")]
    public class TopicCategory
    {
        public TopicCategory()
        {
            ltl_Topics = new HashSet<Topic>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicCategoryId { get; set; }

        public int SortOrder { get; set; }

        public Status Status { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        public int CreatedByUserId { get; set; }

        [ForeignKey("CreatedByUserId")]
        public virtual User CreatedByUser { get; set; }
        
        public virtual ICollection<Topic> ltl_Topics { get; set; }
    }
}
