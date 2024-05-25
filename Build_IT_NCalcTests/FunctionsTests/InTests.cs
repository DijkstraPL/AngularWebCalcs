using Build_IT_NCalc.Units;
using NCalc;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class InTests
    {
        #region Not Lambda
        [Fact]
        public void InFunctionTest_True_NotLambda()
        {
            var expr = new Expression("in([a],[b],[c],[d])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,   "kN"));
            expr.AddParameter("b", new ValueUnit(2,   "kN"));
            expr.AddParameter("c", new ValueUnit(100,   "N"));
            expr.AddParameter("d", new ValueUnit(1800,   "N"));

            var result = expr.Evaluate();
            Assert.IsType<bool>(result);
            Assert.True(((bool)result));
        }
        #endregion Not Lambda
        #region Lambda
        [Fact]
        public void InFunctionTest_True()
        {
            var expr = new Expression("in([a],[b],[c],[d])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,   "kN"));
            expr.AddParameter("b", new ValueUnit(2,   "kN"));
            expr.AddParameter("c", new ValueUnit(100,   "N"));
            expr.AddParameter("d", new ValueUnit(1800,   "N"));

            var sut = expr.ToLambda<bool>();
            var result = sut();
            Assert.IsType<bool>(result);
            Assert.True(((bool)result));
        }
        #endregion Lambda
    }
}
