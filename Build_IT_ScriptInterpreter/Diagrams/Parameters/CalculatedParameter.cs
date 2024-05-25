using System;

namespace Build_IT_ScriptInterpreter.Diagrams.Parameters
{
    public class CalculatedParameter<T> : CalculatedParameter
    {
        public new T Value { get; }

        public CalculatedParameter(string name, T value) : base(name, value)
        {
            Value = value;
        }
    }

    public class CalculatedParameter
    {
        public string Name { get; }
        public object Value { get; }

        public ParameterMetadata ParameterMetadata { get; }

        public CalculatedParameter(string name, object value)
        {
            Name = name;
            Value = value;

            ParameterMetadata = new ParameterMetadata(name, value.GetTypeCode());
        }
    }
}
