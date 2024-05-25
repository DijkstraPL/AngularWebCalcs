using Build_IT_NCalc.Units;
using FluentAssertions;
using NCalc;
using System;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class MaxTests
    {
        #region Not Lambda
        [Fact]
        public void MaxFunctionTest_Returns2kN()
        {
            var expr = new Expression("Max([a],[b])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1800, "N"));
            expr.AddParameter("b", new ValueUnit(2, "kN"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(2, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void MaxFunctionTest_DifferentUnits_AllowUnitCalculations()
        {
            var expr = new Expression("Max([a],[b])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1800, "N"));
            expr.AddParameter("b", new ValueUnit(2, "m"));

            Assert.Throws<ArgumentException>(() => expr.Evaluate());
        }
        #endregion // Not Lambda

        #region Lambda
        [Fact]
        public void MaxFunctionTest_SameUnits_ShouldCompare()
        {
            var expression = new Expression("Max([a],[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(1, "m"));
            expression.SetParameter("b", new ValueUnit(3, "mm"));
            var sut = expression.ToLambda(typeof(ValueUnit));

            expression.SetParameter("a", new ValueUnit(30, "mm"));
            expression.SetParameter("b", new ValueUnit(1, "m"));
            var result = sut.Invoke();

            result.Should().Be(new ValueUnit(1000, "mm"));
        }
        [Fact]
        public void MaxFunctionTest_DifferentUnits_ShouldThrowArgumentException()
        {
            var expression = new Expression("Max([a],[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(1, "kg"));
            expression.SetParameter("b", new ValueUnit(3, "mm"));
            var sut = expression.ToLambda(typeof(ValueUnit));

            Assert.Throws<ArgumentException>(() => sut.Invoke());
        }
        #endregion // Lambda
    }
}
