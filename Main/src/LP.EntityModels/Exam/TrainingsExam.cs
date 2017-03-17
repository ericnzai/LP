namespace LP.EntityModels.Exam
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("exam.TrainingsExams")]
    public partial class TrainingsExam
    {
        public short Id { get; set; }

        public short ExamId { get; set; }

        public int PostId { get; set; }

        public int SectionId { get; set; }

        public int? ParentSectionId { get; set; }

        public int GroupId { get; set; }

        public bool Grouped { get; set; }

        public bool IsLive { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
