using System;

namespace LP.EntityModels.StoredProcedure.Output
{
    public class LastSectionsViewed
    {
        public short UserId { get; set; }
        public int SectionId { get; set; }
        public short GroupId { get; set; }
        public DateTime DateTime { get; set; }
        public string Subject { get; set; }
        public string FriendlyUrl { get; set; }
    }
}
