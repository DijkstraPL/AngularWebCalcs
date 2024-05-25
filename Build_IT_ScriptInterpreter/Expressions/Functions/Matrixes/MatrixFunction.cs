using Build_IT_ScriptInterpreter.Expressions.Functions.Interfaces;
using MathNet.Numerics.LinearAlgebra;
using NCalc;
using System;
using System.Composition;
using System.Linq;

namespace Build_IT_ScriptInterpreter.Expressions.Functions
{
    /// <summary>
    /// Matrix function.
    /// </summary>
    /// <remarks>
    /// par1 - rows
    /// par2 - columns
    /// "MATRIX(3,3,1,2,3,4,5,6,7,8,9)"
    /// 1, 2, 3
    /// 4, 5, 6
    /// 7, 8, 9
    /// </remarks>
    /// <example>
    /// <code>
    /// MATRIX(3,3,1,2,3,4,5,6,7,8,9)
    /// </code>
    /// </example>
    [Export(typeof(IFunction))]
    public class MatrixFunction : IFunction
    {
        #region Properties

        public string Name { get; private set; }
        public Func<FunctionArgs, object> Function { get; private set; }

        #endregion // Properties

        #region Constructors
        
        public MatrixFunction()
        {
            SetFunction();
        }

        #endregion // Constructors

        #region Private_Methods
        
        private void SetFunction()
        {
            Name = "MATRIX";
            Function = (e) =>
            {
                if (e.Parameters.Count() <= 2)
                    throw new ArgumentException("Wrong number of parameters.");
                int rows = (int)e.Parameters[0].Evaluate();
                int columns = (int)e.Parameters[1].Evaluate();
                var matrix = Matrix<double>.Build.Dense(rows, columns);

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                        matrix[i, j] = Convert.ToDouble(e.Parameters[i * columns + j + 2].Evaluate());
                }
                return matrix;
            };
        }

        #endregion // Private_Methods
    }
}
