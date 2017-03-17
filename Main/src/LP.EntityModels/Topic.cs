using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.EntityModels
{
    [Table("ltl_Topics")]
    public class Topic
    {
        public Topic()
        {
            Posts = new HashSet<ltl_Posts>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TopicId { get; set; }

        public int TopicCategoryId { get; set; }

        public DateTime DateCreated { get; set; }

        public int CreatedByUserId { get; set; }

        [ForeignKey("CreatedByUserId")]
        public virtual User CreatedByUser { get; set; }

        public int SortOrder { get; set; }

        public Status Status { get; set; }

        [ForeignKey("TopicCategoryId")]
        public virtual TopicCategory TopicCategory { get; set; }

        public virtual ICollection<ltl_Posts> Posts { get; set; }
    }
}
