using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_SnowLoads.BuildingTypes
{
    /// <summary>
    /// Class for calculation of snow hanging over the roof.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 6.3]</remarks>
    /// <example>
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         BuildingSite buildingSite = new BuildingSite();
    ///         buildingSite.CalculateExposureCoefficient();
    ///         SnowLoad snowLoad = new SnowLoad(buildingSite, DesignSituation.A, false);
    ///         snowLoad.CalculateSnowLoad();
    ///         Building building = new Building(snowLoad, 15, 3);
    ///         building.CalculateThermalCoefficient();
    ///         SnowOverhanging snowOverhanging = new SnowOverhanging(building, 0.9);
    ///         snowOverhanging.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="DriftingAtProjectionsObstructions"/>
    /// <seealso cref="Snowguards"/>
    public class SnowOverhanging : ICalculatable
    {
        /// <summary>
        /// Thickness of the snow [m].
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.6.2]</remarks>
        [Abbreviation("d")]
        [Unit("m")]
        public double SnowLayerDepth { get; }

        /// <summary>
        /// Shape coefficient.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 6.3.(2)]</remarks>
        [Abbreviation("k")]
        [Unit("")]
        public double IrregularShapeCoefficient { get; private set; }

        /// <summary>
        /// Snow load on the roof - the most onerous undrifted load case
        /// appropriate for the roof under consideration [kN/m2].
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 6.3.(2)]</remarks>
        [Abbreviation("s")]
        [Unit("kN/m2")]
        public double SnowLoadOnRoofValue { get; }

        /// <summary>
        /// Snow load per metre length due to the overhang [kN/m].
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.6.2]</remarks>
        [Abbreviation("s_e")]
        [Unit("kN/m")]
        public double SnowLoad { get; private set; }

        /// <summary>
        /// Instance of building.
        /// </summary>
        public IBuilding Building { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnowOverhanging"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="snowLoadOnRoof">Set <see cref="SnowLoadOnRoofValue"/>.</param>
        public SnowOverhanging(IBuilding building, double snowLoadOnRoof)
        {
            Building = building;
            SnowLoadOnRoofValue = snowLoadOnRoof;

            SnowLayerDepth = SnowLoadOnRoofValue / Building.SnowLoad.SnowDensity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnowOverhanging"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="snowLayerDepth">Set <see cref="SnowLayerDepth"/>.</param>
        /// <param name="snowLoadOnRoof">Set <see cref="SnowLoadOnRoofValue"/>.</param>
        public SnowOverhanging(IBuilding building, double snowLayerDepth, double snowLoadOnRoof)
        {
            Building = building;
            SnowLayerDepth = snowLayerDepth > 0 ? snowLayerDepth 
                : throw new ArgumentOutOfRangeException(nameof(snowLayerDepth));
            SnowLoadOnRoofValue = snowLoadOnRoof;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnowOverhanging"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="snowLayerDepth">Set <see cref="SnowLayerDepth"/>.</param>
        /// <param name="roof">Is used to calculate <see cref="SnowLoadOnRoofValue"/>.</param>
        public SnowOverhanging(IBuilding building, double snowLayerDepth, IMonopitchRoof roof)
        {
            Building = building;
            SnowLayerDepth = snowLayerDepth;

            roof.CalculateSnowLoad();
            SnowLoadOnRoofValue = roof.SnowLoadOnRoofValue;
        }

        /// <summary>
        /// Calculate <see cref="IrregularShapeCoefficient"/> and <see cref="SnowLoad"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 6.3.(2)]</remarks>
        public void CalculateSnowLoad()
        {
            CalculateIrregularShapeCoefficient();
            CalculateOverhangingSnowLoad();
        }

        /// <summary>
        /// Calculate <see cref="IrregularShapeCoefficient"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 6.3.(2)]</remarks>
        private void CalculateIrregularShapeCoefficient()
        {
            IrregularShapeCoefficient = Math.Min(3 / SnowLayerDepth, SnowLayerDepth * Building.SnowLoad.SnowDensity);
        }

        /// <summary>
        /// Calculate <see cref="SnowLoad"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 6.3.(2)]</remarks>
        private void CalculateOverhangingSnowLoad()
        {
            SnowLoad = IrregularShapeCoefficient * Math.Pow(SnowLoadOnRoofValue, 2) / Building.SnowLoad.SnowDensity;
        }
    }
}
