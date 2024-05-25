using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class AsinTests
    {
        #region Not Lambda
        [Fact]
        public void AsinFunctionTest_NotLambda()
        {
            var expr = new Expression("Asin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(0.5));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.52360, ((ValueUnit)result).Value, 5);
            Assert.Contains("rad", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void AsinFunctionTest_ParameterWithUnit_ThrowFormatException_NotLambda()
        {
            var expr = new Expression("Asin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(0.5,   "kN"));

            Assert.Throws<FormatException>(() => expr.Evaluate());
        }
        #endregion // Not Lambda

        #region Lambda
        [Fact]
        public void AsinFunctionTest()
        {
            var expr = new Expression("Asin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(0.5));

            var sut = expr.ToLambda(typeof(ValueUnit));
            var result = sut();

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.52360, ((ValueUnit)result).Value, 5);
            Assert.Contains("rad", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void AsinFunctionTest_ParameterWithUnit_ThrowFormatException()
        {
            var expr = new Expression("Asin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(0.5,   "kN"));

            var sut = expr.ToLambda(typeof(ValueUnit));

            Assert.Throws<FormatException>(() => sut());
        }
        #endregion Lambda
    }
}
