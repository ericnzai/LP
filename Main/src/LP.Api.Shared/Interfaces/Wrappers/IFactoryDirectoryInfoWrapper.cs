namespace LP.Api.Shared.Interfaces.Wrappers
{
    public interface IFactoryDirectoryInfoWrapper
    {
        IDirectoryInfoWrapper CreateIfNotExists(string arg);
    }
}
