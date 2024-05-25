using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.Factors.Interfaces;
using System;

namespace Build_IT_WindLoads.Factors
{
    public class ReferenceHeightDueToNeighbouringStructures : IFactorAt
    {
        #region Fields

        [Abbreviation("d_small")]
        [Unit("m")]
        private readonly double _highBuildingSmallLength;
        [Abbreviation("d_large")]
        [Unit("m")]
        private readonly double _highBuildingLargeLength;
        [Abbreviation("h_high")]
        [Unit("m")]
        private readonly double _highBuildingHeight;
        [Abbreviation("x")]
        [Unit("m")]
        private readonly double _distanceToBuilding;
        [Abbreviation("r")]
        [Unit("m")]
        private double _radius;

        #endregion // Fields

        #region Constructors

        public ReferenceHeightDueToNeighbouringStructures(
            double highBuildingWidth, double highBuildingLength,
            double highBuildingHeight, double distanceToBuilding)
        {
            if (_highBuildingSmallLength < 0 || _highBuildingLargeLength < 0 ||
                _highBuildingHeight < 0 || _distanceToBuilding < 0)
                throw new ArgumentOutOfRangeException();
            _highBuildingSmallLength = Math.Min(highBuildingLength, highBuildingWidth);
            _highBuildingLargeLength = Math.Max(highBuildingLength, highBuildingWidth);
            _highBuildingHeight = highBuildingHeight;
            _distanceToBuilding = distanceToBuilding;

            SetRadius();
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetFactorAt(double height)
        {
            if (_distanceToBuilding <= _radius)
                return 0.5 * _radius;
            else if (_radius < _distanceToBuilding &&
                _distanceToBuilding <= 2 * _radius)
                return 0.5 * (_radius -
                    (1 - 2 * height / _radius) *
                    (_distanceToBuilding - _radius));
            else
                return height;
        }

        #endregion // Public_Methods

        #region Private_Methods

        private void SetRadius()
        {
            if (_highBuildingHeight <= 2 * _highBuildingLargeLength)
                _radius = _highBuildingHeight;
            else
                _radius = 2 * _highBuildingLargeLength;
        }

        #endregion // Private_Methods
    }
}
