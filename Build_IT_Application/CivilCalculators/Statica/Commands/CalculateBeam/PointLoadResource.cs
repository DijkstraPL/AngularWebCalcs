using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Enums;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam
{
    public record PointLoadResource : IMapFrom<PointLoadData>, IMapTo<PointLoadData>
    {
        public double Position { get; set; }
        public double Value { get; set; }
        public double Angle { get; set; }
        public PointLoadType PointLoadType { get; set; }
    }
}
