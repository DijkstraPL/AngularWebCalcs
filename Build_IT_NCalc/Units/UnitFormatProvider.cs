using NCalc;
using System;
using System.Globalization;

namespace Build_IT_NCalc.Units
{
    public class UnitFormatProvider : IFormatProvider
    {
        public bool OrganizeUnits => _evaluateOptions.HasFlag(EvaluateOptions.AllowUnitCalculations);

        private readonly EvaluateOptions _evaluateOptions;

        public UnitFormatProvider(EvaluateOptions evaluateOptions)
        {
            _evaluateOptions = evaluateOptions;
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(UnitFormatProvider))
                return this;
            else
                return null;
        }
    }
}
