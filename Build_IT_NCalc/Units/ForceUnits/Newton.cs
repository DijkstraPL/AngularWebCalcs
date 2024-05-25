using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.ForceUnits
{

    public class Newton : Unit, ISubUnit<KiloNewton>
    {
        public const string Unit = "N";

        public Newton() : base(Unit)
        {
        }
        public Newton(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<KiloNewton>(valueUnit, val => val / GetMultiplier(1000));
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Newton>(valueUnit, val => val * GetMultiplier(1000));
        }
    }
}
