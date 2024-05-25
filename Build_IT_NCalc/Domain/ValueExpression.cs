using Build_IT_NCalc.Enums;
using Build_IT_NCalc.Units;
using System;
using System.Reflection;

namespace NCalc.Domain
{
	public class ValueExpression : LogicalExpression
    {
        #region Properties
               
        public object Value { get; set; }
        public ValueType Type { get; set; }

        #endregion // Properties

        #region Constructors
        
        public ValueExpression(object value, ValueType type)
        {
            Value = value;
            Type = type;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public ValueExpression(object value)
        {
            switch (value.GetTypeCode())
            {
                case NCalcTypeCode.Boolean:
                    Type = ValueType.Boolean;
                    break;

                case NCalcTypeCode.DateTime:
                    Type = ValueType.DateTime;
                    break;

                case NCalcTypeCode.Decimal:
                case NCalcTypeCode.Double:
                case NCalcTypeCode.Single:
                    Type = ValueType.Float;
                    break;

                case NCalcTypeCode.Byte:
                case NCalcTypeCode.SByte:
                case NCalcTypeCode.Int16:
                case NCalcTypeCode.Int32:
                case NCalcTypeCode.Int64:
                case NCalcTypeCode.UInt16:
                case NCalcTypeCode.UInt32:
                case NCalcTypeCode.UInt64:
                    Type = ValueType.Integer;
                    break;

                case NCalcTypeCode.String:
                    Type = ValueType.String;
                    break;

                case NCalcTypeCode.Unit:
                    Type = ValueType.Unit;
                    break;

                default:
                    throw new EvaluationException("This value could not be handled: " + value);
            }

            Value = value;
        }

        public ValueExpression(string value)
        {
            Value = value;
            Type = ValueType.String;
        }

        public ValueExpression(int value)
        {
            Value = value;
            Type = ValueType.Integer;
        }
        public ValueExpression(long value)
        {
            Value = value;
            Type = ValueType.Integer;
        }

        public ValueExpression(float value)
        {
            Value = value;
            Type = ValueType.Float;
        }

        public ValueExpression(DateTime value)
        {
            Value = value;
            Type = ValueType.DateTime;
        }

        public ValueExpression(bool value)
        {
            Value = value;
            Type = ValueType.Boolean;
        }
        public ValueExpression(ValueUnit value)
        {
            Value = value;
            Type = ValueType.Unit;
        }

        public override void Accept(LogicalExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion // Public_Methods
    }

	public enum ValueType
	{
		Integer,
		String,
		DateTime,
		Float,
		Boolean,
        Unit
    }
}
