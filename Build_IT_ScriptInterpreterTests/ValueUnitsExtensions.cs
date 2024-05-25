using Build_IT_NCalc.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_ScriptInterpreterTests
{
    internal static class ValueUnitsExtensions
    {
        public static bool AreEqual(this ValueUnit valueUnitA, ValueUnit valueUnitB, double precision)
        {
            valueUnitA.OrganizeUnits();
            valueUnitB.OrganizeUnits();

            valueUnitA.RoundValue(precision);
            valueUnitB.RoundValue(precision);

            return valueUnitA.Equals(valueUnitB);
        }

        public static bool AreEqual(this ValueUnit valueUnitA, object toCompare, double precision)
        {
            if (toCompare is ValueUnit valueUnitB)
                return AreEqual(valueUnitA, valueUnitB, precision);

            valueUnitA.OrganizeUnits();
            valueUnitA.RoundValue(precision);

            if (toCompare is double)
            {
                double val = (double)toCompare;
                double roundedVal = Math.Round(val / precision) * precision;
                return valueUnitA.Equals(roundedVal);
            }
            return valueUnitA.Equals(toCompare);
        }
    }
}
