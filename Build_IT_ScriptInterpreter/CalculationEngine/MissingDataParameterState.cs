using Build_IT_ScriptInterpreter.CalculationEngine.Visitors;
using Build_IT_ScriptInterpreter.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.CalculationEngine
{
    public class MissingDataParameterState : IParameterState
    {
        private readonly IEnumerable<string> _neededParameters;

        public string Name { get; }
        public bool CalculationFinished { get; } = false;

        public Func<object> Calculate { get; }
        public ExpressionEvaluator Expression { get; }

        internal MissingDataParameterState(string name, Func<object> calculate, ExpressionEvaluator expression, IEnumerable<string> neededParameters)
        {
            Name = name;
            Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            _neededParameters = neededParameters;
        }

        public void Accept(ParameterStateVisitor parameterStateVisitor)
        {
            parameterStateVisitor.Visit(this);
        }
        public T Accept<T>(ParameterStateVisitor<T> parameterStateVisitor)
        {
            return parameterStateVisitor.Visit(this);
        }

        public bool CheckAllNeededParameters(IEnumerable<InputDataParameter> inputDataParameters)
        {
            return _neededParameters.All(p => inputDataParameters.Any(idp => idp.Name == p));
        }

        public IParameterState CheckState(IEnumerable<InputDataParameter> inputDataParameters)
        {
            throw new NotImplementedException();
        }
    }
}
