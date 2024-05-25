using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.API;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_SnowLoads.BuildingTypes
{
    /// <summary>
    /// Calculation class for monopitch roofs.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 5.3.2]</remarks>
    /// <example>
    /// <code>
    ///  class TestClass
    ///  {
    ///      static void Main()
    ///      {
    ///          BuildingSite buildingSite = new BuildingSite();
    ///          buildingSite.CalculateExposureCoefficient();
    ///          SnowLoad snowLoad = new SnowLoad(buildingSite, DesignSituation.A, false);
    ///          snowLoad.CalculateSnowLoad();
    ///          Building building = new Building(snowLoad, 15, 3);
    ///          building.CalculateThermalCoefficient();
    ///          MonopitchRoof monopitchRoof = new MonopitchRoof(building, 35, true);
    ///          monopitchRoof.CalculateSnowLoad();
    ///      }
    ///  }
    /// </code>
    /// </example>
    /// <seealso cref="PitchedRoof"/>
    /// <seealso cref="MultiSpanRoof"/>
    /// <seealso cref="CylindricalRoof"/>
    /// <seealso cref="RoofAbuttingToTallerConstruction"/>
    public class MonopitchRoof : IMonopitchRoof
    {
        /// <summary>
        /// Slope for roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.2]</remarks>
        [Abbreviation("alpha")]
        [Unit("degree")]
        public double Slope { get; }

        /// <summary>
        /// Is there any obstacles, like snow fences.
        /// </summary>
        public bool SnowFences { get; }

        /// <summary>
        /// Snow load shape coefficient 1.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.2]</remarks>
        [Abbreviation("mi_1")]
        [Unit("")]
        public double ShapeCoefficient { get; private set; }

        /// <summary>
        /// Snow load on the roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.2]</remarks>
        [Abbreviation("s")]
        [Unit("kN/m2")]
        public double SnowLoadOnRoofValue { get; private set; }

        /// <summary>
        /// Instance of class implementing <see cref="IBuilding"/>.
        /// </summary>
        public IBuilding Building { get; private set; }

        private ISnowLoad _snowLoad;
        private IBuildingSite _buildingSite;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonopitchRoof"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="slope">Set <see cref="Slope"/>.</param>
        /// <param name="snowFences">Set <see cref="SnowFences"/>.</param>
        public MonopitchRoof(IBuilding building, double slope, bool snowFences = false)
        {
            Building = building;
            Slope = slope > 0 ? slope : throw new ArgumentOutOfRangeException(nameof(slope));
            SnowFences = snowFences;
            SetReferences();
        }

        /// <summary>
        /// Calculate <see cref="ShapeCoefficient"/> and <see cref="SnowLoadOnRoofValue"/>
        /// </summary>
        /// <seealso cref="ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(double, bool)"/>
        /// <seealso cref="SnowLoadCalc.CalculateSnowLoad(double, double, double, double)"/>
        public void CalculateSnowLoad()
        {
            CalculateSnowLoadShapeCoefficient();
            CalculateSnowLoadOnRoof();
        }

        private void SetReferences()
        {
            _snowLoad = Building.SnowLoad;
            _buildingSite = _snowLoad.BuildingSite;
        }

        /// <summary>
        /// Method calculate <see cref="ShapeCoefficient"/> for monopitch roof.
        /// </summary>
        /// <seealso cref="ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(double, bool)"/>
        private void CalculateSnowLoadShapeCoefficient()
        {
            ShapeCoefficient = ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(Slope, SnowFences);
        }

        /// <summary>
        /// Calculate <see cref="SnowLoadOnRoofValue"/>.
        /// </summary>
        /// <seealso cref="SnowLoadCalc.CalculateSnowLoad(double, double, double, double)"/>
        private void CalculateSnowLoadOnRoof()
        {
            if (!_snowLoad.ExcepctionalSituation)
                SnowLoadOnRoofValue =
                    SnowLoadCalc.CalculateSnowLoad(
                        ShapeCoefficient,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.SnowLoadForSpecificReturnPeriod);
            else
                SnowLoadOnRoofValue =
                    SnowLoadCalc.CalculateSnowLoad(
                        ShapeCoefficient,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod);
        }
    }
}
