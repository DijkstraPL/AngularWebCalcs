using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class AcosTests
    {
        #region Not Lambda
        [Fact]
        public void AcosFunctionTest_NotLambda()
        {
            var expr = new Expression("Acos([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(0.5));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1.04720, ((ValueUnit)result).Value, 5);
            Assert.Contains("rad", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void AcosFunctionTest_ParameterWithUnit_ThrowFormatException_NotLambda()
        {
            var expr = new Expression("Acos([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(0.5,  "kN"));

            Assert.Throws<FormatException>(() => expr.Evaluate());
        }
        #endregion Not Lambda

        #region Lambda
        [Fact]
        public void AcosFunctionTest()
        {
            var expr = new Expression("Acos([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(0.5));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1.04720, ((ValueUnit)result).Value, 5);
            Assert.Contains("rad", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void AcosFunctionTest_ParameterWithUnit_ThrowFormatException()
        {
            var expr = new Expression("Acos([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(0.5,  "kN"));
            var sut = expr.ToLambda();

            Assert.Throws<FormatException>(() => sut());
        }
        #endregion Lambda
    }
}
