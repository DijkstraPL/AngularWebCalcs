using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_SnowLoads.API
{

    /// <summary>
    /// Class for unit conversion
    /// </summary>
    public static class UnitConversion
    {
        /// <summary>
        /// Convert percentage into degree.
        /// </summary>
        /// <param name="percentage">Slope in percentage [%].</param>
        /// <returns>Angle in degree [degrees].</returns>
        /// <example>
        /// <code>
        /// double percentage = UnitConversion.ConvertToDegrees(50);
        /// </code>
        /// </example>
        public static double ConvertToDegrees(double percentage) =>
            Math.Atan(Convert.ToDouble(percentage) / 100) * 180 / Math.PI;

        /// <summary>
        /// Convert degrees into radians.
        /// </summary>
        /// <param name="degrees">Degrees [degrees].</param>
        /// <returns>Radians [rad].</returns>
        /// <example>
        /// <code>
        /// double radians = UnitConversion.ConvertToRadians(30);
        /// </code>
        /// </example>
        public static double ConvertToRadians(double degrees) =>
            degrees * Math.PI / 180;
    }
}
