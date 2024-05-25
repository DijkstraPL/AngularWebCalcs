using Build_IT_CommonTools.Attributes;

namespace Build_IT_SnowLoads.Interfaces
{
    public interface ISnowLoad //: ISnowDensityProvider, IReturnPeriodProvider, IExceptionalSituationProvider
    {    
        /// <summary>
        /// Current design situation enumerator - <see cref="DesignSituation"/>.
        /// </summary>
        DesignSituation CurrentDesignSituation { get; }
        
        /// <summary>
        /// Is situation for calculations exceptional.
        /// </summary>
        bool ExcepctionalSituation { get; }

        /// <summary>
        /// Coefficient for exceptional snow loads.
        /// </summary>
        [Abbreviation("C_esl")]
        [Unit("")]
        double ExceptionalSnowLoadCoefficient { get; }

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
        int ReturnPeriod { get; }

        /// <summary>
        /// The design value of exceptional snow load on the ground for the given location. 
        /// </summary>
        [Abbreviation("s_Ad")]
        [Unit("kN/m2")]
        double DesignExceptionalSnowLoadForSpecificReturnPeriod { get; }

        /// <summary>
        /// Density of the snow.
        /// </summary>
        [Abbreviation("gamma")]
        [Unit("kN/m3")]
        double SnowDensity { get; }

        /// <summary>
        /// Characteristic value of snow on the ground at the relevant site.
        /// </summary>
        [Abbreviation("s_k")]
        [Unit("kN/m2")]
        double DefaultCharacteristicSnowLoad { get; }

        /// <summary>
        /// Instance of class implementing <see cref="IBuildingSite"/>.
        /// </summary>
        IBuildingSite BuildingSite { get; }

        /// <summary>
        /// Calculate characteristic snow load on ground.
        /// </summary>
        void CalculateSnowLoad();
    }
}
