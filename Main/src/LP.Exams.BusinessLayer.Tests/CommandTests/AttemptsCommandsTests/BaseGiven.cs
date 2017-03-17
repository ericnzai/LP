using System.Collections.Generic;
using System.Linq;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels.Exam;
using LP.Exams.BusinessLayer.Commands;
using Moq;
using SpecsFor;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.AttemptsCommandsTests
{
    public class BaseGiven : SpecsFor<AttemptsCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IFilterAllowedUser> AllowedUserFilterMock = new Mock<IFilterAllowedUser>();
        protected readonly Mock<IFilterAllowedGroups> FilterAllowedGroupsMock = new Mock<IFilterAllowedGroups>();

        protected List<Attempt> Attempts = new List<Attempt>
        {
            new Attempt {ExamId = 1, UserId = 1, AttemptPassed = false},
            new Attempt {ExamId = 2, UserId = 1, AttemptPassed = false},
            new Attempt {ExamId = 3, UserId = 1, AttemptPassed = false},
            new Attempt {ExamId = 4, UserId = 1, AttemptPassed = false},
            new Attempt {ExamId = 5, UserId = 1, AttemptPassed = false},
             new Attempt {ExamId = 1, UserId = 2, AttemptPassed = true},
            new Attempt {ExamId = 2, UserId = 2, AttemptPassed = false},
            new Attempt {ExamId = 3, UserId = 2, AttemptPassed = true},
            new Attempt {ExamId = 4, UserId = 2, AttemptPassed = false},
            new Attempt {ExamId = 5, UserId = 2, AttemptPassed = true},
        };

        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(m => m.GetAllAsync<Attempt>()).ReturnsAsync(Attempts.AsQueryable());

            SUT = new AttemptsCommands(BaseCommandsMock.Object, AllowedUserFilterMock.Object, FilterAllowedGroupsMock.Object);
        }
    }
}
