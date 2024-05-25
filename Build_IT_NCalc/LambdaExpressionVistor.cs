using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Build_IT_NCalc.Enums;
using Build_IT_NCalc.Units;
using NCalc.Domain;
using L = System.Linq.Expressions;

namespace NCalc
{
    /// <summary>
    ///     Implements creating <see cref="L.Expression"/> (expression trees) for formula.
    /// </summary>
    internal class LambdaExpressionVisitor : LogicalExpressionVisitor
    {
        private static readonly Type MathType = typeof(Math);
        private static readonly Type UnitMathType = typeof(UnitMath);
        private static readonly Type StringType = typeof(string);
        private static readonly Type DoubleType = typeof(double);
        private static readonly Type BooleanType = typeof(bool);

        private static readonly Type[] NumericTypePrecedence = {
            typeof(decimal),
            DoubleType,
            typeof(float),
            typeof(ulong),
            typeof(long),
            typeof(uint),
            typeof(int),
            typeof(ushort),
            typeof(short),
            typeof(byte),
            typeof(sbyte),
            typeof(object)
        };

        private readonly string[] _parameterNames;
        private readonly L.ParameterExpression _parametersContext;
        private readonly L.ParameterExpression _dynamicContext;
        private readonly EvaluateOptions _options;
        private readonly MethodInfo _convertMethod;
        private readonly Dictionary<string, MethodInfo> _parameterConvertMethods = new Dictionary<string, MethodInfo>();

        private bool IgnoreCase { get { return (_options & EvaluateOptions.IgnoreCase) == EvaluateOptions.IgnoreCase; } }
        private bool Ordinal { get { return (_options & EvaluateOptions.MatchStringsOrdinal) == EvaluateOptions.MatchStringsOrdinal; } }
        private bool IgnoreCaseString { get { return (_options & EvaluateOptions.MatchStringsWithIgnoreCase) == EvaluateOptions.MatchStringsWithIgnoreCase; } }
        private bool Checked { get { return (_options & EvaluateOptions.OverflowProtection) == EvaluateOptions.OverflowProtection; } }

        public LambdaExpressionVisitor(string[] parameterNames, object[] parameterValues, Type targetType, EvaluateOptions options)
        {
            _parameterNames = parameterNames;
            _parametersContext = L.Expression.Parameter(parameterValues.GetType(), "p");
            _options = options;
            if (!parameterNames.Any())
                return;

            foreach ((string name, object value) parameter in parameterNames.Zip(parameterValues))
            {
                if (parameter.value is not null)
                {
                    var parameterType = parameter.value.GetType();
                    var convertMethod = GetConvertMethod(parameterType);
                    _parameterConvertMethods.Add(parameter.name, convertMethod);
                }
            }

            _convertMethod = GetConvertMethod(targetType);

            if (_convertMethod == null)
                throw new InvalidOperationException($"Unable to identify the required conversion method for base type {targetType.FullName}.");
        }

        private MethodInfo? GetConvertMethod(Type targetType)
        {
            switch (targetType.Name)
            {
                case "Boolean":
                case "Byte":
                case "SByte":
                case "Int16":
                case "Int32":
                case "Int64":
                    return typeof(Convert).GetRuntimeMethod(nameof(Convert.ToInt64), new[] { typeof(object) });
                case "UInt16":
                case "UInt32":
                case "UInt64":
                    return typeof(Convert).GetRuntimeMethod(nameof(Convert.ToUInt64), new[] { typeof(object) });
                case "Single":
                case "Double":
                case "Float":
                    return typeof(Convert).GetRuntimeMethod(nameof(Convert.ToDouble), new[] { typeof(object) });
                case "Decimal":
                    return typeof(Convert).GetRuntimeMethod(nameof(Convert.ToDecimal), new[] { typeof(object) });
                case "ValueUnit":
                    return typeof(ConvertExtensions).GetRuntimeMethod(nameof(ConvertExtensions.ToValueUnit), new[] { typeof(object) });
                default:
                    return typeof(ConvertExtensions).GetRuntimeMethod(nameof(ConvertExtensions.ToObject), new[] { typeof(object) });
            }
        }

