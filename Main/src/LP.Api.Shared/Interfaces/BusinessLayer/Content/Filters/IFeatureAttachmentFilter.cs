using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.Model.Dto;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content.Filters
{
    public interface IFeatureAttachmentFilter
    {
        Task<IQueryable<ltl_FeatureAttachment>> FilterAllowedFeatureAttachments(UserDetails userDetails);
        Task<IEnumerable<FeatureAttachmentTranslationDto>> FilterAllowedFeatureAttachmentTranslations(UserDetails userDetails);
    }
}
