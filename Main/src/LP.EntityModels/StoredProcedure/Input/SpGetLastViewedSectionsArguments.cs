using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.EntityModels.StoredProcedure.Input
{
    public class SpGetLastViewedSectionsArguments
    {
        public int UserId { get; set; }
        public string GroupIdListAsString { get; set; }
    }
}
