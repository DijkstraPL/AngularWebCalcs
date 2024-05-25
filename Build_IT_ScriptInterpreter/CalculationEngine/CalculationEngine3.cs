using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Build_IT_ScriptInterpreter.CalculationEngine
{
    public class CalculationEngine3
    {
        private readonly IEnumerable<CalculationInputParameter> _calculationParameters;

        public CalculationEngine3(IEnumerable<CalculationInputParameter> calculationParameters)
        {
            _calculationParameters = calculationParameters;
        }

        public IEnumerable<InputDataParameter> Calculate(IEnumerable<InputDataParameter> inputDataParameters)
        {
           return CalculationTick(new List<InputDataParameter>(inputDataParameters), _calculationParameters);
        }

        private List<InputDataParameter> CalculationTick(List<InputDataParameter> inputDataParameters, IEnumerable<IParameterState> calculationParameters)
        {
            ConcurrentBag<IParameterState> parameters = new();
            Parallel.ForEach(calculationParameters, p =>
            {
                if (p.CalculationFinished)
                    return;
                var newState = p.CheckState(inputDataParameters);
                if (newState is InputDataParameter inputDataParameter)
                    inputDataParameters.Add(inputDataParameter);
                parameters.Add(newState);
            });
            if (parameters.Count > 0)
               return CalculationTick(inputDataParameters, parameters);
            return inputDataParameters;
        }

        internal void Intialize()
        {
            Parallel.ForEach(_calculationParameters, p => p.Initialize());
        }
    }
}
