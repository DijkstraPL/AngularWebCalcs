using Build_IT_ScriptInterpreter.CalculationEngine.Visitors;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.CalculationEngine
{
    public class UnknownErrorParameterState : IParameterState
    {
        public bool CalculationFinished { get; } = true;

        public void Accept(ParameterStateVisitor parameterStateVisitor)
        {
            parameterStateVisitor.Visit(this);
        }
        public T Accept<T>(ParameterStateVisitor<T> parameterStateVisitor)
        {
            return parameterStateVisitor.Visit(this);
        }

        public IParameterState CheckState(IEnumerable<InputDataParameter> inputDataParameters)
        {
            throw new NotImplementedException();
        }
    }
}
