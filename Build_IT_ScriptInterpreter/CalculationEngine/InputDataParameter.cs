using Build_IT_ScriptInterpreter.CalculationEngine.Visitors;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.CalculationEngine
{
    public class InputDataParameter<T> : InputDataParameter
    {
        public new T Value { get; }

        public InputDataParameter(string name, T value) : base(name, value)
        {
            Value = value;
        }
    }

    public class InputDataParameter : IParameterState
    {
        public string Name { get; }
        public object Value { get; }

        public bool CalculationFinished { get; } = true;

        public InputDataParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

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
