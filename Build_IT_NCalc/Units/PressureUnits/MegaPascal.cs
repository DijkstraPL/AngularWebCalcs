using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.PressureUnits
{
    public class MegaPascal : Unit, ISubUnit<KiloPascal>
    {
        public const string Unit = "MPa";

        public MegaPascal() : base(Unit)
        {
        }
        public MegaPascal(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloPascal>(valueUnit, val => val * GetMultiplier(1000));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<MegaPascal>(valueUnit, val => val / GetMultiplier(1000));
        }
    }
}
