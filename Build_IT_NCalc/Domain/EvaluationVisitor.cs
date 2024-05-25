using Build_IT_NCalc.Enums;
using Build_IT_NCalc.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NCalc.Domain
{
    public class EvaluationVisitor : LogicalExpressionVisitor
    {
        private delegate T Func<T>();

        private readonly EvaluateOptions _options = EvaluateOptions.None;
        private readonly Expression _mainExpression;

        private bool IgnoreCase { get { return (_options & EvaluateOptions.IgnoreCase) == EvaluateOptions.IgnoreCase; } }
        private bool Ordinal { get { return (_options & EvaluateOptions.MatchStringsOrdinal) == EvaluateOptions.MatchStringsOrdinal; } }
        private bool IgnoreCaseString { get { return (_options & EvaluateOptions.MatchStringsWithIgnoreCase) == EvaluateOptions.MatchStringsWithIgnoreCase; } }
        private bool Checked { get { return (_options & EvaluateOptions.OverflowProtection) == EvaluateOptions.OverflowProtection; } }

        public EvaluationVisitor(EvaluateOptions options, Expression expression)
        {
            _options = options;
            _mainExpression = expression;
        }

        public object Result { get; private set; }

        private object Evaluate(LogicalExpression expression)
        {
            expression.Accept(this);
            return Result;
        }

        public override void Visit(LogicalExpression expression)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private static Type[] CommonTypes = new[] { typeof(Int64), typeof(Double), typeof(Boolean), typeof(String), typeof(Decimal) };

        /// <summary>
        /// Gets the the most precise type.
        /// </summary>
        /// <param name="a">Type a.</param>
        /// <param name="b">Type b.</param>
        /// <returns></returns>
        private static Type GetMostPreciseType(Type a, Type b)
        {
            foreach (Type t in CommonTypes)
            {
                if (a == t || b == t)
                {
                    return t;
                }
            }

            return a ?? b;
        }

        public int CompareUsingMostPreciseType(object a, object b)
        {
            var allowNull = (_options & EvaluateOptions.AllowNullParameter) == EvaluateOptions.AllowNullParameter;

            Type mpt = allowNull ? GetMostPreciseType(a?.GetType(), b?.GetType()) ?? typeof(object) : GetMostPreciseType(a.GetType(), b.GetType());

            if (!_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && mpt == typeof(ValueUnit))
            {
                a = Convert.ToDouble(a);
                b = Convert.ToDouble(b);
            }
            else
            {
                bool onlyOneUnit = a?.GetType() != typeof(ValueUnit) && b?.GetType() == typeof(ValueUnit) ||
                    a?.GetType() == typeof(ValueUnit) && b?.GetType() != typeof(ValueUnit);

                if (onlyOneUnit)
                {
                    a = Convert.ToDouble(a);
                    b = Convert.ToDouble(b);
                }
                else
                {
                    a = Convert.ChangeType(a, mpt);
                    b = Convert.ChangeType(b, mpt);
                }
            }

            if (mpt.Equals(typeof(string)) && (Ordinal || IgnoreCaseString))
            {
                if (Ordinal)
                {
                    if (IgnoreCaseString)
                        return StringComparer.OrdinalIgnoreCase.Compare(a?.ToString(), b?.ToString());
                    else
                        StringComparer.Ordinal.Compare(a?.ToString(), b?.ToString());
                }
                else
                    return StringComparer.CurrentCultureIgnoreCase.Compare(a?.ToString(), b?.ToString());
            }

            if (ReferenceEquals(a, b))
            {
                return 0;
            }

            var cmp = a as IComparable;
            if (cmp != null)
                return cmp.CompareTo(b);

            return -1;
            //return Comparer.Default.Compare(Convert.ChangeType(a, mpt), Convert.ChangeType(b, mpt));
        }

        public override void Visit(TernaryExpression expression)
        {
            // Evaluates the left expression and saves the value
            expression.LeftExpression.Accept(this);
            bool left = Convert.ToBoolean(Result);

            if (left)
            {
                expression.MiddleExpression.Accept(this);
            }
            else
            {
                expression.RightExpression.Accept(this);
            }
        }

        private static bool IsReal(object value)
        {
            var typeCode = value.GetTypeCode();
            return typeCode == NCalcTypeCode.Decimal || typeCode == NCalcTypeCode.Double ||
                typeCode == NCalcTypeCode.Single || typeCode == NCalcTypeCode.Unit;
        }

        public override void Visit(BinaryExpression expression)
        {
            // simulate Lazy<Func<>> behavior for late evaluation
            object leftValue = null;
            Func<object> left = () =>
                                 {
                                     if (leftValue == null)
                                     {
                                         expression.LeftExpression.Accept(this);
                                         leftValue = Result;
                                     }
                                     return leftValue;
                                 };

            // simulate Lazy<Func<>> behavior for late evaluation
            object rightValue = null;
            Func<object> right = () =>
            {
                if (rightValue == null)
                {
                    expression.RightExpression.Accept(this);
                    rightValue = Result;
                }
                return rightValue;
            };

            switch (expression.Type)
            {
                case BinaryExpressionType.And:
                    Result = Convert.ToBoolean(left()) && Convert.ToBoolean(right());
                    break;

                case BinaryExpressionType.Or:
                    Result = Convert.ToBoolean(left()) || Convert.ToBoolean(right());
                    break;

                case BinaryExpressionType.Div:
                    //Actually doesn't need checked here, since if one is real,
                    // checked does nothing, and if they are int the result will only be same or smaller
                    // (since anything between 1 and 0 is not int and 0 is an exception anyway
                    Result = IsReal(left()) || IsReal(right())
                                 ? Numbers.Divide(left(), right(), _options)
                                 : Numbers.Divide(Convert.ToDouble(left()), right(), _options);
                    break;

                case BinaryExpressionType.Equal:
                    // Use the type of the left operand to make the comparison
                    Result = CompareUsingMostPreciseType(left(), right()) == 0;
                    break;

                case BinaryExpressionType.Greater:
                    // Use the type of the left operand to make the comparison
                    Result = CompareUsingMostPreciseType(left(), right()) > 0;
                    break;

                case BinaryExpressionType.GreaterOrEqual:
                    // Use the type of the left operand to make the comparison
                    Result = CompareUsingMostPreciseType(left(), right()) >= 0;
                    break;

                case BinaryExpressionType.Lesser:
                    // Use the type of the left operand to make the comparison
                    Result = CompareUsingMostPreciseType(left(), right()) < 0;
                    break;

                case BinaryExpressionType.LesserOrEqual:
                    // Use the type of the left operand to make the comparison
                    Result = CompareUsingMostPreciseType(left(), right()) <= 0;
                    break;

                case BinaryExpressionType.Minus:
                    Result = Checked
                        ? Numbers.SoustractChecked(left(), right(), _options)
                        : Numbers.Soustract(left(), right(), _options);
                    break;


                case BinaryExpressionType.Modulo:
                    Result = Numbers.Modulo(left(), right(), _options);
                    break;

                case BinaryExpressionType.NotEqual:
                    // Use the type of the left operand to make the comparison
                    Result = CompareUsingMostPreciseType(left(), right()) != 0;
                    break;

                case BinaryExpressionType.Plus:
                    if (left() is string)
                    {
                        Result = String.Concat(left(), right());
                    }
                    else
                    {
                        Result = Checked
                            ? Numbers.AddChecked(left(), right(), _options)
                            : Numbers.Add(left(), right(), _options);
                    }

                    break;

                case BinaryExpressionType.Times:
                    Result = Checked
                        ? Numbers.MultiplyChecked(left(), right(), _options)
                        : Numbers.Multiply(left(), right(), _options);
                    break;

                case BinaryExpressionType.BitwiseAnd:
                    Result = Numbers.BitwiseAnd(left(), right());
                    break;


                case BinaryExpressionType.BitwiseOr:
                    Result = Numbers.BitwiseOr(left(), right());
                    break;


                case BinaryExpressionType.BitwiseXOr:
                    Result = Numbers.BitwiseXor(left(), right());
                    break;


                case BinaryExpressionType.LeftShift:
                    Result = Numbers.LeftShift(left(), right());
                    break;


                case BinaryExpressionType.RightShift:
                    Result = Numbers.RightShift(left(), right());
                    break;
            }
        }

        public override void Visit(UnaryExpression expression)
        {
            // Recursively evaluates the underlying expression
            expression.Expression.Accept(this);

            switch (expression.Type)
            {
                case UnaryExpressionType.Not:
                    Result = !Convert.ToBoolean(Result);
                    break;

                case UnaryExpressionType.Negate:
                    Result = Numbers.Soustract(0, Result, _options);
                    break;

                case UnaryExpressionType.BitwiseNot:
                    Result = ~Convert.ToUInt16(Result);
                    break;
            }
        }

        public override void Visit(ValueExpression expression)
        {
            Result = expression.Value;
        }

        public override void Visit(Function function)
        {
            var args = new FunctionArgs
            {
                Parameters = new Expression[function.Expressions.Length],
                MainExpression = _mainExpression
            };

            // Don't call parameters right now, instead let the function do it as needed.
            // Some parameters shouldn't be called, for instance, in a if(), the "not" value might be a division by zero
            // Evaluating every value could produce unexpected behaviour
            for (int i = 0; i < function.Expressions.Length; i++)
            {
                args.Parameters[i] = new Expression(function.Expressions[i], _options);
                args.Parameters[i].EvaluateFunction += EvaluateFunction;
                args.Parameters[i].EvaluateParameter += EvaluateParameter;

                // Assign the parameters of the Expression to the arguments so that custom Functions and Parameters can use them
                args.Parameters[i].SetParameters(Parameters);
            }

            // Calls external implementation
            OnEvaluateFunction(IgnoreCase ? function.Identifier.Name.ToLower() : function.Identifier.Name, args);

            // If an external implementation was found get the result back
            if (args.HasResult)
            {
                Result = args.Result;
                return;
            }

            switch (function.Identifier.Name.ToLower())
            {
                case "abs":
                    Abs(function);
                    break;
                case "acos":
                    ACos(function);
                    break;
                case "asin":
                    ASin(function);
                    break;
                case "atan":
                    ATan(function);
                    break;
                case "ceiling":
                    Ceiling(function);
                    break;
                case "cos":
                    Cos(function);
                    break;
                case "exp":
                    Exp(function);
                    break;
                case "floor":
                    Floor(function);
                    break;
                case "ieeeremainder":
                    IEEERemainder(function);
                    break;
                case "log":
                    Log(function);
                    break;
                case "log10":
                    Log10(function);
                    break;
                case "pow":
                    Pow(function);
                    break;
                case "round":
                    Round(function);
                    break;
                case "sign":
                    Sign(function);
                    break;
                case "sin":
                    Sin(function);
                    break;
                case "sqrt":
                    Sqrt(function);
                    break;
                case "tan":
                    Tan(function);
                    break;
                case "truncate":
                    Truncate(function);
                    break;
                case "max":
                    Max(function);
                    break;
                case "min":
                    Min(function);
                    break;
                case "if":
                    If(function);
                    break;
                case "in":
                    In(function);
                    break;
                case "unit":
                    Unit(function);
                    break;
                case "error":
                    Error(function);
                    break;
                default:
                    throw new ArgumentException("Function not found",
                        function.Identifier.Name);
            }
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

        public event EvaluateFunctionHandler EvaluateFunction;

        private void OnEvaluateFunction(string name, FunctionArgs args)
        {
            if (EvaluateFunction != null)
                EvaluateFunction(name, args);
        }

        public override void Visit(Identifier parameter)
        {
            if (Parameters.ContainsKey(parameter.Name))
            {
                // The parameter is defined in the hashtable
                if (Parameters[parameter.Name] is Expression)
                {
                    // The parameter is itself another Expression
                    var expression = (Expression)Parameters[parameter.Name];

                    // Overloads parameters
                    foreach (var p in Parameters)
                    {
                        expression.SetParameter(p.Key, p.Value);
                    }

                    expression.EvaluateFunction += EvaluateFunction;
                    expression.EvaluateParameter += EvaluateParameter;

                    Result = ((Expression)Parameters[parameter.Name]).Evaluate();
                }
                else
                    Result = Parameters[parameter.Name];
            }
            else
            {
                // The parameter should be defined in a call back method
                var args = new ParameterArgs();

                // Calls external implementation
                OnEvaluateParameter(parameter.Name, args);

                if (!args.HasResult)
                    throw new ArgumentException("Parameter was not defined", parameter.Name);

                Result = args.Result;
            }
        }

        public event EvaluateParameterHandler EvaluateParameter;

        private void OnEvaluateParameter(string name, ParameterArgs args)
        {
            if (EvaluateParameter != null)
            {
                EvaluateParameter(name, args);
                if (args.Result is ValueUnit valueUnit && valueUnit.Units.Any(u => u is CustomUnit))
                    args.Result = new ValueUnit(valueUnit.Value, valueUnit.Units.ToArray());
            }
        }

        public Dictionary<string, object> Parameters { get; set; }

        #region Functions

        #region Abs

        private void Abs(Function function)
        {
            CheckCase("Abs", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Abs() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);
            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && result is ValueUnit unit)
                Result = new ValueUnit(Math.Abs(Convert.ToDouble(unit.Value)), unit.Units.ToArray());
            else if (_options.HasFlag(EvaluateOptions.UseDoubleForAbsFunction))
                Result = Math.Abs(Convert.ToDouble(result));
            else
                Result = Math.Abs(Convert.ToDecimal(result));
        }

        #endregion // Abs

        #region ACos

        private void ACos(Function function)
        {
            CheckCase("Acos", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Acos() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Acos(unit);
            else
                Result = Math.Acos(Convert.ToDouble(result));
        }

        #endregion // ACos

        #region Asin

        private void ASin(Function function)
        {
            CheckCase("Asin", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Asin() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Asin(unit);
            else
                Result = Math.Asin(Convert.ToDouble(result));
        }

        #endregion // Asin

        #region Atan

        private void ATan(Function function)
        {
            CheckCase("Atan", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Atan() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Atan(unit);
            else
                Result = Math.Atan(Convert.ToDouble(result));
        }

        #endregion // Atan

        #region Ceiling

        private void Ceiling(Function function)
        {
            CheckCase("Ceiling", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Ceiling() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Ceiling(unit);
            else
                Result = Math.Ceiling(Convert.ToDouble(result));
        }

        #endregion // Ceiling

        #region Cos

        private void Cos(Function function)
        {
            CheckCase("Cos", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Cos() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Cos(unit);
            else
                Result = Math.Cos(Convert.ToDouble(result));
        }

        #endregion // Cos

        #region Exp

        private void Exp(Function function)
        {
            CheckCase("Exp", function.Identifier.Name);
            if (function.Expressions.Length != 1)
                throw new ArgumentException("Exp() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Exp(unit);
            else
                Result = Math.Exp(Convert.ToDouble(result));
        }

        #endregion // Exp

        #region Floor

        private void Floor(Function function)
        {
            CheckCase("Floor", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Floor() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Floor(unit);
            else
                Result = Math.Floor(Convert.ToDouble(result));
        }

        #endregion // Floor

        #region IEEERemainder

        private void IEEERemainder(Function function)
        {
            CheckCase("IEEERemainder", function.Identifier.Name);

            if (function.Expressions.Length != 2)
                throw new ArgumentException("IEEERemainder() takes exactly 2 arguments");

            var result1 = Evaluate(function.Expressions[0]);
            var result2 = Evaluate(function.Expressions[1]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                (result1 is ValueUnit || result2 is ValueUnit))
                throw new NotSupportedException();
            else
                Result = Math.IEEERemainder(Convert.ToDouble(result1), Convert.ToDouble(result2));
        }

        #endregion // IEEERemainder

        #region Log

        private void Log(Function function)
        {
            CheckCase("Log", function.Identifier.Name);

            if (function.Expressions.Length != 2)
                throw new ArgumentException("Log() takes exactly 2 arguments");

            var result1 = Evaluate(function.Expressions[0]);
            var result2 = Evaluate(function.Expressions[1]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                (result1 is ValueUnit || result2 is ValueUnit))
                throw new NotSupportedException();
            else
                Result = Math.Log(Convert.ToDouble(Evaluate(function.Expressions[0])), Convert.ToDouble(Evaluate(function.Expressions[1])));
        }

        #endregion // Log

        #region Log10

        private void Log10(Function function)
        {
            CheckCase("Log10", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Log10() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit)
                throw new NotSupportedException();
            else
                Result = Math.Log10(Convert.ToDouble(Evaluate(function.Expressions[0])));
        }

        #endregion // Log10

        #region Pow

        private void Pow(Function function)
        {
            CheckCase("Pow", function.Identifier.Name);

            if (function.Expressions.Length != 2)
                throw new ArgumentException("Pow() takes exactly 2 arguments");

            var result1 = Evaluate(function.Expressions[0]);
            var result2 = Evaluate(function.Expressions[1]);
            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && result1 is ValueUnit unit)
                Result = UnitMath.Pow(unit, Convert.ToDouble(result2));
            else
                Result = Math.Pow(Convert.ToDouble(result1), Convert.ToDouble(result2));
        }

        #endregion // Pow

        #region Round
        private void Round(Function function)
        {
            CheckCase("Round", function.Identifier.Name);

            if (function.Expressions.Length != 2)
                throw new ArgumentException("Round() takes exactly 2 arguments");

            MidpointRounding rounding = (_options & EvaluateOptions.RoundAwayFromZero) == EvaluateOptions.RoundAwayFromZero ? MidpointRounding.AwayFromZero : MidpointRounding.ToEven;

            var result1 = Evaluate(function.Expressions[0]);
            var result2 = Evaluate(function.Expressions[1]);
            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && result1 is ValueUnit unit)
                Result = UnitMath.Round(unit, Convert.ToInt32(result2), _options);
            else
                Result = Math.Round(Convert.ToDouble(Evaluate(function.Expressions[0])), Convert.ToInt16(Evaluate(function.Expressions[1])), rounding);
        }
        #endregion // Round

        #region If

        private void If(Function function)
        {
            CheckCase("if", function.Identifier.Name);

            if (function.Expressions.Length != 3)
                throw new ArgumentException("if() takes exactly 3 arguments");

            var conditionResult = Evaluate(function.Expressions[0]);
            bool cond = Convert.ToBoolean(conditionResult);

            if (cond)
            {
                var trueResult = Evaluate(function.Expressions[1]);

                if (!_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                    trueResult is ValueUnit)
                    Result = Convert.ToDouble(trueResult);
                else
                    Result = trueResult;
            }
            else
            {
                var falseResult = Evaluate(function.Expressions[2]);

                if (!_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                    falseResult is ValueUnit)
                    Result = Convert.ToDouble(falseResult);
                else
                    Result = falseResult;
            }
        }

        #endregion // If

        #region In

        private void In(Function function)
        {
            CheckCase("in", function.Identifier.Name);

            if (function.Expressions.Length < 2)
                throw new ArgumentException("in() takes at least 2 arguments");

            object parameter = Evaluate(function.Expressions[0]);

            bool evaluation = false;

            // Goes through any values, and stop whe one is found
            for (int i = 1; i < function.Expressions.Length; i++)
            {
                object argument = Evaluate(function.Expressions[i]);

                if (CompareUsingMostPreciseType(parameter, argument) == 0)
                {
                    evaluation = true;
                    break;
                }
            }

            Result = evaluation;
        }

        #endregion // In

        #region Max

        private void Max(Function function)
        {
            CheckCase("Max", function.Identifier.Name);

            if (function.Expressions.Length != 2)
                throw new ArgumentException("Max() takes exactly 2 arguments");

            object maxleft = Evaluate(function.Expressions[0]);
            object maxright = Evaluate(function.Expressions[1]);

            Result = Numbers.Max(maxleft, maxright, _options);
        }

        #endregion // Max

        #region Min

        private void Min(Function function)
        {
            CheckCase("Min", function.Identifier.Name);

            if (function.Expressions.Length != 2)
                throw new ArgumentException("Min() takes exactly 2 arguments");

            object minleft = Evaluate(function.Expressions[0]);
            object minright = Evaluate(function.Expressions[1]);

            Result = Numbers.Min(minleft, minright, _options);
        }

        #endregion // Min

        #region Sign

        private void Sign(Function function)
        {
            CheckCase("Sign", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Sign() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Sign(unit);
            else
                Result = Math.Sign(Convert.ToDouble(result));
        }

        #endregion Sign

        #region Sin

        private void Sin(Function function)
        {
            CheckCase("Sin", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Sin() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Sin(unit);
            else
                Result = Math.Sin(Convert.ToDouble(result));
        }

        #endregion // Sin

        #region Sqrt
        private void Sqrt(Function function)
        {
            CheckCase("Sqrt", function.Identifier.Name);

            if (function.Expressions.Length != 1 && function.Expressions.Length != 2)
                throw new ArgumentException("Sqrt() takes 1 or 2 arguments");

            var result = Evaluate(function.Expressions[0]);
            if (function.Expressions.Length == 2)
            {
                var power = Evaluate(function.Expressions[1]);

                if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && result is ValueUnit unit)
                    Result = UnitMath.Pow(unit, 1.0/ Convert.ToDouble(power));
                else
                    Result = Math.Pow(Convert.ToDouble(result), 1.0 / Convert.ToDouble(power));
            }
            else
            {
                if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && result is ValueUnit unit)
                    Result = UnitMath.Sqrt(unit);
                else
                    Result = Math.Sqrt(Convert.ToDouble(result));
            }
        }
        #endregion // Sqrt

        #region Tan

        private void Tan(Function function)
        {
            CheckCase("Tan", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Tan() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Tan(unit);
            else
                Result = Math.Tan(Convert.ToDouble(Evaluate(function.Expressions[0])));
        }

        #endregion // Tan

        #region Truncate

        private void Truncate(Function function)
        {
            CheckCase("Truncate", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Truncate() takes exactly 1 argument");

            var result = Evaluate(function.Expressions[0]);

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) &&
                result is ValueUnit unit)
                Result = UnitMath.Truncate(unit);
            else
                Result = Math.Truncate(Convert.ToDouble(result));
        }

        #endregion // Truncate

        #region Unit
        private void Unit(Function function)
        {
            CheckCase("Unit", function.Identifier.Name);

            if (function.Expressions.Length < 2)
                throw new ArgumentException("Unit() takes 2 or more arguments");

            var value = Evaluate(function.Expressions[0]);
            var symbols = new List<string>();
            for (int i = 1; i < function.Expressions.Length; i++)
                symbols.Add(Evaluate(function.Expressions[i]).ToString());

            if (_options.HasFlag(EvaluateOptions.AllowUnitCalculations) && Double.TryParse(value.ToString(), out double valueResult))
            {
                var unit = new ValueUnit(valueResult, symbols.ToArray());
                Result = unit;
            }
            else
                Result = value;
        }
        #endregion

        #region Error
        private void Error(Function function)
        {
            CheckCase("Error", function.Identifier.Name);

            if (function.Expressions.Length != 1)
                throw new ArgumentException("Error() takes 1 argument");

            var value = Evaluate(function.Expressions[0]);

            throw new CalculationException(value?.ToString());
        }
        #endregion

        #endregion // Functions
    }
}
