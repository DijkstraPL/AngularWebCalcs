using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.API;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_SnowLoads.Exceptional
{
    /// <summary>
    /// Calculation class for multi span roofs.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 B2]</remarks>
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
    ///         ExceptionalMultiSpanRoof exceptionalMultiSpanRoof = 
    ///             new ExceptionalMultiSpanRoof(building, 10, 15, 2);
    ///         exceptionalMultiSpanRoof.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="ExceptionalRoofAbuttingToTallerConstruction"/>
    /// <seealso cref="ExceptionalObstructionOnFlatRoof"/>
    /// <seealso cref="ExceptionalObstructionOnPitchedOrCurvedRoof"/>
    /// <seealso cref="ExceptionalOverDoorOrLoadingBay"/>
    /// <seealso cref="ExceptionalSnowBehindParapet"/>
    /// <seealso cref="ExceptionalSnowBehindParapetAtEaves"/>
    /// <seealso cref="ExceptionalSnowInValleyBehindParapet"/>
    public class ExceptionalMultiSpanRoof : ICalculatable
    {
        /// <summary>
        /// Snow load shape coefficient
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B1]</remarks>
        [Abbreviation("mi_1")]
        [Unit("")]
        public double ShapeCoefficient { get; private set; }

        /// <summary>
        /// Snow load on middle roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B1]</remarks>
        [Abbreviation("s_k")]
        [Unit("kN/m2")]
        public double SnowLoad { get; private set; }
        
        /// <summary>
        /// Instance of class implementing <see cref="IBuilding"/>.
        /// </summary>
        public IBuilding Building { get; private set; }

        /// <summary>
        /// Length of the left drift.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B1]</remarks>
        [Abbreviation("l_s1")]
        [Unit("m")]
        public double LeftDriftLength { get; }

        /// <summary>
        /// Length of the right drift.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B1]</remarks>
        [Abbreviation("l_s2")]
        [Unit("m")]
        public double RightDriftLength { get; }

        /// <summary>
        /// Heiight in the lowest part.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B1]</remarks>
        [Abbreviation("h")]
        [Unit("m")]
        public double HeightInTheLowestPart { get; }

        /// <summary>
        /// Horizontal simension of three slopes. It can be set as 1.5*(<see cref="LeftDriftLength"/>+<see cref="RightDriftLength"/>).
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B1]</remarks>
        [Abbreviation("b_3")]
        [Unit("m")]
        public double HorizontalDimensionOfThreeSlopes { get; private set; }

        private ISnowLoad snowLoad;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionalMultiSpanRoof"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="leftDriftLength">Set <see cref="LeftDriftLength"/>.</param>
        /// <param name="rightDriftLength">Set <see cref="RightDriftLength"/>.</param>
        /// <param name="heightInTheLowestPart">Set <see cref="HeightInTheLowestPart"/>.</param>
        public ExceptionalMultiSpanRoof(IBuilding building, double leftDriftLength, double rightDriftLength, double heightInTheLowestPart)
        {
            Building = building;
            LeftDriftLength = leftDriftLength > 0 ? leftDriftLength 
                : throw new ArgumentOutOfRangeException(nameof(leftDriftLength));
            RightDriftLength = rightDriftLength > 0 ? rightDriftLength
                : throw new ArgumentOutOfRangeException(nameof(rightDriftLength));
            HeightInTheLowestPart = heightInTheLowestPart > 0 ? heightInTheLowestPart
                : throw new ArgumentOutOfRangeException(nameof(heightInTheLowestPart));
            SetReferences();
        }

        /// <summary>
        /// Calculate <see cref="SnowLoad"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 B2]</remarks>
        public void CalculateSnowLoad()
        {
            SetHorizontalDimensionOfThreeSlopes();
            CalculateShapeCoefficient();
            CalculateSnowLoadOnRoof();
        }

        private void SetReferences()
        {
            snowLoad = Building.SnowLoad;
        }

        private void SetHorizontalDimensionOfThreeSlopes()
        {
            HorizontalDimensionOfThreeSlopes = (LeftDriftLength + RightDriftLength) * 1.5;
        }

        private void CalculateShapeCoefficient()
        {
            ShapeCoefficient = Math.Min(
                2 * HeightInTheLowestPart / snowLoad.SnowLoadForSpecificReturnPeriod,
                2 * HorizontalDimensionOfThreeSlopes / (LeftDriftLength + RightDriftLength));

            ShapeCoefficient = Math.Min(ShapeCoefficient, 5);
        }

        private void CalculateSnowLoadOnRoof()
        {
            if (ConditionChecker.ForDesignSituation(snowLoad.ExcepctionalSituation, snowLoad.CurrentDesignSituation, annexB: true))
                SnowLoad = SnowLoadCalc.CalculateSnowLoadForAnnexB(ShapeCoefficient, snowLoad.SnowLoadForSpecificReturnPeriod);
            else
                throw new ArgumentException();
        }
    }
}
