using System.Collections.Generic;
using LP.EntityModels;
using LP.Host.Integration.TestDataCreation;
using SpecsFor;
using SpecsFor.Configuration;

namespace LP.Host.Integration.Behaviours.Content.Glossary
{
    public interface INeedSomeGlossaryItemsWithAudio : ISpecs
    {
        List<ltl_HoverOver> GlossaryItems { get; }
        List<ltl_HoverOver> InitialisedGlossaryItems { get; set; }
    }

    public class GlossaryAudioCreationBehaviour : Behavior<INeedSomeGlossaryItemsWithAudio>
    {
        private readonly GlossaryTestDataCreation _glossaryTestDataCreation = new GlossaryTestDataCreation();

        public override void Given(INeedSomeGlossaryItemsWithAudio instance)
        {
            instance.InitialisedGlossaryItems = _glossaryTestDataCreation.CreateGlossaryItems(instance.GlossaryItems);
        }

        public override void AfterSpec(INeedSomeGlossaryItemsWithAudio instance)
        {
            _glossaryTestDataCreation.DeleteGlossaryItems(instance.InitialisedGlossaryItems);
        }
    }
}
