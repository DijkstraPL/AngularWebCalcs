using Build_IT_CommonTools.Attributes;

namespace Build_IT_SnowLoads.Interfaces
{
    public interface IBuilding //: ITemperatureProvider
    {      
        /// <summary>
        /// Thermal coefficient - 
        /// coefficient defining the reduction of snow load on roofs
        /// as a function of the heat flux through the roof, causing snow melting.
        /// </summary>
        [Abbreviation("C_t")]
        [Unit("")]
        double ThermalCoefficient { get; }
        
        /// <summary>
        /// Overall heat transfer coefficient - refers to how well heat is conducted over a series of mediums.
        /// </summary>
        [Abbreviation("U")]
        [Unit("W/(m2*K)")]
        double OverallHeatTransferCoefficient { get;  }

        /// <summary>
        /// Internal temperature in degrees of Celsius
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 NB1.8]</remarks>
        [Abbreviation("t_i")]
        [Unit("C")]
        double InternalTemperature { get;  }

        /// <summary>
        /// Difference between temperatures
        /// </summary>
        [Abbreviation("delta-t")]
        [Unit("C")]
        double TempreatureDifference { get; }

        /// <summary>
        /// Instance of class implementing <see cref="ISnowLoad"/>.
        /// </summary>
        ISnowLoad SnowLoad { get; }

        /// <summary>
        /// Calculate <see cref="ThermalCoefficient"/>.
        /// </summary>
        void CalculateThermalCoefficient();
    }
}
