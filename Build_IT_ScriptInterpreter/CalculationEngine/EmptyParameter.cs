using Build_IT_ScriptInterpreter.CalculationEngine.Visitors;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.CalculationEngine
{
    public class EmptyParameter : IParameterState
    {
        public static EmptyParameter New  => new EmptyParameter();
        public bool CalculationFinished { get; } = true;

        public IParameterState CheckState(IEnumerable<InputDataParameter> inputDataParameters)
        {
            return this;
        }

        public void Accept(ParameterStateVisitor parameterStateVisitor)
        {
            parameterStateVisitor.Visit(this);
        }
        public T Accept<T>(ParameterStateVisitor<T> parameterStateVisitor)
        {
            return parameterStateVisitor.Visit(this);
        }
    }
}
