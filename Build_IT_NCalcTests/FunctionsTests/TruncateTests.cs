using Build_IT_NCalc.Units;
using NCalc;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class TruncateTests
    {
        #region Not Lambda
        [Theory]
        [InlineData(1.8, 1)]
        [InlineData(212.1215, 212)]
        public void TruncateFunctionTest_NotLambda(double value, double expectedValue)
        {
            var expr = new Expression("Truncate([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(value,   "kN", "m"));

            var result = expr.Evaluate();

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(expectedValue, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }
        #endregion Not Lambda

        #region Lambda
        [Theory]
        [InlineData(1.8, 1)]
        [InlineData(212.1215, 212)]
        public void TruncateFunctionTest(double value, double expectedValue)
        {
            var expr = new Expression("Truncate([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(value,   "kN", "m"));

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
