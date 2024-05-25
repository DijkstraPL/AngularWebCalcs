using Build_IT_CommonTools;
using Build_IT_WindLoads.BuildingData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_WindLoads.BuildingData.Walls
{
    public class EqualHeightWalls : Structure, IFlatRoofBuilding
    {
        #region Properties

        public const Rotation DefaultRotation = Rotation.Degrees_0;

        #endregion // Properties

        #region Constructors

        public EqualHeightWalls(
            double length, double width, double height, 
            Rotation rotation = DefaultRotation) : base()
        {
            if (length < 0 || width < 0 || height < 0)
                throw new ArgumentOutOfRangeException("Wrong building dimensions.");

            if (rotation == Rotation.Degrees_90)
            {
                Length = width;
                Width = length;
            }
            else
            {
                Length = length;
                Width = width;
            }
            Height = height;

            SetWallAreas();
        }

        #endregion // Constructors

        #region Public_Methods

        public override double GetReferenceHeight()
            => 0.6 * Height;

        #endregion // Public_Methods

        #region Private_Methods

        private void SetWallAreas()
        {
            var upWindWall = new Wall(Width, Height, EdgeDistance, Wall.Rotation.UpWind);
            var downWindWall = new Wall(Width, Height, EdgeDistance, Wall.Rotation.DownWind);
            var crossWindWall = new Wall(Length, Height, EdgeDistance, Wall.Rotation.Crosswind);

             var areas = upWindWall.Areas.Concat(downWindWall.Areas)
                .Concat(crossWindWall.Areas);

            foreach (var area in areas)
                if (!Areas.ContainsKey(area.Key))
                    Areas.Add(area.Key, area.Value);
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
