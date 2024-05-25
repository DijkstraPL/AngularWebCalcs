using Antlr.Runtime;
using Build_IT_NCalc;
using Build_IT_NCalc.Units;
using NCalc.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly: InternalsVisibleTo("Build_IT_NCalcTests")]
namespace NCalc
{
    public class Expression
    {
        public EvaluateOptions Options { get; set; }
        
        public IReadOnlyDictionary<string, object> Parameters
        {
            get
            {
                return _parameterNames
                    .TakeWhile(x => x != null)
                    .Zip(_parameterValues, (name, value) => new { name, value })
                    .ToDictionary(val => val.name, val => val.value);
            }
        }

        public string Error { get; private set; }

        public Exception ErrorException { get; private set; }

        public LogicalExpression ParsedExpression { get; private set; }

        protected Dictionary<string, IEnumerator> ParameterEnumerators;
        protected Dictionary<string, object> ParametersBackup;

        // Use "self managed" dictionary because the generic dictionary is too slow when accessed from generated lambda.
        private string[] _parameterNames = new string[10];
        private object[] _parameterValues = new object[10];
        private uint _currentParameterIndex;

        /// <summary>
        /// Textual representation of the expression to evaluate.
        /// </summary>
        protected string OriginalExpression;

        public event EvaluateFunctionHandler EvaluateFunction;
        public event EvaluateParameterHandler EvaluateParameter;
        public event EventHandler<FunctionExpressionEventArgs> EvaluateFunctionExpression;
        public event EventHandler<ParameterExpressionEventArgs> EvaluateParameterExpression;

        public Expression(string expression) : this(expression, EvaluateOptions.None)
        {
        }

        public Expression(string expression, EvaluateOptions options)
        {
            if (String.IsNullOrEmpty(expression))
                throw new
                    ArgumentException("Expression can't be empty", "expression");

            OriginalExpression = expression;
            Options = options;
        }

        public Expression(LogicalExpression expression) : this(expression, EvaluateOptions.None)
        {
        }

        public Expression(LogicalExpression expression, EvaluateOptions options)
        {
            if (expression == null)
                throw new
                    ArgumentException("Expression can't be null", "expression");

            ParsedExpression = expression;
            Options = options;
        }

        #region Cache management
        private static bool _cacheEnabled = true;
        private static Dictionary<string, WeakReference> _compiledExpressions = new Dictionary<string, WeakReference>();
        private static readonly ReaderWriterLockSlim Rwl = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        public static bool CacheEnabled
        {
            get { return _cacheEnabled; }
            set
            {
                _cacheEnabled = value;

                if (!CacheEnabled)
                {
                    // Clears cache
                    _compiledExpressions = new Dictionary<string, WeakReference>();
                }
            }
        }

        /// <summary>
        /// Removed unused entries from cached compiled expression
        /// </summary>
        private static void CleanCache()
        {
            var keysToRemove = new List<string>();

            try
            {
                Rwl.EnterReadLock();
                foreach (var de in _compiledExpressions)
                {
                    if (!de.Value.IsAlive)
                    {
                        keysToRemove.Add(de.Key);
                    }
                }


                foreach (string key in keysToRemove)
                {
                    _compiledExpressions.Remove(key);
                    Debug.WriteLine("Cache entry released: " + key);
                }
            }
            finally
            {
                Rwl.ExitReadLock();
            }
        }

        #endregion

        public static LogicalExpression Compile(string expression, bool nocache)
        {
            LogicalExpression logicalExpression = null;

            if (_cacheEnabled && !nocache)
            {
                try
                {
                    Rwl.EnterReadLock();

                    if (_compiledExpressions.ContainsKey(expression))
                    {
                        Debug.WriteLine("Expression retrieved from cache: " + expression);
                        var wr = _compiledExpressions[expression];
                        logicalExpression = wr.Target as LogicalExpression;

                        if (wr.IsAlive && logicalExpression != null)
                        {
                            return logicalExpression;
                        }
                    }
                }
                finally
                {
                    Rwl.ExitReadLock();
                }
            }

            if (logicalExpression == null)
            {
                var lexer = new NCalcLexer(new ANTLRStringStream(expression));
                var parser = new NCalcParser(new CommonTokenStream(lexer));

                logicalExpression = parser.ncalcExpression().value;

                if (parser.Errors != null && parser.Errors.Count > 0)
                {
                    throw new EvaluationException(String.Join(Environment.NewLine, parser.Errors.ToArray()));
                }

                if (_cacheEnabled && !nocache)
                {
                    try
                    {
                        Rwl.EnterWriteLock();
                        _compiledExpressions[expression] = new WeakReference(logicalExpression);
                    }
                    finally
                    {
                        Rwl.ExitWriteLock();
                    }

                    CleanCache();

                    Debug.WriteLine("Expression added to cache: " + expression);
                }
            }

            return logicalExpression;
        }

