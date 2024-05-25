using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Expressions;
using Build_IT_ScriptInterpreter.Expressions.Interfaces;
using Build_IT_ScriptInterpreter.Parameters.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Build_IT_ScriptInterpreter.Parameters
{
    public class CalculatedParameter : Parameter
    {
        public object CalculatedValue { get; }
        public bool IsValid { get;  }
        public bool IsVisible { get;  }

        private readonly Parameter _parameter;

        public CalculatedParameter(Parameter parameter, bool isValid, bool isVisible, object calculatedValue)
            : base(parameter)
        {
            _parameter = parameter ?? throw new System.ArgumentNullException(nameof(parameter));
            IsValid = isValid;
            IsVisible = isVisible;
            CalculatedValue = calculatedValue ?? throw new ArgumentNullException(nameof(calculatedValue));
        }
    }

    public class InputParameter<T> : Parameter<T>
    {
        public InputParameter(
            int number,
            string name,
            T value,
            string visibilityValidator = null,
            string dataValidator = null)
            : base(number, name, value, visibilityValidator, dataValidator)
        {
        }

        internal override CalculatedParameter Calculate(Dictionary<string, object> calculatedParameters)
        {
            var isVisible = CheckVisibility(calculatedParameters);
            if (!isVisible)
                return null;
            var isValid = CheckData(calculatedParameters);
            return new CalculatedParameter(this, isValid, isVisible, this.Value);
        }
    }

    public class Parameter<T> : Parameter 
    {
        public new T Value { get;  }

        public Parameter(
            int number,
            string name,
            T value,
            string visibilityValidator = null,
            string dataValidator = null)
            :base(number, name, value, visibilityValidator, dataValidator)
        {
            Value = value;
        }
    }

    public abstract class Parameter
    {
        public int Number { get; }
        public string Name { get; }
        public object Value { get;  }
        public string VisibilityValidator { get; }
        public string DataValidator { get;  }

        public Parameter(
            int number,
            string name,
            object value,
            string visibilityValidator = null,
            string dataValidator = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Number = number;
            Value = value;
            VisibilityValidator = visibilityValidator;
            DataValidator = dataValidator;
        }

        public Parameter(Parameter parameter)
            : this(parameter.Number, parameter.Name, parameter.Value, 
                  parameter.VisibilityValidator, parameter.DataValidator)
        {

        }

        internal virtual CalculatedParameter Calculate(Dictionary<string, object> calculatedParameters)
        {
            var isVisible = CheckVisibility(calculatedParameters);

            if (!isVisible)
                return null;

            var expressionEvaluator = ExpressionEvaluator.Create(Value?.ToString(), calculatedParameters);
            var result = expressionEvaluator.Evaluate();

            var isValid = CheckData(calculatedParameters);

            return new CalculatedParameter(this, isVisible, isValid, result);
        }

        internal virtual bool CheckVisibility(Dictionary<string, object> calculatedParameters)
        {
            if (string.IsNullOrWhiteSpace(VisibilityValidator))
                return true;

            var visibilityExpressionEvaluator = ExpressionEvaluator.Create(VisibilityValidator, calculatedParameters);
            var isVisible = visibilityExpressionEvaluator.Evaluate();

            if (bool.TryParse(isVisible?.ToString(), out bool isVisibleResult))
                return isVisibleResult;
            return true;
        }
        internal bool CheckData(Dictionary<string, object> calculatedParameters)
        {
            if (string.IsNullOrWhiteSpace(DataValidator))
                return true;

            var dataExpressionEvaluator = ExpressionEvaluator.Create(DataValidator, calculatedParameters);
            var isValid = dataExpressionEvaluator.Evaluate();

            if (bool.TryParse(isValid?.ToString(), out bool isValidResult))
                return isValidResult;
            return true;
        }
    }


}
