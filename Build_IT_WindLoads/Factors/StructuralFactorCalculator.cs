using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.DynamicCharacteristics;
using Build_IT_WindLoads.DynamicCharacteristics.Enums;
using Build_IT_WindLoads.Factors.Interfaces;
using Build_IT_WindLoads.Terrains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.Factors
{
    public class StructuralFactorCalculator : IStructuralFactorCalculator
    {
        #region Properties

        public const double DefaultStructuralFactor = 1;

        #endregion // Properties

        #region Fields

        private readonly IStructure _building;
        private readonly ITerrain _terrain;
        private readonly IWindLoadData _windLoadData;
        private readonly StructuralType _structuralType;
        private IFactor _referenceHeight;
        private IFactorAt _turbulentLengthScale;
        private IFactor _fundamentalFlexuralFrequency;

        #endregion // Fields

        #region Constructors

        public StructuralFactorCalculator(
            IStructure building, ITerrain terrain,
            IWindLoadData windLoadData, StructuralType structuralType)
        {
            _building = building;
            _terrain = terrain;
            _windLoadData = windLoadData;
            _structuralType = structuralType;
        }

        #endregion // Constructors

        #region Public_Methods

        public double GetStructuralFactor(bool calculate = true)
        {
            if (!calculate || _building.Height < 15)
                return DefaultStructuralFactor;

            var structuralFactor = GetStructuralFactor();

            return structuralFactor.GetFactor();
        }

        #endregion // Public_Methods            

        #region Private_Methods

        private IFactor GetStructuralFactor()
        {
            _referenceHeight = new ReferenceHeight(_building, _terrain);
            _turbulentLengthScale = new TurbulentLengthScale(_terrain);

            var logarithmicDecrementOfDamping = GetLogarithmicDecrementOfDamping();
            _fundamentalFlexuralFrequency =
                new FundamentalFlexuralFrequencyForHeightAbove50(_building);

            var resonanceResponse = GetResonanceResponse(
                logarithmicDecrementOfDamping);

            var backgroundFactor =
                new BackgroundFactor(
                    _building,
                    _referenceHeight,
                    _turbulentLengthScale);

            var dynamicFactor = GetDynamicFactor(
                resonanceResponse, backgroundFactor);
            var sizeFactor =
                new SizeFactor(
                    _referenceHeight,
                    _windLoadData,
                    backgroundFactor);
            var structuralFactor =
                new StructuralFactor(sizeFactor, dynamicFactor);
            return structuralFactor;
        }

        private IFactor GetDynamicFactor(
            IFactor resonanceResponse, IFactor backgroundFactor)
        {
            var upCrossingFrequency =
                new UpCrossingFrequency(
                    _fundamentalFlexuralFrequency,
                    backgroundFactor,
                    resonanceResponse);
            var peakFactor =
                new PeakFactor(upCrossingFrequency);

            return new DynamicFactor(
                    _referenceHeight,
                    _windLoadData,
                    peakFactor,
                    backgroundFactor,
                    resonanceResponse);
        }

        private IFactor GetResonanceResponse(IFactor logarithmicDecrementOfDamping)
        {
            var nonDimensionalFrequency =
                new NonDimensionalFrequency(
                    _windLoadData,
                    _fundamentalFlexuralFrequency,
                    _turbulentLengthScale);

            var nonDimensionalPowerSpectralDensity =
                new NonDimensionalPowerSpectralDensity(nonDimensionalFrequency);
            var aerodynamicAdmittanceWidth =
                new AerodynamicAdmittanceWidth(
                    _building,
                    _referenceHeight,
                    _turbulentLengthScale,
                    nonDimensionalFrequency);
            var aerodynamicAdmittanceHeight =
                new AerodynamicAdmittanceHeight(
                    _building,
                    _referenceHeight,
                    _turbulentLengthScale,
                    nonDimensionalFrequency);

            return new ResonanceResponse(
                    _referenceHeight,
                logarithmicDecrementOfDamping,
                nonDimensionalPowerSpectralDensity,
                aerodynamicAdmittanceWidth,
                aerodynamicAdmittanceHeight);
        }

        private IFactor GetLogarithmicDecrementOfDamping()
        {
            var logarithmicDecrementOfStructuralDamping =
                new LogarithmicDecrementOfStructuralDamping(_structuralType);
            return new LogarithmicDecrementOfDamping(
                    logarithmicDecrementOfStructuralDamping, null, null);
        }

        #endregion // Private_Methods
    }
}
