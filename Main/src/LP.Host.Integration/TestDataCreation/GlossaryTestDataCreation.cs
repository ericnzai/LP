using System.Collections.Generic;
using System.Linq;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Host.Integration.Providers;

namespace LP.Host.Integration.TestDataCreation
{
    public class GlossaryTestDataCreation
    {
        private readonly IBaseCommands _baseCommands = BaseCommandsProvider.GetBaseCommandsInstance();

        public List<ltl_HoverOver> CreateGlossaryItems(List<ltl_HoverOver> glossaryItems)
        {
            foreach (var glossaryItem in glossaryItems)
            {
                _baseCommands.Add(glossaryItem);
            }

            _baseCommands.SaveChanges();

            return glossaryItems;
        }

        public void DeleteGlossaryItems(IEnumerable<ltl_HoverOver> glossaryItems)
        {
            var glossaryIds = glossaryItems.Select(g => g.HoverOverID);

            var hoverOvers = _baseCommands.GetAll<ltl_HoverOver>().Where(g => glossaryIds.Contains(g.HoverOverID));

            foreach (var hoverOver in hoverOvers)
            {
                _baseCommands.Delete(hoverOver);
            }

            _baseCommands.SaveChanges();
        }

    }
}
