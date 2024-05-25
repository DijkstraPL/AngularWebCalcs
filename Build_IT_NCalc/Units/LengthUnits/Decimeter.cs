using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.LengthUnits
{
   public class Decimeter : Unit, ISubUnit<Meter>
    {
        public const string Unit = "dm";

        public Decimeter() : base(Unit)
        {
        }
        public Decimeter(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Meter>(valueUnit, val => val / GetMultiplier(10));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Decimeter>(valueUnit, val => val * GetMultiplier(10));
        }
    }
}
