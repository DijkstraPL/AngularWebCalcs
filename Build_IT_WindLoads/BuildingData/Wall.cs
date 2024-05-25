using Build_IT_CommonTools.Attributes;
using Build_IT_WindLoads.BuildingData.Interfaces;
using System;
using System.Collections.Generic;

namespace Build_IT_WindLoads.BuildingData
{
    public class Wall : IWall
    {
        #region Properties
        
        public double Length { get; }
        public double LeftHeight { get; }
        public double MiddleHeight { get; }
        public double RightHeight { get; }

        public IDictionary<Field, double> Areas { get; }

        /// <summary>
        /// e
        /// </summary>
        [Abbreviation("e")]
        [Unit("m")]
        public double EdgeDistance { get; }

        public Rotation CurrentRotation { get; }

        #endregion // Properties

        #region Constructors
        
        public Wall(double length,
    double leftHeight, double middleHeight, double rightHeight,
    double edgeDistance, Rotation rotation)
        {
            Length = length;
            LeftHeight = leftHeight;
            MiddleHeight = middleHeight;
            RightHeight = rightHeight;
            EdgeDistance = edgeDistance;
            CurrentRotation = rotation;

            Areas = new Dictionary<Field, double>();
            SetAreas();
        }

        public Wall(double length, double height, double edgeDistance, Rotation rotation)
            : this(length, height, height, height, edgeDistance, rotation)
        {
        }

        #endregion // Constructors

        #region Private_Methods
        
        private void SetAreas()
        {
            switch (CurrentRotation)
            {
                case Rotation.UpWind:
                    Areas.Add(Field.D, GetArea());
                    break;
                case Rotation.Crosswind:
                    if (EdgeDistance < Length)
                        SetWallAreasForLargeBuildingWidth();
                    else if (EdgeDistance >= 5 * Length)
                        SetWallAreasForSmallBuildingWidth();
                    else
                        SetWallAreasForMediumBuildingWidth();
                    break;
                case Rotation.DownWind:
                    Areas.Add(Field.E, GetArea());
                    break;
                default:
                    break;
            }
        }

        private double GetArea()
            => Length / 2 * (LeftHeight + MiddleHeight) / 2 +
            Length / 2 * (RightHeight + MiddleHeight) / 2;

        private void SetWallAreasForLargeBuildingWidth()
        {
            double areaForAField = EdgeDistance / 5 *
                    (LeftHeight + GetHeightAt(EdgeDistance / 5)) / 2;
            double areaForBField;
            Areas.Add(Field.A, areaForAField);
            if (EdgeDistance <= Length / 2)
                areaForBField = 4 * EdgeDistance / 5 *
                    (GetHeightAt(EdgeDistance / 5) + GetHeightAt(EdgeDistance)) / 2;
            else
                areaForBField = (Length / 2 - EdgeDistance / 5) *
                    (GetHeightAt(EdgeDistance / 5) + MiddleHeight) / 2 +
                    (EdgeDistance - Length / 2) *
                    (MiddleHeight + GetHeightAt(EdgeDistance)) / 2;

            Areas.Add(Field.B, areaForBField);
            Areas.Add(Field.C, GetArea() - areaForAField - areaForBField);
        }

        private void SetWallAreasForSmallBuildingWidth()
        {
            Areas.Add(Field.A, GetArea());
        }

        private void SetWallAreasForMediumBuildingWidth()
        {
            double areaForAField;
            if (EdgeDistance / 5 <= Length / 2)
                areaForAField = EdgeDistance / 5 *
                    (LeftHeight + GetHeightAt(EdgeDistance / 5)) / 2;
            else
                areaForAField = Length / 2 * (LeftHeight + MiddleHeight) / 2 +
                    (EdgeDistance / 5 - Length / 2) *
                    (MiddleHeight + GetHeightAt(EdgeDistance / 5)) / 2;
            Areas.Add(Field.A, areaForAField);
            Areas.Add(Field.B, GetArea() - areaForAField);
        }

        private double GetHeightAt(double position)
        {
            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position));
            if (position <= Length / 2)
                return (MiddleHeight - LeftHeight) / (Length / 2) * position + LeftHeight;
            else
                return (RightHeight - MiddleHeight) / (Length / 2) *
                    (position - Length / 2) + MiddleHeight;
        }

        #endregion // Private_Methods

        #region Enums
        
        public enum Rotation
        {
            UpWind,
            Crosswind,
            DownWind
        }

        #endregion // Enums
    }
}
