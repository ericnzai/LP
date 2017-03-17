using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Api.Shared.Interfaces.Data;
using LP.Content.BusinessLayer.Commands;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using SpecsFor;

namespace LP.Content.BusinessLayer.Tests.CommandTests.VAConversionToolCommandsTests
{
    public class BaseGiven : SpecsFor<VAConversionToolCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IEncryptionHandler> EncryptionHandlerMock = new Mock<IEncryptionHandler>();
        protected readonly Mock<ICultureProvider> CultureProviderMock = new Mock<ICultureProvider>();
        protected readonly Mock<IPdfContent> PdfContentMock = new Mock<IPdfContent>();
        protected readonly Mock<ICultureMenuCommands> CultureMenuCommandsMock = new Mock<ICultureMenuCommands>();

        protected List<VAConversionTool> VAConversionTools = new List<VAConversionTool>()
        {
            new VAConversionTool()
            {
                DateCreated = DateTime.UtcNow,
                FileName = "test.pdf",
                Comments = "comments",
                Culture = "en",
                Status = Status.Live,
                CreatedByUserId = 1,
                CreatedByUser = new User()
                {
                    DisplayName = "admin"
                }
            }
        };

        protected List<VAConversionTool> VAConversionToolTranslations = new List<VAConversionTool>()
        {
            new VAConversionTool()
            {
                DateCreated = DateTime.UtcNow,
                FileName = "test.pdf",
                Status = Status.Live,
                Culture = "en"
            }
        };

        protected UserDetails UserDetail = new UserDetails()
        {
            CurrentCulture = "en",
            CultureRoleIds = new List<int>() {1, 2, 9}
        };

        protected string PermPath = "C:/temp";

        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(
                m => m.GetWithIncludesAsync<VAConversionTool>(It.IsAny<Expression<Func<VAConversionTool, object>>[]>()))
                .ReturnsAsync(VAConversionTools.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetAllAsync<VAConversionTool>())
                .ReturnsAsync(VAConversionToolTranslations.AsQueryable());
            PdfContentMock.Setup(m => m.GetPdfFileContent(It.IsAny<string>())).Returns("test");
            CultureMenuCommandsMock.Setup(m => m.GetAvailableCulturesExceptEnglishGlobal(It.IsAny<UserDetails>()))
                .ReturnsAsync(new CompleteCultureMenuResponseContract()
                {
                    AvailableCultures = new Dictionary<string, string>()
                    {
                        {"en", "English (Global)"},

                        {
                            "en-CA",
                            "English-Canada"
                        },

                        {
                            "fr-CA",
                            "French-Canada"
                        }
                        ,
                        {
                            "zh",
                            "Chinese"
                        }
                    }
                });

            SUT = new VAConversionToolCommands(BaseCommandsMock.Object, EncryptionHandlerMock.Object, CultureProviderMock.Object, null, PdfContentMock.Object, CultureMenuCommandsMock.Object);
        }
    }
}
