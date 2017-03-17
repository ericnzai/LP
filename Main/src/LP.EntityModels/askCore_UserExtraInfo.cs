namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class askCore_UserExtraInfo
    {
        [Key]
        public int UserExtraInfoID { get; set; }

        public int UserID { get; set; }

        public int FieldDefinitionID { get; set; }

        public string Value { get; set; }

        [Column(TypeName = "image")]
        public byte[] BinaryValue { get; set; }

        [StringLength(50)]
        public string FileType { get; set; }

        [StringLength(250)]
        public string FileName { get; set; }

        public virtual askCore_FieldDefinition askCore_FieldDefinition { get; set; }

        public virtual User askCore_Users { get; set; }
    }
}
