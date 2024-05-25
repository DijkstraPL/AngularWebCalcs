using Build_IT_NCalc.Units;
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
    public class SwitchFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }
        public Func<FunctionArgs, object> Function { get; private set; }

        #endregion // Properties

        #region Constructors

        public SwitchFunction()
        {
            SetFunction();
        }

        #endregion // Constructors

        #region Private_Methods

        private void SetFunction()
        {
            Name = "SWITCH";
            Function = (e) =>
            {
                var lookup = e.Parameters[0].Evaluate();// e.Parameters[0].ToString().Contains('[') ? e.Parameters[0].Evaluate() : e.Parameters[0];

                for (int i = 1; i < e.Parameters.Length; i+=2)
                {
                    if (lookup is ValueUnit valueUnit)
                        valueUnit.OrganizeUnits();
                    var result = e.Parameters[i].Evaluate();
                    if (result is ValueUnit resultValueUnit)
                        resultValueUnit.OrganizeUnits();
                    if (lookup.ToString() == result.ToString())
                        return e.Parameters[i + 1].Evaluate();
                }
                return null;
            };
        }

        #endregion // Private_Methods
    }
}
