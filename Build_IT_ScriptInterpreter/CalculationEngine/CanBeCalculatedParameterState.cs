using Build_IT_ScriptInterpreter.CalculationEngine.Visitors;
using Build_IT_ScriptInterpreter.Expressions;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.CalculationEngine
{
    public class CanBeCalculatedParameterState : IParameterState
    {
        public string Name { get; }
        public Func<object> Calculate { get; }

        public bool CalculationFinished { get; } = false;

        private readonly ExpressionEvaluator _expression;
        private readonly CalculationInputParameter _calculationParameter;

        internal CanBeCalculatedParameterState(string name, Func<object> calculate, ExpressionEvaluator expression)
        {
            Name = name;
            Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
            _expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public void Accept(ParameterStateVisitor parameterStateVisitor)
        {
            parameterStateVisitor.Visit(this);
        }
        public T Accept<T>(ParameterStateVisitor<T> parameterStateVisitor)
        {
            return parameterStateVisitor.Visit(this);
        }

        internal void SetParameter(string name, object value)
        {
            _expression.SetParameter(name, value);
        }

        public IParameterState CheckState(IEnumerable<InputDataParameter> inputDataParameters)
        {
            throw new NotImplementedException();
        }
    }
}
