using System.IO;
using LP.Api.Shared.Interfaces.Wrappers;

namespace LP.PresentationLayer.Wrappers
{
    public class DirectoryInfoWrapper : IDirectoryInfoWrapper
    {
        private readonly DirectoryInfo _dirInfo;

        public DirectoryInfoWrapper(DirectoryInfo dirInfo)
        {
            _dirInfo = dirInfo;
        }

        public FileInfo[] GetFiles()
        {
            return _dirInfo.GetFiles();
        }
    }
}