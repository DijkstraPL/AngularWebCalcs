using Build_IT_ScriptInterpreter.Expressions.Functions.Interfaces;
using NCalc;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Expressions.Functions
{
    [Export(typeof(IFunction))]
    public class ValidateFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }

        public Func<FunctionArgs, object> Function { get; private set; }

        #endregion // Properties

        #region Constructors

        public ValidateFunction()
        {
            SetFunction();
        }

        #endregion // Constructors

        #region Private_Methods

        private void SetFunction()
        {
            Name = "VALIDATE";
            Function = (e) =>
            {
                try
                {
                    return e.Parameters[0].Evaluate();
                }
                catch (ArgumentException)
                {
                    return false;
                }
                catch
                {
                    return true;
                }
            };
        }

        #endregion // Private_Methods         
    }
}
