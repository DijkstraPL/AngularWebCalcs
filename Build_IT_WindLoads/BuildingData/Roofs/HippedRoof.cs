using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.BuildingData.Interfaces;
using System;

namespace Build_IT_WindLoads.BuildingData.Roofs
{
    public class HippedRoof : Structure, IHippedRoof
    {
        #region Properties

        public Rotation CurrentRotation { get; }
        public double MiddleHeight { get; private set; }
        public double OuterHeight { get; private set; }

        [Abbreviation("α_0")]
        [Unit("°")]
        public double Angle0 { get; private set; }
        [Abbreviation("α_90")]
        [Unit("°")]
        public double Angle90 { get; private set; }
        public double RidgeLength { get; private set; }

        public double Angle0InRadians => Angle0 * Math.PI / 180;
        public double Angle90InRadians => Angle90 * Math.PI / 180;

        #endregion // Properties

        #region Constructors

        public HippedRoof(
            double length, double width, double ridgeLength,
            double outerHeight, double middleHeight, Rotation rotation)
            : base()
        {
            CurrentRotation = rotation;

            OuterHeight = outerHeight;
            MiddleHeight = middleHeight;
            Height = MiddleHeight;
            RidgeLength = ridgeLength;

            if (CurrentRotation == Rotation.Degrees_0)
            {
                Length = length;
                Width = width;
            }
            else if (CurrentRotation == Rotation.Degrees_90)
            {
                Length = width;
                Width = length;
            }
            else
                throw new ArgumentException(nameof(CurrentRotation));

            Angle0 = Math.Atan((MiddleHeight - OuterHeight) /
                (length / 2)) * 180 / Math.PI;
            Angle90 = Math.Atan((MiddleHeight - OuterHeight) /
                ((width - RidgeLength) / 2)) * 180 / Math.PI;

            SetRoofAreas();
        }

        #endregion // Constructors

        #region Public_Methods

        public override double GetReferenceHeight()
            => 0.6 * Height;

        #endregion // Public_Methods

        #region Private_Methods

        private void SetRoofAreas()
        {
            if (CurrentRotation == Rotation.Degrees_0)
            {
                var restDistance = Width / 2 -
                    GetDistanceForParallelToRidgeWindAt(EdgeDistance / 10) / 2;
                var areaForFField = ((EdgeDistance / 4 +
                    (EdgeDistance / 4 - restDistance)) *
                    EdgeDistance / 10 /
                    Math.Cos(Angle0InRadians)) / 2;
                var areaForGField = (Width - 2 * EdgeDistance / 4) * EdgeDistance / 10 / Math.Cos(Angle0InRadians);
                var areaForHField = (GetDistanceForParallelToRidgeWindAt(EdgeDistance / 10) + RidgeLength) *
                    (Length / 2 - EdgeDistance / 10) / 2 / Math.Cos(Angle0InRadians);

                var areaForKField = (GetDistanceForParallelToRidgeWindAt(Length / 2 - EdgeDistance / 20) + RidgeLength) *
                    EdgeDistance / 20 / 2 / Math.Cos(Angle0InRadians);
                var areaForJField = EdgeDistance / 10 * (Length / 2 - EdgeDistance / 20) / Math.Cos(Angle0InRadians);
                var areaForIField = (Width - 2 * EdgeDistance / 10 +
                    GetDistanceForParallelToRidgeWindAt(Length / 2 - EdgeDistance / 20)) *
                    (Length / 2 - EdgeDistance / 20) / 2 / Math.Cos(Angle0InRadians);

                var areaForMField = (Length - EdgeDistance / 10) *
                    ((Width - RidgeLength) / 2 - EdgeDistance / 20)
                    / 2 / Math.Cos(Angle90InRadians);
                var areaForLField = Length * (Width - RidgeLength) / 2
                    / 2 / Math.Cos(Angle90InRadians) - areaForMField;

                Areas.Add(Field.F, areaForFField);
                Areas.Add(Field.G, areaForGField);
                Areas.Add(Field.H, areaForHField);

                Areas.Add(Field.K, areaForKField);
                Areas.Add(Field.I, areaForIField);
                Areas.Add(Field.J, areaForJField);
                Areas.Add(Field.M, areaForMField);
                Areas.Add(Field.L, areaForLField);

            }
            else if (CurrentRotation == Rotation.Degrees_90)
            {
                var restDistance = Length / 2 -
                    GetDistanceForPerpendicularlyToRidgeWindAt(EdgeDistance / 10) / 2;
                var areaForFField = ((EdgeDistance / 4 +
                    (EdgeDistance / 4 - restDistance)) *
                    EdgeDistance / 10 /
                    Math.Cos(Angle90InRadians)) / 2;
                var areaForGField = (Width - 2 * EdgeDistance / 4) * EdgeDistance / 10 / Math.Cos(Angle90InRadians);
                var areaForHField = Width * (Length - RidgeLength) / 2 / 2 / Math.Cos(Angle90InRadians)
                    - areaForFField * 2 - areaForGField;
                var areaForIField = (Width - EdgeDistance / 10 * 2) *
                    ((Length - RidgeLength) / 2 - EdgeDistance / 10) / 2 / Math.Cos(Angle90InRadians);
                var areaForJField = Width * (Length - RidgeLength) / 2 / 2 / Math.Cos(Angle90InRadians) -
                    areaForIField;
                var areaForLField = EdgeDistance / 10 * Width / 2 / Math.Cos(Angle0InRadians);
                double areaForMField;
                if (EdgeDistance / 2 > (Width - RidgeLength) / 2)
                    areaForMField = (EdgeDistance / 2 - EdgeDistance / 10 +
                        EdgeDistance / 2 - EdgeDistance / 10 - (Length - RidgeLength) / 2) * Width / 2 / 2 /
                        Math.Cos(Angle0InRadians);
                else
                    areaForMField = (EdgeDistance / 2 - EdgeDistance / 10) *
                        Width / (Length - RidgeLength) * EdgeDistance / 2 / Math.Cos(Angle0InRadians);

                var areaForNField = (Length + RidgeLength) * Width / 2 / 2 / Math.Cos(Angle0InRadians)
                    - areaForMField - areaForLField;

                Areas.Add(Field.F, areaForFField);
                Areas.Add(Field.G, areaForGField);
                Areas.Add(Field.H, areaForHField);
                Areas.Add(Field.I, areaForIField);
                Areas.Add(Field.J, areaForJField);
                Areas.Add(Field.L, areaForLField);
                Areas.Add(Field.M, areaForMField);
                Areas.Add(Field.N, areaForNField);
            }
            else
                throw new ArgumentException(nameof(CurrentRotation));
        }

        private double GetDistanceForParallelToRidgeWindAt(double x)
        {
            return ((RidgeLength / Length - Width / Length) *
                     x + Width / 2) * 2;
        }

        private double GetDistanceForPerpendicularlyToRidgeWindAt(double x)
        {
            return -2 * Length / (Width - RidgeLength) * x + Length;
        }

        #endregion // Private_Methods

        #region Enums

        public enum Rotation
        {
            Degrees_0,
            Degrees_90
        }

        #endregion // Enums
    }
}
