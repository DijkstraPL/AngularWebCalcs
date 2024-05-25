using Build_IT_CommonTools.Attributes;

namespace Build_IT_SnowLoads.Interfaces
{
    public interface ISnowDensityProvider
    {       
        /// <summary>
        /// Density of the snow.
        /// </summary>
        [Abbreviation("gamma")]
        [Unit("kN/m3")]
        double SnowDensity { get; set; }
    }
}
