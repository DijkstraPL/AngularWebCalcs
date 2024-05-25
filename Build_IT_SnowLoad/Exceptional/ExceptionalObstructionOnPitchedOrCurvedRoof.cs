using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.API;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_SnowLoads.Exceptional
{
    /// <summary>
    /// Calculation class for exceptional obstruction on pitched or curved roof.
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
    ///         ExceptionalObstructionOnPitchedOrCurvedRoof exceptionalObstructionOnPitchedOrCurvedRoof = 
    ///             new ExceptionalObstructionOnPitchedOrCurvedRoof(building, 10, 15, 1, 0.9);
    ///         exceptionalObstructionOnPitchedOrCurvedRoof.CalculateDriftLength();
    ///         exceptionalObstructionOnPitchedOrCurvedRoof.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="ExceptionalMultiSpanRoof"/>
    /// <seealso cref="ExceptionalRoofAbuttingToTallerConstruction"/>
    /// <seealso cref="ExceptionalObstructionOnFlatRoof"/>
    /// <seealso cref="ExceptionalOverDoorOrLoadingBay"/>
    /// <seealso cref="ExceptionalSnowBehindParapet"/>
    /// <seealso cref="ExceptionalSnowBehindParapetAtEaves"/>
    /// <seealso cref="ExceptionalSnowInValleyBehindParapet"/>
    public class ExceptionalObstructionOnPitchedOrCurvedRoof : ICalculatable
    {
        /// <summary>
        /// Height difference for the left side.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("h_1")]
        [Unit("m")]
        public double LeftHeightDifference { get; }
        /// <summary>
        /// Height difference for the right side.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("h_2")]
        [Unit("m")]
        public double RightHeightDifference { get; }

        /// <summary>
        /// Left width of roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("b_1")]
        [Unit("m")]
        public double LeftWidth { get; }
        /// <summary>
        /// Right width of roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("b_2")]
        [Unit("m")]
        public double RightWidth { get; }

        /// <summary>
        /// Snow load shape coefficient for the left side.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("mi_1")]
        [Unit("")]
        public double LeftShapeCoefficient { get; private set; }
        /// <summary>
        /// Snow load shape coefficient for the left side.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("mi_2")]
        [Unit("")]
        public double RightShapeCoefficient { get; private set; }

        /// <summary>
        /// Length of the left drift.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("l_s1")]
        [Unit("m")]
        public double LeftDriftLength { get; private set; }
        /// <summary>
        /// Length of the right drift.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("l_s2")]
        [Unit("m")]
        public double RightDriftLength { get; private set; }

        /// <summary>
        /// Snow load on left side.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("s_1")]
        [Unit("kN/m2")]
        public double LeftSnowLoad { get; private set; }
        /// <summary>
        /// Snow load on right side.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("s_1")]
        [Unit("kN/m2")]
        public double RightSnowLoad { get; private set; }

        public IBuilding Building { get; private set; }

        private ISnowLoad snowLoad;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionalObstructionOnPitchedOrCurvedRoof"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="leftWidth">Set <see cref="LeftWidth"/>.</param>
        /// <param name="rightWidth">Set <see cref="RightWidth"/>.</param>
        /// <param name="leftHeightDifference">Set <see cref="LeftHeightDifference"/>.</param>
        /// <param name="rightHeightDifference">Set <see cref="RightHeightDifference"/>.</param>
        public ExceptionalObstructionOnPitchedOrCurvedRoof(IBuilding building, double leftWidth, double rightWidth, 
            double leftHeightDifference, double rightHeightDifference)
        {
            Building = building;
            LeftWidth = leftWidth > 0 ? leftWidth
                : throw new ArgumentOutOfRangeException(nameof(leftWidth));
            RightWidth = rightWidth > 0 ? rightWidth
                : throw new ArgumentOutOfRangeException(nameof(rightWidth));
            LeftHeightDifference = leftHeightDifference > 0 ? leftHeightDifference
                : throw new ArgumentOutOfRangeException(nameof(leftHeightDifference));
            RightHeightDifference = rightHeightDifference > 0 ? rightHeightDifference
                : throw new ArgumentOutOfRangeException(nameof(rightHeightDifference));
            SetReferences();
        }

        /// <summary>
        /// Calculate <see cref="LeftDriftLength"/> and <see cref="RightDriftLength"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 B4.(2)</remarks>
        public void CalculateDriftLength()
        {
            if (LeftHeightDifference > 1)
                LeftDriftLength = LeftWidth;
            else
                LeftDriftLength = Math.Min(5 * LeftHeightDifference, LeftWidth);

            if (RightHeightDifference > 1)
                RightDriftLength = RightWidth;
            else
                RightDriftLength = Math.Min(5 * RightHeightDifference, RightWidth);
        }

        /// <summary>
        /// Calculate <see cref="LeftSnowLoad"/> and <see cref="RightSnowLoad"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 B4.(2)</remarks>
        public void CalculateSnowLoad()
        {
            CalculateShapeCoefficient();
            CalculateSnowLoad1();
            CalculateSnowLoad2();
        }

        private void SetReferences()
        {
            snowLoad = Building.SnowLoad;
        }

        private void CalculateShapeCoefficient()
        {
            LeftShapeCoefficient = Math.Min(2 * LeftHeightDifference / snowLoad.SnowLoadForSpecificReturnPeriod, 5);
            RightShapeCoefficient = Math.Min(2 * RightHeightDifference / snowLoad.SnowLoadForSpecificReturnPeriod, 5);
        }

        private void CalculateSnowLoad1()
        {
            if (ConditionChecker.ForDesignSituation(snowLoad.ExcepctionalSituation, snowLoad.CurrentDesignSituation, true))
                LeftSnowLoad = SnowLoadCalc.CalculateSnowLoadForAnnexB(LeftShapeCoefficient, snowLoad.SnowLoadForSpecificReturnPeriod);
            else
                throw new ArgumentException();
        }
        private void CalculateSnowLoad2()
        {
            if (ConditionChecker.ForDesignSituation(snowLoad.ExcepctionalSituation, snowLoad.CurrentDesignSituation, true))
                RightSnowLoad = SnowLoadCalc.CalculateSnowLoadForAnnexB(RightShapeCoefficient, snowLoad.SnowLoadForSpecificReturnPeriod);
            else
                throw new ArgumentException();
        }
    }
}
