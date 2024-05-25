using Build_IT_CommonTools.Extensions;
using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Expressions.Interfaces;
using Build_IT_ScriptInterpreter.Parameters;
using Build_IT_ScriptInterpreter.Parameters.Enums;
using NCalc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Build_IT_ScriptInterpreter.Scripts
{

    public class CalculationEngine
    {
        private ConcurrentBag<CalculatedParameter2> _calculatedParameters = new ConcurrentBag<CalculatedParameter2>();
        private ConcurrentBag<ICalculable> _parameters = new ConcurrentBag<ICalculable>();

        public event EventHandler<IEnumerable<CalculatedParameter2>> Ticked;

        public void RegisterForCalculation(ICalculable parameter)
        {
            var existingParameter = _parameters.FirstOrDefault(p => p.Name == parameter.Name);
            if (existingParameter is not null)
                _parameters.TryTake(out existingParameter);

            Ticked += parameter.OnTick;
            parameter.Initialize();
            _parameters.Add(parameter);
        }

        private void Tick()
        {
            bool anotherTick = false;
            Parallel.ForEach(_parameters, par =>
            {
                if (par.CalculationState == CalculationState.UnknownError)
                    throw new InvalidOperationException("Something goes wrong during calculation.");
                else if (par.CalculationState == CalculationState.CanBeCalculated)
                {
                    var result = par.Calculate(_calculatedParameters);
                    _calculatedParameters.Add(result);
                    anotherTick = true;
                }
                else
                    return;
            });

            Ticked?.Invoke(this, _calculatedParameters);

            if (anotherTick)
                Tick();
        }

        public IEnumerable<CalculatedParameter2> Calculate(IEnumerable<InputParameter> inputParameters)
        {
            Clear();

            foreach (var inputParameter in inputParameters)
                RegisterForCalculation(inputParameter);

            Tick();

            return _calculatedParameters.ToList();


            //var calculationParameters = new List<ICalculable>();
            //calculationParameters.AddRange(inputParameters);
            //calculationParameters.AddRange(parameters);

            //List<CalculatedParameter2> calculatedParameters = new();
            //foreach (var parameter in calculationParameters)
            //{
            //    var calculatedParameter = parameter.Calculate(calculatedParameters);
            //    calculatedParameters.Add(calculatedParameter);
            //}

            //return calculatedParameters;
        }

        private void Clear()
        {
            _calculatedParameters.Clear();
        }




























        //    #region Fields

        //    private readonly ICollection<Parameter> _scriptParameters;

        //    #endregion // Fields

        //    #region Constructors

        //    public CalculationEngine(ICollection<Parameter> scriptParameters)
        //    {
        //        _scriptParameters = scriptParameters;
        //    }

        //    #endregion // Constructors

        //    #region Public_Methods

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    /// <param name="parameterValues">In format "[a]=555,[b]=333"</param>
        //    public void CalculateFromText(string parameterValues)
        //    {
        //        Dictionary<string, object> parameters = ExtractParameters(parameterValues);

        //        SetEditableValues(parameters);
        //        Calculate(parameters);
        //    }


        //    public void Calculate(IDictionary<string, object> parameters)
        //    {
        //        SetVisibilityValidators();
        //        SetStaticParameters(parameters);
        //        ValidateEditableParameters(parameters);
        //        CalculateParameters(parameters);
        //    }

        //    public bool CheckVisibility(string parameterName, IDictionary<string, object> parameters)
        //        => IsVisible(_scriptParameters.SingleOrDefault(p => p.Name == parameterName), parameters);

        //    public bool CalculatePrediction(string equation, IDictionary<string, object> parameters)
        //    {
        //        if (string.IsNullOrWhiteSpace(equation))
        //            return true;

        //        var expressionEvaluator = ExpressionEvaluator.Create(equation, parameters);
        //        try
        //        {
        //            return (bool)expressionEvaluator.Evaluate();
        //        }
        //        catch (ArgumentException)
        //        {
        //            return true;
        //        }
        //    }

        //    public bool IsValid(Parameter parameter, IDictionary<string, object> parameters)
        //    {
        //        var value = parameter.DataValidator?.ToString();
        //        if (string.IsNullOrWhiteSpace(value))
        //            return true;

        //        var expressionEvaluator = ExpressionEvaluator.Create(value, parameters);
        //        try
        //        {
        //            return (bool)expressionEvaluator.Evaluate();
        //        }
        //        catch (ArgumentException)
        //        {
        //            return true;
        //        }
        //    }

        //    public bool IsVisible(Parameter parameter, IDictionary<string, object> parameters)
        //    {
        //        var value = parameter.VisibilityValidator?.ToString();
        //        if (string.IsNullOrWhiteSpace(value))
        //            return true;

        //        var expressionEvaluator = ExpressionEvaluator.Create(value, parameters);
        //        try
        //        {
        //            return (bool)expressionEvaluator.Evaluate();
        //        }
        //        catch (ArgumentException)
        //        {
        //            return true;
        //        }
        //    }

        //    #endregion // Public_Methods

        //    #region Private_Methods

        //    private Dictionary<string, object> ExtractParameters(string parameterValues)
        //    {
        //        var parameters = new Dictionary<string, object>();
        //        bool inParameter = false;
        //        string parameterName = string.Empty;
        //        string parameterValue = string.Empty;
        //        foreach (var ch in parameterValues)
        //        {
        //            if (ch == '=')
        //                continue;
        //            else if (ch == '[')
        //                inParameter = true;
        //            else if (ch == ']')
        //                inParameter = false;
        //            else if (ch == '|' && !inParameter)
        //            {
        //                SetParameter(parameters, parameterName, parameterValue);
        //                parameterName = string.Empty;
        //                parameterValue = string.Empty;
        //            }
        //            else if (inParameter)
        //                parameterName += ch;
        //            else
        //                parameterValue += ch;
        //        }
        //        SetParameter(parameters, parameterName, parameterValue);
        //        return parameters;
        //    }

        //    private void SetParameter(IDictionary<string, object> parameters, string parameterName, string parameterValue)
        //    {
        //        var valueType = _scriptParameters.SingleOrDefault(p => p.Name == parameterName).ValueType;

        //        switch (valueType)
        //        {
        //            case ValueTypes.Number:
        //                parameters.Add(parameterName, parameterValue.GetDouble());
        //                break;
        //            case ValueTypes.Text:
        //                parameters.Add(parameterName, parameterValue.ToString());
        //                break;
        //            case ValueTypes.List:
        //                parameters.Add(parameterName, parameterValue
        //                    .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
        //                    .Select(v => v.GetDouble())
        //                    .ToList());
        //                break;
        //            default:
        //                throw new NotSupportedException(nameof(valueType));
        //        }
        //    }

        //    private void SetEditableValues(IDictionary<string, object> parameters)
        //    {
        //        foreach (var parameter in _scriptParameters
        //            .Where(p => (p.ParameterOptions & ParameterOptions.Editable) != 0))
        //        {
        //            if (!string.IsNullOrWhiteSpace(parameter.Value?.ToString()))
        //                parameter.Value = parameters.SingleOrDefault(p => p.Key == parameter.Name).Value;
        //        }
        //    }

        //    private void SetVisibilityValidators()
        //    {
        //        foreach (var parameter in _scriptParameters)
        //            parameter.VisibilityValidator = GetVisibilityValidator(parameter);
        //    }

        //    private string GetVisibilityValidator(Parameter parameter)
        //    {
        //       return parameter.VisibilityValidator?.ToString();
        //    }

        //    private void SetStaticParameters(IDictionary<string, object> parameters)
        //    {
        //        var staticParameters = _scriptParameters
        //            .Where(p => (p.ParameterOptions & ParameterOptions.StaticData) != 0).ToList();
        //        foreach (var parameter in staticParameters)
        //        {
        //            if (parameter.ValueType == ValueTypes.Number)
        //                parameters.TryAdd(parameter.Name,
        //                    double.Parse(parameter.Value.ToString().Replace(',', '.'), CultureInfo.InvariantCulture));
        //            else
        //                parameters.TryAdd(parameter.Name,
        //                    parameter.Value.ToString());

        //        }
        //    }

        //    private void ValidateEditableParameters(IDictionary<string, object> parameters)
        //    {
        //        foreach (var parameter in _scriptParameters
        //            .Where(p => (p.ParameterOptions & ParameterOptions.Editable) != 0))
        //        {
        //            if (!IsValid(parameter, parameters))
        //                throw new ArgumentException("Wrong data", parameter.Name);
        //            if ((parameter.ParameterOptions & ParameterOptions.Calculation) != 0 &&
        //                parameters.ContainsKey(parameter.Name))
        //                parameter.Value = parameters[parameter.Name];
        //        }
        //    }

        //    private void CalculateParameters(IDictionary<string, object> parameters)
        //    {
        //        foreach (var parameter in _scriptParameters.Where(p =>
        //            (p.ParameterOptions & ParameterOptions.Calculation) != 0))
        //        {
        //            if (!IsVisible(parameter, parameters))
        //                continue;

        //            CalculateParameter(parameter, parameters);
        //            if (parameters.ContainsKey(parameter.Name))
        //                parameters[parameter.Name] = parameter.Value;
        //            else
        //                parameters.Add(parameter.Name, parameter.Value);

        //            if (!string.IsNullOrWhiteSpace(parameter.DataValidator?.ToString()))
        //                CalculateDataValidator(parameter, parameters);
        //        }
        //    }

        //    private void CalculateParameter(Parameter parameter, IDictionary<string, object> parameters)
        //    {
        //        var expressionEvaluator = GetExpressionEvaluator(parameter, parameters);
        //        try
        //        {
        //            var result = expressionEvaluator.Evaluate();
        //            if (result is ValueUnit valueUnit)
        //                valueUnit.TransformTo(parameter.Unit, valueUnit.Units.First());

        //            parameter.Value = result;
        //            if (parameter.ValueType == ValueTypes.Number && parameter.Value.GetType() == typeof(String))
        //                parameter.Value = Convert.ToDouble(parameter.Value.ToString().Replace(',', '.'), CultureInfo.InvariantCulture);
        //        }
        //        catch (ArgumentException ex)
        //        {
        //            parameter.Value = ex.Message;
        //        }
        //    }

        //    private  IExpressionEvaluator GetExpressionEvaluator(Parameter parameter, IDictionary<string, object> parameters)
        //    {
        //        const string regex = @"\s+(?=([^']*'[^']*')*[^']*$)";
        //        if (parameter.ValueType == ValueTypes.List)
        //            return ExpressionEvaluator.Create(
        //                Regex.Replace(parameter.Value.ToString(), regex, string.Empty),
        //                parameters,
        //                NCalc.EvaluateOptions.IgnoreCase | NCalc.EvaluateOptions.IterateParameters);

        //        return ExpressionEvaluator.Create(
        //            Regex.Replace(parameter.Value.ToString(), regex, string.Empty), parameters);
        //    }

        //    private void CalculateDataValidator(Parameter parameter, IDictionary<string, object> parameters)
        //    {
        //        parameter.DataValidator = IsValid(parameter, parameters);
        //    }

        //    #endregion // Private_Methods
    }
}
