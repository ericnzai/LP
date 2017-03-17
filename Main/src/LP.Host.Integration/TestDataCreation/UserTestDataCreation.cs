using System;
using System.Collections.ObjectModel;
using System.Linq;
using LP.Api.Shared.Interfaces.Data;
using LP.Data.Commands;
using LP.EntityModels;
using LP.Host.Integration.IoC;
using Ninject;

namespace LP.Host.Integration.TestDataCreation
{
    internal class UserTestDataCreation
    {
        private IBaseCommands _baseCommands;

        public User CreateUser(string userName, int[] userRoleIds, int countryId = 7, int userStatusId = 1, int parentId = 1, string culture = "en")
        {
            /*
             INSERT INTO askCore_Users (UserName, [Password], DateCreated, CountryID, AuthenticationTypeID, UserStatusID, Culture, ParentID, DisplayName, DeActivated)
VALUES ('TestUser979', '~~LmrPwrd0HfTtx9278W5eAA==', GETDATE(), 7, 1, 1, 'en', 1, 'TestUser979', 0)
             */
            SetupBaseCommands();

            var user = new User
            {
                UserName = userName,
                askCore_UsersRoles = new Collection<UserRole>(),
                Password = "~~LmrPwrd0HfTtx9278W5eAA==",
                DateCreated = DateTime.UtcNow,
                CountryID = countryId,
                AuthenticationTypeID = 1,
                UserStatusID = userStatusId,
                Culture = culture,
                ParentID = parentId,
                DisplayName = string.Format("{0} Display", userName)
            };

            foreach (var userRoleId in userRoleIds)
            {
                user.askCore_UsersRoles.Add(new UserRole {RoleID = userRoleId});
            }

            

            _baseCommands.Add(user);
            _baseCommands.SaveChanges();
            
            return user;

        }

        private void SetupBaseCommands()
        {
            if (_baseCommands == null)
            {
                _baseCommands = SetupNinjectDependencies.CreateKernel().Get<BaseCommands>();
            }
        }

        public void DeleteUser(string userName)
        {
            SetupBaseCommands();

            var user = _baseCommands.GetAll<User>().FirstOrDefault(a => a.UserName == userName);

            if (user != null)
            {
                var userRoles = _baseCommands.GetAll<UserRole>().Where(u => u.UserID == user.UserID);

                if (userRoles != null && userRoles.Any())
                {
                    foreach (var userRole in userRoles)
                    {
                       _baseCommands.Delete(userRole); 
                    }

                    _baseCommands.SaveChanges();
                }

                _baseCommands.Delete(user);
                _baseCommands.SaveChanges();
            }
        }
    }
}
