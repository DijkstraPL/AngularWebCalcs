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
    public class SumFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }
        public Func<FunctionArgs, object> Function { get; private set; }

        #endregion // Properties

        #region Constructors

        public SumFunction()
        {
            SetFunction();
        }

        #endregion // Constructors

        #region Private_Methods

        private void SetFunction()
        {
            Name = "SUM";
            Function = (e) =>
            {
                return e.Parameters.Sum(p => { 
                    var result = p.Evaluate();

                    if (result == null)
                        return 0;

                    if (result is List<double> listOfValues)
                        return listOfValues.Sum(v => double.Parse(v.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture));

                    return double.Parse(result.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture);
                });
            };
        }

        #endregion // Private_Methods
    }
}
