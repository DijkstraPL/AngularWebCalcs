using Build_IT_CommonTools.Maths;
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
    public class InterpolateFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }
        public Func<FunctionArgs, object> Function { get; private set; }

        #endregion // Properties

        #region Constructors

        public InterpolateFunction()
        {
            SetFunction();
        }

        #endregion // Constructors

        #region Private_Methods

        private void SetFunction()
        {
            Name = "INTERPOLATE";
            Function = (e) =>
            {
                var parameters = e.Parameters
                .Select(p => Convert.ToDouble(p.Evaluate(), CultureInfo.InvariantCulture))
                .ToList();

                return Interpolation.InterpolateLinearBetween(
                    (parameters[0], parameters[1]),
                    (parameters[2], parameters[3]),
                    parameters[4]
                    );
            };
        }

        #endregion // Private_Methods
    }
}
