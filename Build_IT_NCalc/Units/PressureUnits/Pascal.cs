using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.PressureUnits
{
    public class Pascal : Unit, ISubUnit<KiloPascal>
    {
        public const string Unit = "Pa";

        public Pascal() : base(Unit)
        {
        }
        public Pascal(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloPascal>(valueUnit, val => val / GetMultiplier(1000));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Pascal>(valueUnit, val => val * GetMultiplier(1000));
        }
    }
}
