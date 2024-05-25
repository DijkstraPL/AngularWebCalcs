using Build_IT_BeamStatica.Data;
using Build_IT_WebApplication.Common.Mappings;
using System.Collections.Generic;

namespace Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam
{
    public record BeamResource : IMapFrom<BeamData>, IMapTo<BeamData>
    {
        public bool IncludeSelfWeight { get; set; }
        public IList<SpanResource> SpanDatas { get; set; }

    }
}
