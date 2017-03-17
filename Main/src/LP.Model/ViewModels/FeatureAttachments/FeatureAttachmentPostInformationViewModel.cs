namespace LP.Model.ViewModels.FeatureAttachments
{
    public class FeatureAttachmentPostInformationViewModel
    {
        public string ParentSectionTitle { get; set; }
        public string SectionTitle { get; set; }
        public string GroupName { get; set; }
        public string PostTitle { get; set; }
        public string PostUrl { get; set; }

        public string DisplayLink
        {
            get
            {
                var result = string.Format("{0} > ", GroupName);

                if (!string.IsNullOrEmpty(ParentSectionTitle))
                {
                    result += string.Format("{0} > ", ParentSectionTitle);
                }
                
                result += string.Format("{0} > {1}", SectionTitle, PostTitle);
                
                return result;
            }
        }
    }
}