        public LambdaExpressionVisitor(Type type, EvaluateOptions options)
        {
            _dynamicContext = L.Expression.Parameter(type, "ctx");
            _options = options;
        }

        /// <summary>
        ///     Occurs when a parameter needs to be resolved.
        /// </summary>
        /// <remarks>
        ///     This event handler can be used to extend the usable parameters in the formula.
        /// </remarks>
        public event EventHandler<ParameterExpressionEventArgs> EvaluateParameter;

        /// <summary>
        ///     Occurs when a function needs to be resolved.
        /// </summary>
        /// <remarks>
        ///     This event handler can be used to extend the usable functions in the formula.
        /// </remarks>
        public event EventHandler<FunctionExpressionEventArgs> EvaluateFunction;

        public L.Expression Result { get; private set; }

        public L.ParameterExpression[] Context => new[] { _parametersContext ?? _dynamicContext };

        public override void Visit(LogicalExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Visit(TernaryExpression expression)
        {
            expression.LeftExpression.Accept(this);
            var test = Result;

            expression.MiddleExpression.Accept(this);
            var ifTrue = Result;

            expression.RightExpression.Accept(this);
            var ifFalse = Result;

            Result = L.Expression.Condition(test, ifTrue, ifFalse);
        }

        public override void Visit(BinaryExpression expression)
        {
            expression.LeftExpression.Accept(this);
            var left = Result;

            expression.RightExpression.Accept(this);
            var right = Result;

            switch (expression.Type)
            {
                case BinaryExpressionType.And:
                    Result = L.Expression.AndAlso(EnsureType(left, BooleanType), EnsureType(right, BooleanType));
                    break;
                case BinaryExpressionType.Or:
                    Result = L.Expression.OrElse(EnsureType(left, BooleanType), EnsureType(right, BooleanType));
                    break;
                case BinaryExpressionType.NotEqual:
                    Result = WithCommonNumericType(left, right, L.Expression.NotEqual, useFloating: true,
                        expressiontype: expression.Type);
                    break;
                case BinaryExpressionType.LesserOrEqual:
                    Result = WithCommonNumericType(left, right, L.Expression.LessThanOrEqual, useFloating: true,
                        expressiontype: expression.Type);
                    break;
                case BinaryExpressionType.GreaterOrEqual:
                    Result = WithCommonNumericType(left, right, L.Expression.GreaterThanOrEqual, useFloating: true,
                        expressiontype: expression.Type);
                    break;
                case BinaryExpressionType.Lesser:
                    Result = WithCommonNumericType(left, right, L.Expression.LessThan, useFloating: true,
                        expressiontype: expression.Type);
                    break;
                case BinaryExpressionType.Greater:
                    Result = WithCommonNumericType(left, right, L.Expression.GreaterThan, useFloating: true,
                        expressiontype: expression.Type);
                    break;
                case BinaryExpressionType.Equal:
                    Result = WithCommonNumericType(left, right, L.Expression.Equal, useFloating: true,
                        expressiontype: expression.Type);
                    break;
                case BinaryExpressionType.Minus:
                    if (Checked) Result = WithCommonNumericType(left, right, L.Expression.SubtractChecked, useFloating: true);
                    else Result = WithCommonNumericType(left, right, L.Expression.Subtract, useFloating: true);
                    break;
                case BinaryExpressionType.Plus:
                    if (Checked) Result = WithCommonNumericType(left, right, L.Expression.AddChecked, useFloating: true);
                    else Result = WithCommonNumericType(left, right, L.Expression.Add, useFloating: true);
                    break;
                case BinaryExpressionType.Modulo:
                    Result = WithCommonNumericType(left, right, L.Expression.Modulo, useFloating: true);
                    break;
                case BinaryExpressionType.Div:
                    Result = WithCommonNumericType(left, right, L.Expression.Divide, useFloating: true);
                    break;
                case BinaryExpressionType.Times:
                    if (Checked) Result = WithCommonNumericType(left, right, L.Expression.MultiplyChecked, useFloating: true);
                    else Result = WithCommonNumericType(left, right, L.Expression.Multiply, useFloating: true);
                    break;
                case BinaryExpressionType.BitwiseOr:
                    Result = WithCommonNumericType(left, right, L.Expression.Or, useFloating: false);
                    break;
                case BinaryExpressionType.BitwiseAnd:
                    Result = WithCommonNumericType(left, right, L.Expression.And, useFloating: false);
                    break;
                case BinaryExpressionType.BitwiseXOr:
                    Result = L.Expression.ExclusiveOr(left, right);
                    break;
                case BinaryExpressionType.LeftShift:
                    Result = L.Expression.LeftShift(left, right);
                    break;
                case BinaryExpressionType.RightShift:
                    Result = L.Expression.RightShift(left, right);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"The expression type '{expression.Type}' is not yet supported.");
            }
        }

