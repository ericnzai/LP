using System.Security.Cryptography;
using LP.Core.Encryption;
using Moq;
using SpecsFor;

namespace LP.Core.Tests.EncryptionHandlerTests
{
    public class BaseGiven : SpecsFor<EncryptionHandler>
    {
        protected Mock<ICryptoTransform> EncryptionTransformMock = new Mock<ICryptoTransform>(); 
        protected Mock<ICryptoTransform> DecryptionTransformMock = new Mock<ICryptoTransform>();

        protected void PrepareSut()
        {
            SUT = new EncryptionHandler();
        }
    }
}
