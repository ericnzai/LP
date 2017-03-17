using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LP.Api.Shared.Interfaces.Wrappers;

namespace LP.PresentationLayer.Wrappers
{
    public class ServerWrapper : IServerWrapper
    {
        public string MapAbsolutePath(string filePath)
        {
            return filePath.Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/");

          
        }
    }
}