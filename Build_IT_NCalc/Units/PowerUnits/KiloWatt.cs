using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.PowerUnits
{
    class KiloWatt : Unit, IMainUnit, ISubUnit<KiloWatt>
    {
        public const string Unit = "kW";

        public KiloWatt() : base(Unit)
        {
        }
        public KiloWatt(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloWatt>(valueUnit, val => val);
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<KiloWatt>(valueUnit, val => val);
        }
    }
}
