using System;
using LP.Host.Integration.TestDataCreation;
using NUnit.Framework;
using SpecsFor;
using SpecsFor.Configuration;

namespace LP.Host.Integration.Actions
{
    public class UserCreateActionAttribute : TestActionAttribute // Attribute, ITestAction
    {
        private readonly string _userNameToCreate;
        public int[] UserRoleIds { get; set; }
        private UserTestDataCreation _userTestDataCreation = new UserTestDataCreation();
        public UserCreateActionAttribute(string userNameToCreate, int[] userRoleIds)
        {
            _userNameToCreate = userNameToCreate;
            UserRoleIds = userRoleIds;
        }

        public override void BeforeTest(TestDetails testDetails)
        {
            Console.WriteLine("Before test: " + _userNameToCreate + "; Roles: " + UserRoleIds.ToString());
            Console.WriteLine("Fixture: " + testDetails.Fixture + " FullName: " + testDetails.FullName + " IsSuite: " + testDetails.IsSuite + " Method: " + testDetails.Method + " Type: " + testDetails.Type);
            _userTestDataCreation.CreateUser(_userNameToCreate, UserRoleIds);
        }

        public override void AfterTest(TestDetails testDetails)
        {
            Console.WriteLine("After test: " + _userNameToCreate + "; Roles: " + UserRoleIds.ToString());
            _userTestDataCreation.DeleteUser(_userNameToCreate);
        }

        public override ActionTargets Targets { get { return ActionTargets.Suite; } }

        
    }
}
