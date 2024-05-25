using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.API;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_SnowLoads.Exceptional
{
    /// <summary>
    /// Calculation class for exceptional snow behind parapet.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 B4]</remarks>
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
    ///         ExceptionalSnowBehindParapet exceptionalSnowBehindParapet = 
    ///             new ExceptionalSnowBehindParapet(building, 20, 0.75);
    ///         exceptionalSnowBehindParapet.CalculateDriftLength();
    ///         exceptionalSnowBehindParapet.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="ExceptionalMultiSpanRoof"/>
    /// <seealso cref="ExceptionalRoofAbuttingToTallerConstruction"/>
    /// <seealso cref="ExceptionalObstructionOnFlatRoof"/>
    /// <seealso cref="ExceptionalObstructionOnPitchedOrCurvedRoof"/>
    /// <seealso cref="ExceptionalOverDoorOrLoadingBay"/>
    /// <seealso cref="ExceptionalSnowBehindParapetAtEaves"/>
    /// <seealso cref="ExceptionalSnowInValleyBehindParapet"/>
    public class ExceptionalSnowBehindParapet : ICalculatable
    {
        /// <summary>
        /// Height difference.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B4]</remarks>
        [Abbreviation("h_1")]
        [Unit("m")]
        public double HeightDifference { get;  }

        /// <summary>
        /// Width of roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("b_1")]
        [Unit("m")]
        public double Width { get; }

        /// <summary>
        /// Snow load shape coefficient 1.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B4]</remarks>
        [Abbreviation("mi_1")]
        [Unit("")]
        public double ShapeCoefficient { get; private set; }

        /// <summary>
        /// Length of the drift.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B4]</remarks>
        [Abbreviation("l_s")]
        [Unit("m")]
        public double DriftLength { get; private set; }

        /// <summary>
        /// Snow load.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B4]</remarks>
        [Abbreviation("s")]
        [Unit("kN/m2")]
        public double SnowLoad { get; private set; }

        public IBuilding Building { get; private set; }


        private ISnowLoad snowLoad;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionalSnowBehindParapet"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="width">Set <see cref="Width"/>.</param>
        /// <param name="heightDifference">Set <see cref="HeightDifference"/>.</param>
        public ExceptionalSnowBehindParapet(IBuilding building, double width, double heightDifference)
        {
            Building = building;
            Width = width > 0 ? width
                : throw new ArgumentOutOfRangeException(nameof(width));
            HeightDifference = heightDifference > 0 ? heightDifference
                : throw new ArgumentOutOfRangeException(nameof(heightDifference));

            SetReferences();
        }

        /// <summary>
        /// Calculate <see cref="DriftLength"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 B4.(4)</remarks>
        public void CalculateDriftLength()
        {
            DriftLength = Math.Min(5 * HeightDifference, Width);
            DriftLength = Math.Min(DriftLength, 15);
        }

        /// <summary>
        /// Calculate <see cref="SnowLoad"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 B4.(4)</remarks>
        public void CalculateSnowLoad()
        {
            CalculateShapeCoefficient();
            CalculateSnowLoadOnRoof();
        }

        private void SetReferences()
        {
            snowLoad = Building.SnowLoad;
        }

        private void CalculateShapeCoefficient()
        {
            ShapeCoefficient = Math.Min(2 * HeightDifference / snowLoad.SnowLoadForSpecificReturnPeriod, 8);
            ShapeCoefficient = Math.Min(ShapeCoefficient, 2 * Width / DriftLength);
        }

        private void CalculateSnowLoadOnRoof()
        {
            if (ConditionChecker.ForDesignSituation(snowLoad.ExcepctionalSituation, snowLoad.CurrentDesignSituation, true))
                SnowLoad = SnowLoadCalc.CalculateSnowLoadForAnnexB(ShapeCoefficient, snowLoad.SnowLoadForSpecificReturnPeriod);
        }
    }
}
