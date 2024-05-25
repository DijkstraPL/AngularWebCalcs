using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_SnowLoads
{
    /// <summary>
    /// Class containing informations about the building.
    /// </summary>
    /// <example>
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         BuildingSite buildingSite = new BuildingSite();
    ///         SnowLoad snowLoad = new SnowLoad(buildingSite, DesignSituation.A, false);
    ///         Building building = new Building(snowLoad, 15, 3);
    ///         building.CalculateThermalCoefficient();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class Building : IBuilding
    {
        /// <summary>
        /// Thermal coefficient - 
        /// coefficient defining the reduction of snow load on roofs
        /// as a function of the heat flux through the roof, causing snow melting.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 5.2.(8)]</remarks>
        [Abbreviation("C_t")]
        [Unit("")]
        public double ThermalCoefficient { get; private set; }

        private double internalTemperature;
        /// <summary>
        /// Internal temperature in degrees of Celsius
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 NB1.8]</remarks>
        [Abbreviation("t_i")]
        [Unit("C")]
        public double InternalTemperature
        {
            get { return internalTemperature; }
            private set
            {
                if (value > 5 && value <= 18)
                    internalTemperature = value;
                else if (value > 18)
                    internalTemperature = 18;
                else
                    internalTemperature = 0;
            }
        }

        /// <summary>
        /// Difference between temperatures
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (NB.2)]</remarks>
        [Abbreviation("delta-t")]
        [Unit("C")]
        public double TempreatureDifference { get; private set; }

        /// <summary>
        /// Overall heat transfer coefficient - refers to how well heat is conducted over a series of mediums.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 NB1.8]</remarks>
        [Abbreviation("U")]
        [Unit("W/(m2*K)")]
        public double OverallHeatTransferCoefficient { get; }

        /// <summary>
        /// Instance of class implementing <see cref="ISnowLoad"/>.
        /// </summary>
        public ISnowLoad SnowLoad { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class.
        /// </summary>
        /// <param name="snowLoad">Set instance of a class implementing <see cref="ISnowLoad"/> for <see cref="SnowLoad"/>.</param>
        public Building(ISnowLoad snowLoad)
        {
            SnowLoad = snowLoad;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Building"/> class.
        /// </summary>
        /// <param name="snowLoad">Set instance of a class implementing <see cref="ISnowLoad"/> for <see cref="SnowLoad"/>.</param>
        /// <param name="internalTemperature">Set <see cref="InternalTemperature"/>.</param>
        /// <param name="overallHeatTransferCoefficient">Set <see cref="OverallHeatTransferCoefficient"/>.</param>
        public Building(ISnowLoad snowLoad, double internalTemperature, double overallHeatTransferCoefficient)
        {
            SnowLoad = snowLoad;
            InternalTemperature = internalTemperature;
            OverallHeatTransferCoefficient = overallHeatTransferCoefficient;
        }

        /// <summary>
        /// Calculate <see cref="ThermalCoefficient"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (NB.1)]</remarks>
        public void CalculateThermalCoefficient()
        {
            CalculateTempreatureDifference();

            if (OverallHeatTransferCoefficient >= 1 && OverallHeatTransferCoefficient <= 4.5 && InternalTemperature > 5)
                ThermalCoefficient = 1 - 0.054 * Math.Pow(SnowLoad.SnowLoadForSpecificReturnPeriod / 3.5, 0.25) *
                    TempreatureDifference * Math.Pow(Math.Sin(Math.PI / 180 * 57.3 * (0.4 * OverallHeatTransferCoefficient - 0.1)), 0.25);
            else
                ThermalCoefficient = 1;
        }

        /// <summary>
        /// Calculate <see cref="TempreatureDifference"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (NB.2)]</remarks>
        private void CalculateTempreatureDifference()
        {
            TempreatureDifference = InternalTemperature - 5;
        }
    }
}
