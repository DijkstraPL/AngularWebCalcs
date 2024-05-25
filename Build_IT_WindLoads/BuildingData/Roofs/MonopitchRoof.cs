using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.BuildingData.Interfaces;
using System;

namespace Build_IT_WindLoads.BuildingData
{
    public class MonopitchRoof : Structure, IMonopitchRoof
    {
        #region Properties

        public Rotation CurrentRotation { get; }
        public double MaxHeight { get; private set; }
        public double MinHeight { get; private set; }
        [Abbreviation("α")]
        [Unit("°")]
        public double Angle { get; private set; }
        public double AngleInRadians => Angle * Math.PI / 180;

        #endregion // Properties

        #region Constructors

        public MonopitchRoof(
            double length, double width,
            double maxHeight, double minHeight, Rotation rotation)
            : base()
        {
            CurrentRotation = rotation;

            MaxHeight = maxHeight;
            MinHeight = minHeight;
            Height = MaxHeight;

            if (CurrentRotation == Rotation.Degrees_0 ||
                CurrentRotation == Rotation.Degrees_180)
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

            Angle = Math.Atan((MaxHeight - MinHeight) / length) * 180 / Math.PI;
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
            if (CurrentRotation == Rotation.Degrees_0 ||
                CurrentRotation == Rotation.Degrees_180)
            {
                Areas.Add(Field.F, 
                    EdgeDistance / 4 * 
                    EdgeDistance / 10 /
                    Math.Cos(AngleInRadians));
                Areas.Add(Field.G, 
                    (Width - 2 * EdgeDistance / 4) * 
                    EdgeDistance / 10 /
                    Math.Cos(AngleInRadians));
                Areas.Add(Field.H, 
                    (Length - EdgeDistance / 10) /
                    Math.Cos(AngleInRadians) 
                    * Width);
            }
            else if (CurrentRotation == Rotation.Degrees_90)
            {
                Areas.Add(Field.Fup, 
                    EdgeDistance / 4 /
                    Math.Cos(AngleInRadians) * 
                    EdgeDistance / 10);
                Areas.Add(Field.Flow,
                    EdgeDistance / 4 /
                    Math.Cos(AngleInRadians) * 
                    EdgeDistance / 10);
                Areas.Add(Field.G, 
                    (Width - 2 * EdgeDistance / 4) /
                    Math.Cos(AngleInRadians) * 
                    EdgeDistance / 10);
                Areas.Add(Field.H, 
                    (EdgeDistance / 2 - EdgeDistance / 10) * 
                    Width /
                    Math.Cos(AngleInRadians));
                Areas.Add(Field.I, 
                    (Length - EdgeDistance / 2) * 
                    Width /
                    Math.Cos(AngleInRadians));
            }
            else
                throw new ArgumentException(nameof(CurrentRotation));
        }

        #endregion // Private_Methods

        #region Enums

        public enum Rotation
        {
            Degrees_0,
            Degrees_90,
            Degrees_180
        }

        #endregion // Enums
    }
}
