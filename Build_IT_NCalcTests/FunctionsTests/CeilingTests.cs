using Build_IT_NCalc.Units;
using NCalc;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class CeilingTests
    {
        #region Not Lambda
        [Fact]
        public void CeilingFunctionTest_NotLambda()
        {
            var expr = new Expression("Ceiling([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.1,   "kN", "m^-1"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(2, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal(1, ((ValueUnit)result)["kN"].Power);
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal(-1, ((ValueUnit)result)["m"].Power);
        }
        #endregion Not Lambda
        #region Lambda
        [Fact]
        public void CeilingFunctionTest()
        {
            var expr = new Expression("Ceiling([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.1,   "kN", "m^-1"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(2, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal(1, ((ValueUnit)result)["kN"].Power);
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal(-1, ((ValueUnit)result)["m"].Power);
        }
        #endregion Lambda
    }
}
