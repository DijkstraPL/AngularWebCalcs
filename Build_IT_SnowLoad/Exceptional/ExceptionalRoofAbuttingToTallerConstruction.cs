using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.API;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_SnowLoads.Exceptional
{
    /// <summary>
    /// Calculation class for exceptional roof abutting to taller construction.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 B3]</remarks>
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
    ///         ExceptionalRoofAbuttingToTallerConstruction exceptionalRoofAbuttingToTallerConstruction = 
    ///             new ExceptionalRoofAbuttingToTallerConstruction(building, 15, 5, 1, 30);
    ///         exceptionalRoofAbuttingToTallerConstruction.CalculateDriftLength();
    ///         exceptionalRoofAbuttingToTallerConstruction.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="ExceptionalMultiSpanRoof"/>
    /// <seealso cref="ExceptionalObstructionOnFlatRoof"/>
    /// <seealso cref="ExceptionalObstructionOnPitchedOrCurvedRoof"/>
    /// <seealso cref="ExceptionalOverDoorOrLoadingBay"/>
    /// <seealso cref="ExceptionalSnowBehindParapet"/>
    /// <seealso cref="ExceptionalSnowBehindParapetAtEaves"/>
    /// <seealso cref="ExceptionalSnowInValleyBehindParapet"/>
    public class ExceptionalRoofAbuttingToTallerConstruction : ICalculatable
    {
        /// <summary>
        /// Height difference.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("h")]
        [Unit("m")]
        public double HeightDifference { get; }

        /// <summary>
        /// Length of the drift.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("l_s")]
        [Unit("m")]
        public double DriftLength { get; private set; }

        /// <summary>
        /// Lower building width.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("b_1")]
        [Unit("m")]
        public double LowerBuildingWidth { get; }

        /// <summary>
        /// Upper building width.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("b_2")]
        [Unit("m")]
        public double UpperBuildingWidth { get; }

        /// <summary>
        /// Snow load shape coefficient 1.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("mi_1")]
        [Unit("")]
        public double ShapeCoefficient1 { get; private set; }
        /// <summary>
        /// Snow load shape coefficient 2.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("mi_2")]
        [Unit("")]
        public double ShapeCoefficient2 { get; private set; }
        /// <summary>
        /// Snow load shape coefficient 3.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("mi_3")]
        [Unit("")]
        public double ShapeCoefficient3 { get; private set; }

        /// <summary>
        /// Snow load near the top.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("s_1")]
        [Unit("kN/m2")]
        public double SnowLoadNearTheTop { get; private set; }
        /// <summary>
        /// Snow load near the edge.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("s_2")]
        [Unit("kN/m2")]
        public double SnowLoadNearTheEdge { get; private set; }

        /// <summary>
        /// slope of the lower roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("alfa")]
        [Unit("degree")]
        public double Angle { get; }

        public IBuilding Building { get; private set; }

        private ISnowLoad snowLoad;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionalRoofAbuttingToTallerConstruction"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="upperBuildingWidth">Set <see cref="UpperBuildingWidth"/>.</param>
        /// <param name="lowerBuildingWidth">Set <see cref="LowerBuildingWidth"/>.</param>
        /// <param name="heightDifference">Set <see cref="HeightDifference"/>.</param>
        /// <param name="angle">Set <see cref="Angle"/>.</param>
        public ExceptionalRoofAbuttingToTallerConstruction(IBuilding building, double upperBuildingWidth, double lowerBuildingWidth,
            double heightDifference, double angle)
        {
            Building = building;
            UpperBuildingWidth = upperBuildingWidth > 0 ? upperBuildingWidth 
                : throw new ArgumentOutOfRangeException(nameof(upperBuildingWidth));
            LowerBuildingWidth = lowerBuildingWidth > 0 ? lowerBuildingWidth
                : throw new ArgumentOutOfRangeException(nameof(lowerBuildingWidth));
            HeightDifference = heightDifference > 0 ? heightDifference
                : throw new ArgumentOutOfRangeException(nameof(heightDifference));
            Angle = angle >= 0 ? angle
                : throw new ArgumentOutOfRangeException(nameof(angle));
            SetReferences();
        }

        /// <summary>
        /// Calculate <see cref="DriftLength"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 B3.(3)</remarks>
        public void CalculateDriftLength()
        {
            DriftLength = Math.Min(5 * HeightDifference, LowerBuildingWidth);

            DriftLength = Math.Min(DriftLength, 15);
        }

        /// <summary>
        /// Calculate <see cref="SnowLoadNearTheTop"/> and <see cref="SnowLoadNearTheEdge"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 B3.(3)</remarks>
        public void CalculateSnowLoad()
        {
            CalculateShapeCoefficient3();
            CalculateShapeCoefficient1();
            CalculateShapeCoefficient2();
            CalculateSnowLoad1();
            CalculateSnowLoad2();
        }

        private void SetReferences()
        {
            snowLoad = Building.SnowLoad;
        }

        private void CalculateShapeCoefficient1()
        {
            if (Angle < 0)
                throw new ArgumentOutOfRangeException("Angle shouldn't be less than 0.");
            else if (Angle <= 15)
                ShapeCoefficient1 = ShapeCoefficient3;
            else if (Angle <= 30)
                ShapeCoefficient1 = ShapeCoefficient3 * ((30 - Angle) / 15);
            else
                ShapeCoefficient1 = 0;
        }

        private void CalculateShapeCoefficient2()
        {
            if (Angle < 0)
                throw new ArgumentOutOfRangeException("Angle shouldn't be less than 0.");
            else if (Angle <= 30)
                ShapeCoefficient2 = ShapeCoefficient3;
            else if (Angle <= 60)
                ShapeCoefficient2 = ShapeCoefficient3 * ((60 - Angle) / 30);
            else
                ShapeCoefficient2 = 0;
        }

        private void CalculateShapeCoefficient3()
        {
            if (DriftLength == 0)
                CalculateDriftLength();

            ShapeCoefficient3 = Math.Min(
                2 * HeightDifference / snowLoad.SnowLoadForSpecificReturnPeriod,
                2 * Math.Max(LowerBuildingWidth, UpperBuildingWidth) / DriftLength);

            ShapeCoefficient3 = Math.Min(ShapeCoefficient3, 8);
        }

        private void CalculateSnowLoad1()
        {
            if (ConditionChecker.ForDesignSituation(snowLoad.ExcepctionalSituation, snowLoad.CurrentDesignSituation, true))
                SnowLoadNearTheTop = SnowLoadCalc.CalculateSnowLoadForAnnexB(ShapeCoefficient1, snowLoad.SnowLoadForSpecificReturnPeriod);
        }
        
        private void CalculateSnowLoad2()
        {
            if (ConditionChecker.ForDesignSituation(snowLoad.ExcepctionalSituation, snowLoad.CurrentDesignSituation, true))
                SnowLoadNearTheEdge = SnowLoadCalc.CalculateSnowLoadForAnnexB(ShapeCoefficient2, snowLoad.SnowLoadForSpecificReturnPeriod);
        }
    }
}
