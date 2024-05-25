using Build_IT_BeamStatica.Data;
using Build_IT_CommonTools.Attributes;
using Build_IT_WebApplication.Common.Mappings;
using MediatR;

namespace Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam
{
    public record MaterialResource : IMapFrom<MaterialData>, IMapTo<MaterialData>
    {
        [Abbreviation("E_cm")]
        [Unit("GPa")]
        public double YoungModulus { get; set; }

        [Abbreviation("γ")]
        [Unit("100*kN/m3")]
        public double Density { get; set; }

        [Abbreviation("l_x")]
        [Unit("1/K")]
        public double ThermalExpansionCoefficient { get; set; }
    }
}
