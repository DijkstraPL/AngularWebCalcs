using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.TerrainOrographies
{
    public class HillRidgeOrography : TerrainOrography
    {
        #region Constructors

        public HillRidgeOrography(
            double actualLengthUpwindSlope, 
            double actualLengthDownwindSlope, 
            double effectiveFeatureHeight, 
            double horizontalDistanceFromCrestTop) 
            : base(actualLengthUpwindSlope, 
                  actualLengthDownwindSlope, 
                  effectiveFeatureHeight, 
                  horizontalDistanceFromCrestTop)
        {
        }

        #endregion // Constructors

        #region Protected_Methods

        protected override bool IsOrographicFactorNeeded()
        {
            if (HorizontalDistanceFromCrestTop < 0 &&
                0.05 < UpwindSlope && UpwindSlope <= 0.3 &&
                Math.Abs(HorizontalDistanceFromCrestTop) <= ActualLengthUpwindSlope / 2)
                return true;
            if (HorizontalDistanceFromCrestTop >= 0 &&
                (UpwindSlope < 0.3 &&
                HorizontalDistanceFromCrestTop < ActualLengthDownwindSlope / 2)
                ||
                (UpwindSlope >= 0.3 &&
                HorizontalDistanceFromCrestTop < 1.6 * EffectiveFeatureHeight))
                return true;
            return false;
        }

        protected override double GetOrographicLocationFactorAtDownwindSide(double verticalDistanceFromCrestTop)
        {
            double verticalRatio = verticalDistanceFromCrestTop / EffectiveLengthUpwindSlope;

            if (HorizontalDistanceFromCrestTop / ActualLengthDownwindSlope > 2 ||
                verticalRatio > 2)
                return 0;

            if( 0 <= HorizontalDistanceFromCrestTop / ActualLengthDownwindSlope &&
               HorizontalDistanceFromCrestTop / ActualLengthDownwindSlope <= 2 &&
               0 <= verticalRatio &&  verticalRatio <= 2)
            {
                double coefficientA =
                    0.1552 * Math.Pow(verticalRatio, 4)
                    - 0.8575 * Math.Pow(verticalRatio, 3)
                    + 1.8133 * Math.Pow(verticalRatio, 2)
                    - 1.9115 * verticalRatio
                    + 1.0124;

                double coefficientB =
                    - 0.3056 * Math.Pow(verticalRatio, 2)
                    + 1.0212 * verticalRatio
                    - 1.7637;

                return coefficientA * Math.Pow(Math.E, 
                    coefficientB * HorizontalDistanceFromCrestTop / ActualLengthDownwindSlope);
            }
            throw new ArgumentException("Wrong ratio.");
        }

        #endregion // Protected_Methods
    }
}
