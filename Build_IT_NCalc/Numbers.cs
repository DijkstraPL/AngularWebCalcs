using Build_IT_NCalc.Enums;
using Build_IT_NCalc.Units;
using System;
using System.Globalization;
using System.Linq;

namespace NCalc
{
    public static class Numbers
    {
        private static object ConvertIfString(object s)
        {
            if (s is String || s is char)
                return Decimal.Parse(s.ToString());

            return s;
        }

        private static object ConvertIfBoolean(object input)
        {
            if (input is bool boolean)
                return boolean ? 1 : 0;

            return input;
        }

        public static object Add(object a, object b, EvaluateOptions options)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            if (options.HasFlag(EvaluateOptions.BooleanCalculation))
            {
                a = ConvertIfBoolean(a);
                b = ConvertIfBoolean(b);
            }

            NCalcTypeCode typeCodeA = a.GetTypeCode();
            NCalcTypeCode typeCodeB = b.GetTypeCode();

            if (!options.HasFlag(EvaluateOptions.AllowUnitCalculations))
            {
                SetDoubleFromUnit(ref a, ref typeCodeA);
                SetDoubleFromUnit(ref b, ref typeCodeB);
            }

            switch (typeCodeA)
            {
                case NCalcTypeCode.Boolean:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'bool'");
                        case NCalcTypeCode.Byte: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'byte'");
                        case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'sbyte'");
                        case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'int16'");
                        case NCalcTypeCode.UInt16: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'uint16'");
                        case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'int32'");
                        case NCalcTypeCode.UInt32: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'uint32'");
                        case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'int64'");
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'uint64'");
                        case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'single'");
                        case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'double'");
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'decimal'");
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'unit'");
                    }
                    break;
                case NCalcTypeCode.Byte:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'byte' and 'bool'");
                        case NCalcTypeCode.Byte: return (Byte)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (Byte)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (Byte)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (Byte)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (Byte)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (Byte)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (Byte)a + (Int64)b;
                        case NCalcTypeCode.UInt64: return (Byte)a + (UInt64)b;
                        case NCalcTypeCode.Single: return (Byte)a + (Single)b;
                        case NCalcTypeCode.Double: return (Byte)a + (Double)b;
                        case NCalcTypeCode.Decimal: return (Byte)a + (Decimal)b;
                        case NCalcTypeCode.Unit: return (Byte)a + (ValueUnit)b;
                    }
                    break;
                case NCalcTypeCode.SByte:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'sbyte' and 'bool'");
                        case NCalcTypeCode.Byte: return (SByte)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (SByte)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (SByte)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (SByte)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (SByte)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (SByte)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (SByte)a + (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'sbyte' and 'ulong'");
                        case NCalcTypeCode.Single: return (SByte)a + (Single)b;
                        case NCalcTypeCode.Double: return (SByte)a + (Double)b;
                        case NCalcTypeCode.Decimal: return (SByte)a + (Decimal)b;
                        case NCalcTypeCode.Unit: return (SByte)a + (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int16:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'short' and 'bool'");
                        case NCalcTypeCode.Byte: return (Int16)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (Int16)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (Int16)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int16)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int16)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int16)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int16)a + (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'short' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int16)a + (Single)b;
                        case NCalcTypeCode.Double: return (Int16)a + (Double)b;
                        case NCalcTypeCode.Decimal: return (Int16)a + (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int16)a + (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt16:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ushort' and 'bool'");
                        case NCalcTypeCode.Byte: return (UInt16)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (UInt16)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (UInt16)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (UInt16)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (UInt16)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (UInt16)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (UInt16)a + (Int64)b;
                        case NCalcTypeCode.UInt64: return (UInt16)a + (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt16)a + (Single)b;
                        case NCalcTypeCode.Double: return (UInt16)a + (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt16)a + (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt16)a + (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int32:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'int' and 'bool'");
                        case NCalcTypeCode.Byte: return (Int32)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (Int32)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (Int32)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int32)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int32)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int32)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int32)a + (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'int' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int32)a + (Single)b;
                        case NCalcTypeCode.Double: return (Int32)a + (Double)b;
                        case NCalcTypeCode.Decimal: return (Int32)a + (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int32)a + (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt32:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'unit' and 'bool'");
                        case NCalcTypeCode.Byte: return (UInt32)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (UInt32)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (UInt32)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (UInt32)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (UInt32)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (UInt32)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (UInt32)a + (Int64)b;
                        case NCalcTypeCode.UInt64: return (UInt32)a + (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt32)a + (Single)b;
                        case NCalcTypeCode.Double: return (UInt32)a + (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt32)a + (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt32)a + (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int64:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'long' and 'bool'");
                        case NCalcTypeCode.Byte: return (Int64)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (Int64)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (Int64)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int64)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int64)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int64)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int64)a + (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'long' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int64)a + (Single)b;
                        case NCalcTypeCode.Double: return (Int64)a + (Double)b;
                        case NCalcTypeCode.Decimal: return (Int64)a + (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int64)a + (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt64:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ulong' and 'bool'");
                        case NCalcTypeCode.Byte: return (UInt64)a + (Byte)b;
                        case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ulong' and 'sbyte'");
                        case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ulong' and 'short'");
                        case NCalcTypeCode.UInt16: return (UInt64)a + (UInt16)b;
                        case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ulong' and 'int'");
                        case NCalcTypeCode.UInt32: return (UInt64)a + (UInt32)b;
                        case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ulong' and 'ulong'");
                        case NCalcTypeCode.UInt64: return (UInt64)a + (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt64)a + (Single)b;
                        case NCalcTypeCode.Double: return (UInt64)a + (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt64)a + (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt64)a + (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Single:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'float' and 'bool'");
                        case NCalcTypeCode.Byte: return (Single)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (Single)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (Single)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (Single)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (Single)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (Single)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (Single)a + (Int64)b;
                        case NCalcTypeCode.UInt64: return (Single)a + (UInt64)b;
                        case NCalcTypeCode.Single: return (Single)a + (Single)b;
                        case NCalcTypeCode.Double: return (Single)a + (Double)b;
                        case NCalcTypeCode.Decimal: return Convert.ToDecimal(a) + (Decimal)b;
                        case NCalcTypeCode.Unit: return (Single)a + (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Double:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'double' and 'bool'");
                        case NCalcTypeCode.Byte: return (Double)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (Double)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (Double)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (Double)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (Double)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (Double)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (Double)a + (Int64)b;
                        case NCalcTypeCode.UInt64: return (Double)a + (UInt64)b;
                        case NCalcTypeCode.Single: return (Double)a + (Single)b;
                        case NCalcTypeCode.Double: return (Double)a + (Double)b;
                        case NCalcTypeCode.Decimal: return Convert.ToDecimal(a) + (Decimal)b;
                        case NCalcTypeCode.Unit: return (Double)a + (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Decimal:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'decimal' and 'bool'");
                        case NCalcTypeCode.Byte: return (Decimal)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (Decimal)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (Decimal)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (Decimal)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (Decimal)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (Decimal)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (Decimal)a + (Int64)b;
                        case NCalcTypeCode.UInt64: return (Decimal)a + (UInt64)b;
                        case NCalcTypeCode.Single: return (Decimal)a + Convert.ToDecimal(b);
                        case NCalcTypeCode.Double: return (Decimal)a + Convert.ToDecimal(b);
                        case NCalcTypeCode.Decimal: return (Decimal)a + (Decimal)b;
                        case NCalcTypeCode.Unit: return (Decimal)a + (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Unit:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'unit' and 'bool'");
                        case NCalcTypeCode.Byte: return (ValueUnit)a + (Byte)b;
                        case NCalcTypeCode.SByte: return (ValueUnit)a + (SByte)b;
                        case NCalcTypeCode.Int16: return (ValueUnit)a + (Int16)b;
                        case NCalcTypeCode.UInt16: return (ValueUnit)a + (UInt16)b;
                        case NCalcTypeCode.Int32: return (ValueUnit)a + (Int32)b;
                        case NCalcTypeCode.UInt32: return (ValueUnit)a + (UInt32)b;
                        case NCalcTypeCode.Int64: return (ValueUnit)a + (Int64)b;
                        case NCalcTypeCode.UInt64: return (ValueUnit)a + (UInt64)b;
                        case NCalcTypeCode.Single: return (ValueUnit)a + (Single)b;
                        case NCalcTypeCode.Double: return (ValueUnit)a + (Double)b;
                        case NCalcTypeCode.Decimal: return (ValueUnit)a + (Decimal)b;
                        case NCalcTypeCode.Unit: return (ValueUnit)a + (ValueUnit)b;
                    }
                    break;
            }

            return null;
        }

        public static object AddChecked(object a, object b, EvaluateOptions options)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            if (options.HasFlag(EvaluateOptions.BooleanCalculation))
            {
                a = ConvertIfBoolean(a);
                b = ConvertIfBoolean(b);
            }

            NCalcTypeCode typeCodeA = a.GetTypeCode();
            NCalcTypeCode typeCodeB = b.GetTypeCode();

            if (!options.HasFlag(EvaluateOptions.AllowUnitCalculations))
            {
                SetDoubleFromUnit(ref a, ref typeCodeA);
                SetDoubleFromUnit(ref b, ref typeCodeB);
            }

            checked
            {
                switch (typeCodeA)
                {
                    case NCalcTypeCode.Boolean:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'bool'");
                            case NCalcTypeCode.Byte: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'byte'");
                            case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'sbyte'");
                            case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'int16'");
                            case NCalcTypeCode.UInt16: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'uint16'");
                            case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'int32'");
                            case NCalcTypeCode.UInt32: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'uint32'");
                            case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'int64'");
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'uint64'");
                            case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'single'");
                            case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'double'");
                            case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'decimal'");
                            case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'bool' and 'unit'");
                        }
                        break;
                    case NCalcTypeCode.Byte:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'byte' and 'bool'");
                            case NCalcTypeCode.Byte: return (Byte)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (Byte)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (Byte)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (Byte)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (Byte)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (Byte)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (Byte)a + (Int64)b;
                            case NCalcTypeCode.UInt64: return (Byte)a + (UInt64)b;
                            case NCalcTypeCode.Single: return (Byte)a + (Single)b;
                            case NCalcTypeCode.Double: return (Byte)a + (Double)b;
                            case NCalcTypeCode.Decimal: return (Byte)a + (Decimal)b;
                            case NCalcTypeCode.Unit: return (Byte)a + (ValueUnit)b;
                        }
                        break;
                    case NCalcTypeCode.SByte:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'sbyte' and 'bool'");
                            case NCalcTypeCode.Byte: return (SByte)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (SByte)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (SByte)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (SByte)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (SByte)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (SByte)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (SByte)a + (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'sbyte' and 'ulong'");
                            case NCalcTypeCode.Single: return (SByte)a + (Single)b;
                            case NCalcTypeCode.Double: return (SByte)a + (Double)b;
                            case NCalcTypeCode.Decimal: return (SByte)a + (Decimal)b;
                            case NCalcTypeCode.Unit: return (SByte)a + (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Int16:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'short' and 'bool'");
                            case NCalcTypeCode.Byte: return (Int16)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (Int16)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (Int16)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (Int16)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (Int16)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (Int16)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (Int16)a + (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'short' and 'ulong'");
                            case NCalcTypeCode.Single: return (Int16)a + (Single)b;
                            case NCalcTypeCode.Double: return (Int16)a + (Double)b;
                            case NCalcTypeCode.Decimal: return (Int16)a + (Decimal)b;
                            case NCalcTypeCode.Unit: return (Int16)a + (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.UInt16:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ushort' and 'bool'");
                            case NCalcTypeCode.Byte: return (UInt16)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (UInt16)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (UInt16)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (UInt16)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (UInt16)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (UInt16)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (UInt16)a + (Int64)b;
                            case NCalcTypeCode.UInt64: return (UInt16)a + (UInt64)b;
                            case NCalcTypeCode.Single: return (UInt16)a + (Single)b;
                            case NCalcTypeCode.Double: return (UInt16)a + (Double)b;
                            case NCalcTypeCode.Decimal: return (UInt16)a + (Decimal)b;
                            case NCalcTypeCode.Unit: return (UInt16)a + (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Int32:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'int' and 'bool'");
                            case NCalcTypeCode.Byte: return (Int32)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (Int32)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (Int32)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (Int32)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (Int32)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (Int32)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (Int32)a + (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'int' and 'ulong'");
                            case NCalcTypeCode.Single: return (Int32)a + (Single)b;
                            case NCalcTypeCode.Double: return (Int32)a + (Double)b;
                            case NCalcTypeCode.Decimal: return (Int32)a + (Decimal)b;
                            case NCalcTypeCode.Unit: return (Int32)a + (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.UInt32:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'unit' and 'bool'");
                            case NCalcTypeCode.Byte: return (UInt32)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (UInt32)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (UInt32)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (UInt32)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (UInt32)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (UInt32)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (UInt32)a + (Int64)b;
                            case NCalcTypeCode.UInt64: return (UInt32)a + (UInt64)b;
                            case NCalcTypeCode.Single: return (UInt32)a + (Single)b;
                            case NCalcTypeCode.Double: return (UInt32)a + (Double)b;
                            case NCalcTypeCode.Decimal: return (UInt32)a + (Decimal)b;
                            case NCalcTypeCode.Unit: return (UInt32)a + (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Int64:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'long' and 'bool'");
                            case NCalcTypeCode.Byte: return (Int64)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (Int64)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (Int64)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (Int64)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (Int64)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (Int64)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (Int64)a + (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'long' and 'ulong'");
                            case NCalcTypeCode.Single: return (Int64)a + (Single)b;
                            case NCalcTypeCode.Double: return (Int64)a + (Double)b;
                            case NCalcTypeCode.Decimal: return (Int64)a + (Decimal)b;
                            case NCalcTypeCode.Unit: return (Int64)a + (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.UInt64:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ulong' and 'bool'");
                            case NCalcTypeCode.Byte: return (UInt64)a + (Byte)b;
                            case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ulong' and 'sbyte'");
                            case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ulong' and 'short'");
                            case NCalcTypeCode.UInt16: return (UInt64)a + (UInt16)b;
                            case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ulong' and 'int'");
                            case NCalcTypeCode.UInt32: return (UInt64)a + (UInt32)b;
                            case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'ulong' and 'ulong'");
                            case NCalcTypeCode.UInt64: return (UInt64)a + (UInt64)b;
                            case NCalcTypeCode.Single: return (UInt64)a + (Single)b;
                            case NCalcTypeCode.Double: return (UInt64)a + (Double)b;
                            case NCalcTypeCode.Decimal: return (UInt64)a + (Decimal)b;
                            case NCalcTypeCode.Unit: return (UInt64)a + (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Single:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'float' and 'bool'");
                            case NCalcTypeCode.Byte: return (Single)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (Single)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (Single)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (Single)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (Single)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (Single)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (Single)a + (Int64)b;
                            case NCalcTypeCode.UInt64: return (Single)a + (UInt64)b;
                            case NCalcTypeCode.Single: return (Single)a + (Single)b;
                            case NCalcTypeCode.Double: return (Single)a + (Double)b;
                            case NCalcTypeCode.Decimal: return Convert.ToDecimal(a) + (Decimal)b;
                            case NCalcTypeCode.Unit: return (Single)a + (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Double:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'double' and 'bool'");
                            case NCalcTypeCode.Byte: return (Double)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (Double)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (Double)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (Double)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (Double)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (Double)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (Double)a + (Int64)b;
                            case NCalcTypeCode.UInt64: return (Double)a + (UInt64)b;
                            case NCalcTypeCode.Single: return (Double)a + (Single)b;
                            case NCalcTypeCode.Double: return (Double)a + (Double)b;
                            case NCalcTypeCode.Decimal: return Convert.ToDecimal(a) + (Decimal)b;
                            case NCalcTypeCode.Unit: return (Double)a + (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Decimal:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'decimal' and 'bool'");
                            case NCalcTypeCode.Byte: return (Decimal)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (Decimal)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (Decimal)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (Decimal)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (Decimal)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (Decimal)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (Decimal)a + (Int64)b;
                            case NCalcTypeCode.UInt64: return (Decimal)a + (UInt64)b;
                            case NCalcTypeCode.Single: return (Decimal)a + Convert.ToDecimal(b);
                            case NCalcTypeCode.Double: return (Decimal)a + Convert.ToDecimal(b);
                            case NCalcTypeCode.Decimal: return (Decimal)a + (Decimal)b;
                            case NCalcTypeCode.Unit: return (Decimal)a + (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Unit:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '+' can't be applied to operands of types 'unit' and 'bool'");
                            case NCalcTypeCode.Byte: return (ValueUnit)a + (Byte)b;
                            case NCalcTypeCode.SByte: return (ValueUnit)a + (SByte)b;
                            case NCalcTypeCode.Int16: return (ValueUnit)a + (Int16)b;
                            case NCalcTypeCode.UInt16: return (ValueUnit)a + (UInt16)b;
                            case NCalcTypeCode.Int32: return (ValueUnit)a + (Int32)b;
                            case NCalcTypeCode.UInt32: return (ValueUnit)a + (UInt32)b;
                            case NCalcTypeCode.Int64: return (ValueUnit)a + (Int64)b;
                            case NCalcTypeCode.UInt64: return (ValueUnit)a + (UInt64)b;
                            case NCalcTypeCode.Single: return (ValueUnit)a + (Single)b;
                            case NCalcTypeCode.Double: return (ValueUnit)a + (Double)b;
                            case NCalcTypeCode.Decimal: return (ValueUnit)a + (Decimal)b;
                            case NCalcTypeCode.Unit: return (ValueUnit)a + (ValueUnit)b;
                        }
                        break;
                }

                return null;
            }
        }

        public static object Soustract(object a, object b, EvaluateOptions options)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            if (options.HasFlag(EvaluateOptions.BooleanCalculation))
            {
                a = ConvertIfBoolean(a);
                b = ConvertIfBoolean(b);
            }

            NCalcTypeCode typeCodeA = a.GetTypeCode();
            NCalcTypeCode typeCodeB = b.GetTypeCode();

            if (!options.HasFlag(EvaluateOptions.AllowUnitCalculations))
            {
                SetDoubleFromUnit(ref a, ref typeCodeA);
                SetDoubleFromUnit(ref b, ref typeCodeB);
            }

            switch (typeCodeA)
            {
                case NCalcTypeCode.Boolean:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'bool'");
                        case NCalcTypeCode.Byte: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'byte'");
                        case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'sbyte'");
                        case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'int16'");
                        case NCalcTypeCode.UInt16: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'uint16'");
                        case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'int32'");
                        case NCalcTypeCode.UInt32: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'uint32'");
                        case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'int64'");
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'uint64'");
                        case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'single'");
                        case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'double'");
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'decimal'");
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'unit'");
                    }
                    break;
                case NCalcTypeCode.Byte:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'byte' and 'bool'");
                        case NCalcTypeCode.SByte: return (Byte)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (Byte)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (Byte)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (Byte)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (Byte)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (Byte)a - (Int64)b;
                        case NCalcTypeCode.UInt64: return (Byte)a - (UInt64)b;
                        case NCalcTypeCode.Single: return (Byte)a - (Single)b;
                        case NCalcTypeCode.Double: return (Byte)a - (Double)b;
                        case NCalcTypeCode.Decimal: return (Byte)a - (Decimal)b;
                        case NCalcTypeCode.Unit: return (Byte)a - (ValueUnit)b;
                    }
                    break;
                case NCalcTypeCode.SByte:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'sbyte' and 'bool'");
                        case NCalcTypeCode.SByte: return (SByte)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (SByte)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (SByte)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (SByte)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (SByte)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (SByte)a - (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'sbyte' and 'ulong'");
                        case NCalcTypeCode.Single: return (SByte)a - (Single)b;
                        case NCalcTypeCode.Double: return (SByte)a - (Double)b;
                        case NCalcTypeCode.Decimal: return (SByte)a - (Decimal)b;
                        case NCalcTypeCode.Unit: return (SByte)a - (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int16:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'short' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int16)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (Int16)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int16)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int16)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int16)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int16)a - (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'short' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int16)a - (Single)b;
                        case NCalcTypeCode.Double: return (Int16)a - (Double)b;
                        case NCalcTypeCode.Decimal: return (Int16)a - (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int16)a - (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt16:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ushort' and 'bool'");
                        case NCalcTypeCode.SByte: return (UInt16)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (UInt16)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (UInt16)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (UInt16)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (UInt16)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (UInt16)a - (Int64)b;
                        case NCalcTypeCode.UInt64: return (UInt16)a - (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt16)a - (Single)b;
                        case NCalcTypeCode.Double: return (UInt16)a - (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt16)a - (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt16)a - (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int32:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'int' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int32)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (Int32)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int32)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int32)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int32)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int32)a - (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'int' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int32)a - (Single)b;
                        case NCalcTypeCode.Double: return (Int32)a - (Double)b;
                        case NCalcTypeCode.Decimal: return (Int32)a - (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int32)a - (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt32:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'uint' and 'bool'");
                        case NCalcTypeCode.SByte: return (UInt32)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (UInt32)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (UInt32)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (UInt32)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (UInt32)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (UInt32)a - (Int64)b;
                        case NCalcTypeCode.UInt64: return (UInt32)a - (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt32)a - (Single)b;
                        case NCalcTypeCode.Double: return (UInt32)a - (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt32)a - (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt32)a - (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int64:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'long' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int64)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (Int64)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int64)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int64)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int64)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int64)a - (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'long' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int64)a - (Single)b;
                        case NCalcTypeCode.Double: return (Int64)a - (Double)b;
                        case NCalcTypeCode.Decimal: return (Int64)a - (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int64)a - (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt64:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'bool'");
                        case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'double'");
                        case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'short'");
                        case NCalcTypeCode.UInt16: return (UInt64)a - (UInt16)b;
                        case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'int'");
                        case NCalcTypeCode.UInt32: return (UInt64)a - (UInt32)b;
                        case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'long'");
                        case NCalcTypeCode.UInt64: return (UInt64)a - (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt64)a - (Single)b;
                        case NCalcTypeCode.Double: return (UInt64)a - (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt64)a - (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt64)a - (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Single:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'float' and 'bool'");
                        case NCalcTypeCode.SByte: return (Single)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (Single)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (Single)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (Single)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (Single)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (Single)a - (Int64)b;
                        case NCalcTypeCode.UInt64: return (Single)a - (UInt64)b;
                        case NCalcTypeCode.Single: return (Single)a - (Single)b;
                        case NCalcTypeCode.Double: return (Single)a - (Double)b;
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'float' and 'decimal'");
                        case NCalcTypeCode.Unit: return (Single)a - (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Double:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'double' and 'bool'");
                        case NCalcTypeCode.SByte: return (Double)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (Double)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (Double)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (Double)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (Double)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (Double)a - (Int64)b;
                        case NCalcTypeCode.UInt64: return (Double)a - (UInt64)b;
                        case NCalcTypeCode.Single: return (Double)a - (Single)b;
                        case NCalcTypeCode.Double: return (Double)a - (Double)b;
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'double' and 'decimal'");
                        case NCalcTypeCode.Unit: return (Double)a - (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Decimal:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'decimal' and 'bool'");
                        case NCalcTypeCode.SByte: return (Decimal)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (Decimal)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (Decimal)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (Decimal)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (Decimal)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (Decimal)a - (Int64)b;
                        case NCalcTypeCode.UInt64: return (Decimal)a - (UInt64)b;
                        case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'decimal' and 'float'");
                        case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'decimal' and 'double'");
                        case NCalcTypeCode.Decimal: return (Decimal)a - (Decimal)b;
                        case NCalcTypeCode.Unit: return (Decimal)a - (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Unit:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'unit' and 'bool'");
                        case NCalcTypeCode.Byte: return (ValueUnit)a - (Byte)b;
                        case NCalcTypeCode.SByte: return (ValueUnit)a - (SByte)b;
                        case NCalcTypeCode.Int16: return (ValueUnit)a - (Int16)b;
                        case NCalcTypeCode.UInt16: return (ValueUnit)a - (UInt16)b;
                        case NCalcTypeCode.Int32: return (ValueUnit)a - (Int32)b;
                        case NCalcTypeCode.UInt32: return (ValueUnit)a - (UInt32)b;
                        case NCalcTypeCode.Int64: return (ValueUnit)a - (Int64)b;
                        case NCalcTypeCode.UInt64: return (ValueUnit)a - (UInt64)b;
                        case NCalcTypeCode.Single: return (ValueUnit)a - (Single)b;
                        case NCalcTypeCode.Double: return (ValueUnit)a - (Double)b;
                        case NCalcTypeCode.Decimal: return (ValueUnit)a - (Decimal)b;
                        case NCalcTypeCode.Unit: return (ValueUnit)a - (ValueUnit)b;
                    }
                    break;
            }

            return null;
        }

        public static object SoustractChecked(object a, object b, EvaluateOptions options)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            if (options.HasFlag(EvaluateOptions.BooleanCalculation))
            {
                a = ConvertIfBoolean(a);
                b = ConvertIfBoolean(b);
            }

            NCalcTypeCode typeCodeA = a.GetTypeCode();
            NCalcTypeCode typeCodeB = b.GetTypeCode();

            if (!options.HasFlag(EvaluateOptions.AllowUnitCalculations))
            {
                SetDoubleFromUnit(ref a, ref typeCodeA);
                SetDoubleFromUnit(ref b, ref typeCodeB);
            }

            checked
            {
                switch (typeCodeA)
                {
                    case NCalcTypeCode.Boolean:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'bool'");
                            case NCalcTypeCode.Byte: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'byte'");
                            case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'sbyte'");
                            case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'int16'");
                            case NCalcTypeCode.UInt16: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'uint16'");
                            case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'int32'");
                            case NCalcTypeCode.UInt32: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'uint32'");
                            case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'int64'");
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'uint64'");
                            case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'single'");
                            case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'double'");
                            case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'decimal'");
                            case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'bool' and 'unit'");
                        }
                        break;
                    case NCalcTypeCode.Byte:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'byte' and 'bool'");
                            case NCalcTypeCode.SByte: return (Byte)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (Byte)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (Byte)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (Byte)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (Byte)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (Byte)a - (Int64)b;
                            case NCalcTypeCode.UInt64: return (Byte)a - (UInt64)b;
                            case NCalcTypeCode.Single: return (Byte)a - (Single)b;
                            case NCalcTypeCode.Double: return (Byte)a - (Double)b;
                            case NCalcTypeCode.Decimal: return (Byte)a - (Decimal)b;
                            case NCalcTypeCode.Unit: return (Byte)a - (ValueUnit)b;
                        }
                        break;
                    case NCalcTypeCode.SByte:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'sbyte' and 'bool'");
                            case NCalcTypeCode.SByte: return (SByte)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (SByte)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (SByte)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (SByte)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (SByte)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (SByte)a - (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'sbyte' and 'ulong'");
                            case NCalcTypeCode.Single: return (SByte)a - (Single)b;
                            case NCalcTypeCode.Double: return (SByte)a - (Double)b;
                            case NCalcTypeCode.Decimal: return (SByte)a - (Decimal)b;
                            case NCalcTypeCode.Unit: return (SByte)a - (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Int16:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'short' and 'bool'");
                            case NCalcTypeCode.SByte: return (Int16)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (Int16)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (Int16)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (Int16)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (Int16)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (Int16)a - (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'short' and 'ulong'");
                            case NCalcTypeCode.Single: return (Int16)a - (Single)b;
                            case NCalcTypeCode.Double: return (Int16)a - (Double)b;
                            case NCalcTypeCode.Decimal: return (Int16)a - (Decimal)b;
                            case NCalcTypeCode.Unit: return (Int16)a - (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.UInt16:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ushort' and 'bool'");
                            case NCalcTypeCode.SByte: return (UInt16)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (UInt16)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (UInt16)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (UInt16)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (UInt16)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (UInt16)a - (Int64)b;
                            case NCalcTypeCode.UInt64: return (UInt16)a - (UInt64)b;
                            case NCalcTypeCode.Single: return (UInt16)a - (Single)b;
                            case NCalcTypeCode.Double: return (UInt16)a - (Double)b;
                            case NCalcTypeCode.Decimal: return (UInt16)a - (Decimal)b;
                            case NCalcTypeCode.Unit: return (UInt16)a - (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Int32:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'int' and 'bool'");
                            case NCalcTypeCode.SByte: return (Int32)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (Int32)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (Int32)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (Int32)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (Int32)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (Int32)a - (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'int' and 'ulong'");
                            case NCalcTypeCode.Single: return (Int32)a - (Single)b;
                            case NCalcTypeCode.Double: return (Int32)a - (Double)b;
                            case NCalcTypeCode.Decimal: return (Int32)a - (Decimal)b;
                            case NCalcTypeCode.Unit: return (Int32)a - (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.UInt32:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'uint' and 'bool'");
                            case NCalcTypeCode.SByte: return (UInt32)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (UInt32)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (UInt32)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (UInt32)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (UInt32)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (UInt32)a - (Int64)b;
                            case NCalcTypeCode.UInt64: return (UInt32)a - (UInt64)b;
                            case NCalcTypeCode.Single: return (UInt32)a - (Single)b;
                            case NCalcTypeCode.Double: return (UInt32)a - (Double)b;
                            case NCalcTypeCode.Decimal: return (UInt32)a - (Decimal)b;
                            case NCalcTypeCode.Unit: return (UInt32)a - (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Int64:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'long' and 'bool'");
                            case NCalcTypeCode.SByte: return (Int64)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (Int64)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (Int64)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (Int64)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (Int64)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (Int64)a - (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'long' and 'ulong'");
                            case NCalcTypeCode.Single: return (Int64)a - (Single)b;
                            case NCalcTypeCode.Double: return (Int64)a - (Double)b;
                            case NCalcTypeCode.Decimal: return (Int64)a - (Decimal)b;
                            case NCalcTypeCode.Unit: return (Int64)a - (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.UInt64:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'bool'");
                            case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'double'");
                            case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'short'");
                            case NCalcTypeCode.UInt16: return (UInt64)a - (UInt16)b;
                            case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'int'");
                            case NCalcTypeCode.UInt32: return (UInt64)a - (UInt32)b;
                            case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'long'");
                            case NCalcTypeCode.UInt64: return (UInt64)a - (UInt64)b;
                            case NCalcTypeCode.Single: return (UInt64)a - (Single)b;
                            case NCalcTypeCode.Double: return (UInt64)a - (Double)b;
                            case NCalcTypeCode.Decimal: return (UInt64)a - (Decimal)b;
                            case NCalcTypeCode.Unit: return (UInt64)a - (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Single:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'float' and 'bool'");
                            case NCalcTypeCode.SByte: return (Single)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (Single)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (Single)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (Single)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (Single)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (Single)a - (Int64)b;
                            case NCalcTypeCode.UInt64: return (Single)a - (UInt64)b;
                            case NCalcTypeCode.Single: return (Single)a - (Single)b;
                            case NCalcTypeCode.Double: return (Single)a - (Double)b;
                            case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'float' and 'decimal'");
                            case NCalcTypeCode.Unit: return (Single)a - (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Double:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'double' and 'bool'");
                            case NCalcTypeCode.SByte: return (Double)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (Double)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (Double)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (Double)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (Double)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (Double)a - (Int64)b;
                            case NCalcTypeCode.UInt64: return (Double)a - (UInt64)b;
                            case NCalcTypeCode.Single: return (Double)a - (Single)b;
                            case NCalcTypeCode.Double: return (Double)a - (Double)b;
                            case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'double' and 'decimal'");
                            case NCalcTypeCode.Unit: return (Double)a - (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Decimal:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'decimal' and 'bool'");
                            case NCalcTypeCode.SByte: return (Decimal)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (Decimal)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (Decimal)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (Decimal)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (Decimal)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (Decimal)a - (Int64)b;
                            case NCalcTypeCode.UInt64: return (Decimal)a - (UInt64)b;
                            case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'decimal' and 'float'");
                            case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'decimal' and 'double'");
                            case NCalcTypeCode.Decimal: return (Decimal)a - (Decimal)b;
                            case NCalcTypeCode.Unit: return (Decimal)a - (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Unit:
                        switch (typeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'unit' and 'bool'");
                            case NCalcTypeCode.Byte: return (ValueUnit)a - (Byte)b;
                            case NCalcTypeCode.SByte: return (ValueUnit)a - (SByte)b;
                            case NCalcTypeCode.Int16: return (ValueUnit)a - (Int16)b;
                            case NCalcTypeCode.UInt16: return (ValueUnit)a - (UInt16)b;
                            case NCalcTypeCode.Int32: return (ValueUnit)a - (Int32)b;
                            case NCalcTypeCode.UInt32: return (ValueUnit)a - (UInt32)b;
                            case NCalcTypeCode.Int64: return (ValueUnit)a - (Int64)b;
                            case NCalcTypeCode.UInt64: return (ValueUnit)a - (UInt64)b;
                            case NCalcTypeCode.Single: return (ValueUnit)a - (Single)b;
                            case NCalcTypeCode.Double: return (ValueUnit)a - (Double)b;
                            case NCalcTypeCode.Decimal: return (ValueUnit)a - (Decimal)b;
                            case NCalcTypeCode.Unit: return (ValueUnit)a - (ValueUnit)b;
                        }
                        break;
                }
            }
            return null;
        }
        public static object Multiply(object a, object b, EvaluateOptions options)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);


            if (options.HasFlag(EvaluateOptions.BooleanCalculation))
            {
                a = ConvertIfBoolean(a);
                b = ConvertIfBoolean(b);
            }

            NCalcTypeCode typeCodeA = a.GetTypeCode();
            NCalcTypeCode typeCodeB = b.GetTypeCode();

            if (!options.HasFlag(EvaluateOptions.AllowUnitCalculations))
            {
                SetDoubleFromUnit(ref a, ref typeCodeA);
                SetDoubleFromUnit(ref b, ref typeCodeB);
            }

            switch (typeCodeA)
            {
                case NCalcTypeCode.Byte:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'byte' and 'bool'");
                        case NCalcTypeCode.SByte: return (Byte)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (Byte)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (Byte)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (Byte)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (Byte)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (Byte)a * (Int64)b;
                        case NCalcTypeCode.UInt64: return (Byte)a * (UInt64)b;
                        case NCalcTypeCode.Single: return (Byte)a * (Single)b;
                        case NCalcTypeCode.Double: return (Byte)a * (Double)b;
                        case NCalcTypeCode.Decimal: return (Byte)a * (Decimal)b;
                        case NCalcTypeCode.Unit: return (Byte)a * (ValueUnit)b;
                    }
                    break;
                case NCalcTypeCode.SByte:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'sbyte' and 'bool'");
                        case NCalcTypeCode.SByte: return (SByte)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (SByte)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (SByte)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (SByte)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (SByte)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (SByte)a * (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'sbyte' and 'ulong'");
                        case NCalcTypeCode.Single: return (SByte)a * (Single)b;
                        case NCalcTypeCode.Double: return (SByte)a * (Double)b;
                        case NCalcTypeCode.Decimal: return (SByte)a * (Decimal)b;
                        case NCalcTypeCode.Unit: return (SByte)a * (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int16:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'short' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int16)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (Int16)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int16)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int16)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int16)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int16)a * (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'short' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int16)a * (Single)b;
                        case NCalcTypeCode.Double: return (Int16)a * (Double)b;
                        case NCalcTypeCode.Decimal: return (Int16)a * (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int16)a * (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt16:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ushort' and 'bool'");
                        case NCalcTypeCode.SByte: return (UInt16)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (UInt16)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (UInt16)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (UInt16)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (UInt16)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (UInt16)a * (Int64)b;
                        case NCalcTypeCode.UInt64: return (UInt16)a * (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt16)a * (Single)b;
                        case NCalcTypeCode.Double: return (UInt16)a * (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt16)a * (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt16)a * (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int32:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'int' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int32)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (Int32)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int32)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int32)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int32)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int32)a * (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'int' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int32)a * (Single)b;
                        case NCalcTypeCode.Double: return (Int32)a * (Double)b;
                        case NCalcTypeCode.Decimal: return (Int32)a * (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int32)a * (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt32:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'uint' and 'bool'");
                        case NCalcTypeCode.SByte: return (UInt32)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (UInt32)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (UInt32)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (UInt32)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (UInt32)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (UInt32)a * (Int64)b;
                        case NCalcTypeCode.UInt64: return (UInt32)a * (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt32)a * (Single)b;
                        case NCalcTypeCode.Double: return (UInt32)a * (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt32)a * (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt32)a * (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int64:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'long' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int64)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (Int64)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int64)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int64)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int64)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int64)a * (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'long' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int64)a * (Single)b;
                        case NCalcTypeCode.Double: return (Int64)a * (Double)b;
                        case NCalcTypeCode.Decimal: return (Int64)a * (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int64)a * (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt64:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'bool'");
                        case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'sbyte'");
                        case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'short'");
                        case NCalcTypeCode.UInt16: return (UInt64)a * (UInt16)b;
                        case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'int'");
                        case NCalcTypeCode.UInt32: return (UInt64)a * (UInt32)b;
                        case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'long'");
                        case NCalcTypeCode.UInt64: return (UInt64)a * (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt64)a * (Single)b;
                        case NCalcTypeCode.Double: return (UInt64)a * (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt64)a * (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt64)a * (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Single:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'float' and 'bool'");
                        case NCalcTypeCode.SByte: return (Single)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (Single)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (Single)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (Single)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (Single)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (Single)a * (Int64)b;
                        case NCalcTypeCode.UInt64: return (Single)a * (UInt64)b;
                        case NCalcTypeCode.Single: return (Single)a * (Single)b;
                        case NCalcTypeCode.Double: return (Single)a * (Double)b;
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'float' and 'decimal'");
                        case NCalcTypeCode.Unit: return (Single)a * (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Double:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'double' and 'bool'");
                        case NCalcTypeCode.SByte: return (Double)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (Double)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (Double)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (Double)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (Double)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (Double)a * (Int64)b;
                        case NCalcTypeCode.UInt64: return (Double)a * (UInt64)b;
                        case NCalcTypeCode.Single: return (Double)a * (Single)b;
                        case NCalcTypeCode.Double: return (Double)a * (Double)b;
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'double' and 'decimal'");
                        case NCalcTypeCode.Unit: return (Double)a * (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Decimal:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'decimal' and 'bool'");
                        case NCalcTypeCode.SByte: return (Decimal)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (Decimal)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (Decimal)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (Decimal)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (Decimal)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (Decimal)a * (Int64)b;
                        case NCalcTypeCode.UInt64: return (Decimal)a * (UInt64)b;
                        case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'decimal' and 'float'");
                        case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'decimal' and 'double'");
                        case NCalcTypeCode.Decimal: return (Decimal)a * (Decimal)b;
                        case NCalcTypeCode.Unit: return (Decimal)a * (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Unit:
                    switch (typeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'unit' and 'bool'");
                        case NCalcTypeCode.Byte: return (ValueUnit)a * (Byte)b;
                        case NCalcTypeCode.SByte: return (ValueUnit)a * (SByte)b;
                        case NCalcTypeCode.Int16: return (ValueUnit)a * (Int16)b;
                        case NCalcTypeCode.UInt16: return (ValueUnit)a * (UInt16)b;
                        case NCalcTypeCode.Int32: return (ValueUnit)a * (Int32)b;
                        case NCalcTypeCode.UInt32: return (ValueUnit)a * (UInt32)b;
                        case NCalcTypeCode.Int64: return (ValueUnit)a * (Int64)b;
                        case NCalcTypeCode.UInt64: return (ValueUnit)a * (UInt64)b;
                        case NCalcTypeCode.Single: return (ValueUnit)a * (Single)b;
                        case NCalcTypeCode.Double: return (ValueUnit)a * (Double)b;
                        case NCalcTypeCode.Decimal: return (ValueUnit)a * (Decimal)b;
                        case NCalcTypeCode.Unit: return (ValueUnit)a * (ValueUnit)b;
                    }
                    break;
            }

            return null;
        }
        public static object MultiplyChecked(object a, object b, EvaluateOptions options)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            if (options.HasFlag(EvaluateOptions.BooleanCalculation))
            {
                a = ConvertIfBoolean(a);
                b = ConvertIfBoolean(b);
            }

            NCalcTypeCode NCalcTypeCodeA = a.GetTypeCode();
            NCalcTypeCode NCalcTypeCodeB = b.GetTypeCode();

            if (!options.HasFlag(EvaluateOptions.AllowUnitCalculations))
            {
                SetDoubleFromUnit(ref a, ref NCalcTypeCodeA);
                SetDoubleFromUnit(ref b, ref NCalcTypeCodeB);
            }

            checked
            {
                switch (NCalcTypeCodeA)
                {
                    case NCalcTypeCode.Byte:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'byte' and 'bool'");
                            case NCalcTypeCode.SByte: return (Byte)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (Byte)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (Byte)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (Byte)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (Byte)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (Byte)a * (Int64)b;
                            case NCalcTypeCode.UInt64: return (Byte)a * (UInt64)b;
                            case NCalcTypeCode.Single: return (Byte)a * (Single)b;
                            case NCalcTypeCode.Double: return (Byte)a * (Double)b;
                            case NCalcTypeCode.Decimal: return (Byte)a * (Decimal)b;
                            case NCalcTypeCode.Unit: return (Byte)a * (ValueUnit)b;
                        }
                        break;
                    case NCalcTypeCode.SByte:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'sbyte' and 'bool'");
                            case NCalcTypeCode.SByte: return (SByte)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (SByte)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (SByte)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (SByte)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (SByte)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (SByte)a * (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'sbyte' and 'ulong'");
                            case NCalcTypeCode.Single: return (SByte)a * (Single)b;
                            case NCalcTypeCode.Double: return (SByte)a * (Double)b;
                            case NCalcTypeCode.Decimal: return (SByte)a * (Decimal)b;
                            case NCalcTypeCode.Unit: return (SByte)a * (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Int16:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'short' and 'bool'");
                            case NCalcTypeCode.SByte: return (Int16)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (Int16)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (Int16)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (Int16)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (Int16)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (Int16)a * (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'short' and 'ulong'");
                            case NCalcTypeCode.Single: return (Int16)a * (Single)b;
                            case NCalcTypeCode.Double: return (Int16)a * (Double)b;
                            case NCalcTypeCode.Decimal: return (Int16)a * (Decimal)b;
                            case NCalcTypeCode.Unit: return (Int16)a * (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.UInt16:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ushort' and 'bool'");
                            case NCalcTypeCode.SByte: return (UInt16)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (UInt16)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (UInt16)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (UInt16)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (UInt16)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (UInt16)a * (Int64)b;
                            case NCalcTypeCode.UInt64: return (UInt16)a * (UInt64)b;
                            case NCalcTypeCode.Single: return (UInt16)a * (Single)b;
                            case NCalcTypeCode.Double: return (UInt16)a * (Double)b;
                            case NCalcTypeCode.Decimal: return (UInt16)a * (Decimal)b;
                            case NCalcTypeCode.Unit: return (UInt16)a * (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Int32:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'int' and 'bool'");
                            case NCalcTypeCode.SByte: return (Int32)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (Int32)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (Int32)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (Int32)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (Int32)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (Int32)a * (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'int' and 'ulong'");
                            case NCalcTypeCode.Single: return (Int32)a * (Single)b;
                            case NCalcTypeCode.Double: return (Int32)a * (Double)b;
                            case NCalcTypeCode.Decimal: return (Int32)a * (Decimal)b;
                            case NCalcTypeCode.Unit: return (Int32)a * (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.UInt32:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'uint' and 'bool'");
                            case NCalcTypeCode.SByte: return (UInt32)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (UInt32)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (UInt32)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (UInt32)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (UInt32)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (UInt32)a * (Int64)b;
                            case NCalcTypeCode.UInt64: return (UInt32)a * (UInt64)b;
                            case NCalcTypeCode.Single: return (UInt32)a * (Single)b;
                            case NCalcTypeCode.Double: return (UInt32)a * (Double)b;
                            case NCalcTypeCode.Decimal: return (UInt32)a * (Decimal)b;
                            case NCalcTypeCode.Unit: return (UInt32)a * (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Int64:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'long' and 'bool'");
                            case NCalcTypeCode.SByte: return (Int64)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (Int64)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (Int64)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (Int64)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (Int64)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (Int64)a * (Int64)b;
                            case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'long' and 'ulong'");
                            case NCalcTypeCode.Single: return (Int64)a * (Single)b;
                            case NCalcTypeCode.Double: return (Int64)a * (Double)b;
                            case NCalcTypeCode.Decimal: return (Int64)a * (Decimal)b;
                            case NCalcTypeCode.Unit: return (Int64)a * (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.UInt64:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'bool'");
                            case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'sbyte'");
                            case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'short'");
                            case NCalcTypeCode.UInt16: return (UInt64)a * (UInt16)b;
                            case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'int'");
                            case NCalcTypeCode.UInt32: return (UInt64)a * (UInt32)b;
                            case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'ulong' and 'long'");
                            case NCalcTypeCode.UInt64: return (UInt64)a * (UInt64)b;
                            case NCalcTypeCode.Single: return (UInt64)a * (Single)b;
                            case NCalcTypeCode.Double: return (UInt64)a * (Double)b;
                            case NCalcTypeCode.Decimal: return (UInt64)a * (Decimal)b;
                            case NCalcTypeCode.Unit: return (UInt64)a * (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Single:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'float' and 'bool'");
                            case NCalcTypeCode.SByte: return (Single)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (Single)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (Single)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (Single)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (Single)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (Single)a * (Int64)b;
                            case NCalcTypeCode.UInt64: return (Single)a * (UInt64)b;
                            case NCalcTypeCode.Single: return (Single)a * (Single)b;
                            case NCalcTypeCode.Double: return (Single)a * (Double)b;
                            case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'float' and 'decimal'");
                            case NCalcTypeCode.Unit: return (Single)a * (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Double:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'double' and 'bool'");
                            case NCalcTypeCode.SByte: return (Double)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (Double)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (Double)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (Double)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (Double)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (Double)a * (Int64)b;
                            case NCalcTypeCode.UInt64: return (Double)a * (UInt64)b;
                            case NCalcTypeCode.Single: return (Double)a * (Single)b;
                            case NCalcTypeCode.Double: return (Double)a * (Double)b;
                            case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'double' and 'decimal'");
                            case NCalcTypeCode.Unit: return (Double)a * (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Decimal:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'decimal' and 'bool'");
                            case NCalcTypeCode.SByte: return (Decimal)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (Decimal)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (Decimal)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (Decimal)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (Decimal)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (Decimal)a * (Int64)b;
                            case NCalcTypeCode.UInt64: return (Decimal)a * (UInt64)b;
                            case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'decimal' and 'float'");
                            case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'decimal' and 'double'");
                            case NCalcTypeCode.Decimal: return (Decimal)a * (Decimal)b;
                            case NCalcTypeCode.Unit: return (Decimal)a * (ValueUnit)b;
                        }
                        break;

                    case NCalcTypeCode.Unit:
                        switch (NCalcTypeCodeB)
                        {
                            case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '*' can't be applied to operands of types 'unit' and 'bool'");
                            case NCalcTypeCode.Byte: return (ValueUnit)a * (Byte)b;
                            case NCalcTypeCode.SByte: return (ValueUnit)a * (SByte)b;
                            case NCalcTypeCode.Int16: return (ValueUnit)a * (Int16)b;
                            case NCalcTypeCode.UInt16: return (ValueUnit)a * (UInt16)b;
                            case NCalcTypeCode.Int32: return (ValueUnit)a * (Int32)b;
                            case NCalcTypeCode.UInt32: return (ValueUnit)a * (UInt32)b;
                            case NCalcTypeCode.Int64: return (ValueUnit)a * (Int64)b;
                            case NCalcTypeCode.UInt64: return (ValueUnit)a * (UInt64)b;
                            case NCalcTypeCode.Single: return (ValueUnit)a * (Single)b;
                            case NCalcTypeCode.Double: return (ValueUnit)a * (Double)b;
                            case NCalcTypeCode.Decimal: return (ValueUnit)a * (Decimal)b;
                            case NCalcTypeCode.Unit: return (ValueUnit)a * (ValueUnit)b;
                        }
                        break;
                }
            }
            return null;
        }
        public static object Divide(object a, object b, EvaluateOptions options)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            if (options.HasFlag(EvaluateOptions.BooleanCalculation))
            {
                a = ConvertIfBoolean(a);
                b = ConvertIfBoolean(b);
            }

            NCalcTypeCode NCalcTypeCodeA = a.GetTypeCode();
            NCalcTypeCode NCalcTypeCodeB = b.GetTypeCode();

            if (!options.HasFlag(EvaluateOptions.AllowUnitCalculations))
            {
                SetDoubleFromUnit(ref a, ref NCalcTypeCodeA);
                SetDoubleFromUnit(ref b, ref NCalcTypeCodeB);
            }

            switch (NCalcTypeCodeA)
            {
                case NCalcTypeCode.Byte:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'byte' and 'bool'");
                        case NCalcTypeCode.SByte: return (Byte)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (Byte)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (Byte)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (Byte)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (Byte)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (Byte)a / (Int64)b;
                        case NCalcTypeCode.UInt64: return (Byte)a / (UInt64)b;
                        case NCalcTypeCode.Single: return (Byte)a / (Single)b;
                        case NCalcTypeCode.Double: return (Byte)a / (Double)b;
                        case NCalcTypeCode.Decimal: return (Byte)a / (Decimal)b;
                        case NCalcTypeCode.Unit: return (Byte)a / (ValueUnit)b;
                    }
                    break;
                case NCalcTypeCode.SByte:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'sbyte' and 'bool'");
                        case NCalcTypeCode.SByte: return (SByte)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (SByte)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (SByte)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (SByte)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (SByte)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (SByte)a / (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'sbyte' and 'ulong'");
                        case NCalcTypeCode.Single: return (SByte)a / (Single)b;
                        case NCalcTypeCode.Double: return (SByte)a / (Double)b;
                        case NCalcTypeCode.Decimal: return (SByte)a / (Decimal)b;
                        case NCalcTypeCode.Unit: return (SByte)a / (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int16:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'short' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int16)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (Int16)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int16)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int16)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int16)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int16)a / (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'short' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int16)a / (Single)b;
                        case NCalcTypeCode.Double: return (Int16)a / (Double)b;
                        case NCalcTypeCode.Decimal: return (Int16)a / (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int16)a / (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt16:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ushort' and 'bool'");
                        case NCalcTypeCode.SByte: return (UInt16)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (UInt16)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (UInt16)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (UInt16)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (UInt16)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (UInt16)a / (Int64)b;
                        case NCalcTypeCode.UInt64: return (UInt16)a / (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt16)a / (Single)b;
                        case NCalcTypeCode.Double: return (UInt16)a / (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt16)a / (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt16)a / (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int32:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'int' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int32)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (Int32)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int32)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int32)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int32)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int32)a / (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'int' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int32)a / (Single)b;
                        case NCalcTypeCode.Double: return (Int32)a / (Double)b;
                        case NCalcTypeCode.Decimal: return (Int32)a / (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int32)a / (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt32:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'uint' and 'bool'");
                        case NCalcTypeCode.SByte: return (UInt32)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (UInt32)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (UInt32)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (UInt32)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (UInt32)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (UInt32)a / (Int64)b;
                        case NCalcTypeCode.UInt64: return (UInt32)a / (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt32)a / (Single)b;
                        case NCalcTypeCode.Double: return (UInt32)a / (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt32)a / (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt32)a / (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Int64:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'long' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int64)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (Int64)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int64)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int64)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int64)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int64)a / (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'long' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int64)a / (Single)b;
                        case NCalcTypeCode.Double: return (Int64)a / (Double)b;
                        case NCalcTypeCode.Decimal: return (Int64)a / (Decimal)b;
                        case NCalcTypeCode.Unit: return (Int64)a / (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.UInt64:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '-' can't be applied to operands of types 'ulong' and 'bool'");
                        case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ulong' and 'sbyte'");
                        case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ulong' and 'short'");
                        case NCalcTypeCode.UInt16: return (UInt64)a / (UInt16)b;
                        case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ulong' and 'int'");
                        case NCalcTypeCode.UInt32: return (UInt64)a / (UInt32)b;
                        case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'ulong' and 'long'");
                        case NCalcTypeCode.UInt64: return (UInt64)a / (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt64)a / (Single)b;
                        case NCalcTypeCode.Double: return (UInt64)a / (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt64)a / (Decimal)b;
                        case NCalcTypeCode.Unit: return (UInt64)a / (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Single:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'float' and 'bool'");
                        case NCalcTypeCode.SByte: return (Single)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (Single)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (Single)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (Single)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (Single)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (Single)a / (Int64)b;
                        case NCalcTypeCode.UInt64: return (Single)a / (UInt64)b;
                        case NCalcTypeCode.Single: return (Single)a / (Single)b;
                        case NCalcTypeCode.Double: return (Single)a / (Double)b;
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'float' and 'decimal'");
                        case NCalcTypeCode.Unit: return (Single)a / (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Double:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'double' and 'bool'");
                        case NCalcTypeCode.SByte: return (Double)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (Double)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (Double)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (Double)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (Double)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (Double)a / (Int64)b;
                        case NCalcTypeCode.UInt64: return (Double)a / (UInt64)b;
                        case NCalcTypeCode.Single: return (Double)a / (Single)b;
                        case NCalcTypeCode.Double: return (Double)a / (Double)b;
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'double' and 'decimal'");
                        case NCalcTypeCode.Unit: return (Double)a / (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Decimal:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'decimal' and 'bool'");
                        case NCalcTypeCode.SByte: return (Decimal)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (Decimal)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (Decimal)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (Decimal)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (Decimal)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (Decimal)a / (Int64)b;
                        case NCalcTypeCode.UInt64: return (Decimal)a / (UInt64)b;
                        case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'decimal' and 'float'");
                        case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'decimal' and 'double'");
                        case NCalcTypeCode.Decimal: return (Decimal)a / (Decimal)b;
                        case NCalcTypeCode.Unit: return (Decimal)a / (ValueUnit)b;
                    }
                    break;

                case NCalcTypeCode.Unit:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '/' can't be applied to operands of types 'unit' and 'bool'");
                        case NCalcTypeCode.Byte: return (ValueUnit)a / (Byte)b;
                        case NCalcTypeCode.SByte: return (ValueUnit)a / (SByte)b;
                        case NCalcTypeCode.Int16: return (ValueUnit)a / (Int16)b;
                        case NCalcTypeCode.UInt16: return (ValueUnit)a / (UInt16)b;
                        case NCalcTypeCode.Int32: return (ValueUnit)a / (Int32)b;
                        case NCalcTypeCode.UInt32: return (ValueUnit)a / (UInt32)b;
                        case NCalcTypeCode.Int64: return (ValueUnit)a / (Int64)b;
                        case NCalcTypeCode.UInt64: return (ValueUnit)a / (UInt64)b;
                        case NCalcTypeCode.Single: return (ValueUnit)a / (Single)b;
                        case NCalcTypeCode.Double: return (ValueUnit)a / (Double)b;
                        case NCalcTypeCode.Decimal: return (ValueUnit)a / (Decimal)b;
                        case NCalcTypeCode.Unit: return (ValueUnit)a / (ValueUnit)b;
                    }
                    break;
            }

            return null;
        }

        public static object Modulo(object a, object b, EvaluateOptions options)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            NCalcTypeCode NCalcTypeCodeA = a.GetTypeCode();
            NCalcTypeCode NCalcTypeCodeB = b.GetTypeCode();

            if (!options.HasFlag(EvaluateOptions.AllowUnitCalculations))
            {
                SetDoubleFromUnit(ref a, ref NCalcTypeCodeA);
                SetDoubleFromUnit(ref b, ref NCalcTypeCodeB);
            }

            switch (NCalcTypeCodeA)
            {
                case NCalcTypeCode.Byte:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'byte' and 'bool'");
                        case NCalcTypeCode.SByte: return (Byte)a % (SByte)b;
                        case NCalcTypeCode.Int16: return (Byte)a % (Int16)b;
                        case NCalcTypeCode.UInt16: return (Byte)a % (UInt16)b;
                        case NCalcTypeCode.Int32: return (Byte)a % (Int32)b;
                        case NCalcTypeCode.UInt32: return (Byte)a % (UInt32)b;
                        case NCalcTypeCode.Int64: return (Byte)a % (Int64)b;
                        case NCalcTypeCode.UInt64: return (Byte)a % (UInt64)b;
                        case NCalcTypeCode.Single: return (Byte)a % (Single)b;
                        case NCalcTypeCode.Double: return (Byte)a % (Double)b;
                        case NCalcTypeCode.Decimal: return (Byte)a % (Decimal)b;
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'byte' and 'unit'");
                    }
                    break;
                case NCalcTypeCode.SByte:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'sbyte' and 'bool'");
                        case NCalcTypeCode.SByte: return (SByte)a % (SByte)b;
                        case NCalcTypeCode.Int16: return (SByte)a % (Int16)b;
                        case NCalcTypeCode.UInt16: return (SByte)a % (UInt16)b;
                        case NCalcTypeCode.Int32: return (SByte)a % (Int32)b;
                        case NCalcTypeCode.UInt32: return (SByte)a % (UInt32)b;
                        case NCalcTypeCode.Int64: return (SByte)a % (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'sbyte' and 'ulong'");
                        case NCalcTypeCode.Single: return (SByte)a % (Single)b;
                        case NCalcTypeCode.Double: return (SByte)a % (Double)b;
                        case NCalcTypeCode.Decimal: return (SByte)a % (Decimal)b;
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'sbyte' and 'unit'");
                    }
                    break;

                case NCalcTypeCode.Int16:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'short' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int16)a % (SByte)b;
                        case NCalcTypeCode.Int16: return (Int16)a % (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int16)a % (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int16)a % (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int16)a % (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int16)a % (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'short' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int16)a % (Single)b;
                        case NCalcTypeCode.Double: return (Int16)a % (Double)b;
                        case NCalcTypeCode.Decimal: return (Int16)a % (Decimal)b;
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'int16' and 'unit'");
                    }
                    break;

                case NCalcTypeCode.UInt16:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ushort' and 'bool'");
                        case NCalcTypeCode.SByte: return (UInt16)a % (SByte)b;
                        case NCalcTypeCode.Int16: return (UInt16)a % (Int16)b;
                        case NCalcTypeCode.UInt16: return (UInt16)a % (UInt16)b;
                        case NCalcTypeCode.Int32: return (UInt16)a % (Int32)b;
                        case NCalcTypeCode.UInt32: return (UInt16)a % (UInt32)b;
                        case NCalcTypeCode.Int64: return (UInt16)a % (Int64)b;
                        case NCalcTypeCode.UInt64: return (UInt16)a % (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt16)a % (Single)b;
                        case NCalcTypeCode.Double: return (UInt16)a % (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt16)a % (Decimal)b;
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'uint16' and 'unit'");
                    }
                    break;

                case NCalcTypeCode.Int32:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'int' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int32)a % (SByte)b;
                        case NCalcTypeCode.Int16: return (Int32)a % (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int32)a % (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int32)a % (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int32)a % (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int32)a % (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'int' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int32)a % (Single)b;
                        case NCalcTypeCode.Double: return (Int32)a % (Double)b;
                        case NCalcTypeCode.Decimal: return (Int32)a % (Decimal)b;
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'int32' and 'unit'");
                    }
                    break;

                case NCalcTypeCode.UInt32:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'uint' and 'bool'");
                        case NCalcTypeCode.SByte: return (UInt32)a % (SByte)b;
                        case NCalcTypeCode.Int16: return (UInt32)a % (Int16)b;
                        case NCalcTypeCode.UInt16: return (UInt32)a % (UInt16)b;
                        case NCalcTypeCode.Int32: return (UInt32)a % (Int32)b;
                        case NCalcTypeCode.UInt32: return (UInt32)a % (UInt32)b;
                        case NCalcTypeCode.Int64: return (UInt32)a % (Int64)b;
                        case NCalcTypeCode.UInt64: return (UInt32)a % (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt32)a % (Single)b;
                        case NCalcTypeCode.Double: return (UInt32)a % (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt32)a % (Decimal)b;
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'uint32' and 'unit'");
                    }
                    break;

                case NCalcTypeCode.Int64:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'long' and 'bool'");
                        case NCalcTypeCode.SByte: return (Int64)a % (SByte)b;
                        case NCalcTypeCode.Int16: return (Int64)a % (Int16)b;
                        case NCalcTypeCode.UInt16: return (Int64)a % (UInt16)b;
                        case NCalcTypeCode.Int32: return (Int64)a % (Int32)b;
                        case NCalcTypeCode.UInt32: return (Int64)a % (UInt32)b;
                        case NCalcTypeCode.Int64: return (Int64)a % (Int64)b;
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'long' and 'ulong'");
                        case NCalcTypeCode.Single: return (Int64)a % (Single)b;
                        case NCalcTypeCode.Double: return (Int64)a % (Double)b;
                        case NCalcTypeCode.Decimal: return (Int64)a % (Decimal)b;
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'int64' and 'unit'");
                    }
                    break;

                case NCalcTypeCode.UInt64:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ulong' and 'bool'");
                        case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ulong' and 'sbyte'");
                        case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ulong' and 'short'");
                        case NCalcTypeCode.UInt16: return (UInt64)a % (UInt16)b;
                        case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ulong' and 'int'");
                        case NCalcTypeCode.UInt32: return (UInt64)a % (UInt32)b;
                        case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'ulong' and 'long'");
                        case NCalcTypeCode.UInt64: return (UInt64)a % (UInt64)b;
                        case NCalcTypeCode.Single: return (UInt64)a % (Single)b;
                        case NCalcTypeCode.Double: return (UInt64)a % (Double)b;
                        case NCalcTypeCode.Decimal: return (UInt64)a % (Decimal)b;
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'uint64' and 'unit'");
                    }
                    break;

                case NCalcTypeCode.Single:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'float' and 'bool'");
                        case NCalcTypeCode.SByte: return (Single)a % (SByte)b;
                        case NCalcTypeCode.Int16: return (Single)a % (Int16)b;
                        case NCalcTypeCode.UInt16: return (Single)a % (UInt16)b;
                        case NCalcTypeCode.Int32: return (Single)a % (Int32)b;
                        case NCalcTypeCode.UInt32: return (Single)a % (UInt32)b;
                        case NCalcTypeCode.Int64: return (Single)a % (Int64)b;
                        case NCalcTypeCode.UInt64: return (Single)a % (UInt64)b;
                        case NCalcTypeCode.Single: return (Single)a % (Single)b;
                        case NCalcTypeCode.Double: return (Single)a % (Double)b;
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'float' and 'decimal'");
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'float' and 'unit'");
                    }
                    break;

                case NCalcTypeCode.Double:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'double' and 'bool'");
                        case NCalcTypeCode.SByte: return (Double)a % (SByte)b;
                        case NCalcTypeCode.Int16: return (Double)a % (Int16)b;
                        case NCalcTypeCode.UInt16: return (Double)a % (UInt16)b;
                        case NCalcTypeCode.Int32: return (Double)a % (Int32)b;
                        case NCalcTypeCode.UInt32: return (Double)a % (UInt32)b;
                        case NCalcTypeCode.Int64: return (Double)a % (Int64)b;
                        case NCalcTypeCode.UInt64: return (Double)a % (UInt64)b;
                        case NCalcTypeCode.Single: return (Double)a % (Single)b;
                        case NCalcTypeCode.Double: return (Double)a % (Double)b;
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'double' and 'decimal'");
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'double' and 'unit'");
                    }
                    break;

                case NCalcTypeCode.Decimal:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'decimal' and 'bool'");
                        case NCalcTypeCode.SByte: return (Decimal)a % (SByte)b;
                        case NCalcTypeCode.Int16: return (Decimal)a % (Int16)b;
                        case NCalcTypeCode.UInt16: return (Decimal)a % (UInt16)b;
                        case NCalcTypeCode.Int32: return (Decimal)a % (Int32)b;
                        case NCalcTypeCode.UInt32: return (Decimal)a % (UInt32)b;
                        case NCalcTypeCode.Int64: return (Decimal)a % (Int64)b;
                        case NCalcTypeCode.UInt64: return (Decimal)a % (UInt64)b;
                        case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'decimal' and 'float'");
                        case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'decimal' and 'decimal'");
                        case NCalcTypeCode.Decimal: return (Decimal)a % (Decimal)b;
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'decimal' and 'unit'");
                    }
                    break;

                case NCalcTypeCode.Unit:
                    switch (NCalcTypeCodeB)
                    {
                        case NCalcTypeCode.Boolean: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'bool'");
                        case NCalcTypeCode.Byte: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'byte'");
                        case NCalcTypeCode.SByte: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'sbyte'");
                        case NCalcTypeCode.Int16: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'int16'");
                        case NCalcTypeCode.UInt16: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'uint16'");
                        case NCalcTypeCode.Int32: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'int32'");
                        case NCalcTypeCode.UInt32: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'uint32'");
                        case NCalcTypeCode.Int64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'int64'");
                        case NCalcTypeCode.UInt64: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'uint64'");
                        case NCalcTypeCode.Single: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'single'");
                        case NCalcTypeCode.Double: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'double'");
                        case NCalcTypeCode.Decimal: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'decimal'");
                        case NCalcTypeCode.Unit: throw new InvalidOperationException("Operator '%' can't be applied to operands of types 'unit' and 'unit'");
                    }
                    break;
            }

            return null;
        }
        public static object Max(object a, object b, EvaluateOptions options)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            if (a == null && b == null)
            {
                return null;
            }

            if (a == null)
            {
                return b;
            }

            if (b == null)
            {
                return a;
            }

            NCalcTypeCode NCalcTypeCode = ConvertToHighestPrecision(ref a, ref b, options);

            if (NCalcTypeCode == NCalcTypeCode.Unit && !options.HasFlag(EvaluateOptions.AllowUnitCalculations))
                return Math.Max(Convert.ToDouble(a, new UnitFormatProvider(options)), Convert.ToDouble(b, new UnitFormatProvider(options)));

            switch (NCalcTypeCode)
            {
                case NCalcTypeCode.Byte:
                    return Math.Max((Byte)a, (Byte)b);
                case NCalcTypeCode.SByte:
                    return Math.Max((SByte)a, (SByte)b);
                case NCalcTypeCode.Int16:
                    return Math.Max((Int16)a, (Int16)b);
                case NCalcTypeCode.UInt16:
                    return Math.Max((UInt16)a, (UInt16)b);
                case NCalcTypeCode.Int32:
                    return Math.Max((Int32)a, (Int32)b);
                case NCalcTypeCode.UInt32:
                    return Math.Max((UInt32)a, (UInt32)b);
                case NCalcTypeCode.Int64:
                    return Math.Max((Int64)a, (Int64)b);
                case NCalcTypeCode.UInt64:
                    return Math.Max((UInt64)a, (UInt64)b);
                case NCalcTypeCode.Single:
                    return Math.Max((Single)a, (Single)b);
                case NCalcTypeCode.Double:
                    return Math.Max((Double)a, (Double)b);
                case NCalcTypeCode.Decimal:
                    return Math.Max((Decimal)a, (Decimal)b);
                case NCalcTypeCode.Unit:
                    return UnitMath.Max((ValueUnit)a, (ValueUnit)b );
            }

            return null;
        }
        public static object Min(object a, object b, EvaluateOptions options)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            if (a == null && b == null)
            {
                return null;
            }

            if (a == null)
            {
                return b;
            }

            if (b == null)
            {
                return a;
            }

            NCalcTypeCode NCalcTypeCode = ConvertToHighestPrecision(ref a, ref b, options);

            if (NCalcTypeCode == NCalcTypeCode.Unit && !options.HasFlag(EvaluateOptions.AllowUnitCalculations))
                return Math.Min(Convert.ToDouble(a, new UnitFormatProvider(options)), Convert.ToDouble(b, new UnitFormatProvider(options)));

            switch (NCalcTypeCode)
            {
                case NCalcTypeCode.Byte:
                    return Math.Min((Byte)a, (Byte)b);
                case NCalcTypeCode.SByte:
                    return Math.Min((SByte)a, (SByte)b);
                case NCalcTypeCode.Int16:
                    return Math.Min((Int16)a, (Int16)b);
                case NCalcTypeCode.UInt16:
                    return Math.Min((UInt16)a, (UInt16)b);
                case NCalcTypeCode.Int32:
                    return Math.Min((Int32)a, (Int32)b);
                case NCalcTypeCode.UInt32:
                    return Math.Min((UInt32)a, (UInt32)b);
                case NCalcTypeCode.Int64:
                    return Math.Min((Int64)a, (Int64)b);
                case NCalcTypeCode.UInt64:
                    return Math.Min((UInt64)a, (UInt64)b);
                case NCalcTypeCode.Single:
                    return Math.Min((Single)a, (Single)b);
                case NCalcTypeCode.Double:
                    return Math.Min((Double)a, (Double)b);
                case NCalcTypeCode.Decimal:
                    return Math.Min((Decimal)a, (Decimal)b);
                case NCalcTypeCode.Unit:
                    return UnitMath.Min((ValueUnit)a, (ValueUnit)b);
            }

            return null;
        }

        public static object LeftShift(object a, object b)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            NCalcTypeCode typeCodeA = a.GetTypeCode();

            var bits = Convert.ToByte(b);

            switch (typeCodeA)
            {
                case NCalcTypeCode.Boolean:
                    throw new InvalidOperationException("Operator '<<' can't be applied to operands of types 'decimal' and 'bool'");
                case NCalcTypeCode.Byte:
                    return (byte)a << bits;
                case NCalcTypeCode.SByte:
                    return (sbyte)a << bits;
                case NCalcTypeCode.Int16:
                    return (short)a << bits;
                case NCalcTypeCode.UInt16:
                    return (ushort)a << bits;
                case NCalcTypeCode.Int32:
                    return (int)a << bits;
                case NCalcTypeCode.UInt32:
                    return (uint)a << bits;
                case NCalcTypeCode.Int64:
                    return (long)a << bits;
                case NCalcTypeCode.UInt64:
                    return (ulong)a << bits;
                case NCalcTypeCode.Single:
                    return Convert.ToInt32(a) << bits;
                case NCalcTypeCode.Double:
                    return Convert.ToInt64(a) << bits;
                case NCalcTypeCode.Decimal:
                    return Convert.ToInt64(a) << bits;
                case NCalcTypeCode.Unit:
                    throw new InvalidOperationException(
                        string.Format(
                            "Operation LeftShift can't be applied to operands of types '{0}'",
                            typeCodeA));
            }

            return null;
        }

        public static object RightShift(object a, object b)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            NCalcTypeCode typeCodeA = a.GetTypeCode();

            var bits = Convert.ToByte(b);

            switch (typeCodeA)
            {
                case NCalcTypeCode.Boolean:
                    throw new InvalidOperationException("Operator '>>' can't be applied to operands of types 'decimal' and 'bool'");
                case NCalcTypeCode.Byte:
                    return (byte)a >> bits;
                case NCalcTypeCode.SByte:
                    return (sbyte)a >> bits;
                case NCalcTypeCode.Int16:
                    return (short)a >> bits;
                case NCalcTypeCode.UInt16:
                    return (ushort)a >> bits;
                case NCalcTypeCode.Int32:
                    return (int)a >> bits;
                case NCalcTypeCode.UInt32:
                    return (uint)a >> bits;
                case NCalcTypeCode.Int64:
                    return (long)a >> bits;
                case NCalcTypeCode.UInt64:
                    return (ulong)a >> bits;
                case NCalcTypeCode.Single:
                    return Convert.ToInt64(a) >> bits;
                case NCalcTypeCode.Double:
                    return Convert.ToInt64(a) >> bits;
                case NCalcTypeCode.Decimal:
                    return Convert.ToInt64(a) >> bits;
                case NCalcTypeCode.Unit:
                    throw new InvalidOperationException(
                        string.Format(
                            "Operation RightShift can't be applied to operands of types '{0}'",
                            typeCodeA));
            }

            return null;
        }

        public static object BitwiseAnd(object a, object b)
        {
            return ApplyBitwiseOperator(a, b, "&");
        }

        public static object BitwiseOr(object a, object b)
        {
            return ApplyBitwiseOperator(a, b, "|");
        }

        public static object BitwiseXor(object a, object b)
        {
            return ApplyBitwiseOperator(a, b, "^");
        }

        private static object ApplyBitwiseOperator(object a, object b, string @operator)
        {
            a = ConvertIfString(a);
            b = ConvertIfString(b);

            NCalcTypeCode typeCodeA = a.GetTypeCode();
            NCalcTypeCode typeCodeB = b.GetTypeCode();

            // Are both values numerical?
            if (typeCodeA < NCalcTypeCode.SByte || typeCodeA > NCalcTypeCode.Double || typeCodeB < NCalcTypeCode.SByte
                || typeCodeB > NCalcTypeCode.Double)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Operator '{0}' can't be applied to operands of types '{1}' and '{2}'",
                        @operator,
                        typeCodeA,
                        typeCodeB));
            }

            // Determine the result type, float and double are converted to long
            var resultType = typeCodeA > NCalcTypeCode.UInt64 || typeCodeB > NCalcTypeCode.UInt64
                ? TypeCode.Int64
                : (TypeCode)Math.Max((int)typeCodeA, (int)typeCodeB);

            if (resultType == TypeCode.UInt64)
            {
                var aul = (ulong)Convert.ChangeType(a, TypeCode.UInt64, CultureInfo.CurrentCulture);
                var bul = (ulong)Convert.ChangeType(b, TypeCode.UInt64, CultureInfo.CurrentCulture);

                switch (@operator)
                {
                    case "&": return aul & bul;
                    case "|": return aul | bul;
                    case "^": return aul ^ bul;
                    default:
                        throw new InvalidOperationException("Unknown operator");
                }
            }

            // Convert temporarily to long since it is large enough for the other types
            var al = (long)Convert.ChangeType(a, TypeCode.Int64, CultureInfo.CurrentCulture);
            var bl = (long)Convert.ChangeType(b, TypeCode.Int64, CultureInfo.CurrentCulture);
            long result;
            switch (@operator)
            {
                case "&": result = al & bl; break;
                case "|": result = al | bl; break;
                case "^": result = al ^ bl; break;
                default:
                    throw new InvalidOperationException("Unknown operator");
            }

            // Convert to the smallest type required
            return Convert.ChangeType(result, resultType, CultureInfo.CurrentCulture);
        }

        private static NCalcTypeCode ConvertToHighestPrecision(ref object a, ref object b, EvaluateOptions options)
        {
            NCalcTypeCode NCalcTypeCodeA = a.GetTypeCode();
            NCalcTypeCode NCalcTypeCodeB = b.GetTypeCode();

            if (NCalcTypeCodeA == NCalcTypeCodeB)
                return NCalcTypeCodeA;

            if (options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                (NCalcTypeCodeA == NCalcTypeCode.Unit ^ NCalcTypeCodeB == NCalcTypeCode.Unit))
            {
                if (NCalcTypeCodeA == NCalcTypeCode.Unit && a is ValueUnit valueUnitA)
                {
                    if (ConvertUnitToDoubleIfPossible(ref a, valueUnitA))
                        NCalcTypeCodeA = NCalcTypeCode.Double;
                }
                if (NCalcTypeCodeB == NCalcTypeCode.Unit && b is ValueUnit valueUnitB)
                {
                    if (ConvertUnitToDoubleIfPossible(ref b, valueUnitB))
                        NCalcTypeCodeB = NCalcTypeCode.Double;
                }

                if (NCalcTypeCodeA == NCalcTypeCodeB)
                    return NCalcTypeCodeA;
            }

            if (!(NCalcTypeCodeBitSize(NCalcTypeCodeA, out bool floatingPointA) is int bitSizeA))
                return NCalcTypeCode.Empty;
            if (!(NCalcTypeCodeBitSize(NCalcTypeCodeB, out bool floatingPointB) is int bitSizeB))
                return NCalcTypeCode.Empty;

            if (floatingPointA != floatingPointB)
            {
                if (floatingPointA)
                {
                    b = ConvertTo(b, NCalcTypeCodeA, options);

                    return NCalcTypeCodeA;
                }
                else
                {
                    a = ConvertTo(a, NCalcTypeCodeB, options);

                    return NCalcTypeCodeB;
                }
            }

            if (bitSizeA > bitSizeB)
            {
                b = ConvertTo(b, NCalcTypeCodeA, options);

                return NCalcTypeCodeA;
            }
            else
            {
                a = ConvertTo(a, NCalcTypeCodeB, options);

                return NCalcTypeCodeB;
            }

        }

        private static bool ConvertUnitToDoubleIfPossible(ref object origin, ValueUnit valueUnit)
        {
            if (valueUnit.Units.Count() == 0)
            {
                origin = valueUnit.Value;
                return true;
            }

            valueUnit.OrganizeUnits();
            if (valueUnit.Units.Count() == 0)
            {
                origin = valueUnit.Value;
                return true;
            }
            return false;
        }

        private static int? NCalcTypeCodeBitSize(NCalcTypeCode NCalcTypeCode, out bool floatingPoint)
        {
            floatingPoint = false;
            switch (NCalcTypeCode)
            {
                case NCalcTypeCode.SByte: return 8;
                case NCalcTypeCode.Byte: return 8;
                case NCalcTypeCode.Int16: return 16;
                case NCalcTypeCode.UInt16: return 16;
                case NCalcTypeCode.Int32: return 32;
                case NCalcTypeCode.UInt32: return 32;
                case NCalcTypeCode.Int64: return 64;
                case NCalcTypeCode.UInt64: return 64;
                case NCalcTypeCode.Single:
                    floatingPoint = true;
                    return 32;
                case NCalcTypeCode.Double:
                    floatingPoint = true;
                    return 64;
                case NCalcTypeCode.Decimal:
                    floatingPoint = true;
                    return 128;
                default: return null;
            }
        }

        private static object ConvertTo(object value, NCalcTypeCode toType, EvaluateOptions options)
        {
            switch (toType)
            {
                case NCalcTypeCode.Byte:
                    return Convert.ToByte(value);

                case NCalcTypeCode.SByte:
                    return Convert.ToSByte(value);

                case NCalcTypeCode.Int16:
                    return Convert.ToInt16(value);

                case NCalcTypeCode.UInt16:
                    return Convert.ToUInt16(value);

                case NCalcTypeCode.Int32:
                    return Convert.ToInt32(value);

                case NCalcTypeCode.UInt32:
                    return Convert.ToUInt32(value);

                case NCalcTypeCode.Int64:
                    return Convert.ToInt64(value);

                case NCalcTypeCode.UInt64:
                    return Convert.ToUInt64(value);

                case NCalcTypeCode.Single:
                    return Convert.ToSingle(value);

                case NCalcTypeCode.Double:
                    return Convert.ToDouble(value);

                case NCalcTypeCode.Decimal:
                    return Convert.ToDecimal(value);

                case NCalcTypeCode.Unit:
                    return new ValueUnit(Convert.ToDouble(value, new UnitFormatProvider(options)));
            }

            return null;
        }

        private static void SetDoubleFromUnit(ref object obj, ref NCalcTypeCode typeCode)
        {
            if (typeCode == NCalcTypeCode.Unit)
            {
                typeCode = NCalcTypeCode.Double;
                obj = (obj as ValueUnit)?.Value ?? 0.0;
            }
        }
    }
}
