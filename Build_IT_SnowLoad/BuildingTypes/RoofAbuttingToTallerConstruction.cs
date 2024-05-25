using Build_IT_CommonTools;
using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.API;
using Build_IT_SnowLoads.Interfaces;
using System;
using System.Collections.Generic;

namespace Build_IT_SnowLoads.BuildingTypes
{
    /// <summary>
    /// Calculation class for roofs abutting to taller constructions.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 5.3.6]</remarks>
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
    ///         RoofAbuttingToTallerConstruction roofAbuttingToTallerConstruction =
    ///           new RoofAbuttingToTallerConstruction(building, 20, 10, 2, 25, false);
    ///         roofAbuttingToTallerConstruction.CalculateDriftLength();
    ///         roofAbuttingToTallerConstruction.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="MonopitchRoof"/>
    /// <seealso cref="PitchedRoof"/>
    /// <seealso cref="MultiSpanRoof"/>
    /// <seealso cref="CylindricalRoof"/>
    public class RoofAbuttingToTallerConstruction : ICalculatable, ILengthProvider
    {
        /// <summary>
        /// Snow load shape coefficient due to sliiding of snow.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        [Abbreviation("mi_s")]
        [Unit("")]
        public double ShapeCoefficientSlidingSnow { get; private set; }

        /// <summary>
        /// Snow load shape coefficient due to wind.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        [Abbreviation("mi_w")]
        [Unit("")]
        public double ShapeCoefficientWind { get; private set; }

        /// <summary>
        /// Snow load shape coefficient.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        [Abbreviation("mi_2")]
        [Unit("")]
        public double ShapeCoefficient { get; private set; }

        /// <summary>
        /// Snow load shape coefficient at the end of drift length.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        [Abbreviation("mi")]
        [Unit("")]
        public double ShapeCoefficientAtTheEnd { get; private set; }

        /// <summary>
        /// Instance of class implementing <see cref="IMonopitchRoof"/>.
        /// </summary>
        public IMonopitchRoof UpperRoof { get; }

        /// <summary>
        /// Width of the taller building.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        [Abbreviation("b_1")]
        [Unit("m")]
        public double WidthOfUpperBuilding { get; }

        /// <summary>
        /// Width of the lower building.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        [Abbreviation("b_2")]
        [Unit("m")]
        public double WidthOfLowerBuilding { get; }

        /// <summary>
        /// Height difference between buildings.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        [Abbreviation("h")]
        [Unit("m")]
        public double HeightDifference { get; }

        /// <summary>
        /// Snow load on the roof (near the taller building).
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        [Abbreviation("s")]
        [Unit("kN/m2")]
        public double SnowLoadOnRoofValue { get; private set; }

        /// <summary>
        /// Snow load at the end of the drifting length. 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        [Abbreviation("s_2")]
        [Unit("kN/m2")]
        public double SnowLoadOnRoofValueAtTheEnd { get; private set; }

        /// <summary>
        /// Length of the drift.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        [Abbreviation("l_s")]
        [Unit("m")]
        public double DriftLength { get; private set; }

        /// <summary>
        /// Instance of class implementing <see cref="IBuilding"/>.
        /// </summary>
        public IBuilding Building { get; private set; }

        public Dictionary<int, double> SnowLoadsNearTallerBuilding { get; private set; }

        public const bool DefaultSnowFences = false;

        private ISnowLoad _snowLoad;
        private IBuildingSite _buildingSite;

