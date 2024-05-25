using Build_IT_CommonTools.Attributes;
using Build_IT_SnowLoads.API;
using Build_IT_SnowLoads.Interfaces;
using System;

namespace Build_IT_SnowLoads.BuildingTypes
{
    /// <summary>
    /// Calculation class for loads on snow guards or other obstacles.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 6.4.(1)]</remarks>
    /// <example>
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         Snowguards snowguards = new Snowguards(20, 30, 0.72);
    ///         snowguards.CalculateSnowLoad();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="DriftingAtProjectionsObstructions"/>
    /// <seealso cref="SnowOverhanging"/>
    public class Snowguards : ICalculatable
    {
        /// <summary>
        /// Snow load on the roof - the most onerous undrifted load case
        /// appropriate for the roof under consideration [kN/m2].
        /// </summary>
        [Abbreviation("s")]
        [Unit("kN/m2")]
        public double SnowLoadOnRoofValue { get;  }

        /// <summary>
        /// Width on plan (horizontal) from the guard or obstacle to the next guard or to the ridge [m].
        /// </summary>
        [Abbreviation("b")]
        [Unit("m")]
        public double Width { get; }

        /// <summary>
        /// Pitch of the roof, measured from the horizontal [degree].
        /// </summary>
        [Abbreviation("alpha")]
        [Unit("degree")]
        public double Slope { get;  }

        /// <summary>
        /// Force on snow guard [kN/m].
        /// </summary>
        [Abbreviation("F_s")]
        [Unit("kN/m")]
        public double ForceExertedBySnow { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Snowguards"/> class.
        /// </summary>
        /// <param name="width">Set <see cref="Width"/>.</param>
        /// <param name="slope">Set <see cref="Slope"/>.</param>
        /// <param name="snowLoadOnRoof">Set <see cref="SnowLoadOnRoofValue"/>.</param>
        public Snowguards(double width, double slope, double snowLoadOnRoof)
        {
            Width = width > 0 ? width : throw new ArgumentOutOfRangeException(nameof(width));
            Slope = slope > 0 ? slope : throw new ArgumentOutOfRangeException(nameof(slope));
            SnowLoadOnRoofValue = snowLoadOnRoof;
        }

        /// <summary>
        /// Calculate <see cref="ForceExertedBySnow"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 6.4.(1)]</remarks>
        public void CalculateSnowLoad()
        {
            CalculateForce();
        }

        /// <summary>
        /// Calculate <see cref="ForceExertedBySnow"/>.
        /// </summary>
        /// <remarks>[PN-EN 1991-1-3 6.4.(1)]</remarks>
        private void CalculateForce()
        {
            ForceExertedBySnow = SnowLoadOnRoofValue * Width * Math.Sin(UnitConversion.ConvertToRadians(Slope));
        }
    }
}
