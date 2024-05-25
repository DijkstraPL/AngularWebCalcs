using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Enums;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam
{
    public record ContinuousLoadResource : IMapFrom<ContinuousLoadData> , IMapTo<ContinuousLoadData>
    {
        public double StartPosition { get; set; }
        public double EndPosition { get; set; }
        public double StartValue { get; set; }
        public double EndValue { get; set; }
        public double Angle { get; set; }
        public ContinuousLoadType ContinuousLoadType { get; set; }
    }
}
