using Build_IT_CommonTools.Attributes;

namespace Build_IT_SnowLoads.Interfaces
{
    public interface IReturnPeriodProvider
    {
        /// <summary>
        /// Characteristic ground snow load with a return period of n years.
        /// </summary>
        [Abbreviation("s_n")]
        [Unit("kN/m2")]
        double SnowLoadForSpecificReturnPeriod { get; }

        /// <summary>
        /// Coefficient of variation of annual maximum snow load.
        /// </summary>
        [Abbreviation("V")]
        [Unit("")]
        double VariationCoefficient { get; }
        
        /// <summary>
        /// Years of return period.
        /// </summary>
        [Abbreviation("n")]
        [Unit("year")]
        int ReturnPeriod { get; set; }

        /// <summary>
        /// The design value of exceptional snow load on the ground for the given location. 
        /// </summary>
        [Abbreviation("s_Ad")]
        [Unit("kN/m2")]
        double DesignExceptionalSnowLoadForSpecificReturnPeriod { get; }
    }
}