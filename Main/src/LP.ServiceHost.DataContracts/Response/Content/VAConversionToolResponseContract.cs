using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class VAConversionToolResponseContract
    {
        public string Content { get; set; }

        public string FileName { get; set; }

        public string History { get; set; }

        public List<VAConversionToolContract> ConversionToolHistory { get; set; }
    }
}
