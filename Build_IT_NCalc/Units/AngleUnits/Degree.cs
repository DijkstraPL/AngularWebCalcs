using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.AngleUnits
{
    public class Degree : AngleUnit
    {
        public const string Unit = "°";

        public Degree() : base(Unit)
        {
        }
        public Degree(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Degree>(valueUnit, val => val * GetMultiplier(180/Math.PI));
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Radian>(valueUnit, val => val * GetMultiplier(Math.PI / 180));
        }
    }

    public class Degree2 : AngleUnit
    {
        public const string Unit = "deg";

        public Degree2() : base(Unit)
        {
        }
        public Degree2(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Degree>(valueUnit, val => val * GetMultiplier(180 / Math.PI));
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Radian>(valueUnit, val => val * GetMultiplier(Math.PI / 180));
        }
    }
}
