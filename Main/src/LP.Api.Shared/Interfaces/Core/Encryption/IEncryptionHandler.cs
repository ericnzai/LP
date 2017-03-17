namespace LP.Api.Shared.Interfaces.Core.Encryption
{
    public interface IEncryptionHandler
    {
        string EncryptString(string text);
        string DecryptString(string text);
    }
}
