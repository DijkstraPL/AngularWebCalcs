using Build_IT_SnowLoads.Interfaces;
using System.Collections.Generic;

namespace Build_IT_SnowLoads.BuildingTypes
{
    /// <summary>
    /// Calculation class for pitched roofs.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 5.3.3]</remarks>
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
    ///         PitchedRoof pitchedRoof = new PitchedRoof(building, 35, 25, false , true);
    ///         pitchedRoof.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="MonopitchRoof"/>
    /// <seealso cref="MultiSpanRoof"/>
    /// <seealso cref="CylindricalRoof"/>
    /// <seealso cref="RoofAbuttingToTallerConstruction"/>
    public class PitchedRoof : ICalculatable
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
        public IMonopitchRoof RightRoof { get;  }

        /// <summary>
        /// Snow load on left roof for all cases.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.3]</remarks>
        public Dictionary<int, double> LeftRoofCasesSnowLoad { get; private set; }

        /// <summary>
        /// Snow load on right roof for all cases.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.3]</remarks>
        public Dictionary<int, double> RightRoofCasesSnowLoad { get; private set; }

        /// <summary>
        /// Instance of class implementing <see cref="IBuilding"/>.
        /// </summary>
        public IBuilding Building { get; private set; }

        public const bool DefaultSnowFences = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="PitchedRoof"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="leftRoofSlope">Set <see cref="IMonopitchRoof.Slope"/> for <see cref="LeftRoof"/>.</param>
        /// <param name="rightRoofSlope">Set <see cref="IMonopitchRoof.Slope"/> for <see cref="RightRoof"/>.</param>
        /// <param name="leftRoofSnowFences">Set <see cref="IMonopitchRoof.SnowFences"/> for <see cref="LeftRoof"/>.</param>
        /// <param name="rightRoofSnowFences">Set <see cref="IMonopitchRoof.SnowFences"/> for <see cref="RightRoof"/>.</param>
        public PitchedRoof(IBuilding building, double leftRoofSlope, double rightRoofSlope,
            bool leftRoofSnowFences = DefaultSnowFences, bool rightRoofSnowFences = DefaultSnowFences)
        {
            LeftRoofCasesSnowLoad = new Dictionary<int, double>();
            RightRoofCasesSnowLoad = new Dictionary<int, double>();

            Building = building;

            LeftRoof = new MonopitchRoof(Building, leftRoofSlope, leftRoofSnowFences);
            RightRoof = new MonopitchRoof(Building, rightRoofSlope, rightRoofSnowFences);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PitchedRoof"/> class.
        /// </summary>
        /// <param name="building">Set instance of a class implementing <see cref="IBuilding"/> for <see cref="Building"/>.</param>
        /// <param name="leftRoof">Set instance of a class implementing <see cref="IMonopitchRoof"/> for <see cref="LeftRoof"/>.</param>
        /// <param name="rightRoof">Set instance of a class implementing <see cref="IMonopitchRoof"/> for <see cref="RightRoof"/>.</param>
        public PitchedRoof(IBuilding building, IMonopitchRoof leftRoof, IMonopitchRoof rightRoof)
        {
            LeftRoofCasesSnowLoad = new Dictionary<int, double>();
            RightRoofCasesSnowLoad = new Dictionary<int, double>();

            Building = building;

            LeftRoof = leftRoof;
            RightRoof = rightRoof;
        }

        /// <summary>
        /// Calculate <see cref="IMonopitchRoof.SnowLoadOnRoofValue"/> for <see cref="LeftRoof"/> and <see cref="RightRoof"/> 
        /// </summary>
        /// <seealso cref="ICalculatable.CalculateSnowLoad"/>
        public void CalculateSnowLoad()
        {
            LeftRoof.CalculateSnowLoad();
            RightRoof.CalculateSnowLoad();
            SetCasesSnowLoad();
        }

        /// <summary>
        /// Set <see cref="LeftRoofCasesSnowLoad"/> and <see cref="RightRoofCasesSnowLoad"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 Fig.5.3]</remarks>
        private void SetCasesSnowLoad()
        {
            LeftRoofCasesSnowLoad.Clear();
            RightRoofCasesSnowLoad.Clear();

            LeftRoofCasesSnowLoad.Add(1, LeftRoof.SnowLoadOnRoofValue);
            LeftRoofCasesSnowLoad.Add(2, 0.5 * LeftRoof.SnowLoadOnRoofValue);
            LeftRoofCasesSnowLoad.Add(3, LeftRoof.SnowLoadOnRoofValue);

            RightRoofCasesSnowLoad.Add(1, RightRoof.SnowLoadOnRoofValue);
            RightRoofCasesSnowLoad.Add(2, RightRoof.SnowLoadOnRoofValue);
            RightRoofCasesSnowLoad.Add(3, 0.5 * RightRoof.SnowLoadOnRoofValue);
        }
    }
}
