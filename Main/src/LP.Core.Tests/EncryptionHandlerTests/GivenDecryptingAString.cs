using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LP.Core.Tests.EncryptionHandlerTests
{
    public class GivenDecryptingAString : BaseGiven
    {
        private string _encryptedString;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheStringIsNotEncrypted : GivenDecryptingAString
        {
            private const string NonEncryptedString = "this is not an encrypted string";

            protected override void When()
            {
                _encryptedString = SUT.DecryptString(NonEncryptedString);
            }

            [Test]
            public void ThenEncryptedStringIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_encryptedString));
            }

            [Test]
            public void ThenStringIsDecryptedCorrectly()
            {
                Assert.AreEqual(NonEncryptedString, _encryptedString);
            }
        }
        public class WhenStringLengthIsOneCharacter : GivenDecryptingAString
        {
            protected override void When()
            {
                _encryptedString = SUT.DecryptString("~~G90b5bULXUA=");
            }

            [Test]
            public void ThenEncryptedStringIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_encryptedString));
            }

            [Test]
            public void ThenStringIsDecryptedCorrectly()
            {
                const string expected = "t";

                Assert.AreEqual(expected, _encryptedString);
            }
        }
        public class WhenTheStringIsAlreadyEncrypted : GivenDecryptingAString
        {
            private const string EncryptedString = "this is not an encrypted string";

            protected override void When()
            {
                _encryptedString = SUT.DecryptString(EncryptedString);
            }

            [Test]
            public void ThenEncryptedStringIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_encryptedString));
            }

            [Test]
            public void ThenStringIsDecryptedCorrectly()
            {
                Assert.AreEqual(EncryptedString, _encryptedString);
            }
        }
    }
}
