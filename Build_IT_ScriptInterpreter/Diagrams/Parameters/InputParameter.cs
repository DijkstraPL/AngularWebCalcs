using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Diagrams.Parameters
{
    public class InputParameter<T> : InputParameter
    {
        public new T Value => (T)base.Value;

        public InputParameter(string name, T value = default) : base(name, value)
        {
        }

        public override sealed CalculatedParameter Calculate() 
            => new CalculatedParameter<T>(Name, Value);
        public override sealed CalculatedParameter CalculateNoLambda()
            => new CalculatedParameter(Name, Value);

        public void SetValue(T value)
        {
            base.Value = value;
        }
    }

    public class InputParameter
    {
        public string Name { get; }
        public object Value { get; protected set; }

        public InputParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public virtual CalculatedParameter Calculate() 
            => new CalculatedParameter(Name, Value);

        public virtual CalculatedParameter CalculateNoLambda()
            => new CalculatedParameter(Name, Value);

        public void SetValue(object value)
        {
            Value = value;
        }
    }
}