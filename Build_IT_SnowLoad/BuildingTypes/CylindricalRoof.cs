using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.API;
using Build_IT_SnowLoads.Interfaces;
using System;
using System.Collections.Generic;

namespace Build_IT_SnowLoads.BuildingTypes
{
    /// <summary>
    /// Calculation class for cylindrical roofs.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 5.3.5]</remarks>
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
    ///         CylindricalRoof cylindricalRoof = new CylindricalRoof(building, 20, 10);
    ///         cylindricalRoof.CalculateDriftLength();
    ///         cylindricalRoof.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="MonopitchRoof"/>
    /// <seealso cref="PitchedRoof"/>
    /// <seealso cref="MultiSpanRoof"/>
    /// <seealso cref="RoofAbuttingToTallerConstruction"/>
    public class CylindricalRoof : ICalculatable, ILengthProvider
    {
        /// <summary>
        /// Width of the roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.6]</remarks>
        [Abbreviation("b")]
        [Unit("m")]
        public double Width { get; }

        /// <summary>
        /// Height of the roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.6]</remarks>
        [Abbreviation("h")]
        [Unit("m")]
        public double Height { get; }

        /// <summary>
        /// Length of the load.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.6]</remarks>
        [Abbreviation("l_s")]
        [Unit("m")]
        public double DriftLength { get; private set; }

        /// <summary>
        /// Snow load shape coefficient 3.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.6]</remarks>
        [Abbreviation("mi_3")]
        [Unit("")]
        public double ShapeCoefficient { get; private set; }

        /// <summary>
        /// Snow load on the roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.6]</remarks>
        [Abbreviation("s")]
        [Unit("kN/m2")]
        public double SnowLoadOnRoofValue { get; private set; }

        /// <summary>
        /// Snow load on roof for all cases.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.6]</remarks>
        public Dictionary<int, double> RoofCasesSnowLoad { get; private set; }

        /// <summary>
        /// Instance of class implementing <see cref="IBuilding"/>.
        /// </summary>
        public IBuilding Building { get; private set; }

        private ISnowLoad _snowLoad;
        private IBuildingSite _buildingSite;

        /// <summary>
        /// Initializes a new instance of the <see cref="CylindricalRoof"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="width">Set <see cref="Width"/> of the roof.</param>
        /// <param name="height">Set <see cref="Height"/> of the roof.</param>
        public CylindricalRoof(IBuilding building, double width, double height)
        {
            RoofCasesSnowLoad = new Dictionary<int, double>();

            Building = building;
            Width = width > 0 ? width : throw new ArgumentOutOfRangeException(nameof(width));
            Height = height > 0 ? height : throw new ArgumentOutOfRangeException(nameof(height));
            SetReferences();
        }

        /// <summary>
        /// Calculate <see cref="DriftLength"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.6]</remarks>
        public void CalculateDriftLength()
        {
            DriftLength = Math.Min((Math.Pow(Width, 2) + 4 * Math.Pow(Height, 2)) / (8 * Height) * Math.Sqrt(3), Width);
        }

        /// <summary>
        /// Calculate <see cref="SnowLoadOnRoofValue"/> 
        /// </summary>
        /// <seealso cref="CalculateSnowLoadShapeCoefficient"/>
        /// <seealso cref="CalculateSnowLoadOnRoof"/>
        /// <seealso cref="SetCasesSnowLoad"/>
        public void CalculateSnowLoad()
        {
            CalculateSnowLoadShapeCoefficient();
            CalculateSnowLoadOnRoof();
            SetCasesSnowLoad();
        }

        private void SetReferences()
        {
            _snowLoad = Building.SnowLoad;
            _buildingSite = _snowLoad.BuildingSite;
        }

        /// <summary>
        /// Calculate <see cref="ShapeCoefficient"/> for cylindrical roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.5]</remarks>
        private void CalculateSnowLoadShapeCoefficient()
        {
            ShapeCoefficient = 0.2 + 10 * Height / Width;
            if (ShapeCoefficient > 2)
                ShapeCoefficient = 2;
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

        /// <summary>
        /// Set <see cref="RoofCasesSnowLoad"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.6]</remarks>
        private void SetCasesSnowLoad()
        {
            RoofCasesSnowLoad.Clear();

            double snowLoad = CalculateSnowLoadOnRoof(0.8);
            RoofCasesSnowLoad.Add(1, snowLoad);
            RoofCasesSnowLoad.Add(2, SnowLoadOnRoofValue * 0.5);
            RoofCasesSnowLoad.Add(3, SnowLoadOnRoofValue);
        }

        /// <summary>
        /// Calculate snow load on roof.
        /// </summary>
        /// <param name="shapeCoefficient">Shape coefficient for roof.</param>
        /// <returns>Snow load on rooof.</returns>
        /// <seealso cref="SnowLoadCalc.CalculateSnowLoad(double, double, double, double)"/>
        private double CalculateSnowLoadOnRoof(double shapeCoefficient)
        {
            if (!_snowLoad.ExcepctionalSituation)
                return SnowLoadCalc.CalculateSnowLoad(
                        shapeCoefficient,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.SnowLoadForSpecificReturnPeriod);
            else
                return SnowLoadCalc.CalculateSnowLoad(
                        shapeCoefficient,
                        _buildingSite.ExposureCoefficient,
                        Building.ThermalCoefficient,
                        _snowLoad.DesignExceptionalSnowLoadForSpecificReturnPeriod);
        }
    }
}
