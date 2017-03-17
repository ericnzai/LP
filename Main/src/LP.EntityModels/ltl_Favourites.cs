namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_Favourites
    {
        [Key]
        public int FavouriteID { get; set; }

        public int? UserID { get; set; }

        public int? PostID { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual ltl_Posts ltl_Posts { get; set; }
    }
}
