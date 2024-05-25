using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class AtanTests
    {
        #region Not Lambda
        [Fact]
        public void AtanFunctionTest_NotLambda()
        {
            var expr = new Expression("Atan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1 ));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.78540, ((ValueUnit)result).Value, 5);
            Assert.Contains("rad", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void AtanFunctionTest_ParameterWithUnit_ThrowFormatException_NotLambda()
        {
            var expr = new Expression("Atan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(0.5 , "kN"));

            Assert.Throws<FormatException>(() => expr.Evaluate());
        }
        #endregion Not Lambda

        #region Lambda
        [Fact]
        public void AtanFunctionTest()
        {
            var expr = new Expression("Atan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1 ));

            var sut = expr.ToLambda<object>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.78540, ((ValueUnit)result).Value, 5);
            Assert.Contains("rad", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void AtanFunctionTest_ParameterWithUnit_ThrowFormatException()
        {
            var expr = new Expression("Atan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(0.5 , "kN"));
            var sut = expr.ToLambda<object>();

            Assert.Throws<FormatException>(() => sut());
        }
        #endregion Lambda
    }
}
