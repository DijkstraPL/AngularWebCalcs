using Build_IT_CommonTools.Maths;
using System;

namespace Build_IT_WindLoads.TerrainOrographies
{
    public class CliffEscarpmentOrography : TerrainOrography
    {
        #region Constructors

        public CliffEscarpmentOrography(
            double actualLengthUpwindSlope,
            double effectiveFeatureHeight,
            double horizontalDistanceFromCrestTop)
            : base(actualLengthUpwindSlope,
                  0,
                  effectiveFeatureHeight,
                  horizontalDistanceFromCrestTop)
        {
        }

        #endregion // Constructors

        #region Protected_Methods

        protected override bool IsOrographicFactorNeeded()
        {
            if (HorizontalDistanceFromCrestTop < 0 &&
                UpwindSlope > 0.05 &&
                UpwindSlope <= 0.3 &&
                Math.Abs(HorizontalDistanceFromCrestTop) <= ActualLengthUpwindSlope / 2)
                return true;
            if (HorizontalDistanceFromCrestTop >= 0 &&
                (UpwindSlope < 0.3 &&
                HorizontalDistanceFromCrestTop < 1.5 * EffectiveLengthUpwindSlope)
                ||
                (UpwindSlope >= 0.3 &&
                HorizontalDistanceFromCrestTop < 5 * EffectiveFeatureHeight))
                return true;
            return false;
        }

        protected override double GetOrographicLocationFactorAtDownwindSide(double verticalDistanceFromCrestTop)
        {
            double verticalRatio = verticalDistanceFromCrestTop / EffectiveLengthUpwindSlope;
            double horizontalRatio = HorizontalDistanceFromCrestTop / EffectiveLengthUpwindSlope;

            if (horizontalRatio > 3.5 ||
                verticalRatio > 2)
                return 0;
            if (verticalRatio < 0.1)
                verticalRatio = 0.1;

            if (0.1 <= horizontalRatio && horizontalRatio <= 3.5 &&
              0.1 <= verticalRatio && verticalRatio <= 2)
            {
                return GetOrographicLocationFactorAtFurtherDistance(
                    verticalRatio, horizontalRatio);
            }
            else if (0 <= horizontalRatio && horizontalRatio < 0.1)
            {
                return Interpolation.InterpolateLinearBetween(
                     start: (0, GetOrographicLocationFactorAtUpwindSide(verticalRatio, horizontalRatio: 0)),
                     end: (0.1, GetOrographicLocationFactorAtFurtherDistance(verticalRatio, horizontalRatio)),
                     at: horizontalRatio);
            }
            throw new ArgumentException("Wrong ratio.");
        }

        #endregion // Protected_Methods

        #region Private_Methods

        private double GetOrographicLocationFactorAtFurtherDistance(double verticalRatio, double horizontalRatio)
        {
            double coefficientA =
                -1.3420 * Math.Pow(Math.Log10(verticalRatio), 3)
                - 0.8222 * Math.Pow(Math.Log10(verticalRatio), 2)
                + 0.4609 * Math.Log10(verticalRatio)
                - 0.0791;
            double coefficientB =
                -1.0196 * Math.Pow(Math.Log10(verticalRatio), 3)
                - 0.8910 * Math.Pow(Math.Log10(verticalRatio), 2)
                + 0.5343 * Math.Log10(verticalRatio)
                - 0.1156;
            double coefficientC =
                0.8030 * Math.Pow(Math.Log10(verticalRatio), 3)
                + 0.4236 * Math.Pow(Math.Log10(verticalRatio), 2)
                - 0.5738 * Math.Log10(verticalRatio)
                + 0.1606;
            return coefficientA * Math.Pow(Math.Log10(horizontalRatio), 2)
                + coefficientB * Math.Log10(horizontalRatio)
                + coefficientC;
        }

        #endregion // Private_Methods
    }
}
