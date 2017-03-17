using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.Content.BusinessLayer.Filters;
using LP.EntityModels;
using LP.Model.Authentication;
using Moq;
using SpecsFor;

namespace LP.Content.BusinessLayer.Tests.FilterTests.FeatureAttachmentFilterTests
{
    public class BaseGiven : SpecsFor<FeatureAttachmentFilter>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IPostPermissionFilter> PostPermissionFilterMock = new Mock<IPostPermissionFilter>();
        protected UserDetails UserDetails = new UserDetails
        {
            UserId = 34,
            AvailableStatuses = new List<int> {1, 2, 3},
            RoleIds = new List<int> {6, 7, 8}
        };

        protected List<ltl_FeatureAttachment> FeatureAttachments = new List<ltl_FeatureAttachment>();
        protected List<GroupPermission> GroupPermissions = new List<GroupPermission>(); 
        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(
                m =>
                    m.GetConditionalWithIncludesAsync(
                        It.IsAny<Expression<Func<ltl_FeatureAttachment, bool>>>(),
                        It.IsAny<Expression<Func<ltl_FeatureAttachment, object>>[]>()))
                .ReturnsAsync(FeatureAttachments.AsQueryable());

            BaseCommandsMock.Setup(
                m =>
                    m.GetConditionalWithIncludesAsync(
                        It.IsAny<Expression<Func<GroupPermission, bool>>>(),
                        It.IsAny<Expression<Func<GroupPermission, object>>[]>()))
                .ReturnsAsync(GroupPermissions.AsQueryable());

            SUT = new FeatureAttachmentFilter(BaseCommandsMock.Object, PostPermissionFilterMock.Object);
        }
    }
}
