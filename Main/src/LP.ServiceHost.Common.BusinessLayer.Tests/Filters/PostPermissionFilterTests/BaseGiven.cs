using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.Common.BusinessLayer.Filters;
using Moq;
using SpecsFor;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Filters.PostPermissionFilterTests
{
    public class BaseGiven : SpecsFor<PostPermissionFilter>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();

        protected List<ltl_Posts> Posts = new List<ltl_Posts>();
        protected List<GroupPermission> GroupPermissions = new List<GroupPermission>();
        protected UserDetails UserDetails = new UserDetails {UserId = 365, RoleIds = new List<int>{1,2,3}};

        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(m => m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<GroupPermission, bool>>>(),
                    It.IsAny<Expression<Func<GroupPermission, object>>[]>())).ReturnsAsync(GroupPermissions.AsQueryable());

            SUT = new PostPermissionFilter(BaseCommandsMock.Object);
        }
    }
}
