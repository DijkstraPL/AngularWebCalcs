using Build_IT_ScriptInterpreter.Expressions.Functions.Interfaces;
using NCalc;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreter.Expressions.Functions
{
    [Export(typeof(IFunction))]
    public class MinnFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }
        public Func<FunctionArgs, object> Function { get; private set; }

        #endregion // Properties

        #region Constructors
        
        public MinnFunction()
        {
            SetFunction();
        }

        #endregion // Constructors

        #region Private_Methods

        private void SetFunction()
        {
            Name = "MINN";
            Function = (e) =>
            {
                return e.Parameters.Select(p => p.Evaluate()).Min();
            };
        }

        #endregion // Private_Methods
    }
}
