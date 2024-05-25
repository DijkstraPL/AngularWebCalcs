using Build_IT_NCalc.Units;
using Build_IT_NCalc.Units.AngleUnits;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Build_IT_NCalc.Units
{
    public static class UnitMath
    {
        public static ValueUnit Abs(ValueUnit unit)
        {
            try
            {
                double value = Math.Abs(unit.Value);
                return new ValueUnit(value,  unit.Units.ToArray());
            }
            catch
            {
                throw;
            }
        }

        public static ValueUnit Pow(ValueUnit unit, double power)
        {
            unit.OrganizeUnits();
            var value = Math.Pow(unit.Value, power);
            return new ValueUnit(value, unit.Units.Select(u => u.Copy(u.Power * power)).ToArray());
        }


        public static ValueUnit Acos(ValueUnit unit)
        {
            if (unit.Units.Count() > 0)
                throw new FormatException("Couldn't calculate Acos for unit with parameters");

            try
            {
                double value = Math.Acos(unit.Value);
                return new ValueUnit(value, new Radian());
            }
            catch
            {
                throw;
            }
        }

        public static ValueUnit Asin(ValueUnit unit)
        {
            if (unit.Units.Count() > 0)
                throw new FormatException("Couldn't calculate Asin for unit with parameters");

            try
            {
                double value = Math.Asin(unit.Value);
                return new ValueUnit(value, new Radian());
            }
            catch
            {
                throw;
            }
        }

        public static ValueUnit Atan(ValueUnit unit)
        {
            if (unit.Units.Count() > 0)
                throw new FormatException("Couldn't calculate Asin for unit with parameters");

            try
            {
                double value = Math.Atan(unit.Value);
                return new ValueUnit(value,  new Radian());
            }
            catch
            {
                throw;
            }
        }

        public static ValueUnit Ceiling(ValueUnit unit)
        {
            double value = Math.Ceiling(unit.Value);
            return new ValueUnit(value, unit.Units.ToArray());
        }

        public static ValueUnit Cos(ValueUnit unit)
        {
            if(unit.Units.Count() == 0)
                return new ValueUnit(Math.Cos(unit.Value));

            if (unit.Units.Count() != 1 || unit.Units.First().Power != 1)
                throw new FormatException("Could not calculate cos due to format of parameters.");

            if (!(unit.Units.First() is AngleUnit))
                throw new FormatException("Could not calculate cos due to format of parameters.");

            unit.TransformTo<Radian>(unit.Units.First());
            return  new ValueUnit(Math.Cos(unit.Value));
        }

        public static ValueUnit Exp(ValueUnit unit)
        {
            if (unit.Units.Count() > 0)
                unit.OrganizeUnits();
            if (unit.Units.Count() > 0)
                throw new FormatException("Couldn't calculate Exp for unit with parameters");

            double value = Math.Exp(unit.Value);
            return new ValueUnit(value);
        }

        public static ValueUnit Floor(ValueUnit unit)
        {
            double value = Math.Floor(unit.Value);
            return new ValueUnit(value, unit.Units.ToArray());
        }

        public static ValueUnit Sin(ValueUnit unit)
        {
            if (unit.Units.Count() == 0)
                return new ValueUnit(Math.Sin(unit.Value));

            if (unit.Units.Count() != 1 || unit.Units.First().Power != 1)
                throw new FormatException("Could not calculate sin due to format of parameters.");

            if (!(unit.Units.First() is AngleUnit))
                throw new FormatException("Could not calculate sin due to format of parameters.");

            unit.TransformTo<Radian>(unit.Units.First());
            return new ValueUnit(Math.Sin(unit.Value));
        }

        public static ValueUnit Truncate(ValueUnit unit)
        {
            var value = Math.Truncate(unit.Value);
            return new ValueUnit(value, unit.Units.ToArray());
        }

        public static ValueUnit Max(ValueUnit unitA, ValueUnit unitB)
            => unitA > unitB ? unitA : unitB;
        public static ValueUnit Min(ValueUnit unitA, ValueUnit unitB)
            => unitA < unitB ? unitA : unitB;

        public static ValueUnit IEEERemainder(ValueUnit valueUnitA, ValueUnit valueUnitB)
        {
            throw new NotSupportedException();
        }
        public static ValueUnit Log10(ValueUnit valueUnitA)
        {
            throw new NotSupportedException();
        }
        public static ValueUnit Log(ValueUnit valueUnitA, ValueUnit valueUnitB)
        {
            throw new NotSupportedException();
        }

        public static object Sqrt(ValueUnit unit)
        {
            unit.OrganizeUnits();
            var value = Math.Sqrt(unit.Value);
           return new ValueUnit(value,  unit.Units.Select(u => u.Copy(u.Power / 2)).ToArray());
        }

        public static ValueUnit Round(ValueUnit unit, int rounding, EvaluateOptions evaluateOptions)
        {
            MidpointRounding midpointRounding = (evaluateOptions & EvaluateOptions.RoundAwayFromZero) == EvaluateOptions.RoundAwayFromZero ? MidpointRounding.AwayFromZero : MidpointRounding.ToEven;

            return new ValueUnit(Math.Round(unit.Value, rounding, midpointRounding),  unit.Units.ToArray());
        }

        public static int Sign(ValueUnit unit)
        {
            return Math.Sign(unit.Value);
        }
        public static ValueUnit Tan(ValueUnit unit)
        {
            if (unit.Units.Count() == 0)
                return new ValueUnit(Math.Tan(unit.Value));

            if (unit.Units.Count() != 1 || unit.Units.First().Power != 1)
                throw new FormatException("Could not calculate sin due to format of parameters.");

            if (!(unit.Units.First() is AngleUnit))
                throw new FormatException("Could not calculate sin due to format of parameters.");

            unit.TransformTo<Radian>(unit.Units.First());
            return new ValueUnit(Math.Tan(unit.Value));
        }

    }
}