        public override void Visit(UnaryExpression expression)
        {
            expression.Expression.Accept(this);
            switch (expression.Type)
            {
                case UnaryExpressionType.Not:
                    Result = L.Expression.Not(Result);
                    break;
                case UnaryExpressionType.Negate:
                    Result = L.Expression.Negate(Result);
                    break;
                case UnaryExpressionType.BitwiseNot:
                    Result = L.Expression.Not(Result);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"The expression type '{expression.Type}' is not yet supported.");
            }
        }

        public override void Visit(ValueExpression expression)
        {
            Result = L.Expression.Constant(expression.Value);
        }

        public override void Visit(Function function)
        {
            var functionArgs = new L.Expression[function.Expressions.Length];
            for (int i = 0; i < function.Expressions.Length; i++)
            {
                function.Expressions[i].Accept(this);
                functionArgs[i] = Result;
            }

            var extensionArgs = new FunctionExpressionEventArgs(function.Identifier.Name, functionArgs);
            EvaluateFunction?.Invoke(this, extensionArgs);
            if (extensionArgs.HasResult)
            {
                Result = extensionArgs.Result;
                return;
            }

            var functionName = function.Identifier.Name.ToLowerInvariant();
            switch (functionName)
            {
                case "if":
                    CheckCase("if", function.Identifier.Name);
                    If(functionArgs);
                    break;
                case "in":
                    CheckCase("in", function.Identifier.Name);
                    In(functionArgs);
                    break;
                case "min":
                    CheckCase(nameof(Min), function.Identifier.Name);
                    Min(functionArgs);
                    break;
                case "max":
                    CheckCase(nameof(Max), function.Identifier.Name);
                    Max(functionArgs);
                    break;
                case "pow":
                    CheckCase(nameof(Pow), function.Identifier.Name);
                    Pow(functionArgs);
                    break;
                case "unit":
                    CheckCase(nameof(Unit), function.Identifier.Name);
                    Unit(functionArgs);
                    break;
                case "error":
                    CheckCase(nameof(Error), function.Identifier.Name);
                    Error(functionArgs);
                    break;
                default:
                    if (TrySetFromDynamicContext(function.Identifier.Name, functionArgs))
                        break;
                    if (TrySetFromUnitMath(function.Identifier.Name, functionArgs))
                        break;
                    if (TrySetFromMath(function.Identifier.Name, functionArgs))
                        break;
                    throw new MissingMethodException($"The function '{function.Identifier.Name}' was not defined.");
            }
        }

        private bool TrySetFromMath(string functionName, L.Expression[] functionArgs)
        {
            var mathMethod = FindMethod(MathType, functionName, functionArgs, MathMethodPredicate);
            if (mathMethod is null)
                return false;
            Result = L.Expression.Call(_dynamicContext, mathMethod.BaseMethodInfo, mathMethod.PreparedArguments);
            return true;
        }

