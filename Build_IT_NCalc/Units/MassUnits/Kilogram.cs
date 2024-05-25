using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.MassUnits
{
    public class Kilogram : MassUnit, IMainUnit
    {
        public const string Unit = "kg";

        public Kilogram() : base(Unit)
        {
        }
        public Kilogram(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Kilogram>(valueUnit, val => val);
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Kilogram>(valueUnit, val => val);
        }
    }
}
