using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.PowerUnits
{
    public class MegaWatt : Unit, ISubUnit<KiloWatt>
    {
        public const string Unit = "MW";

        public MegaWatt() : base(Unit)
        {
        }
        public MegaWatt(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloWatt>(valueUnit, val => val * GetMultiplier(1000));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<MegaWatt>(valueUnit, val => val / GetMultiplier(1000));
        }
    }
}
