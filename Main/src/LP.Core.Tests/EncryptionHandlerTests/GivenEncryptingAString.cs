using NUnit.Framework;

namespace LP.Core.Tests.EncryptionHandlerTests
{
    public class GivenEncryptingAString : BaseGiven
    {
        private string _encryptedString;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheStringIsNotEncrypted : GivenEncryptingAString
        {
            protected override void When()
            {
                _encryptedString = SUT.EncryptString("this is not an encrypted string");
            }

            [Test]
            public void ThenEncryptedStringIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_encryptedString));
            }

            [Test]
            public void ThenStringIsEncryptedCorrectly()
            {
                const string expected = "~~cAvLqyav/Avnb+d0qi4ElzhTNBJuZ2cAmlbOWI/zbzc=";

                Assert.AreEqual(expected, _encryptedString);
            }
        }
        public class WhenStringLengthIsOneCharacter : GivenEncryptingAString
        {
            protected override void When()
            {
                _encryptedString = SUT.EncryptString("t");
            }

            [Test]
            public void ThenEncryptedStringIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_encryptedString));
            }

            [Test]
            public void ThenStringIsEncryptedCorrectly()
            {
                const string expected = "~~G90b5bULXUA=";

                Assert.AreEqual(expected, _encryptedString);
            }
        }
        public class WhenTheStringIsAlreadyEncrypted : GivenEncryptingAString
        {
            private const string EncryptedString = "~~cAvLqyav/Avnb+d0qi4ElzhTNBJuZ2cAmlbOWI/zbzc=";

            protected override void When()
            {
                _encryptedString = SUT.EncryptString(EncryptedString);
            }

            [Test]
            public void ThenEncryptedStringIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_encryptedString));
            }

            [Test]
            public void ThenStringIsEncryptedCorrectly()
            {
                Assert.AreEqual(EncryptedString, _encryptedString);
            }
        }
    }
}
