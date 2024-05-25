using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoads.BuildingData.Roofs
{
    public class FlatRoofWithParapetes : FlatRoof
    {
        public FlatRoofWithParapetes(double length, double width, double height, 
            double parapetHeight, Rotation rotation = Rotation.Degrees_0) 
            : base(length, width, height + parapetHeight, rotation)
        {
        }
    }
}
