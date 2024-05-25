using Build_IT_NCalc.Units;
using NCalc;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class AbsTests
    {
        #region Not Lambda
        [Fact]
        public void AbsFunctionTest_FlagForUnitCalculations_NotLambda()
        {
            var expr = new Expression("Abs([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(-2,  "kN"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(2, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("2kN", ((ValueUnit)result).ToString());
        }
        #endregion Not Lambda

        #region Lambda
        [Fact]
        public void AbsFunctionTest_FlagForUnitCalculations()
        {
            var expr = new Expression("Abs([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(-2,  "kN"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(2, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("2kN", ((ValueUnit)result).ToString());
        }
        #endregion Lambda
    }
}