        public void SetUnitRegistry(UnitRegistry unitRegistry)
        {
            if (!Options.HasFlag(EvaluateOptions.AllowUnitCalculations))
                throw new InvalidOperationException("Unit calculations are not allowed");
            UnitRegistry.SetInstance(unitRegistry);
        }

        /// <summary>
        /// Pre-compiles the expression in order to check syntax errors.
        /// If errors are detected, the Error property contains the message.
        /// </summary>
        /// <returns>True if the expression syntax is correct, otherwiser False</returns>
        public bool HasErrors()
        {
            try
            {
                if (ParsedExpression == null)
                {
                    ParsedExpression = Compile(OriginalExpression, (Options & EvaluateOptions.NoCache) == EvaluateOptions.NoCache);
                }

                // In case HasErrors() is called multiple times for the same expression
                return ParsedExpression != null && Error != null;
            }
            catch (Exception e)
            {
                Error = e.Message;
                ErrorException = e;
                return true;
            }
        }

        public Action ToLambda()
        {
            if (HasErrors())
                throw new EvaluationException(Error, ErrorException);

            if (ParsedExpression == null)
                ParsedExpression = Compile(OriginalExpression, (Options & EvaluateOptions.NoCache) == EvaluateOptions.NoCache);

            if (!Options.HasFlag(EvaluateOptions.AllowUnitCalculations) && Parameters.Any(p => p.Value is ValueUnit))
                throw new ArgumentException("Unit calculations are not allowed");

            var visitor = new LambdaExpressionVisitor(_parameterNames, _parameterValues, typeof(void), Options);
            visitor.EvaluateFunction += EvaluateFunctionExpression;
            visitor.EvaluateParameter += EvaluateParameterExpression;
            ParsedExpression.Accept(visitor);

            var body = visitor.Result;

            if (!Parameters.Any())
            {
                // no parameter used
                // formula is static and can be used directly
                var lambda = System.Linq.Expressions.Expression.Lambda<Action>(body);
                return lambda.Compile();
            }

            // create lambda with parameters context
            var innerLambda = System.Linq.Expressions.Expression.Lambda<Action<object[]>>(
                body,
                visitor.Context);
            var invokeInner = System.Linq.Expressions.Expression.Invoke(
                innerLambda,
                System.Linq.Expressions.Expression.Constant(_parameterValues));

            // create parameter free lambda to be called directly
            var outerLambda = System.Linq.Expressions.Expression.Lambda<Action>(invokeInner);
            return outerLambda.Compile();
        }

        public Func<TResult> ToLambda<TResult>()
        {
            if (HasErrors())
                throw new EvaluationException(Error, ErrorException);

            if (ParsedExpression == null)
                ParsedExpression = Compile(OriginalExpression, (Options & EvaluateOptions.NoCache) == EvaluateOptions.NoCache);

            if (!Options.HasFlag(EvaluateOptions.AllowUnitCalculations) && Parameters.Any(p => p.Value is ValueUnit))
                throw new ArgumentException("Unit calculations are not allowed");

            var visitor = new LambdaExpressionVisitor(_parameterNames, _parameterValues, typeof(TResult), Options);
            visitor.EvaluateFunction += EvaluateFunctionExpression;
            visitor.EvaluateParameter += EvaluateParameterExpression;
            ParsedExpression.Accept(visitor);

            var body = visitor.Result;
            if (body.Type != typeof(TResult))
                body = System.Linq.Expressions.Expression.Convert(body, typeof(TResult));

            if (!Parameters.Any())
            {
                // no parameter used
                // formula is static and can be used directly
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<TResult>>(body);
                return lambda.Compile();
            }

            // create lambda with parameters context
            var innerLambda = System.Linq.Expressions.Expression.Lambda<Func<object[], TResult>>(
                body,
                visitor.Context);
            var invokeInner = System.Linq.Expressions.Expression.Invoke(
                innerLambda,
                System.Linq.Expressions.Expression.Constant(_parameterValues));

            // create parameter free lambda to be called directly
            var outerLambda = System.Linq.Expressions.Expression.Lambda<Func<TResult>>(invokeInner);
            return outerLambda.Compile();
        }

