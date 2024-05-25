using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class SqrtTests
    {
        #region Not Lambda
        [Fact]
        public void SqrtFunctionTest_NotLambda()
        {
            var expr = new Expression("Sqrt([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(4,  "kN", "kN"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(2, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Single(((ValueUnit)result).Units);
        }
        #endregion Not Lambda
        #region Lambda
        [Fact]
        public void SqrtFunctionTest()
        {
            var expr = new Expression("Sqrt([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(4, "kN", "kN"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(2, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Single(((ValueUnit)result).Units);
        }
        #endregion Lambda
    }
}
