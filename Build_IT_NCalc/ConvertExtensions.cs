using Build_IT_NCalc.Units;
using System;

namespace NCalc
{
    public static class ConvertExtensions
    {
        public static ValueUnit ToValueUnit(object? value)
        {
            if (value is not null && value is ValueUnit valueUnit)
                return valueUnit;
            if (value is not null && ValueUnit.TryParse(value, out ValueUnit result))
                return result;
            return null;
        }

        public static object ToObject(object value) => value;

        public static object Error(string errorMessage)
        {
            throw new CalculationException(errorMessage);
            return null;
        }
    }
}