using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Nodes.Enums;
using Build_IT_WebApplication.Common.Mappings;
using System.Collections.Generic;

namespace Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam
{
    public record NodeResource : IMapFrom<NodeData>, IMapTo<NodeData>
    {
        public double Angle { get; set; }
        public NodeType NodeType { get; set; }
        public IList<PointLoadResource> PointLoads { get; set; }
    }
}
