using Build_IT_ScriptInterpreter.CalculationEngine.Visitors;
using Build_IT_ScriptInterpreter.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreter.CalculationEngine
{
    public class CalculationInputParameter : IParameterState
    {
        public string Name { get; }
        public string Value { get; }
        public bool Initialized { get; private set; }
        public bool CalculationFinished { get; } = false;
        public Func<object> Lambda { get; private set; }

        public ExpressionEvaluator Expression { get; private set; }
        public IEnumerable<string> NeededParameters { get; private set; }

        private readonly Type _resultType;

        public CalculationInputParameter(string name, string value, Type resultType)
        {
            Name = name;
            Value = value;
            _resultType = resultType;
        }

        public void Initialize()
        {
            Expression = ExpressionEvaluator.Create(Value);
            NeededParameters = Expression.GetUsedParameterNamesFromExpression();
            Lambda = Expression.ToLambda(_resultType);

            Initialized = true;
        }

        public bool CheckAllNeededParameters(IEnumerable<InputDataParameter> inputDataParameters)
        {
            return NeededParameters.All(p => inputDataParameters.Any(idp => idp.Name == p));
        }

        internal bool HasErrors()
        {
            return Expression.HasErrors();
        }

        public void Accept(ParameterStateVisitor parameterStateVisitor)
        {
            parameterStateVisitor.Visit(this);
        }
        public T Accept<T>(ParameterStateVisitor<T> parameterStateVisitor)
        {
            return parameterStateVisitor.Visit(this);
        }

        public IParameterState CheckState(IEnumerable<InputDataParameter> inputDataParameters)
        {
            throw new NotImplementedException();
        }
    }
}
