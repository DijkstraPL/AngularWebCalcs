using Build_IT_ScriptInterpreter.Expressions.Functions.Interfaces;
using NCalc;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Globalization;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Expressions.Functions
{
    [Export(typeof(IFunction))]
    public class MaxxFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }
        public Func<FunctionArgs, object> Function { get; private set; }

        #endregion // Properties

        #region Constructors

        public MaxxFunction()
        {
            SetFunction();
        }

        #endregion // Constructors

        #region Private_Methods
        
        private void SetFunction()
        {
            Name = "MAXX";
            Function = (e) =>
            {
                return e.Parameters.Select(p => p.Evaluate()).Max();
            };
        }

        #endregion // Private_Methods
    }
}
