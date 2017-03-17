namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_SectionTranslationNotifications
    {
        [Key]
        public int SectionTranslationNotificationId { get; set; }

        public int SectionId { get; set; }

        public int GroupTypeId { get; set; }

        public int GlobalGroupId { get; set; }

        public int CultureGroupId { get; set; }

        public int TranslationNotificationActionsId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Comments { get; set; }

        public virtual ltl_Sections ltl_Sections { get; set; }
    }
}
