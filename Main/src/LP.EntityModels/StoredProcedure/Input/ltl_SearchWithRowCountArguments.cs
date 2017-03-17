namespace LP.EntityModels.StoredProcedure.Input
{
    public class ltl_SearchWithRowCountArguments
    {
        public string SearchString { get; set; }
        public string Culture { get; set; }
        public string UserRoles { get; set; }
        public int FromGroupID { get; set; }
        public int NotFromGroupID { get; set; }
        public string GroupID { get; set; }
        public string TopicID { get; set; }
    }
}
