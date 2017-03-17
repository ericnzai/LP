namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class GroupItemResponseContract
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public string NewsgroupName { get; set; }

        public int SortOrder { get; set; }

        public string Description { get; set; }

        public int? StatusBankID { get; set; }

        public int? TrainingAreaID { get; set; }

        public string FriendlyUrl { get; set; }
    }
}
