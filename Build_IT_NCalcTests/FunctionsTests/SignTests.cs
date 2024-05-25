using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class SignTests
    {
        #region Not Lambda
        [Theory]
        [InlineData(2, 1)]
        [InlineData(-2, -1)]
        public void SignFunctionTest_NotLambda(int value, int expectedResult)
        {
            var expr = new Expression("Sign([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(value,   "kN", "m"));

            var result = expr.Evaluate();
            Assert.IsType<int>(result);
            Assert.Equal(expectedResult, ((int)result));
        }
        #endregion Not Lambda
        #region Lambda
        [Theory]
        [InlineData(2, 1)]
        [InlineData(-2, -1)]
        public void SignFunctionTest(int value, int expectedResult)
        {
            var expr = new Expression("Sign([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(value,   "kN", "m"));

            var sut = expr.ToLambda<object>();
            var result = sut();
            Assert.IsType<int>(result);
            Assert.Equal(expectedResult, ((int)result));
        }
        #endregion Lambda
    }
}
