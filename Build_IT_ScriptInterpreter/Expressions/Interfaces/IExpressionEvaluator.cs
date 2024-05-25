using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreter.Expressions.Interfaces
{
    public interface IExpressionEvaluator
    {
        IReadOnlyDictionary<string, object> Parameters { get; }
        object Evaluate();
    }
}
