using Build_IT_ScriptInterpreter.CalculationEngine.Visitors;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.CalculationEngine
{
    public class CalculatedParameterState : IParameterState
    {
        public string Name { get; }
        public object Result { get; }

        public bool CalculationFinished => throw new NotImplementedException();

        internal CalculatedParameterState(string name, object result)
        {
            Name = name;
            Result = result;
        }

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
