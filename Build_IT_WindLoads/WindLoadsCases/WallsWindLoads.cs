using Build_IT_CommonTools;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_WindLoads.WindLoadsCases
{
    public class WallsWindLoads : WindLoadCase
    {
        #region Fields
        
        private readonly IDictionary<Field, double> _ratio5For10SquareMeters =
            new Dictionary<Field, double>
            {
                {Field.A, -1.2 },
                {Field.B, -0.8 },
                {Field.C, -0.5 },
                {Field.D, 0.8 },
                {Field.E, -0.7 }
            };

        private readonly IDictionary<Field, double> _ratio1For10SquareMeters
            = new Dictionary<Field, double>
            {
                {Field.A, -1.2 },
                {Field.B, -0.8 },
                {Field.C, -0.5 },
                {Field.D, 0.8 },
                {Field.E, -0.5 }
            };

        private readonly IDictionary<Field, double> _ratio0_25For10SquareMeters
            = new Dictionary<Field, double>
            {
                {Field.A, -1.2 },
                {Field.B, -0.8 },
                {Field.C, -0.5 },
                {Field.D, 0.7 },
                {Field.E, -0.3 }
            };

        private readonly IDictionary<Field, double> _ratio5For1SquareMeter =
            new Dictionary<Field, double>
            {
                {Field.A, -1.4 },
                {Field.B, -1.1 },
                {Field.C, -0.5 },
                {Field.D, 1 },
                {Field.E, -0.7 }
            };

        private readonly IDictionary<Field, double> _ratio1For1SquareMeter
            = new Dictionary<Field, double>
            {
                {Field.A, -1.4 },
                {Field.B, -1.1 },
                {Field.C, -0.5 },
                {Field.D, 1 },
                {Field.E, -0.5 }
            };

        private readonly IDictionary<Field, double> _ratio0_25For1SquareMeter
            = new Dictionary<Field, double>
            {
                {Field.A, -1.4 },
                {Field.B, -1.1 },
                {Field.C, -0.5 },
                {Field.D, 1 },
                {Field.E, -0.3 }
            };

        #endregion // Fields

        #region Constructors

        public WallsWindLoads(IStructure building, IWindLoadData windLoadData)
            : base(building, windLoadData)
        {
        }

        #endregion // Constructors

        #region Public_Methods
        public override IEnumerable<IDictionary<Field, double>> CalculatePressureCoefficients()
        {
            yield return GetExternalPressureCoefficientsMax();
        }

        public override IDictionary<Field, double> GetExternalPressureCoefficientsMax() 
            => GetExternalPressureCoefficients(
                GetExternalPressureCoefficientTenSquareMeters(),
                GetExternalPressureCoefficientOneSquareMeter());

        public override IDictionary<Field, double> GetExternalPressureCoefficientsMin()
            => GetExternalPressureCoefficientsMax();

        #endregion // Public_Methods

        #region Private_Methods

        private IDictionary<Field, double> GetExternalPressureCoefficientOneSquareMeter()
        {
            double heightToWindParallelWidthRatio = _building.Height / _building.Length;

            if (heightToWindParallelWidthRatio < 0)
                throw new ArgumentOutOfRangeException("Height or width is less than 0.");
            if (heightToWindParallelWidthRatio >= 5)
                return _ratio5For1SquareMeter;
            else if (heightToWindParallelWidthRatio == 1)
                return _ratio1For1SquareMeter;
            else if (heightToWindParallelWidthRatio <= 0.25)
                return _ratio0_25For1SquareMeter;
            else if (heightToWindParallelWidthRatio < 1)
                return InterpolateBetweenFor(
                    (1, _ratio1For1SquareMeter),
                    (0.25, _ratio0_25For1SquareMeter),
                    heightToWindParallelWidthRatio);
            else if (heightToWindParallelWidthRatio < 5)
                return InterpolateBetweenFor(
                    (5, _ratio5For1SquareMeter),
                    (1, _ratio1For1SquareMeter),
                    heightToWindParallelWidthRatio);

            throw new ArgumentOutOfRangeException();
        }

        private IDictionary<Field, double> GetExternalPressureCoefficientTenSquareMeters()
        {
            double heightToWindParallelWidthRatio = _building.Height / _building.Length;

            if (heightToWindParallelWidthRatio < 0)
                throw new ArgumentOutOfRangeException("Height or width is less than 0.");
            if (heightToWindParallelWidthRatio >= 5)
                return _ratio5For10SquareMeters;
            else if (heightToWindParallelWidthRatio == 1)
                return _ratio1For10SquareMeters;
            else if (heightToWindParallelWidthRatio <= 0.25)
                return _ratio0_25For10SquareMeters;
            else if (heightToWindParallelWidthRatio < 1)
                return InterpolateBetweenFor(
                    (1, _ratio1For10SquareMeters),
                    (0.25, _ratio0_25For10SquareMeters),
                    heightToWindParallelWidthRatio);
            else if (heightToWindParallelWidthRatio < 5)
                return InterpolateBetweenFor(
                    (5, _ratio5For10SquareMeters),
                    (1, _ratio1For10SquareMeters),
                    heightToWindParallelWidthRatio);

            throw new ArgumentOutOfRangeException();
        }
        
        #endregion // Private_Methods
    }
}