        /// <summary>
        /// Initializes a new instance of the <see cref="CylindricalRoof"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="widthOfUpperBuilding">Set <see cref="WidthOfUpperBuilding"/>.</param>
        /// <param name="widthOfLowerBuilding">Set <see cref="WidthOfLowerBuilding"/>.</param>
        /// <param name="heightDifference">Set <see cref="HeightDifference"/>.</param>
        /// <param name="slopeOfHigherRoof">Set <see cref="IMonopitchRoof.Slope"/> for <see cref="UpperRoof"/>.</param>
        /// <param name="snowFencesOnHigherRoof">Set <see cref="IMonopitchRoof.SnowFences"/> for <see cref="UpperRoof"/>.</param>
        public RoofAbuttingToTallerConstruction(IBuilding building, double widthOfUpperBuilding, double widthOfLowerBuilding,
           double heightDifference, double slopeOfHigherRoof, bool snowFencesOnHigherRoof = DefaultSnowFences)
        {
            Building = building;

            UpperRoof = new MonopitchRoof(Building, slopeOfHigherRoof, snowFencesOnHigherRoof);

            WidthOfUpperBuilding = widthOfUpperBuilding > 0 ? widthOfUpperBuilding
                : throw new ArgumentOutOfRangeException(nameof(widthOfUpperBuilding));
            WidthOfLowerBuilding = widthOfLowerBuilding > 0 ? widthOfLowerBuilding
                : throw new ArgumentOutOfRangeException(nameof(widthOfLowerBuilding));
            HeightDifference = heightDifference > 0 ? heightDifference
                : throw new ArgumentOutOfRangeException(nameof(heightDifference));

            SetReferences();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CylindricalRoof"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="widthOfUpperBuilding">Set <see cref="WidthOfUpperBuilding"/>.</param>
        /// <param name="widthOfLowerBuilding">Set <see cref="WidthOfLowerBuilding"/>.</param>
        /// <param name="heightDifference">Set <see cref="HeightDifference"/>.</param>
        /// <param name="upperRoof">Set <see cref="UpperRoof"/>.</param>
        public RoofAbuttingToTallerConstruction(IBuilding building, double widthOfUpperBuilding, double widthOfLowerBuilding,
           double heightDifference, IMonopitchRoof upperRoof)
        {
            Building = building;

            UpperRoof = upperRoof;

            WidthOfUpperBuilding = widthOfUpperBuilding > 0 ? widthOfUpperBuilding
                : throw new ArgumentOutOfRangeException(nameof(widthOfUpperBuilding));
            WidthOfLowerBuilding = widthOfLowerBuilding > 0 ? widthOfLowerBuilding
                : throw new ArgumentOutOfRangeException(nameof(widthOfLowerBuilding));
            HeightDifference = heightDifference > 0 ? heightDifference
                : throw new ArgumentOutOfRangeException(nameof(heightDifference));

            SetReferences();
        }

        /// <summary>
        /// Calculate <see cref="DriftLength"/>.
        /// </summary>
        public void CalculateDriftLength()
        {
            DriftLength = 2 * HeightDifference;
            if (DriftLength < 5)
                DriftLength = 5;
            else if (DriftLength > 15)
                DriftLength = 15;
        }

        /// <summary>
        /// Calculate <see cref="SnowLoadOnRoofValue"/> and <see cref="SnowLoadOnRoofValueAtTheEnd"/>..
        /// </summary>
        /// <seealso cref="ICalculatable.CalculateSnowLoad"/>
        /// <seealso cref="CaluclateShapeCoefficients"/>
        /// <seealso cref="CalculateSnowLoadOnRoof"/>
        public void CalculateSnowLoad()
        {
            UpperRoof.CalculateSnowLoad();
            CaluclateShapeCoefficients();
            CalculateSnowLoadOnRoof();
            SetSnowLoadsNearTallerBuilding();
        }

        private void SetReferences()
        {
            _snowLoad = Building.SnowLoad;
            _buildingSite = _snowLoad.BuildingSite;
        }

        /// <summary>
        /// Calculate <see cref="ShapeCoefficientSlidingSnow"/>, <see cref="ShapeCoefficientWind"/>, 
        /// <see cref="ShapeCoefficient"/> and <see cref="ShapeCoefficientAtTheEnd"/>.
        /// </summary>
        /// <seealso cref="CalculateShapeCoefficientSlidingSnow"/>
        /// <seealso cref="CalculateShapeCoefficientWind"/>
        /// <seealso cref="CaluclateShapeCoefficient"/>
        /// <seealso cref="CalculateDriftLength"/>
        /// <seealso cref="CalculateShapeCoefficientAtTheEnd"/>
        private void CaluclateShapeCoefficients()
        {
            CalculateShapeCoefficientSlidingSnow();
            CalculateShapeCoefficientWind();
            CaluclateShapeCoefficient();

            if (DriftLength == 0)
                CalculateDriftLength();
            CalculateShapeCoefficientAtTheEnd();
        }

        /// <summary>
        /// Calculate <see cref="ShapeCoefficientSlidingSnow"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 5.3.6.(1)]</remarks>
        private void CalculateShapeCoefficientSlidingSnow()
        {
            if (UpperRoof.Slope <= 15)
                ShapeCoefficientSlidingSnow = 0.8;
            else
                ShapeCoefficientSlidingSnow = 0.5 * UpperRoof.SnowLoadOnRoofValue;
        }

        /// <summary>
        /// Calculate <see cref="ShapeCoefficientWind"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (5.8)]</remarks>
        private void CalculateShapeCoefficientWind()
        {
            ShapeCoefficientWind = Math.Min((WidthOfUpperBuilding + WidthOfLowerBuilding) / (2 * HeightDifference),
                _snowLoad.SnowDensity * HeightDifference / _snowLoad.SnowLoadForSpecificReturnPeriod);

            if (ShapeCoefficientWind < 0.8)
                ShapeCoefficientWind = 0.8;
            else if (ShapeCoefficientWind > 4)
                ShapeCoefficientWind = 4;
        }

        /// <summary>
        /// Calculate <see cref="ShapeCoefficient"/> for the roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 (5.7)]</remarks>
        private void CaluclateShapeCoefficient()
        {
            ShapeCoefficient = ShapeCoefficientSlidingSnow + ShapeCoefficientWind;
        }

        /// <summary>
        /// Calculate <see cref="ShapeCoefficientAtTheEnd"/> of drift length.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.7]</remarks>
        private void CalculateShapeCoefficientAtTheEnd()
        {
            if (DriftLength <= WidthOfLowerBuilding)
                ShapeCoefficientAtTheEnd = 0.8;
            else
                ShapeCoefficientAtTheEnd = ShapeCoefficient - WidthOfLowerBuilding / DriftLength * (ShapeCoefficient - 0.8);
        }

        /// <summary>
        /// Calculate <see cref="SnowLoadOnRoofValue"/> and <see cref="SnowLoadOnRoofValueAtTheEnd"/>.
        /// </summary>
        /// <seealso cref="SnowLoadCalc.CalculateSnowLoad(double, double, double, double)"/>
        private void CalculateSnowLoadOnRoof()
        {
            if (!_snowLoad.ExcepctionalSituation)
            {
                SnowLoadOnRoofValue =
                    SnowLoadCalc.CalculateSnowLoad(
                        ShapeCoefficient,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.SnowLoadForSpecificReturnPeriod);

                SnowLoadOnRoofValueAtTheEnd =
                    SnowLoadCalc.CalculateSnowLoad(
                        ShapeCoefficientAtTheEnd,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.SnowLoadForSpecificReturnPeriod);
            }
            else
            {
                SnowLoadOnRoofValue =
                    SnowLoadCalc.CalculateSnowLoad(
                        ShapeCoefficient,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod);

                SnowLoadOnRoofValueAtTheEnd =
                    SnowLoadCalc.CalculateSnowLoad(
                        ShapeCoefficientAtTheEnd,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod);
            }
        }

        private void SetSnowLoadsNearTallerBuilding()
        {
            SnowLoadsNearTallerBuilding = new Dictionary<int, double>();
            double snowLoadInFirstCase;

            if (!_snowLoad.ExcepctionalSituation)
            {
                snowLoadInFirstCase =
                    SnowLoadCalc.CalculateSnowLoad(
                        0.8,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.SnowLoadForSpecificReturnPeriod);
            }
            else
            {
                snowLoadInFirstCase =
                    SnowLoadCalc.CalculateSnowLoad(
                        0.8,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod);
            }

            SnowLoadsNearTallerBuilding.Add(1, snowLoadInFirstCase);
            SnowLoadsNearTallerBuilding.Add(2, SnowLoadOnRoofValue);
        }
    }
}
