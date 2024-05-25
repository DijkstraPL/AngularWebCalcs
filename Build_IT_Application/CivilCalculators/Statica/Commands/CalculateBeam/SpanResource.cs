using Build_IT_BeamStatica.Data;
using Build_IT_WebApplication.Common.Mappings;
using System.Collections.Generic;

namespace Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam
{
    public record SpanResource : IMapFrom<SpanData>, IMapTo<SpanData>
    {
        public double Length { get; set; }
        public MaterialResource Material { get; set; }
        public SectionResource Section { get; set; }
        public NodeResource LeftNode { get; set; }
        public NodeResource RightNode { get; set; }
        public bool IncludeSelfWeight { get; set; }

        public IList<ContinuousLoadResource> ContinuousLoads { get; set; }
        public IList<PointLoadResource> PointLoads { get; set; }

    }
}
