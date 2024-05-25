using Build_IT_NCalc.Enums;
using Build_IT_NCalc.Units;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class TypeExtensions
    {
        #region Fields

        private static readonly Dictionary<Type, NCalcTypeCode> TypeCodeMap =
            new Dictionary<Type, NCalcTypeCode>
            {
                  {typeof(bool), NCalcTypeCode.Boolean},
                  {typeof(byte), NCalcTypeCode.Byte},
                  {typeof(sbyte), NCalcTypeCode.SByte},
                  {typeof(char), NCalcTypeCode.Char},
                  {typeof(DateTime), NCalcTypeCode.DateTime},
                  {typeof(decimal), NCalcTypeCode.Decimal},
                  {typeof(double), NCalcTypeCode.Double},
                  {typeof(float), NCalcTypeCode.Single},
                  {typeof(short), NCalcTypeCode.Int16},
                  {typeof(int), NCalcTypeCode.Int32},
                  {typeof(long), NCalcTypeCode.Int64},
                  {typeof(ushort), NCalcTypeCode.UInt16},
                  {typeof(uint), NCalcTypeCode.UInt32},
                  {typeof(ulong), NCalcTypeCode.UInt64},
                  {typeof(string), NCalcTypeCode.String },
                  {typeof(ValueUnit), NCalcTypeCode.Unit },
                  {typeof(void), NCalcTypeCode.Void },
            };

        #endregion // Fields

        #region Public_Methods

        public static NCalcTypeCode GetTypeCode(this object obj)
        {
            if (obj == null)
                return NCalcTypeCode.Empty;

            return obj.GetType().ToTypeCode();
        }
        public static NCalcTypeCode ToTypeCode(this Type type)
        {
            if (type == null)
                return NCalcTypeCode.Empty;

            NCalcTypeCode tc;
            if (!TypeCodeMap.TryGetValue(type, out tc))
            {
                tc = NCalcTypeCode.Object;
            }

            return tc;
        }

        public static Type GetTypeFromCode(this NCalcTypeCode typeCode)
        {
            if (!TypeCodeMap.ContainsValue(typeCode))
                return typeof(object);
            var typeCodePair = TypeCodeMap.First(tc => tc.Value == typeCode);
            return typeCodePair.Key;
        }

        #endregion // Public_Methods
    }
}
