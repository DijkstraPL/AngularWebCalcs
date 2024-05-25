using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.TimeUnits
{
    public class Minute : Unit, ISubUnit<Hour>
    {
        public const string Unit = "min";

        public Minute() : base(Unit)
        {
        }
        public Minute(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Minute>(valueUnit, val => val * GetMultiplier(60));
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Hour>(valueUnit, val => val / GetMultiplier(60));
        }
    }
}
