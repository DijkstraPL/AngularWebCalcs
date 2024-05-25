using Build_IT_ScriptInterpreter.Expressions.Functions.Interfaces;
using NCalc;
using System;
using System.Composition;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Expressions.Functions
{
    [Export(typeof(IFunction))]
    public class SetFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }

        public Func<FunctionArgs, object> Function { get; private set; }

        #endregion // Properties

        #region Constructors

        public SetFunction()
        {
            Set();
        }

        #endregion // Constructors

        #region Private_Methods

        private void Set()
        {
            Name = "SET";
            Function = (e) =>
            {
                for (int i = 0; i < e.Parameters.Length; i += 2)
                {
                    var expression = e.MainExpression;
                    var parameterName = e.Parameters[i].Evaluate().ToString();
                    expression.SetParameter(parameterName, e.Parameters[i + 1].Evaluate());
                }

                return true;
            };
        }

        #endregion // Private_Methods         
    }
}
