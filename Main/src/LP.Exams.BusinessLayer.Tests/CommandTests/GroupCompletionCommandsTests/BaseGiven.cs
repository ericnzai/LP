using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.Exams.BusinessLayer.Commands;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.GroupCompletionCommandsTests
{
    public class BaseGiven : SpecsFor<GroupCompletionCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IPercentageCompletionCommands> PercentageCompletionCommandsMock = new Mock<IPercentageCompletionCommands>();
        protected readonly Mock<ITrainingAreaCommands> TrainingAreaCommandsMock = new Mock<ITrainingAreaCommands>();
        protected readonly Mock<ILastAreasViewedProvider> LastAreasViewedProviderMock = new Mock<ILastAreasViewedProvider>();
        protected readonly Mock<ICultureProvider> CultureProviderMock = new Mock<ICultureProvider>();
        protected readonly Mock<ICertificatesAchievedCommands> CertificatesAchievedCommandsMock = new Mock<ICertificatesAchievedCommands>();
        protected IGrouping<TrainingArea, IGrouping<ltl_GroupType, Group>> TrainingAreaGroupedByGroupType;
        protected List<GroupPercentageComplete> GroupsPercentageComplete = new List<GroupPercentageComplete>();
        protected List<ltl_UsersFavouriteGroup> UsersFavouriteGroups = new List<ltl_UsersFavouriteGroup>();
        protected const string DefaultGlobalCulture = "en";
        protected UserDetails UserDetails = new UserDetails
        {
            RoleIds = new List<int> { 1,3,5},
            UserId = 1
        };

        protected List<TrainingsExam> TrainingsExams =new List<TrainingsExam>(); 
        protected void PrepareSut()
        {
            TrainingAreaCommandsMock.Setup(
                m => m.GetTrainingAreaWithAllGroupInfo(It.IsAny<int>(), It.IsAny<List<int>>(), It.IsAny<AccessType>()))
                .ReturnsAsync(TrainingAreaGroupedByGroupType);

            CultureProviderMock.Setup(m => m.GetCultureInfoWithDefault(It.IsAny<string>()))
                .Returns(CultureInfo.GetCultureInfo(DefaultGlobalCulture));

            CultureProviderMock.Setup(m => m.DefaultCultureString)
                .Returns(DefaultGlobalCulture);

            PercentageCompletionCommandsMock.Setup(
                m => m.PercentageAchievedForGroups(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(GroupsPercentageComplete);

            BaseCommandsMock.Setup(m => m.GetAllAsync<TrainingsExam>()).ReturnsAsync(TrainingsExams.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetAll<TrainingsExam>()).Returns(TrainingsExams.AsQueryable());

            BaseCommandsMock.Setup(m => m.GetConditionalWithIncludesAsync<TrainingsExam>(It.IsAny<Expression<Func<TrainingsExam, bool>>>(), It.IsAny<Expression<Func<TrainingsExam, object>>[]>())).ReturnsAsync(TrainingsExams.AsQueryable());
            
            BaseCommandsMock.Setup(
                m =>
                    m.GetConditionalAsync(
                        It.IsAny<Expression<Func<ltl_UsersFavouriteGroup, bool>>>()))
                .ReturnsAsync(UsersFavouriteGroups.AsQueryable());

            SUT = new GroupCompletionCommands(BaseCommandsMock.Object, PercentageCompletionCommandsMock.Object, TrainingAreaCommandsMock.Object, LastAreasViewedProviderMock.Object, 
                                                 CultureProviderMock.Object,  CertificatesAchievedCommandsMock.Object);
        }
    }
}