        public Func<TContext, TResult> ToLambda<TContext, TResult>() where TContext : class
        {
            if (HasErrors())
                throw new EvaluationException(Error, ErrorException);

            if (ParsedExpression == null)
                ParsedExpression = Compile(OriginalExpression, (Options & EvaluateOptions.NoCache) == EvaluateOptions.NoCache);

            var visitor = new LambdaExpressionVisitor(typeof(TContext), Options);
            visitor.EvaluateFunction += EvaluateFunctionExpression;
            visitor.EvaluateParameter += EvaluateParameterExpression;
            ParsedExpression.Accept(visitor);

            var body = visitor.Result;
            if (body.Type != typeof(TResult))
                body = System.Linq.Expressions.Expression.Convert(body, typeof(TResult));

            var lambda = System.Linq.Expressions.Expression.Lambda<Func<TContext, TResult>>(body, visitor.Context);
            return lambda.Compile();
        }

        public Func<object> ToLambda(Type resultType)
        {
            var typeCode = resultType.ToTypeCode();

            switch (typeCode)
            {
                case Build_IT_NCalc.Enums.NCalcTypeCode.Empty:
                    return () => null;
                case Build_IT_NCalc.Enums.NCalcTypeCode.Object:
                    return ToLambda<Object>();
                case Build_IT_NCalc.Enums.NCalcTypeCode.DBNull:
                    return ToLambda<DBNull>();
                case Build_IT_NCalc.Enums.NCalcTypeCode.Boolean:
                    return () => (object)(ToLambda<Boolean>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.Char:
                    return () => (object)(ToLambda<Char>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.SByte:
                    return () => (object)(ToLambda<SByte>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.Byte:
                    return () => (object)(ToLambda<Byte>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.Int16:
                    return () => (object)(ToLambda<Int16>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.UInt16:
                    return () => (object)(ToLambda<UInt16>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.Int32:
                    return () => (object)(ToLambda<Int32>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.UInt32:
                    return () => (object)(ToLambda<UInt32>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.Int64:
                    return () => (object)(ToLambda<Int64>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.UInt64:
                    return () => (object)(ToLambda<UInt64>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.Single:
                    return () => (object)(ToLambda<Single>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.Double:
                    return () => (object)(ToLambda<Double>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.Decimal:
                    return () => (object)(ToLambda<Decimal>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.DateTime:
                    return () => (object)(ToLambda<DateTime>()());
                case Build_IT_NCalc.Enums.NCalcTypeCode.String:
                    return ToLambda<string>();
                case Build_IT_NCalc.Enums.NCalcTypeCode.Unit:
                    return ToLambda<ValueUnit>();
                case Build_IT_NCalc.Enums.NCalcTypeCode.Void:
                    return () =>
                    {
                        ToLambda()();
                        return null;
                    };
                default:
                    throw new NotImplementedException("No implementation for the specified type.");
            }
        }

        public object Evaluate()
        {
            if (HasErrors())
            {
                throw new EvaluationException(Error, ErrorException);
            }

            if (ParsedExpression == null)
            {
                ParsedExpression = Compile(OriginalExpression, (Options & EvaluateOptions.NoCache) == EvaluateOptions.NoCache);
            }

            if (!Options.HasFlag(EvaluateOptions.AllowUnitCalculations) && Parameters.Any(p => p.Value is ValueUnit))
                throw new ArgumentException("Unit calculations are not allowed");

            var visitor = new EvaluationVisitor(Options, this);
            visitor.EvaluateFunction += EvaluateFunction;
            visitor.EvaluateParameter += EvaluateParameter;
            visitor.Parameters = _parameterNames
                .TakeWhile(x => x != null)
                .Zip(_parameterValues, (name, value) => new { name, value })
                .ToDictionary(val => val.name, val => val.value);

            // Add a "null" parameter which returns null if configured to do so
            // Configured as an option to ensure no breaking changes for historical use
            if ((Options & EvaluateOptions.AllowNullParameter) == EvaluateOptions.AllowNullParameter && !visitor.Parameters.ContainsKey("null"))
            {
                visitor.Parameters["null"] = null;
            }

            // if array evaluation, execute the same expression multiple times
            if ((Options & EvaluateOptions.IterateParameters) == EvaluateOptions.IterateParameters)
            {
                int size = -1;
                ParametersBackup = Parameters.ToDictionary(x => x.Key, x => x.Value);

                ParameterEnumerators = new Dictionary<string, IEnumerator>();

                foreach (object parameter in Parameters.Values)
                {
                    if (parameter is IEnumerable)
                    {
                        int localsize = 0;
                        foreach (object o in (IEnumerable)parameter)
                        {
                            localsize++;
                        }

                        if (size == -1)
                        {
                            size = localsize;
                        }
                        else if (localsize != size)
                        {
                            throw new EvaluationException("When IterateParameters option is used, IEnumerable parameters must have the same number of items");
                        }
                    }
                }

                foreach (string key in Parameters.Keys)
                {
                    var parameter = Parameters[key] as IEnumerable;
                    if (parameter != null)
                    {
                        ParameterEnumerators.Add(key, parameter.GetEnumerator());
                    }
                }

                var results = new List<object>();
                for (int i = 0; i < size; i++)
                {
                    foreach (string key in ParameterEnumerators.Keys)
                    {
                        IEnumerator enumerator = ParameterEnumerators[key];
                        enumerator.MoveNext();
                        var index = Array.IndexOf(_parameterNames, key);
                        _parameterValues[index] = enumerator.Current;
                    }

                    visitor.Parameters = _parameterNames
                        .TakeWhile(x => x != null)
                        .Zip(_parameterValues, (name, value) => new { name, value })
                        .ToDictionary(val => val.name, val => val.value);

                    ParsedExpression.Accept(visitor);
                    results.Add(visitor.Result);
                }

                return results;
            }

            ParsedExpression.Accept(visitor);
            return visitor.Result;

        }

        public IEnumerable<string> GetMissingParameters()
            => GetUsedParameterNamesFromExpression().Where(p => !Parameters.ContainsKey(p));

        public void SetParameters(IReadOnlyDictionary<string, object> parameters)
        {
            _parameterNames = parameters.Keys.ToArray();
            _parameterValues = parameters.Values.ToArray();
        }

        public void SetParameter(string key, object value)
        {
            var index = Array.IndexOf(_parameterNames, key);
            if (index < 0)
            {
                AddParameter(key, value);
                return;
            }

            if (!CheckParameter(index, value))
                throw new ArgumentException("Couldn't change type. Was " + _parameterValues[index]?.GetType()?.Name + ". Is " + value?.GetType()?.Name);

            _parameterValues[index] = value;
        }

        private bool CheckParameter(int index, object value)
        {
            var previousParameter = _parameterValues[index];
            if (value is null || previousParameter is null)
                return true;

            return value.GetType() == previousParameter.GetType();
        }

        public void SetParameter(int index, object value)
        {
            if (!CheckParameter(index, value))
                throw new ArgumentException("Couldn't change type. Was " + _parameterValues[index]?.GetType()?.Name + ". Is " + value?.GetType()?.Name);

            _parameterValues[index] = value;
        }

        public int AddParameter(string key, object value)
        {
            if (_currentParameterIndex >= _parameterNames.Length)
            {
                Resize();
            }

            _parameterNames[_currentParameterIndex] = key;
            _parameterValues[_currentParameterIndex] = value;

            return (int)_currentParameterIndex++;
        }

        public void SetAdditionalFunction(string fuctionName, Func<FunctionArgs, object> function)
        {
            EvaluateFunction += delegate (string name, FunctionArgs args)
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(fuctionName))
                    return;

                if (name == fuctionName || Options.HasFlag(EvaluateOptions.IgnoreCase) && name.ToLower() == fuctionName.ToLower())
                    args.Result = function.Invoke(args);
            };
        }

        public bool ContainsParameter(string name)
            => GetUsedParameterNamesFromExpression().Any(p => p == name);

        public IEnumerable<string> GetUsedParameterNamesFromExpression()
        {
            if (HasErrors())
                throw new InvalidOperationException("Expression has some errors. See: " + Error);

            var parameterCounter = new ParameterCounterExpressionVisitor();
            ParsedExpression.Accept(parameterCounter);

            return parameterCounter.Parameters;
        }

        public ValueUnit ValueUnit(double value, params string[] units)
        {
            if (Options.HasFlag(EvaluateOptions.AllowUnitCalculations))
                return new ValueUnit(value, units);

            throw new InvalidOperationException("Unit calculations are not allowed");
        }

        private void Resize()
        {
            var names = _parameterNames;
            _parameterNames = new string[names.Length + 10];
            Array.Copy(names, _parameterNames, names.Length);
            var values = _parameterValues;
            _parameterValues = new object[values.Length + 10];
            Array.Copy(values, _parameterValues, values.Length);
        }

        internal void RegisterUnit<TNewUnit>() where TNewUnit: Unit, new()
        {
            UnitRegistry.Instance.Register<TNewUnit>();
        }
    }
}
