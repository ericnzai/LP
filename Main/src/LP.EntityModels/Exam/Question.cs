using LP.ServiceHost.DataContracts.Enums;

namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.Questions")]
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Answers = new HashSet<Answer>();
            ExamsQuestions = new HashSet<ExamsQuestion>();
            QuestionsOrders = new HashSet<QuestionsOrder>();
            Responses = new HashSet<Response>();
        }

        public int Id { get; set; }

        [Column("Question")]
        [Required]
        public string Question1 { get; set; }

        public byte StatusId { get; set; }

        public byte QuestionTypeId { get; set; }

        public int CreatedByUserId { get; set; }

        public int? LastUpdatedByUserId { get; set; }

        public string Comment { get; set; }

        public byte QuestionCategoryId { get; set; }

        [StringLength(50)]
        public string ImageName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastUpdated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamsQuestion> ExamsQuestions { get; set; }

        public virtual QuestionCategory QuestionCategory { get; set; }

        public virtual QuestionType QuestionType { get; set; }

        public Status Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionsOrder> QuestionsOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Response> Responses { get; set; }
    }
}
