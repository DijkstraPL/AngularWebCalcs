using Build_IT_ScriptInterpreter.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Scripts
{

    public class CalculationParameter : ICalculable
    {
        public string Name { get; }
        public bool Initialized { get; private set; }

        public CalculationState CalculationState { get; private set; } = CalculationState.MissingParameters;

        private readonly string _equation;
        private readonly Type _expectedType;
        private ExpressionEvaluator _expression;
        private IEnumerable<string> _neededParameters;
        private Func<object> _lambda;

        public event EventHandler<CalculatedParameter2> Calculated;

        public CalculationParameter(string name, string equation, Type expectedType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

            if (string.IsNullOrWhiteSpace(equation))
                throw new ArgumentException($"'{nameof(equation)}' cannot be null or whitespace.", nameof(equation));

            Name = name;
            _equation  = equation;
            _expectedType = expectedType ?? throw new ArgumentNullException(nameof(expectedType));
        }

        public CalculatedParameter2 Calculate(IEnumerable<CalculatedParameter2> parameters)
        {
            CalculationState = CalculationState.Calculating;

            if (!Initialized)
                Initialize();

            foreach (var parameter in parameters)
                _expression.SetParameter(parameter.Name, parameter.Value);

            var result = _lambda();
            var calculatedParameter = new CalculatedParameter2(Name, result);

            Calculated?.Invoke(this, calculatedParameter);
            CalculationState = CalculationState.Calculated;

            return calculatedParameter;
        }

        public void Initialize()
        {
            _expression = ExpressionEvaluator.Create(_equation);
            _neededParameters = _expression.GetUsedParameterNamesFromExpression();
            _lambda = _expression.ToLambda(_expectedType);

            Initialized = true;
        }

        public void OnTick(object sender, IEnumerable<CalculatedParameter2> availableParameters)
        {
            if (!Initialized)
                Initialize();

            if (CalculationState == CalculationState.Calculated)
                return;

            if (_expression.HasErrors())
                CalculationState = CalculationState.UnknownError;

            if (_neededParameters.All(np => availableParameters.Any(ap => np == ap.Name)))
                CalculationState = CalculationState.CanBeCalculated;
        }
    }
}
