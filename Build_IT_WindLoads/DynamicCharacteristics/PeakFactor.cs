using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.Factors.Interfaces;
using System;

namespace Build_IT_WindLoads.DynamicCharacteristics
{
    /// <summary>
    /// k_p
    /// </summary>
    /// <remarks>[PN-EN 1991-1-4 Eq.B.4]</remarks>
    public class PeakFactor : IFactor
    {
        #region Fields

        [Abbreviation("T")]
        [Unit("seconds")]
        private readonly double _averagingTimeMeanWindVelocity = 600;
        private readonly IFactor _upCrossingFrequency;

        #endregion // Fields

        #region Constructors
        
        public PeakFactor(IFactor upCrossingFrequency)
        {
            _upCrossingFrequency = upCrossingFrequency;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactor()
        {
            double upCrossingFrequency = _upCrossingFrequency.GetFactor();
            var peakFactor =
                Math.Sqrt(2 * Math.Log(upCrossingFrequency * _averagingTimeMeanWindVelocity)) +
                0.6 / Math.Sqrt(2 * Math.Log(upCrossingFrequency * _averagingTimeMeanWindVelocity));
            return Math.Max(peakFactor, 3);
        }

        #endregion // Public_Methods
    }
}
