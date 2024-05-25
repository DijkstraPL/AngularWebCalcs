using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.PressureUnits
{
    public class GigaPascal : Unit, ISubUnit<KiloPascal>
    {
        public const string Unit = "GPa";

        public GigaPascal() : base(Unit)
        {
        }
        public GigaPascal(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloPascal>(valueUnit, val => val * GetMultiplier(1000000));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<MegaPascal>(valueUnit, val => val / GetMultiplier(1000000));
        }
    }
}
