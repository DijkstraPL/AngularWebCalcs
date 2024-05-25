using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Scripts
{
    public interface ICalculable
    {
        public string Name { get; }
        public CalculationState CalculationState { get; }

        public void Initialize();
        public CalculatedParameter2 Calculate(IEnumerable<CalculatedParameter2> parameters);

        public event EventHandler<CalculatedParameter2> Calculated;

        void OnTick(object sender, IEnumerable<CalculatedParameter2> availableParameters);
    }
}
