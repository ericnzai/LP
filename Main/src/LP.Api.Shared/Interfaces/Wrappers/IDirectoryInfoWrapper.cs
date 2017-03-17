using System.IO;

namespace LP.Api.Shared.Interfaces.Wrappers
{
    public interface IDirectoryInfoWrapper
    {
        FileInfo[] GetFiles();
    }
}
