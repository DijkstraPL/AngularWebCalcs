using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.PowerUnits
{
    public class Watt : Unit, ISubUnit<KiloWatt>
    {
        public const string Unit = "W";

        public Watt() : base(Unit)
        {
        }
        public Watt(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloWatt>(valueUnit, val => val / GetMultiplier(1000));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Watt>(valueUnit, val => val * GetMultiplier(1000));
        }
    }
}
