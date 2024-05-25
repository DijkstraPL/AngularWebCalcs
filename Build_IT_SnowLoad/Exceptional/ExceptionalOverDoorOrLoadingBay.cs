using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.API;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_SnowLoads.Exceptional
{
    /// <summary>
    /// Calculation class for exceptional over door or loading bay.
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
    ///         ExceptionalOverDoorOrLoadingBay exceptionalOverDoorOrLoadingBay = 
    ///             new ExceptionalOverDoorOrLoadingBay(building, 5, 15, 1);
    ///         exceptionalOverDoorOrLoadingBay.CalculateDriftLength();
    ///         exceptionalOverDoorOrLoadingBay.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="ExceptionalMultiSpanRoof"/>
    /// <seealso cref="ExceptionalRoofAbuttingToTallerConstruction"/>
    /// <seealso cref="ExceptionalObstructionOnFlatRoof"/>
    /// <seealso cref="ExceptionalObstructionOnPitchedOrCurvedRoof"/>
    /// <seealso cref="ExceptionalSnowBehindParapet"/>
    /// <seealso cref="ExceptionalSnowBehindParapetAtEaves"/>
    /// <seealso cref="ExceptionalSnowInValleyBehindParapet"/>
    public class ExceptionalOverDoorOrLoadingBay : ICalculatable
    {
        /// <summary>
        /// Height difference.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("h")]
        [Unit("m")]
        public double HeightDifference { get; }
        
        private double widthAboveDoor;
        /// <summary>
        /// Width above the roof.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("b_1")]
        [Unit("m")]
        public double WidthAboveDoor
        {
            get { return widthAboveDoor; }
            set {
                if (value > 5)
                    throw new ArgumentOutOfRangeException("Width shouldn't be over 5m");
                widthAboveDoor = value;
            }
        }

        /// <summary>
        /// Width of the building.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("b_2")]
        [Unit("m")]
        public double BuildingWidth { get; }

        /// <summary>
        /// Snow load shape coefficient.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("mi_1")]
        [Unit("")]
        public double ShapeCoefficient { get; private set; }

        /// <summary>
        /// Length of the drift.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("l_s")]
        [Unit("m")]
        public double DriftLength { get; private set; }

        /// <summary>
        /// Snow load.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.B3]</remarks>
        [Abbreviation("s")]
        [Unit("kN/m2")]
        public double SnowLoad { get; private set; }

        public IBuilding Building { get; private set; }

        private ISnowLoad snowLoad;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionalOverDoorOrLoadingBay"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="widthAboveDoor">Set <see cref="WidthAboveDoor"/>.</param>
        /// <param name="buildingWidth">Set <see cref="BuildingWidth"/>.</param>
        /// <param name="heightDifference">Set <see cref="HeightDifference"/>.</param>
        public ExceptionalOverDoorOrLoadingBay(IBuilding building, double widthAboveDoor, double buildingWidth, double heightDifference)
        {
            Building = building;
            WidthAboveDoor = widthAboveDoor > 0 ? widthAboveDoor 
                : throw new ArgumentOutOfRangeException(nameof(widthAboveDoor));
            BuildingWidth = buildingWidth > 0 ? buildingWidth
                : throw new ArgumentOutOfRangeException(nameof(buildingWidth));
            HeightDifference = heightDifference > 0 ? heightDifference
                : throw new ArgumentOutOfRangeException(nameof(heightDifference));
            SetReferences();
        }

        /// <summary>
        /// Calculate <see cref="DriftLength"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 B4.(2)</remarks>
        public void CalculateDriftLength()
        {
            if (HeightDifference > 1)
                DriftLength = WidthAboveDoor;
            else
                DriftLength = Math.Min(5 * HeightDifference, WidthAboveDoor);
        }

        /// <summary>
        /// Calculate <see cref="SnowLoad"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 B4.(2)</remarks>
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
            ShapeCoefficient = Math.Min(2 * HeightDifference / snowLoad.SnowLoadForSpecificReturnPeriod, 5);
            if (WidthAboveDoor <= 5)
                ShapeCoefficient = Math.Min(ShapeCoefficient, 2 * Math.Max(WidthAboveDoor, BuildingWidth) / DriftLength);
        }

        private void CalculateSnowLoadOnRoof()
        {
            if (ConditionChecker.ForDesignSituation(snowLoad.ExcepctionalSituation, snowLoad.CurrentDesignSituation, true))
                SnowLoad = SnowLoadCalc.CalculateSnowLoadForAnnexB(ShapeCoefficient, snowLoad.SnowLoadForSpecificReturnPeriod);
        }
    }
}
