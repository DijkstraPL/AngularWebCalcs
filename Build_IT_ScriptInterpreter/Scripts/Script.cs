using Build_IT_ScriptInterpreter.Expressions;
using Build_IT_ScriptInterpreter.Parameters;
using Build_IT_ScriptInterpreter.Parameters.Enums;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Build_IT_ScriptInterpreterTests")]
namespace Build_IT_ScriptInterpreter.Scripts
{
    public class Script
    {
        #region Properties

        public string Name { get; }

        private readonly IList<Parameter> _parameters;
        public IEnumerable<Parameter> Parameters => _parameters.OrderBy(p => p.Number);

        #endregion // Properties

        public Script(string name, params Parameter[] parameters)
        {
            Name = name;
            _parameters = parameters;
        }

        #region Public_Methods

        public Parameter GetParameterByName(string name)
            => Parameters.FirstOrDefault(p => p.Name == name);

        public IEnumerable<CalculatedParameter> CalculateScript(params (string name, object value)[] parameters)
        {
            var calculatedParameters = new List<CalculatedParameter>();
            var calculatedParametersDict = new Dictionary<string, object>();
            foreach (var parameter in Parameters)
            {
                var calculationParameter = parameter;
                var parameterDataInput = parameters.FirstOrDefault(p => p.name == parameter.Name);
                if (parameterDataInput != default)
                    calculationParameter = new InputParameter<object>(
                        parameter.Number, 
                        parameter.Name, 
                        parameterDataInput.value, 
                        parameter.VisibilityValidator, 
                        parameter.DataValidator);

                if (!calculationParameter.CheckVisibility(calculatedParametersDict))
                    continue;
                var calculatedParameter = calculationParameter.Calculate(calculatedParametersDict);
                if (calculatedParameter is null)
                    continue;

                if (!calculatedParameter.IsValid)
                    throw new EvaluationException("Data is not valid for parameter " + calculationParameter.Name);

                calculatedParameters.Add(calculatedParameter);
                calculatedParametersDict.Add(calculatedParameter.Name, calculatedParameter.CalculatedValue);
            }
            return calculatedParameters;
        }

        #endregion // Public_Methods
        public bool IsValid(Parameter parameter, IEnumerable<Parameter> calculatedParameters)
        {
            var value = parameter.DataValidator?.ToString();
            if (string.IsNullOrWhiteSpace(value))
                return true;

            // var expressionEvaluator = ExpressionEvaluator.Create(value, parameters);
            try
            {
                throw new NotImplementedException();
                //       return (bool)expressionEvaluator.Evaluate();
            }
            catch (ArgumentException)
            {
                return true;
            }
        }
    }
}
