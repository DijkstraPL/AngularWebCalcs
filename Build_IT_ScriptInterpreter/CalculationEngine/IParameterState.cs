using Build_IT_ScriptInterpreter.CalculationEngine.Visitors;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.CalculationEngine
{
    public interface IParameterState
    {
        public bool CalculationFinished { get; }

        IParameterState CheckState(IEnumerable<InputDataParameter> inputDataParameters);

        void Accept(ParameterStateVisitor parameterStateVisitor);
        T Accept<T>(ParameterStateVisitor<T> parameterStateVisitor);
    }
}
