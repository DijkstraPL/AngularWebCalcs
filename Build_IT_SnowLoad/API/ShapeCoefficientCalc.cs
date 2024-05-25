using System;

namespace Build_IT_SnowLoads.API
{
    /// <summary>
    /// Class for calculating snow load coefficients.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 Table 5.2]</remarks>
    public static class ShapeCoefficientCalc
    {
        /// <summary>
        /// Calculate snow load coefficient number 1.
        /// </summary>
        /// <param name="slope">Angle of the roof [degrees].</param>
        /// <param name="snowFences">Is there any obstacles fe. snow fences.</param>
        /// <returns>Return snow load shape coefficient 1</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when slope is less than 0.</exception>
        /// <remarks>[PN-EN 1991-1-3 Table 5.2]</remarks>
        /// <example>
        /// <code>
        /// double snowLoadShapeCoefficient1 = ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(30, true);
        /// </code>
        /// </example>
        public static double CalculateSnowLoadShapeCoefficient1(double slope, bool snowFences = false)
        {
            if (slope < 0)
                throw new ArgumentOutOfRangeException(nameof(slope), "Value couldn't be less than 0.");
            else if (snowFences)
                return 0.8;
            else if (slope <= 30)
                return 0.8;
            else if (slope < 60)
                return 0.8 * (60 - slope) / 30;
            else
                return 0;
        }

        /// <summary>
        /// Calculate snow load coefficient number 2.
        /// </summary>
        /// <param name="slope">Angle of the roof in degrees.</param>
        /// <returns>Return snow load shape coefficient 2</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when slope is less than 0.</exception>
        /// <remarks>[PN-EN 1991-1-3 Table 5.2]</remarks>
        /// <example>
        /// <code>
        /// double snowLoadShapeCoefficient2 = ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient2(30);
        /// </code>
        /// </example>
        public static double CalculateSnowLoadShapeCoefficient2(double slope)
        {
            if (slope < 0)
                throw new ArgumentOutOfRangeException(nameof(slope), "Value couldn't be less than 0.");
            else if (slope <= 30)
                return 0.8 + 0.8 * slope / 30;
            else if (slope < 60)
                return 1.6;
            else
                return 1.6;
        }
    }
}
