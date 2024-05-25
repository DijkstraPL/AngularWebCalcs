using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Scripts
{
    public class InputParameter : ICalculable
    {
        public string Name { get; }
        private readonly object _value;

        public InputParameter(string name, object value)
        {
            Name = name;
            _value = value;
        }

        public CalculationState CalculationState { get; private set; } = CalculationState.CanBeCalculated;

        public event EventHandler<CalculatedParameter2> Calculated;

        public CalculatedParameter2 Calculate(IEnumerable<CalculatedParameter2> parameters)
        {
            var result = new CalculatedParameter2(Name, _value);
            Calculated?.Invoke(this, result);

            CalculationState = CalculationState.Calculated;
            return result;
        }

        public void Initialize()
        {
            CalculationState = CalculationState.CanBeCalculated;
        }

        public void OnTick(object sender, IEnumerable<CalculatedParameter2> availableParameters)
        {
        }
    }
}
