using LP.Model.Authentication;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.CacheCommandsTests
{
    public class GivenGettingADecryptedUser : BaseGiven
    {
        private DecryptedUser _decryptedUser;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenGettingADecryptedUserByIdWhenTheUserExists : GivenGettingADecryptedUser
        {
            protected override void When()
            {
                _decryptedUser = SUT.GetDecryptedUser(1);
            }

            [Test]
            public void ThenDecryptedUserIsNotNull()
            {
                Assert.IsNotNull(_decryptedUser);
            }

            [Test]
            public void ThenDecryptedUserNameIsCorrect()
            {
                const string expected = "User 1";

                Assert.AreEqual(expected, _decryptedUser.DecryptedUserName);
            }
        }
        public class WhenGettingADecryptedUserByNameWhenTheUserExists : GivenGettingADecryptedUser
        {
            protected override void When()
            {
                _decryptedUser = SUT.GetDecryptedUser("User 1");
            }

            [Test]
            public void ThenDecryptedUserIsNotNull()
            {
                Assert.IsNotNull(_decryptedUser);
            }

            [Test]
            public void ThenDecryptedUserNameIsCorrect()
            {
                const int expected = 1;

                Assert.AreEqual(expected, _decryptedUser.UserId);
            }
        }
    }
}
