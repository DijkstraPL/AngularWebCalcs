using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Diagrams.Parameters
{
    public class CalculationParameter
    {
        public string Name { get; }
        public string Value { get; }
        public bool Initialized { get; private set; }

        public ExpressionEvaluator Expression { get; private set; }
        public IEnumerable<string> NeededParameters { get; private set; }

        private Func<object> _lambda;
        private readonly Type _resultType;

        public CalculationParameter(string name, string value)
        {
            Name = name;
            Value = value;
            _resultType = typeof(ValueUnit);
        }
        public CalculationParameter(string name, string value, Type resultType)
        {
            Name = name;
            Value = value;
            _resultType = resultType;
        }

        public void Initialize(IEnumerable<ParameterMetadata> parameters)
        {
            Expression = ExpressionEvaluator.Create(Value);
            NeededParameters = Expression.GetUsedParameterNamesFromExpression();
            foreach (var parameter in parameters)
                Expression.SetParameter(parameter.Name, parameter.Type.IsValueType ? Activator.CreateInstance(parameter.Type) : null);

            _lambda = Expression.ToLambda(_resultType);

            Initialized = true;
        }

        public bool CheckAllNeededParameters(IEnumerable<InputParameter> inputDataParameters)
        {
            return NeededParameters.All(p => inputDataParameters.Any(idp => idp.Name == p));
        }

        internal bool HasErrors()
        {
            return Expression.HasErrors();
        }

        public CalculatedParameter Calculate(IEnumerable<CalculatedParameter> calculatedParameters)
        {
            if (!Initialized)
                Initialize(calculatedParameters.Select(cp => cp.ParameterMetadata));

            foreach (var parameter in calculatedParameters)
                Expression.SetParameter(parameter.Name, parameter.Value);

            var result = _lambda();
            var calculatedParameter = new CalculatedParameter(Name, result);
            return calculatedParameter;
        }
        public  CalculatedParameter CalculateNoLambda(IEnumerable<CalculatedParameter> calculatedParameters)
        {
            if (!Initialized)
                Expression = ExpressionEvaluator.Create(Value);

            foreach (var parameter in calculatedParameters)
                Expression.SetParameter(parameter.Name, parameter.Value);

            var result = Expression.Evaluate();
            var calculatedParameter = new CalculatedParameter(Name, result);
            return calculatedParameter;
        }

    }
}