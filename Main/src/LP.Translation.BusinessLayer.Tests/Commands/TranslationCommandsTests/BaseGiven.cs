using System.Collections.Generic;
using System.Linq;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.Translation.BusinessLayer.Commands;
using Moq;
using SpecsFor;

namespace LP.Translation.BusinessLayer.Tests.Commands.TranslationCommandsTests
{
    public class BaseGiven : SpecsFor<TranslationCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();

        protected List<ResourceLocalization> ResourceLocalizations = new List<ResourceLocalization>
        {
            new ResourceLocalization {ResourceId = "ResId1", ResourceSet = "ResSet1", LocaleId = string.Empty, Value = "ResId1 ResSet1 global"},
            new ResourceLocalization {ResourceId = "ResId2", ResourceSet = "ResSet1", LocaleId = string.Empty, Value = "ResId2 ResSet1 global"},
            new ResourceLocalization {ResourceId = "ResId3", ResourceSet = "ResSet1", LocaleId = string.Empty, Value = "ResId3 ResSet1 global"},
            new ResourceLocalization {ResourceId = "ResId4", ResourceSet = "ResSet1", LocaleId = string.Empty, Value = "ResId4 ResSet1 global"},
            new ResourceLocalization {ResourceId = "ResId1", ResourceSet = "ResSet2", LocaleId = string.Empty, Value = "ResId1 ResSet2 global"},
            new ResourceLocalization {ResourceId = "ResId2", ResourceSet = "ResSet2", LocaleId = string.Empty, Value = "ResId2 ResSet2 global"},
            new ResourceLocalization {ResourceId = "ResId3", ResourceSet = "ResSet2", LocaleId = string.Empty, Value = "ResId3 ResSet2 global"},
            new ResourceLocalization {ResourceId = "ResId4", ResourceSet = "ResSet2", LocaleId = string.Empty, Value = "ResId4 ResSet2 global"},

            new ResourceLocalization {ResourceId = "ResId1", ResourceSet = "ResSet1", LocaleId = "en", Value = "ResId1 ResSet1 en"},
            new ResourceLocalization {ResourceId = "ResId2", ResourceSet = "ResSet1", LocaleId = "en", Value = "ResId2 ResSet1 en"},
            new ResourceLocalization {ResourceId = "ResId3", ResourceSet = "ResSet1", LocaleId = "en", Value = "ResId3 ResSet1 en"},
            new ResourceLocalization {ResourceId = "ResId4", ResourceSet = "ResSet1", LocaleId = "en", Value = "ResId4 ResSet1 en"},
            new ResourceLocalization {ResourceId = "ResId1", ResourceSet = "ResSet2", LocaleId = "en", Value = "ResId1 ResSet2 en"},
            new ResourceLocalization {ResourceId = "ResId2", ResourceSet = "ResSet2", LocaleId = "en", Value = "ResId2 ResSet2 en"},
            new ResourceLocalization {ResourceId = "ResId3", ResourceSet = "ResSet2", LocaleId = "en", Value = "ResId3 ResSet2 en"},
            new ResourceLocalization {ResourceId = "ResId4", ResourceSet = "ResSet2", LocaleId = "en", Value = "ResId4 ResSet2 en"},

            new ResourceLocalization {ResourceId = "ResId1", ResourceSet = "ResSet1", LocaleId = "ru", Value = "ResId1 ResSet1 ru"},
            new ResourceLocalization {ResourceId = "ResId2", ResourceSet = "ResSet1", LocaleId = "ru", Value = "ResId2 ResSet1 ru"},
            //new ResourceLocalization {ResourceId = "ResId3", ResourceSet = "ResSet1", LocaleId = "ru", Value = "ResId3 ResSet1 ru"},
            new ResourceLocalization {ResourceId = "ResId4", ResourceSet = "ResSet1", LocaleId = "ru", Value = "ResId4 ResSet1 ru"},
            new ResourceLocalization {ResourceId = "ResId1", ResourceSet = "ResSet2", LocaleId = "ru", Value = "ResId1 ResSet2 ru"},
            new ResourceLocalization {ResourceId = "ResId2", ResourceSet = "ResSet2", LocaleId = "ru", Value = "ResId2 ResSet2 ru"},
            new ResourceLocalization {ResourceId = "ResId3", ResourceSet = "ResSet2", LocaleId = "ru", Value = "ResId3 ResSet2 ru"},
            new ResourceLocalization {ResourceId = "ResId4", ResourceSet = "ResSet2", LocaleId = "ru", Value = "ResId4 ResSet2 ru"},
        }; 

        protected TranslationRequestContract TranslationRequestContract = new TranslationRequestContract
        {
            TranslationRequests = new List<TranslationRequest>
            {
                new TranslationRequest {ResourceId = "ResId1", ResourceSet = "ResSet1"},
                new TranslationRequest {ResourceId = "ResId2", ResourceSet = "ResSet1"},
                new TranslationRequest {ResourceId = "ResId3", ResourceSet = "ResSet1"},
                new TranslationRequest {ResourceId = "ResId4", ResourceSet = "ResSet1"},
                new TranslationRequest {ResourceId = "ResId1", ResourceSet = "ResSet2"},
                new TranslationRequest {ResourceId = "ResId2", ResourceSet = "ResSet2"},
                new TranslationRequest {ResourceId = "ResId3", ResourceSet = "ResSet2"},
                new TranslationRequest {ResourceId = "ResId4", ResourceSet = "ResSet2"}
            }
        };

        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(m => m.GetAllAsync<ResourceLocalization>())
                .ReturnsAsync(ResourceLocalizations.AsQueryable());

            SUT = new TranslationCommands(BaseCommandsMock.Object);
        }
    }
}
