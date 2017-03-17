using System.Collections.Generic;
using LP.EntityModels;

namespace LP.Model.Dto
{
    

    public class FeatureAttachmentDto
    {
        public FeatureAttachmentDto()
        {
            
        }

        public FeatureAttachmentDto(ltl_FeatureAttachment featureAttachment)
        {
            FeatureAttachmentID = featureAttachment.FeatureAttachmentID;

        }

        public int FeatureAttachmentID { get; set; }

        public int FeatureAttachmentTypeID { get; set; }

        public int? CSPostID { get; set; }

        public string FileName { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int? Status { get; set; }

        public string Extra { get; set; }

        public string Parameters { get; set; }

        public int? CategoryID { get; set; }

        public int? SortOrder { get; set; }

        public ICollection<ltl_ClientAppFeatureAttachmentVisiblity> ltl_ClientAppFeatureAttachmentVisiblity { get; set; }

        public ltl_FeatureAttachmentCategory ltl_FeatureAttachmentCategory { get; set; }

        public ltl_FeatureAttachmentType ltl_FeatureAttachmentType { get; set; }

        public ltl_Posts ltl_Posts { get; set; }

        public ICollection<ltl_FeatureAttachment_CustomField> ltl_FeatureAttachment_CustomField { get; set; }
    }
}
