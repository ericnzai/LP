using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Interfaces.Wrappers;
using LP.Content.BusinessLayer.Commands;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;

namespace LP.Content.BusinessLayer.Tests.CommandTests.VAConversionToolTranslationCommandsTests
{
    public class BaseGiven:SpecsFor<VAConversionToolTranslationCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IEncryptionHandler> EncryptionHandlerMock = new Mock<IEncryptionHandler>();
        protected readonly Mock<ICultureProvider> CultureProviderMock = new Mock<ICultureProvider>();
        protected readonly Mock<IPdfContent> PdfContentMock = new Mock<IPdfContent>();
        protected readonly Mock<IFactoryDirectoryInfoWrapper> FactoryDirectoryInfoWrapperMock = new Mock<IFactoryDirectoryInfoWrapper>();
        protected readonly Mock<IDirectoryInfoWrapper> DirectoryInfoWrapperMock = new Mock<IDirectoryInfoWrapper>();

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

        protected string PermPath = "C:/temp";

        protected User User = new User()
        {
            DisplayName = "admin"
        };

        protected FileInfo[] TempFiles = new List<FileInfo>()
        {
            new FileInfo("C:/temp/test.pdf")
        }.ToArray();

        protected FileInfo[] PermFiles = new List<FileInfo>()
        {
            new FileInfo("C:/perm/test.pdf")
        }.ToArray();

        
    

        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(
                m => m.GetWithIncludesAsync<VAConversionTool>(It.IsAny<Expression<Func<VAConversionTool, object>>[]>()))
                .ReturnsAsync(VAConversionTools.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetAllAsync<VAConversionTool>())
                .ReturnsAsync(VAConversionToolTranslations.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetById<User>(It.IsAny<int>())).Returns(User);
            PdfContentMock.Setup(m => m.GetPdfFileContent(It.IsAny<string>())).Returns("test");
            FactoryDirectoryInfoWrapperMock.Setup(m => m.CreateIfNotExists(It.IsAny<string>())).Returns(DirectoryInfoWrapperMock.Object);
            DirectoryInfoWrapperMock.Setup(m => m.GetFiles()).Returns(TempFiles);

            SUT = new VAConversionToolTranslationCommands(BaseCommandsMock.Object, EncryptionHandlerMock.Object, CultureProviderMock.Object, PdfContentMock.Object, FactoryDirectoryInfoWrapperMock.Object);
        }
    }
}
