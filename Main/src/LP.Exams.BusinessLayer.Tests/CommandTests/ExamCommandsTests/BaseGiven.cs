using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpectedObjects;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.Exams.BusinessLayer.Commands;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using NUnit.Framework;
using SpecsFor;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.ExamCommandsTests
{
    public class BaseGiven : SpecsFor<ExamCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();

        protected List<TrainingsExam> TrainingsExams = new List<TrainingsExam>
        {
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 1},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Deleted}, ExamId = 2},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 3},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 4},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.ComingSoon}, ExamId = 5},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 6},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 7},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.TranslationInProgress}, ExamId = 8},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 9},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.PreImport}, ExamId = 10},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 11},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 12},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Archived}, ExamId = 13},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 14},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 15},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Test}, ExamId = 16},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 17},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Review}, ExamId = 18},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 19},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 20},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.PreImport}, ExamId = 21},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 22},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.ComingSoon}, ExamId = 23},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 24},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 25},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Deleted}, ExamId = 26},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 27},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Review}, ExamId = 28},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 29},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 30},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.PreImport}, ExamId = 31},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 32},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.ComingSoon}, ExamId = 33},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 34},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 35},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Deleted}, ExamId = 36},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 1},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Deleted}, ExamId = 2},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 3},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 4},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.ComingSoon}, ExamId = 5},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 6},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 7},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.TranslationInProgress}, ExamId = 8},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 9},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.PreImport}, ExamId = 10},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 11},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 12},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Archived}, ExamId = 13},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 14},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 15},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Test}, ExamId = 16},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 17},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Review}, ExamId = 18},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 19},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 20},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.PreImport}, ExamId = 21},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 22},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.ComingSoon}, ExamId = 23},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 24},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Live}, ExamId = 25},
            new TrainingsExam {Exam = new Exam {StatusId = (byte) Status.Deleted}, ExamId = 26},

        };

        protected void PrepareSut()
        {
            var trainingsExamsMoqDbSet = new MoqDbSetProvider<TrainingsExam>().DbSet(TrainingsExams);

            BaseCommandsMock.Setup(
                m =>
                    m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<TrainingsExam, bool>>>(),
                        It.IsAny<Expression<Func<TrainingsExam, object>>[]>()))
                .ReturnsAsync(trainingsExamsMoqDbSet.Object);

            SUT = new ExamCommands(BaseCommandsMock.Object);
        }
    }
}
