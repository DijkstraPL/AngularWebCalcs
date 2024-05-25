using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreter.Expressions.Interfaces
{
    public interface IExpression
    {
        #region Properties

        IReadOnlyDictionary<string,object> Parameters { get; }
        EvaluateOptions Options { get; set; }
        UnitRegistry UnitRegistry { get; }

        #endregion // Properties

        #region Public_Methods

        void SetParameters(IReadOnlyDictionary<string, object> parameters);
        void SetAdditionalFunction(string fuctionName, Func<FunctionArgs, object> function);
        object Evaluate();
        bool ContainsParameter(string name);

        #endregion // Public_Methods
    }
}
