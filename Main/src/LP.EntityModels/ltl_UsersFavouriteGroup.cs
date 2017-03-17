namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_UsersFavouriteGroup
    {
        [Key]
        public int UsersFavouriteGroupID { get; set; }

        public int UserID { get; set; }

        public int GroupTypeID { get; set; }

        public int FavouriteGroupID { get; set; }

        public int? LinkTypeID { get; set; }

        public virtual User askCore_Users { get; set; }

        public virtual ltl_GroupType ltl_GroupType { get; set; }
    }
}
