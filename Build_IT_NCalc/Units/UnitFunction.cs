using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_NCalc.Units
{
    public static class UnitFunction
    {
        public static ValueUnit Create(double value, params string[] units)
        {
            return new ValueUnit(value, units);
        }
    }
}
