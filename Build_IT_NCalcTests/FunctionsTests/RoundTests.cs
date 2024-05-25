using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
   public  class RoundTests
    {
        #region Not Lambda
        [Theory]
        [InlineData(1.8234, 1.82)]
        [InlineData(2.2354, 2.24)]
        [InlineData(3.7486, 3.75)]
        [InlineData(-1.8234, -1.82)]
        [InlineData(-2.2354, -2.24)]
        [InlineData(-3.7486, -3.75)]
        public void RoundFunctionTest_NotLambda(double value, double expectedValue)
        {
            var expr = new Expression("Round([a],2)", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(value,  "kN", "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(expectedValue, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }
        #endregion Not Lambda
        #region Lambda
        [Theory]
        [InlineData(1.8234, 1.82)]
        [InlineData(2.2354, 2.24)]
        [InlineData(3.7486, 3.75)]
        [InlineData(-1.8234, -1.82)]
        [InlineData(-2.2354, -2.24)]
        [InlineData(-3.7486, -3.75)]
        public void RoundFunctionTest(double value, double expectedValue)
        {
            var expr = new Expression("Round([a],2)", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(value,  "kN", "m"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(expectedValue, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }
        #endregion Lambda
    }
}
