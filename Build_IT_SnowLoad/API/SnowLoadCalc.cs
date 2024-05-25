using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_SnowLoads.API
{

    /// <summary>
    /// Class which contains metods for calculating snow loads on roofs.
    /// </summary>
    /// <remarks>[PN-EN 1991-1-3 5.2.(3)]</remarks>
    public static class SnowLoadCalc
    {
        /// <summary>
        /// Calculate snow load on roof.
        /// </summary>
        /// <param name="shapeCoefficient">Shape coefficient for the roof.</param>
        /// <param name="exposureCoefficient">Exposure coefficient.</param>
        /// <param name="thermalCoefficient">Thermal coefficient.</param>
        /// <param name="snowLoad">Value of snow load laying on ground [kN/m2].</param>
        /// <returns>Snow load on roof [kN/m2].</returns>
        /// <remarks>[PN-EN 1991-1-3 5.2.(3)a/b]</remarks>
        /// <example>
        /// <code>
        /// double snowLoadOnRoof = SnowLoadCalc.CalculateSnowLoad(0.8, 1, 1, 0.9);
        /// </code>
        /// </example>
        /// See <see cref="ConditionChecker.ForDesignSituation(bool, DesignSituation, bool)"/> for checking the condition for current design situation.
        /// See <see cref="ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(double, bool)"/> for calculating shape coefficient 1.
        /// See <see cref="ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient2(double)"/> for calculating shape coefficient 2.
        /// <seealso cref="CalculateSnowLoadForAnnexB(double, double)"/>
        public static double CalculateSnowLoad(double shapeCoefficient,
            double exposureCoefficient, double thermalCoefficient, double snowLoad) =>
            shapeCoefficient * exposureCoefficient * thermalCoefficient * snowLoad;

        /// <summary>
        /// Calculate snow load.
        /// </summary>
        /// <param name="shapeCoefficient">Shape coefficient for the roof.</param>
        /// <param name="exposureCoefficient">Exposure coefficient.</param>
        /// <param name="thermalCoefficient">Thermal coefficient.</param>
        /// <param name="snowLoad">Value of snow load laying on ground [kN/m2].</param>
        /// <returns>Snow load on roof [kN/m2].</returns>
        /// <remarks>[PN-EN 1991-1-3 5.2.(3)c]</remarks>
        /// <example>
        /// <code>
        /// double snowLoadOnRoof = SnowLoadCalc.CalculateSnowLoadForAnnexB(0.8, 0.9);
        /// </code>
        /// </example>
        /// See <see cref="ConditionChecker.ForDesignSituation(bool, DesignSituation, bool)"/> for checking the condition for current design situation.
        /// See <see cref="ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient1(double, bool)"/> for calculating shape coefficient 1.
        /// See <see cref="ShapeCoefficientCalc.CalculateSnowLoadShapeCoefficient2(double)"/> for calculating shape coefficient 2.
        /// <seealso cref="CalculateSnowLoad(double, double, double, double)"/>
        public static double CalculateSnowLoadForAnnexB(double shapeCoefficient, double snowLoad) =>
            shapeCoefficient * snowLoad;
    }
}
