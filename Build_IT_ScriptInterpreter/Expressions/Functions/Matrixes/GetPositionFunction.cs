using Build_IT_ScriptInterpreter.Expressions.Functions.Interfaces;
using MathNet.Numerics.LinearAlgebra;
using NCalc;
using System;
using System.Composition;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Expressions.Functions.Matrixes
{
    [Export(typeof(IFunction))]
    public class GetPositionFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }
        public Func<FunctionArgs, object> Function { get; private set; }

        #endregion // Properties

        #region Constructors

        public GetPositionFunction()
        {
            SetFunction();
        }

        #endregion // Constructors

        #region Private_Methods
        
        private void SetFunction()
        {
            Name = "GETPOSITION";
            Function = (e) =>
            {
                if (e.Parameters.Count() != 3)
                    throw new ArgumentException("Wrong number of parameters.");

                Matrix<double> matrix = (Matrix<double>)(e.Parameters[0].Evaluate());

                if (matrix == null)
                    throw new ArgumentException("Wrong data type.");

                return matrix[(int)e.Parameters[1].Evaluate(), (int)e.Parameters[2].Evaluate()];
            };
        }

        #endregion // Private_Methods
    }
}
