namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_ResponseBank
    {
        [Key]
        public int ResponseBankID { get; set; }

        public int StatusID { get; set; }

        public int QuestionID { get; set; }

        [StringLength(200)]
        public string AnswerIDs { get; set; }

        public int? CommentID { get; set; }

        public int? CBMValueID { get; set; }

        public DateTime RowVersion { get; set; }

        public int UserID { get; set; }

        public int AttemptID { get; set; }

        public virtual quiz_Attempt quiz_Attempt { get; set; }

        public virtual quiz_CBMValue quiz_CBMValue { get; set; }

        public virtual quiz_CommentBank quiz_CommentBank { get; set; }

        public virtual quiz_QuestionBank quiz_QuestionBank { get; set; }

        public virtual quiz_StatusBank quiz_StatusBank { get; set; }
    }
}
