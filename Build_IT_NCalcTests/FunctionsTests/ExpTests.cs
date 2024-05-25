using Build_IT_NCalc.Units;
using NCalc;
using System;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class ExpTests
    {
        #region Not Lambda
        [Fact]
        public void ExpFunctionTest_NotLambda()
        {
            var expr = new Expression("Exp([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(2));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(7.38906, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void ExpFunctionTest_WithUnits_ThrowsFormatException_NotLambda()
        {
            var expr = new Expression("Exp([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(2,   "mm"));

            Assert.Throws<FormatException>(() => expr.Evaluate());
        }
        #endregion Not Lambda
        #region Lambda
        [Fact]
        public void ExpFunctionTest()
        {
            var expr = new Expression("Exp([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(2));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(7.38906, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void ExpFunctionTest_WithUnits_ThrowsFormatException()
        {
            var expr = new Expression("Exp([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(2,   "mm"));

            var sut = expr.ToLambda<ValueUnit>();
            Assert.Throws<FormatException>(() => sut());
        }
        #endregion Lambda
    }
}
