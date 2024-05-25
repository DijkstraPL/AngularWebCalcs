using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Expressions.Functions.Interfaces;
using Build_IT_ScriptInterpreter.Expressions.Interfaces;
using Build_IT_ScriptInterpreter.Expressions.Parameters.Interfaces;
using NCalc;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Build_IT_ScriptInterpreter.Expressions
{
    public class ExpressionEvaluator : IExpressionEvaluator
    {
        #region Properties

        public ICollection<string> Functions { get; private set; }
        public IReadOnlyDictionary<string, object> Parameters => _expression.Parameters;

        #endregion // Properties

        #region Fields

        private readonly Expression _expression;

        #endregion // Fields

        #region Factories

        public static ExpressionEvaluator Create(
            string expression, 
            IReadOnlyDictionary<string, object> parameters = null, 
            EvaluateOptions evaluateOptions = EvaluateOptions.IgnoreCase | EvaluateOptions.AllowUnitCalculations)
            => new ExpressionEvaluator(expression, parameters, evaluateOptions);

        #endregion // Factories

        #region Constructors

        private ExpressionEvaluator(string expression,
            IReadOnlyDictionary<string, object> parameters = null,
            EvaluateOptions evaluateOptions = EvaluateOptions.IgnoreCase | EvaluateOptions.AllowUnitCalculations)
            : this(new Expression(expression, evaluateOptions), parameters)
        {

        }

        private ExpressionEvaluator(
            Expression expression,
            IReadOnlyDictionary<string, object> parameters = null)
        {
            Functions = new List<string>();
            _expression = expression;

            if (parameters is null)
                parameters = new Dictionary<string, object>();

            SetAdditionalParameters(parameters);
            SetParameters(parameters);
            SetAdditionalFunctions();
        }

        #endregion // Constructors

        #region Public_Methods

        public object Evaluate()
        {
            try
            {
                return _expression.Evaluate();
            }
            catch (EvaluationException ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public IEnumerable<Unit> SetUnits(string unit)
        {
            if (UnitRegistry.Instance.UnitNames.Contains(unit))
                yield return UnitRegistry.Instance.RegisteredUnits[unit](1);
        }

        internal bool HasErrors()
        {
            return _expression.HasErrors();
        }

        public Func<object> ToLambda(Type expectedType)
        {
            return _expression.ToLambda(expectedType);
        }

        public Func<TResult> ToLambda<TResult>()
        {
           return _expression.ToLambda<TResult>();
        }

        public Func<TContext, TResult> ToLambda<TContext, TResult>() where TContext : class
        {
            return _expression.ToLambda<TContext, TResult>();
        }

        public void SetParameter(string name, object value)
        {
            _expression.SetParameter(name, value);
        }

        public IEnumerable<string> GetUsedParameterNamesFromExpression()
        {
            return _expression.GetUsedParameterNamesFromExpression();
        }

        #endregion // Public_Methods

        #region Private_Methods

        private IReadOnlyDictionary<string, object> SetAdditionalParameters(
            IReadOnlyDictionary<string, object> parameters)
        {
            var newParameters = new Dictionary<string, object>(parameters);
            var configuration = new ContainerConfiguration().WithAssembly(Assembly.GetExecutingAssembly());

            using (var container = configuration.CreateContainer())
            {
                var customParameters = container.GetExports<ICustomParameter>();
                foreach (var customParameter in customParameters)
                    PopulateParameters(newParameters, customParameter);
            }

            return newParameters;
        }

        private void PopulateParameters(IDictionary<string, object> parameters, ICustomParameter customParameter)
        {
            foreach (var name in customParameter.Names)
                if (!parameters.ContainsKey(name))
                    parameters.Add(name, customParameter.Value);
        }

        private void SetParameters(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters != null)
                _expression.SetParameters(parameters);
        }


        private void SetAdditionalFunctions()
        {
            var configuration = new ContainerConfiguration().WithAssembly(Assembly.GetExecutingAssembly());

            using (var container = configuration.CreateContainer())
            {
                var functions = container.GetExports<IFunction>();
                foreach (var function in functions)
                {
                    _expression.SetAdditionalFunction(function.Name, function.Function);
                    Functions.Add(function.Name);
                }
            }
        }
        
        #endregion // Private_Methods
    }
}
