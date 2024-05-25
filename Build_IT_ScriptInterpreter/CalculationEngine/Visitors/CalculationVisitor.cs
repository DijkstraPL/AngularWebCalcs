using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.CalculationEngine.Visitors
{
    public class CalculationVisitor : ParameterStateVisitor<IParameterState>
    {
        private readonly List<InputDataParameter> _inputDataParameters;

        public CalculationVisitor(IEnumerable<InputDataParameter> inputDataParameters)
        {
            _inputDataParameters = inputDataParameters.ToList();
        }

        public override IParameterState Visit(CalculatedParameterState calculatedParameterState)
        {
            var inputDataParameter = new InputDataParameter(calculatedParameterState.Name, calculatedParameterState.Result);
            _inputDataParameters.Add(inputDataParameter);
            return inputDataParameter;
        }

        public override IParameterState Visit(CanBeCalculatedParameterState canBeCalculatedParameterState)
        {
            foreach (var inputDataParameter in _inputDataParameters)
                canBeCalculatedParameterState.SetParameter(inputDataParameter.Name, inputDataParameter.Value);

            var result = canBeCalculatedParameterState.Calculate();

            return new InputDataParameter(canBeCalculatedParameterState.Name, result);
        }

        public override IParameterState Visit(CalculationInputParameter calculationInputParameter)
        {
            if (!calculationInputParameter.Initialized)
                calculationInputParameter.Initialize();

            if (calculationInputParameter.HasErrors())
                return new UnknownErrorParameterState();

            if (calculationInputParameter.CheckAllNeededParameters(_inputDataParameters))
                return new CanBeCalculatedParameterState(calculationInputParameter.Name, calculationInputParameter.Lambda, calculationInputParameter.Expression);

            return new MissingDataParameterState(calculationInputParameter.Name, calculationInputParameter.Lambda, calculationInputParameter.Expression, calculationInputParameter.NeededParameters);
        }

        public override IParameterState Visit(EmptyParameter emptyParameter)
        {
            return emptyParameter;
        }

        public override IParameterState Visit(InputDataParameter inputDataParameter)
        {
            _inputDataParameters.Add(inputDataParameter);
            return inputDataParameter;
        }

        public override IParameterState Visit(MissingDataParameterState missingDataParameterState)
        {
            if (missingDataParameterState.CheckAllNeededParameters(_inputDataParameters))
                return new CanBeCalculatedParameterState(missingDataParameterState.Name, missingDataParameterState.Calculate, missingDataParameterState.Expression);

            return missingDataParameterState;
        }

        public override IParameterState Visit(UnknownErrorParameterState unknownErrorParameterState)
        {
            throw new InvalidOperationException("Equation has some errors.");
        }
    }
}
