namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_TranslationNotificationActions
    {
        [Key]
        public int TranslationNotificationActionsId { get; set; }

        [Required]
        [StringLength(50)]
        public string TranslationNotificationAction { get; set; }
    }
}
