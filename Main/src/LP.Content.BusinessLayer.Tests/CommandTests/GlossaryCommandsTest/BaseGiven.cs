using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.Data;
using LP.Content.BusinessLayer.Commands;
using LP.EntityModels;
using Moq;
using SpecsFor;

namespace LP.Content.BusinessLayer.Tests.CommandTests.GlossaryCommandsTest
{
    public class BaseGiven : SpecsFor<GlossaryCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();

        protected List<ltl_HoverOver> GlossaryItems = new List<ltl_HoverOver>();
        protected static ltl_HoverOverAudio HoverOverAudio = new ltl_HoverOverAudio
        {
            FileName = "Test glossary.mp3",
            HoverOverAudioID = ExistingHoverOverAudioId,
            IsEnabled = true,
            SourceFile = new byte[] { 0x1, 0x2, 0x5, 0x8}
        };

        protected static ltl_HoverOverAudio DisabledHoverOverAudio = new ltl_HoverOverAudio
        {
            FileName = "Test glossary Disabled.mp3",
            HoverOverAudioID = ExistingDisabledHoverOverAudioId,
            IsEnabled = false,
            SourceFile = new byte[] { 0x1, 0x2, 0x5, 0x8 }
        };

        protected const int ExistingHoverOverAudioId = 1;
        protected const int ExistingDisabledHoverOverAudioId = 2;
        protected const int NonExistantHoverOverAudioId = 3;
       
        protected ltl_HoverOver HoverOverWithEnabledAudio = new ltl_HoverOver
        {
            HoverOverID = 1,
            ltl_HoverOverAudio = HoverOverAudio,
            AudioFileID = ExistingHoverOverAudioId
        };
        protected ltl_HoverOver HoverOverWithDisabledAudio = new ltl_HoverOver
        {
            HoverOverID = 2,
            ltl_HoverOverAudio = DisabledHoverOverAudio,
            AudioFileID = ExistingDisabledHoverOverAudioId
        };
        protected void PrepareSut()
        {
            
            BaseCommandsMock.Setup(
                m =>
                    m.GetByIdAsync<ltl_HoverOverAudio>(
                        It.Is<int>(hoverOverAudioId => hoverOverAudioId == ExistingHoverOverAudioId)))
                .ReturnsAsync(HoverOverAudio);

            BaseCommandsMock.Setup(
               m =>
                   m.GetByIdAsync<ltl_HoverOverAudio>(
                       It.Is<int>(hoverOverAudioId => hoverOverAudioId == ExistingDisabledHoverOverAudioId)))
               .ReturnsAsync(DisabledHoverOverAudio);

            BaseCommandsMock.Setup(m => m.GetWithIncludesAsync<ltl_HoverOver>(It.IsAny<Expression<Func<ltl_HoverOver, object>>[]>())).ReturnsAsync(GlossaryItems.AsQueryable());

            BaseCommandsMock.Setup(
                m =>
                    m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<ltl_HoverOver, bool>>>(),
                        It.IsAny<Expression<Func<ltl_HoverOver, object>>[]>()))
                .ReturnsAsync(GlossaryItems.AsQueryable());
                

            SUT = new GlossaryCommands(BaseCommandsMock.Object);
        }
    }
}
