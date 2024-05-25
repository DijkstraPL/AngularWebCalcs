using System;
using System.Collections.Generic;
using System.Text;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;

namespace Build_IT_WindLoads.WindLoadsCases.Roofs
{
    public class DuopitchRoofWindLoadsRotation90 : DuopitchRoofWindLoads
    {
        #region Properties
        protected override IDictionary<Field, double> RatioFor10SquareMetersAngleMinus45Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -1.4 },
                {Field.G, -1.2 },
                {Field.H, -1 },
                {Field.I, -0.9 },
            };

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngleMinus45Min
            => RatioFor10SquareMetersAngleMinus45Max;

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngleMinus45Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -2 },
                {Field.G, -2 },
                {Field.H, -1.3 },
                {Field.I, -1.2 },
            };

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngleMinus45Min
            => RatioFor1SquareMeterAngleMinus45Max;

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngleMinus30Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -1.5 },
                {Field.G, -1.2 },
                {Field.H, -1 },
                {Field.I, -0.9 }
            };

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngleMinus30Min
            => RatioFor10SquareMetersAngleMinus30Max;

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngleMinus30Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -2.1 },
                {Field.G, -2 },
                {Field.H, -1.3 },
                {Field.I, -1.2 }
            };

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngleMinus30Min
            => RatioFor1SquareMeterAngleMinus30Max;

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngleMinus15Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -1.9 },
                {Field.G, -1.2 },
                {Field.H, -0.8 },
                {Field.I, -0.8 }
            };

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngleMinus15Min
            => RatioFor10SquareMetersAngleMinus15Max;

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngleMinus15Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -2.5 },
                {Field.G, -2 },
                {Field.H, -1.2 },
                {Field.I, -1.2 }
            };

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngleMinus15Min
            => RatioFor1SquareMeterAngleMinus15Max;

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngleMinus5Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -1.8 },
                {Field.G, -1.2 },
                {Field.H, -0.7 },
                {Field.I, -0.6 }
            };

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngleMinus5Min
            => RatioFor10SquareMetersAngleMinus5Max;

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngleMinus5Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -2.5},
                {Field.G, -2 },
                {Field.H, -1.2 },
                {Field.I, -1.2 }
            };

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngleMinus5Min
            => RatioFor1SquareMeterAngleMinus5Max;

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle5Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -1.6},
                {Field.G, -1.3 },
                {Field.H, -0.7 },
                {Field.I, -0.6 }
            };

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle5Min
            => RatioFor10SquareMetersAngle5Max;

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle5Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -2.2},
                {Field.G, -2 },
                {Field.H, -1.2 },
                {Field.I, -0.6 }
            };

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle5Min
            => RatioFor1SquareMeterAngle5Max;

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle15Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -1.3},
                {Field.G, -1.3 },
                {Field.H, -0.6 },
                {Field.I, -0.5 }
            };

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle15Min
            => RatioFor10SquareMetersAngle15Max;

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle15Max { get; } =
            new Dictionary<Field, double>
            {
                {Field.F, -2},
                {Field.G, -2 },
                {Field.H, -1.2 },
                {Field.I, -0.5 }
            };

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle15Min
            => RatioFor1SquareMeterAngle15Max;

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle30Max { get; } =
           new Dictionary<Field, double>
           {
                {Field.F, -1.1},
                {Field.G, -1.4},
                {Field.H, -0.8 },
                {Field.I, -0.5 }
           };

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle30Min
            => RatioFor10SquareMetersAngle30Max;

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle30Max { get; } =
           new Dictionary<Field, double>
           {
                {Field.F, -1.5},
                {Field.G, -2},
                {Field.H, -1.2 },
                {Field.I, -0.5 }
           };

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle30Min
            => RatioFor1SquareMeterAngle30Max;

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle45Max { get; } =
           new Dictionary<Field, double>
           {
                {Field.F, -1.1},
                {Field.G, -1.4},
                {Field.H, -0.9 },
                {Field.I, -0.5 }
           };

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle45Min
            => RatioFor10SquareMetersAngle45Max;

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle45Max { get; } =
           new Dictionary<Field, double>
           {
                {Field.F, -1.5},
                {Field.G, -2},
                {Field.H, -1.2 },
                {Field.I, -0.5 }
           };

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle45Min
            => RatioFor1SquareMeterAngle45Max;

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle60Max { get; } =
           new Dictionary<Field, double>
           {
                {Field.F, -1.1},
                {Field.G, -1.2},
                {Field.H, -0.8 },
                {Field.I, -0.5 }
           };

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle60Min
            => RatioFor10SquareMetersAngle60Max;

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle60Max { get; } =
           new Dictionary<Field, double>
           {
                {Field.F, -1.5},
                {Field.G, -2},
                {Field.H, -1 },
                {Field.I, -0.5 }
           };

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle60Min 
            => RatioFor1SquareMeterAngle60Max;

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle75Max { get; } =
           new Dictionary<Field, double>
           {
                {Field.F, -1.1},
                {Field.G, -1.2},
                {Field.H, -0.8 },
                {Field.I, -0.5 }
           };

        protected override IDictionary<Field, double> RatioFor10SquareMetersAngle75Min 
            => RatioFor10SquareMetersAngle75Max;

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle75Max { get; } =
           new Dictionary<Field, double>
           {
                {Field.F, -1.5},
                {Field.G, -2},
                {Field.H, -1 },
                {Field.I, -0.5 }
           };

        protected override IDictionary<Field, double> RatioFor1SquareMeterAngle75Min 
            => RatioFor1SquareMeterAngle75Max;

        #endregion // Properties

        #region Constructors

        public DuopitchRoofWindLoadsRotation90(
            IDuopitchRoof building, IWindLoadData windLoadData)
            : base(building, windLoadData)
        {
        }

        #endregion // Constructors
    }
}
