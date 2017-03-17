using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using iTextSharp.text;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Response.Content;
namespace LP.Content.BusinessLayer.Commands
{
    public class GlossaryCommands : IGlossaryCommands
    {
        private readonly IBaseCommands _baseCommands;

        public GlossaryCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<GlossaryItemsResponseContract> GetAllGlossaryItems(string culture)
        {
            // to do: include also the mapping table for glossary and training modules
            var glossaryItems = await _baseCommands.GetWithIncludesAsync<ltl_HoverOver>(x=>x.ltl_HoverOverAudio);

            var result = new GlossaryItemsResponseContract();
            result.GlossaryItems.AddRange(glossaryItems.Where(g=>g.Culture == culture).Select(g=>new GlossaryItem
            {
                Description = g.Description,
                Title = g.Title,
                //TrainingModules = g.TrainingModules
                TrainingModules = "1",
                GlossaryItemId = g.HoverOverID,
                HasAudio = g.AudioFileID.HasValue && (g.ltl_HoverOverAudio != null && g.ltl_HoverOverAudio.IsEnabled)
            }
            )
        );

            return result;
        }

        public async Task<GlossaryAudioResponseContract> GetGlossaryAudio(int glossaryItemId)
        {
            var hoverOvers = await _baseCommands.GetConditionalWithIncludesAsync<ltl_HoverOver>(y => y.HoverOverID == glossaryItemId, x => x.ltl_HoverOverAudio);//.GetConditionalWithIncludesAsync()

            var hoverOver = hoverOvers.FirstOrDefault(id => id.HoverOverID == glossaryItemId);

            if (hoverOver == null || !hoverOver.AudioFileID.HasValue) return null;

            var hoverOverAudio = await _baseCommands.GetByIdAsync<ltl_HoverOverAudio>(hoverOver.AudioFileID.Value);// hoverOver.ltl_HoverOverAudio;

            if (hoverOverAudio == null) return null;

            var audioBytesAsBase64String = string.Format("data:audio/mp3;base64,{0}", Convert.ToBase64String(hoverOverAudio.SourceFile));

            var glossaryAudioResponseContract = new GlossaryAudioResponseContract
            {
                FileName = hoverOverAudio.FileName,
                AudioBase64 = audioBytesAsBase64String,
                IsEnabled = hoverOverAudio.IsEnabled
            };

            return glossaryAudioResponseContract;
        }
    }
}
