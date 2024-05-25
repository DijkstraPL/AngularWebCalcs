using Build_IT_BeamStatica.Data;
using Build_IT_CommonTools.Attributes;
using Build_IT_WebApplication.Common.Mappings;
using MediatR;

namespace Build_IT_WebApplication.CivilCalculators.Statica.Commands.CalculateBeam
{
    public record SectionResource : IMapFrom<SectionData>, IMapTo<SectionData>
    {
        /// <summary>
        /// I - moment of inertia in cm^4
        /// </summary>
        public double MomentOfInteria { get; set; }

        /// <summary>
        /// A - area in cm^2
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// C - circumference in cm
        /// </summary>
        public double Circumference { get; set; }

        /// <summary>
        /// h - solid height in cm
        /// </summary>
        public double SolidHeight { get; set; }
    }
}
