using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.LengthUnits
{
    public class Milimeter : Unit, ISubUnit<Meter>
    {
        public const string Unit = "mm";

        public Milimeter() : base(Unit)
        {
        }
        public Milimeter(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Meter>(valueUnit, val => val / GetMultiplier(1000));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Milimeter>(valueUnit, val => val * GetMultiplier(1000));
        }
    }
}
