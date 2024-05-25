using Build_IT_NCalc.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_NCalc.Units.AngleUnits
{
    public abstract class AngleUnit : Unit, ISubUnit<Radian>
    {
        protected AngleUnit(string symbol, double power = 1) : base(symbol, power)
        {
        }
    }
}
