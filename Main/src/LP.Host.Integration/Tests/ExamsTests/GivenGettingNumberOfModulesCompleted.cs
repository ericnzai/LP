using LP.EntityModels;
using LP.Host.Integration.Behaviours.Authentication.UserInformation;

namespace LP.Host.Integration.Tests.ExamsTests
{
    public class GivenGettingNumberOfModulesCompleted : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenCreatingANewUserAndGivingThemSomeNewGroupsAndResultsInThoseGroups :
            GivenGettingNumberOfModulesCompleted, INeedANewlyCreatedUser
        {
            public User User { get; set; }
            public string UserName { get { return "trainingareacompletetester@asandk.com"; }  }
            public int[] RolesIds { get { return new[] {1}; }}
        }
    }
}
