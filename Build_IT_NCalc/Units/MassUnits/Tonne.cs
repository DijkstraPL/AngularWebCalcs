using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.MassUnits
{
    public class Tonne : MassUnit
    {
        public const string Unit = "t";

        public Tonne() : base(Unit)
        {
        }
        public Tonne(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Tonne>(valueUnit, val => val / GetMultiplier(1000));
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Kilogram>(valueUnit, val => val * GetMultiplier(1000));
        }
    }
}
