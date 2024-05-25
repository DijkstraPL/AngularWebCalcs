using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.Factors.Interfaces;
using System;

namespace Build_IT_WindLoads.TerrainOrographies
{
    public abstract class TerrainOrography : IFactorAt
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Fig.A.2]</remarks>
        [Abbreviation("L_u")]
        [Unit("m")]
        public double ActualLengthUpwindSlope { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Fig.A.2]</remarks>
        [Abbreviation("L_d")]
        [Unit("m")]
        public double ActualLengthDownwindSlope { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Fig.A.2]</remarks>
        [Abbreviation("H")]
        [Unit("m")]
        public double EffectiveFeatureHeight { get; }

        [Abbreviation("Φ")]
        [Unit("")]
        public double UpwindSlope => EffectiveFeatureHeight / ActualLengthUpwindSlope;

        [Abbreviation("L_e")]
        [Unit("m")]
        public double EffectiveLengthUpwindSlope => GetEffectiveLengthUpwindSlope();

        [Abbreviation("x")]
        [Unit("m")]
        public double HorizontalDistanceFromCrestTop { get; }

        public const double DefaultOrographyFactor = 1;

        #endregion // Properties

        #region Constructors

        public TerrainOrography(
            double actualLengthUpwindSlope,
            double actualLengthDownwindSlope,
            double effectiveFeatureHeight,
            double horizontalDistanceFromCrestTop)
        {
            ActualLengthUpwindSlope = actualLengthUpwindSlope;
            ActualLengthDownwindSlope = actualLengthDownwindSlope;
            EffectiveFeatureHeight = effectiveFeatureHeight;
            HorizontalDistanceFromCrestTop = horizontalDistanceFromCrestTop;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactorAt(double verticalDistanceFromCrestTop)
        {
            if (!IsOrographicFactorNeeded())
                return DefaultOrographyFactor;

            double orographicLocationFactor = GetOrographicLocationFactorAt(verticalDistanceFromCrestTop);

            if (UpwindSlope < 0.05)
                return 1;
            else if (UpwindSlope <= 0.3)
                return 1 + 2 * orographicLocationFactor * UpwindSlope;
            else
                return 1 + 0.6 * orographicLocationFactor;
        }

        #endregion // Public_Methods

        #region Protected_Methods

        protected abstract bool IsOrographicFactorNeeded();
        protected abstract double GetOrographicLocationFactorAtDownwindSide(double verticalDistanceFromCrestTop);

        protected double GetOrographicLocationFactorAtUpwindSide(double verticalRatio, double horizontalRatio)
        {
            if (horizontalRatio < -1.5 ||
                verticalRatio > 2)
                return 0;
            double coefficientA = 0.1552 * Math.Pow(verticalRatio, 4)
                - 0.8575 * Math.Pow(verticalRatio, 3)
                + 1.8133 * Math.Pow(verticalRatio, 2)
                - 1.9115 * verticalRatio
                + 1.0124;
            double coefficientB = 0.3542 * Math.Pow(verticalRatio, 2)
                - 1.0577 * verticalRatio
                + 2.6456;
            return coefficientA * Math.Pow(
                Math.E, coefficientB * horizontalRatio);
        }

        #endregion // Protected_Methods

        #region Private_Methods

        private double GetOrographicLocationFactorAt(double verticalDistanceFromCrestTop)
        {
            if (HorizontalDistanceFromCrestTop < 0)
                return GetOrographicLocationFactorAtUpwindSide(
                    verticalDistanceFromCrestTop / EffectiveLengthUpwindSlope,
                    HorizontalDistanceFromCrestTop / ActualLengthUpwindSlope);
            return GetOrographicLocationFactorAtDownwindSide(verticalDistanceFromCrestTop);
        }
        
        private double GetEffectiveLengthUpwindSlope()
        {
            if (UpwindSlope == 0.3)
                return (ActualLengthUpwindSlope + EffectiveFeatureHeight / 0.3) / 2;
            if (UpwindSlope > 0.05 && UpwindSlope < 0.3)
                return ActualLengthUpwindSlope;
            if (UpwindSlope > 0.3)
                return EffectiveFeatureHeight / 0.3;
            throw new ArgumentOutOfRangeException("Wrong slope.");
        }

        #endregion // Private_Methods
    }
}
