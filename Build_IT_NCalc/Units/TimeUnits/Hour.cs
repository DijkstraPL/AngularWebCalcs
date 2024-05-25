using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.TimeUnits
{
    public class Hour : Unit, IMainUnit, ISubUnit<Hour>
    {
        public const string Unit = "hr";

        public Hour() : base(Unit)
        {
        }
        public Hour(double power = 1) : base(Unit, power)
        {
        }

        public override void TransformFromMain(ValueUnit valueUnit)
        {
            TransformTo<Hour>(valueUnit, val => val);
        }

        public override void TransformToMain(ValueUnit valueUnit)
        {
            TransformTo<Hour>(valueUnit, val => val);
        }
    }
    }
