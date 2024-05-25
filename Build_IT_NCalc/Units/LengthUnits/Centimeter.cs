using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.LengthUnits
{
    public class Centimeter : Unit, ISubUnit<Meter>
    {
        public const string Unit = "cm";

        public Centimeter() : base(Unit)
        {
        }
        public Centimeter(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Meter>(valueUnit, val => val / GetMultiplier(100));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Decimeter>(valueUnit, val => val * GetMultiplier(100));
        }
    }
}
