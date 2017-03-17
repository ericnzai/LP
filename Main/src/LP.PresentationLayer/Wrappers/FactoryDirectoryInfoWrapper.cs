using System.IO;
using LP.Api.Shared.Interfaces.Wrappers;

namespace LP.PresentationLayer.Wrappers
{
    public class FactoryDirectoryInfoWrapper : IFactoryDirectoryInfoWrapper
    {
        public IDirectoryInfoWrapper CreateIfNotExists(string arg)
        {
            var dirInfo = new DirectoryInfo(arg);
            if(!dirInfo.Exists) dirInfo.Create();
            var wrapper = new DirectoryInfoWrapper(dirInfo);
            return wrapper;
        }
    }
}