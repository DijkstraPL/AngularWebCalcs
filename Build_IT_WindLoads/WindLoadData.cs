using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.Factors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Build_IT_WindLoadsTests")]
namespace Build_IT_WindLoads
{
    public class WindLoadData : IWindLoadData
    {
        #region Properties

        public const double DefaultHeightStrip = 1;
        public const bool AllowCustomReferenceHeight = false;

        #endregion // Properties

        #region Fields

        private IDictionary<double, double> _referenceHeights = new Dictionary<double, double>();

        private readonly IBuildingSite _buildingSite;
        private readonly IStructure _building;
        private readonly double _heightStrip;
        private readonly bool _allowCustomReferenceHeight;
        private readonly IFactorAt _referenceHeightDueToNeighbouringStructures;

        [Abbreviation("k_l")]
        [Unit("")]
        private readonly double _turbulenceFactor = 1;

        [Abbreviation("ρ")]
        [Unit("kg/m3")]
        private double _airDensity;

        #endregion // Fields

        #region Constructors

        public WindLoadData(IBuildingSite buildingSite, IStructure building,
            double heightStrip = DefaultHeightStrip, bool allowCustomReferenceHeight = AllowCustomReferenceHeight, 
            IFactorAt referenceHeightDueToNeighbouringStructures = null)
        {
            _buildingSite = buildingSite;
            _building = building;
            _heightStrip = heightStrip;
            _allowCustomReferenceHeight = allowCustomReferenceHeight;
            _referenceHeightDueToNeighbouringStructures = referenceHeightDueToNeighbouringStructures;

            SetAirDensity();
            SetReferenceHeights();
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetReferenceHeightAt(double height)
        {
            if (height < 0 || height > _building.Height)
                throw new IndexOutOfRangeException("Requested height is outside the building solid.");

            return _referenceHeights.First(rh => height <= rh.Key).Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Eq.4.3]</remarks>
        public double GetMeanWindVelocityAt(double height, bool adjustHeight = true)
        {
            if (adjustHeight)
                height = SetHeight(height);

            var terrain = _buildingSite.Terrain;
            return terrain.GetRoughnessFactorAt(height) *
                           terrain.TerrainOrography.GetFactorAt(height) *
                           _buildingSite.BasicWindVelocity;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>[PN-EN 1991-1-4 Eq.4.7]</remarks>
        public double GetTurbulenceIntensityAt(double height, bool adjustHeight = true)
        {
            if (adjustHeight)
                height = SetHeight(height);

            var terrain = _buildingSite.Terrain;
            if (height < terrain.MinimumHeight)
                height = terrain.MinimumHeight;

            return _turbulenceFactor /
                (terrain.TerrainOrography.GetFactorAt(height) *
                Math.Log(height / terrain.RoughnessLength));
        }

        public double GetPeakVelocityPressureAt(double height, bool adjustHeight = true)
        {
            return (1 + 7 * GetTurbulenceIntensityAt(height, adjustHeight)) * 0.5 * _airDensity *
                Math.Pow(GetMeanWindVelocityAt(height, adjustHeight), 2) / 1000; // Change to kN/m2
        }

        #endregion // Public_Methods

        #region Private_Methods

        private double SetHeight(double height)
        {
            if (!_allowCustomReferenceHeight)
                height = GetReferenceHeightAt(height);
            if (_buildingSite.Terrain.HeightDisplacement != null)
                height -= _buildingSite.Terrain.HeightDisplacement.GetFactor();
            if (_referenceHeightDueToNeighbouringStructures != null)
                height = _referenceHeightDueToNeighbouringStructures.GetFactorAt(height);

            return height;
        }

        private void SetAirDensity()
        {
            if (_buildingSite.HeightAboveSeaLevel > 300 &&
                (_buildingSite.WindZone == WindZone.III ||
                _buildingSite.WindZone == WindZone.I_III))
                _airDensity = 1.25 *
                        (20000 - _buildingSite.HeightAboveSeaLevel) /
                        (20000 + _buildingSite.HeightAboveSeaLevel);
            else
                _airDensity = 1.25;
        }

        private void SetReferenceHeights()
        {
            if (_building.Height <= _building.Width)
                _referenceHeights.Add(_building.Height, _building.Height);
            else if (_building.Height <= 2 * _building.Width)
            {
                _referenceHeights.Add(_building.Width, _building.Width);
                _referenceHeights.Add(_building.Height, _building.Height);
            }
            else
            {
                _referenceHeights.Add(_building.Width, _building.Width);
                int index = 1;
                while (_building.Width + index * _heightStrip < _building.Height - _building.Width)
                {
                    var referenceHeightAtStrip = _building.Width + index * _heightStrip;
                    _referenceHeights.Add(referenceHeightAtStrip, referenceHeightAtStrip);
                    index++;
                }
                if (!_referenceHeights.ContainsKey(_building.Height - _building.Width))
                    _referenceHeights.Add(_building.Height - _building.Width, _building.Height - _building.Width);
                _referenceHeights.Add(_building.Height, _building.Height);
            }
        }

        #endregion // Private_Methods
    }
}
