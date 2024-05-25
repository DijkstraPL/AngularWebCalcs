using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.API;
using Build_IT_SnowLoads.Interfaces;

namespace Build_IT_SnowLoads.BuildingTypes
{
    /// <summary>
    /// Calculation class for multi span roofs.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 5.3.4]</remarks>
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
    ///         MultiSpanRoof multiSpanRoof = new MultiSpanRoof(building, 35, 25, false , true);
    ///         multiSpanRoof.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="MonopitchRoof"/>
    /// <seealso cref="PitchedRoof"/>
    /// <seealso cref="CylindricalRoof"/>
    /// <seealso cref="RoofAbuttingToTallerConstruction"/>
    public class MultiSpanRoof : ICalculatable
    {
        /// <summary>
        /// Instance of class implementing <see cref="IMonopitchRoof"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 5.3.2]</remarks>
        public IMonopitchRoof LeftRoof { get; }

        /// <summary>
        /// Instance of class implementing <see cref="IMonopitchRoof"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 5.3.2]</remarks>
        public IMonopitchRoof RightRoof { get; }

        /// <summary>
        /// Snow load on middle roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.4]</remarks>
        [Abbreviation("s")]
        [Unit("kN/m2")]
        public double SnowLoadOnMiddleRoof { get; private set; }

        /// <summary>
        /// Snow load shape coefficient
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.4]</remarks>
        [Abbreviation("mi_2")]
        [Unit("")]
        public double ShapeCoefficient { get; private set; }
        
        /// <summary>
        /// Instance of class implementing <see cref="IBuilding"/>.
        /// </summary>
        public IBuilding Building { get; private set; }

        public const bool DefaultSnowFences = false;

        private ISnowLoad _snowLoad;
        private IBuildingSite _buildingSite;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSpanRoof"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="leftRoofSlope">Set <see cref="IMonopitchRoof.Slope"/> for <see cref="LeftRoof"/>.</param>
        /// <param name="rightRoofSlope">Set <see cref="IMonopitchRoof.Slope"/> for <see cref="RightRoof"/>.</param>
        /// <param name="leftRoofSnowFences">Set <see cref="IMonopitchRoof.SnowFences"/> for <see cref="LeftRoof"/>.</param>
        /// <param name="rightRoofSnowFences">Set <see cref="IMonopitchRoof.SnowFences"/> for <see cref="RightRoof"/>.</param>
        public MultiSpanRoof(IBuilding building, double leftRoofSlope, double rightRoofSlope,
            bool leftRoofSnowFences = DefaultSnowFences, bool rightRoofSnowFences = DefaultSnowFences)
        {
            Building = building;

            LeftRoof = new MonopitchRoof(Building, leftRoofSlope, leftRoofSnowFences);
            RightRoof = new MonopitchRoof(Building, rightRoofSlope, rightRoofSnowFences);

            SetReferences();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSpanRoof"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="leftRoof">Set instance of a class implementing <see cref="IMonopitchRoof"/> for <see cref="LeftRoof"/>.</param>
        /// <param name="rightRoof">Set instance of a class implementing <see cref="IMonopitchRoof"/> for <see cref="RightRoof"/>.</param>
        public MultiSpanRoof(IBuilding building, IMonopitchRoof leftRoof, IMonopitchRoof rightRoof)
        {
            Building = building;

            LeftRoof = leftRoof;
            RightRoof = rightRoof;

            SetReferences();
        }

        /// <summary>
        /// Calculate <see cref="IMonopitchRoof.SnowLoadOnRoofValue"/> for <see cref="LeftRoof"/> and <see cref="RightRoof"/>
        /// and in the middle <see cref="SnowLoadOnMiddleRoof"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.3]</remarks>
        /// <seealso cref="ICalculatable.CalculateSnowLoad"/>
        /// <seealso cref="ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient2(double)"/>
        /// <seealso cref="SnowLoadCalc.CalculateSnowLoad(double, double, double, double)"/>
        public void CalculateSnowLoad()
        {
            LeftRoof.CalculateSnowLoad();
            RightRoof.CalculateSnowLoad();
            CalculateSnowLoadShapeCoefficient();
            CalculateSnowLoadOnRoof();
        }

        private void SetReferences()
        {
            _snowLoad = Building.SnowLoad;
            _buildingSite = _snowLoad.BuildingSite;
        }

        /// <summary>
        /// Method calculate shape coefficient for multispan roof.
        /// </summary>
        /// <seealso cref="ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient2(double)"/>
        private void CalculateSnowLoadShapeCoefficient()
        {
            if (LeftRoof.Slope > 60 || RightRoof.Slope > 60)
                ShapeCoefficient = 1.6;
            else
                ShapeCoefficient = 
                    ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient2(
                        LeftRoof.Slope + RightRoof.Slope);
        }

        /// <summary>
        /// Calculate snow load on roof.
        /// </summary>
        /// <seealso cref="SnowLoadCalc.CalculateSnowLoad(double, double, double, double)"/>
        private void CalculateSnowLoadOnRoof()
        {
            if (!_snowLoad.ExcepctionalSituation)
                SnowLoadOnMiddleRoof =
                    SnowLoadCalc.CalculateSnowLoad(
                        ShapeCoefficient,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.SnowLoadForSpecificReturnPeriod);
            else
                SnowLoadOnMiddleRoof =
                    SnowLoadCalc.CalculateSnowLoad(
                        ShapeCoefficient,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod);
        }
    }
}
