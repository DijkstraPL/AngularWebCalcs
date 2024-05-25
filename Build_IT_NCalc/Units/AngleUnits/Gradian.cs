using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.AngleUnits
{
    public class Gradian : AngleUnit
    {
        public const string Unit = "grad";

        public Gradian() : base(Unit)
        {
        }
        public Gradian(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Gradian>(valueUnit, val => val * GetMultiplier(200 / Math.PI));
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Radian>(valueUnit, val => val * GetMultiplier(Math.PI / 200));
        }
    }
}
