using Build_IT_NCalc.Units;
using NCalc;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class FloorTests
    {
        #region Not Lambda
        [Fact]
        public void CeilingFunctionTest_NotLambda()
        {
            var expr = new Expression("Floor([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,  "kN", "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }
        #endregion Not Lambda
        #region Lambda
        [Fact]
        public void CeilingFunctionTest()
        {
            var expr = new Expression("Floor([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,  "kN", "m"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }
        #endregion Lambda
    }
}