        private bool TrySetFromUnitMath(string functionName, L.Expression[] functionArgs)
        {
            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && functionArgs[0].Type == typeof(ValueUnit))
            {
                var args = functionArgs;
                var unitMathMethod = FindMethod(UnitMathType, functionName, args, MathMethodPredicate);
                if(unitMathMethod is null)
                {
                    args = args.Append(L.Expression.Constant(_options)).ToArray();
                    unitMathMethod = FindMethod(UnitMathType, functionName, args, MathMethodPredicate);
                }
                if (unitMathMethod is not null)
                {
                    Result = L.Expression.Call(_dynamicContext, unitMathMethod.BaseMethodInfo, unitMathMethod.PreparedArguments);
                    return true;
                }
            }
            return false;
        }


        private bool TrySetFromDynamicContext(string functionName, L.Expression[] functionArgs)
        {
            if (_dynamicContext is null)
                return false;

            var method = FindMethod(_dynamicContext.Type, functionName, functionArgs, ContextMethodPredicate);
            if (method is not null)
            {
                Result = L.Expression.Call(_dynamicContext, method.BaseMethodInfo, method.PreparedArguments);
                return true;
            }
            return false;
        }

        private void CheckCase(string function, string called)
        {
            if (IgnoreCase)
            {
                if (function.ToLower() == called.ToLower())
                    return;

                throw new ArgumentException("Function not found", called);
            }

            if (function != called)
                throw new ArgumentException(String.Format("Function not found {0}. Try {1} instead.", called, function));
        }

        private bool ContextMethodPredicate(MethodInfo method)
        {
            return !method.IsStatic;
        }

        private bool MathMethodPredicate(MethodInfo arg)
        {
            return true;
        }

        public override void Visit(Identifier identifier)
        {
            if (_parametersContext == null)
            {
                Result = L.Expression.PropertyOrField(_dynamicContext, identifier.Name);
                return;
            }

            var index = Array.IndexOf(_parameterNames, identifier.Name);
            if (index >= 0)
            {
                // get the parameter (stored as object in the dictionary), convert into the target type
                var value = L.Expression.ArrayAccess(_parametersContext, L.Expression.Constant(index));
                if (_parameterConvertMethods.TryGetValue(identifier.Name, out MethodInfo convertMethod))
                {
                    Result = L.Expression.Call(convertMethod, value);
                    return;
                }
                if (_convertMethod != null)
                {
                    Result = L.Expression.Call(_convertMethod, value);
                    return;
                }

                Result = value;
                return;
            }

            // check for extension
            // The parameter should be defined in a call back method
            var args = new ParameterExpressionEventArgs(identifier.Name);

            // Calls external implementation
            EvaluateParameter?.Invoke(this, args);

            if (!args.HasResult)
            {
                throw new ArgumentException("Parameter was not defined.", identifier.Name);
            }

            Result = args.Result;
        }

        private static L.Expression EnsureType(L.Expression expression, Type type)
        {
            if (expression.Type == type)
            {
                return expression;
            }

            if (type == BooleanType)
            {
                return L.Expression.GreaterThan(EnsureType(expression, DoubleType), L.Expression.Constant(0.0));
            }

            return L.Expression.Convert(expression, type);
        }

        private ExtendedMethodInfo FindMethod(Type type, string methodName, L.Expression[] methodArgs, Func<MethodInfo, bool> methodPredicate)
        {
            StringComparison stringComparison = GetComparisionForMethodsName();

            var methods = type.GetTypeInfo().DeclaredMethods.Where(m => m.Name.Equals(methodName, stringComparison) && m.IsPublic && methodPredicate(m));
            foreach (var potentialMethod in methods)
            {
                var methodParams = potentialMethod.GetParameters();
                var newArguments = PrepareMethodArgumentsIfValid(methodParams, methodArgs);

                if (newArguments != null)
                    return new ExtendedMethodInfo { BaseMethodInfo = potentialMethod, PreparedArguments = newArguments };
            }

            return null;
        }

        private StringComparison GetComparisionForMethodsName()
        {
            StringComparison stringComparison = StringComparison.CurrentCulture;
            if (Ordinal || IgnoreCase)
            {
                if (Ordinal)
                {
                    if (IgnoreCase)
                        stringComparison = StringComparison.OrdinalIgnoreCase;
                    else
                        stringComparison = StringComparison.Ordinal;
                }
                else
                    stringComparison = StringComparison.CurrentCultureIgnoreCase;
            }

            return stringComparison;
        }

        private L.Expression[] PrepareMethodArgumentsIfValid(ParameterInfo[] parameters, L.Expression[] arguments)
        {
            if (!parameters.Any() && !arguments.Any()) return arguments;
            if (!parameters.Any()) return null;

            var lastParameter = parameters.Last();
            bool hasParamsKeyword = lastParameter.IsDefined(typeof(ParamArrayAttribute));
            if (hasParamsKeyword && parameters.Length > arguments.Length) return null;
            L.Expression[] newArguments = new L.Expression[parameters.Length];
            L.Expression[] paramsKeywordArgument = null;
            Type paramsElementType = null;
            int paramsParameterPosition = 0;
            if (!hasParamsKeyword)
            {
                if (parameters.Length != arguments.Length) return null;
            }
            else
            {
                paramsParameterPosition = lastParameter.Position;
                paramsElementType = lastParameter.ParameterType.GetElementType();
                paramsKeywordArgument = new L.Expression[arguments.Length - parameters.Length + 1];
            }

            for (int i = 0; i < arguments.Length; i++)
            {
                var isParamsElement = hasParamsKeyword && i >= paramsParameterPosition;
                var argument = arguments[i];
                var argumentType = argument.Type;
                var parameterType = isParamsElement ? paramsElementType : parameters[i].ParameterType;
                if (argumentType != parameterType && !CanConvert(argumentType.ToTypeCode(), parameterType.ToTypeCode())) return null;
                if (!isParamsElement)
                {
                    if (argumentType != parameterType)
                    {
                        newArguments[i] = L.Expression.Convert(argument, parameterType);
                    }
                    else
                    {
                        newArguments[i] = argument;
                    }
                }
                else
                {
                    paramsKeywordArgument[i - paramsParameterPosition] = argument;
                }
            }

            if (hasParamsKeyword)
            {
                newArguments[paramsParameterPosition] = L.Expression.NewArrayInit(paramsElementType, paramsKeywordArgument);
            }
            return newArguments;
        }

        private bool CanConvert(NCalcTypeCode fromType, NCalcTypeCode toType)
        {
            if (toType == NCalcTypeCode.Double)
            {
                switch (fromType)
                {
                    case NCalcTypeCode.Int32:
                        return true;

                    default:
                        return false;
                }
            }

            return false;
        }

        private L.Expression WithCommonNumericType(L.Expression left, L.Expression right,
            Func<L.Expression, L.Expression, L.Expression> action,
            bool useFloating,
            BinaryExpressionType expressiontype = BinaryExpressionType.Unknown)
        {
            left = UnwrapNullable(left);
            right = UnwrapNullable(right);

            if (_options.HasFlag(EvaluateOptions.BooleanCalculation))
            {
                if (left.Type == typeof(bool))
                {
                    left = L.Expression.Condition(left, L.Expression.Constant(1.0), L.Expression.Constant(0.0));
                }

                if (right.Type == typeof(bool))
                {
                    right = L.Expression.Condition(right, L.Expression.Constant(1.0), L.Expression.Constant(0.0));
                }
            }

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations))
            {
                if (left.Type != typeof(ValueUnit))
                {
                    left = L.Expression.Convert(left, typeof(ValueUnit));
                }
                if (right.Type != typeof(ValueUnit))
                {
                    right = L.Expression.Convert(right, typeof(ValueUnit));
                }
            }
            else if (left.Type != StringType && right.Type != StringType)
            {
                int l = Array.IndexOf(NumericTypePrecedence, left.Type);
                int r = Array.IndexOf(NumericTypePrecedence, right.Type);

                var minIndex = useFloating ? 0 : 3;
                if (l >= 0 && r >= 0)
                {
                    var newType = Math.Max(Math.Min(l, r), minIndex);
                    var type = NumericTypePrecedence[newType];
                    if (left.Type != type)
                    {
                        left = L.Expression.Convert(left, type);
                    }

                    if (right.Type != type)
                    {
                        right = L.Expression.Convert(right, type);
                    }
                }
            }

            L.Expression comparer;
            if (IgnoreCaseString)
            {
                if (Ordinal) comparer = L.Expression.Property(null, typeof(StringComparer), "OrdinalIgnoreCase");
                else comparer = L.Expression.Property(null, typeof(StringComparer), "CurrentCultureIgnoreCase");
            }
            else comparer = L.Expression.Property(null, typeof(StringComparer), "Ordinal");

            if (comparer != null && (typeof(string).Equals(left.Type) || typeof(string).Equals(right.Type)))
            {
                switch (expressiontype)
                {
                    case BinaryExpressionType.Equal: return L.Expression.Call(comparer, typeof(StringComparer).GetRuntimeMethod("Equals", new[] { typeof(string), typeof(string) }), new L.Expression[] { left, right });
                    case BinaryExpressionType.NotEqual: return L.Expression.Not(L.Expression.Call(comparer, typeof(StringComparer).GetRuntimeMethod("Equals", new[] { typeof(string), typeof(string) }), new L.Expression[] { left, right }));
                    case BinaryExpressionType.GreaterOrEqual: return L.Expression.GreaterThanOrEqual(L.Expression.Call(comparer, typeof(StringComparer).GetRuntimeMethod("Compare", new[] { typeof(string), typeof(string) }), new L.Expression[] { left, right }), L.Expression.Constant(0));
                    case BinaryExpressionType.LesserOrEqual: return L.Expression.LessThanOrEqual(L.Expression.Call(comparer, typeof(StringComparer).GetRuntimeMethod("Compare", new[] { typeof(string), typeof(string) }), new L.Expression[] { left, right }), L.Expression.Constant(0));
                    case BinaryExpressionType.Greater: return L.Expression.GreaterThan(L.Expression.Call(comparer, typeof(StringComparer).GetRuntimeMethod("Compare", new[] { typeof(string), typeof(string) }), new L.Expression[] { left, right }), L.Expression.Constant(0));
                    case BinaryExpressionType.Lesser: return L.Expression.LessThan(L.Expression.Call(comparer, typeof(StringComparer).GetRuntimeMethod("Compare", new[] { typeof(string), typeof(string) }), new L.Expression[] { left, right }), L.Expression.Constant(0));
                }
            }
            return action(left, right);
        }

        private L.Expression UnwrapNullable(L.Expression expression)
        {
            var ti = expression.Type.GetTypeInfo();
            if (ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return L.Expression.Condition(
                    L.Expression.Property(expression, "HasValue"),
                    L.Expression.Property(expression, "Value"),
                    L.Expression.Default(expression.Type.GetTypeInfo().GenericTypeArguments[0]));
            }

            return expression;
        }

        private void If(L.Expression[] functionArgs)
        {
            Result = L.Expression.Condition(functionArgs[0], L.Expression.Convert(functionArgs[1], typeof(object)), L.Expression.Convert(functionArgs[2], typeof(object)));
        }

        private void In(L.Expression[] functionArgs)
        {
            var items = L.Expression.NewArrayInit(functionArgs[0].Type,
                new ArraySegment<L.Expression>(functionArgs, 1, functionArgs.Length - 1));
            var smi = typeof(Array).GetRuntimeMethod("IndexOf", new[] { typeof(Array), typeof(object) });
            var r = L.Expression.Call(smi, L.Expression.Convert(items, typeof(Array)), L.Expression.Convert(functionArgs[0], typeof(object)));
            Result = L.Expression.GreaterThanOrEqual(r, L.Expression.Constant(0));
        }

        private void Min(L.Expression[] functionArgs)
        {
            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && (functionArgs[0].Type == typeof(ValueUnit) || functionArgs[1].Type == typeof(ValueUnit)))
            {
                var min_argVU0 = L.Expression.Convert(functionArgs[0], typeof(ValueUnit));
                var min_argVU1 = L.Expression.Convert(functionArgs[1], typeof(ValueUnit));
                Result = L.Expression.Condition(L.Expression.LessThan(min_argVU0, min_argVU1), min_argVU0, min_argVU1);
            }
            //else if (functionArgs[0].Type == typeof(ValueUnit) ^ functionArgs[1].Type == typeof(ValueUnit))
            //    throw new ArgumentException("Couldn't compare the values.");
            else
            {
                var min_arg0 = L.Expression.Convert(functionArgs[0], typeof(double));
                var min_arg1 = L.Expression.Convert(functionArgs[1], typeof(double));
                Result = L.Expression.Condition(L.Expression.LessThan(min_arg0, min_arg1), min_arg0, min_arg1);
            }
        }

        private void Max(L.Expression[] functionArgs)
        {
            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && (functionArgs[0].Type == typeof(ValueUnit) || functionArgs[1].Type == typeof(ValueUnit)))
            {
                var max_argVU0 = L.Expression.Convert(functionArgs[0], typeof(ValueUnit));
                var max_argVU1 = L.Expression.Convert(functionArgs[1], typeof(ValueUnit));
                Result = L.Expression.Condition(L.Expression.GreaterThan(max_argVU0, max_argVU1), max_argVU0, max_argVU1);
            }
            //else if (functionArgs[0].Type == typeof(ValueUnit) ^ functionArgs[1].Type == typeof(ValueUnit))
            //    throw new ArgumentException("Couldn't compare the values.");
            else
            {
                var max_arg0 = L.Expression.Convert(functionArgs[0], typeof(double));
                var max_arg1 = L.Expression.Convert(functionArgs[1], typeof(double));
                Result = L.Expression.Condition(L.Expression.GreaterThan(max_arg0, max_arg1), max_arg0, max_arg1);
            }
        }

        private void Pow(L.Expression[] functionArgs)
        {
            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && functionArgs[0].Type == typeof(ValueUnit) && functionArgs[1].Type != typeof(ValueUnit))
            {
                var pow_argVU0 = L.Expression.Convert(functionArgs[0], typeof(ValueUnit));
                var pow_arg1 = L.Expression.Convert(functionArgs[1], typeof(double));
                var powerMethod = typeof(UnitMath).GetRuntimeMethod(nameof(UnitMath.Pow), new[] { typeof(ValueUnit), typeof(double) });
                Result = L.Expression.Call(powerMethod, pow_argVU0, pow_arg1);
            }
            //else if ( functionArgs[1].Type == typeof(ValueUnit))
            //    throw new ArgumentException("Couldn't power to a unit.");
            else if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && functionArgs[1].Type == typeof(ValueUnit))
            {
                var pow_argVU0 = L.Expression.Convert(functionArgs[0], typeof(ValueUnit));
                var pow_arg1 = L.Expression.Convert(functionArgs[1], typeof(double));
                var powerMethod = typeof(UnitMath).GetRuntimeMethod(nameof(UnitMath.Pow), new[] { typeof(ValueUnit), typeof(double)});
                Result = L.Expression.Call(powerMethod, pow_argVU0, pow_arg1);
            }
            else
            {
                var pow_arg0 = L.Expression.Convert(functionArgs[0], typeof(double));
                var pow_arg1 = L.Expression.Convert(functionArgs[1], typeof(double));
                Result = L.Expression.Power(pow_arg0, pow_arg1);
            }
        }

        private void Unit(L.Expression[] functionArgs)
        {
            var unit_arg0 = L.Expression.Convert(functionArgs[0], typeof(double));
            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations))
            {
                var symbols = L.Expression.NewArrayInit(typeof(string),
                    new ArraySegment<L.Expression>(functionArgs, 1, functionArgs.Length - 1));
                var umi = typeof(UnitFunction).GetRuntimeMethod("Create", new[] { typeof(double), typeof(string[]) });
                var c = L.Expression.Call(umi, L.Expression.Convert(functionArgs[0], typeof(double)),  L.Expression.Convert(symbols, typeof(string[])));
                Result = c;
            }
            else
                Result = unit_arg0;
        }

        private void Error(L.Expression[] functionArgs)
        {
            var error_arg0 = L.Expression.Convert(functionArgs[0], typeof(string));
            var errorMethod = typeof(ConvertExtensions).GetRuntimeMethod(nameof(ConvertExtensions.Error), new[] { typeof(string) });

            Result = L.Expression.Call(errorMethod, error_arg0);
        }
    }
}