using LP.EntityModels;
using LP.Host.Integration.TestDataCreation;
using SpecsFor;
using SpecsFor.Configuration;

namespace LP.Host.Integration.Behaviours.Authentication.UserInformation
{
    public interface INeedANewlyCreatedUser : ISpecs
    {
        User User { get; set; }
        string UserName { get;  }
        int[] RolesIds { get;  }
    }

    public class UserCreationBehaviour : Behavior<INeedANewlyCreatedUser>
    {
        private readonly UserTestDataCreation _userTestDataCreation = new UserTestDataCreation();
      
        public override void Given(INeedANewlyCreatedUser instance)
        {
            InitUser(instance);
        }

        private void InitUser(INeedANewlyCreatedUser instance)
        {
            if (instance.User == null)
            {
                instance.User = _userTestDataCreation.CreateUser(instance.UserName, instance.RolesIds);
            }
        }

        public override void AfterSpec(INeedANewlyCreatedUser instance)
        {
            base.AfterSpec(instance);

            _userTestDataCreation.DeleteUser(instance.User.UserName);
        }
    }
}
