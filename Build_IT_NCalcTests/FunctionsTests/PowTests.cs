using Build_IT_NCalc.Units;
using FluentAssertions;
using FluentAssertions.Execution;
using NCalc;
using System;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class PowTests
    {
        #region Not Lambda
        [Fact]
        public void PowerFunctionTest()
        {
            var expr = new Expression("Pow([a],2)*Pow([b],3)", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(2,   "kN"));
            expr.AddParameter("b", new ValueUnit(3,   "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(108, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("108kN^2*m^3", ((ValueUnit)result).ToString());
        }

        [Fact]
        public void PowerFunctionTest_Square()
        {
            var expr = new Expression("Pow([a],0.5)", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(4,   "m", "m"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(2, ((ValueUnit)result).Value);
            Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol));
            Assert.Equal("2m", ((ValueUnit)result).ToString());
        }

        [Fact]
        public void MoreComplicatedTest_PowerLessThan1()
        {
            using (new AssertionScope())
            {
                var expr = new Expression("Pow([a]/[b],0.3)*c", EvaluateOptions.AllowUnitCalculations);

                expr.AddParameter("a", new ValueUnit(10.07937 * 2,   "MPa"));
                expr.AddParameter("b", new ValueUnit(2,   "MPa"));
                expr.AddParameter("c", new ValueUnit(3,   "GPa"));

                var result = expr.Evaluate();

                Assert.IsType<ValueUnit>(result);
                Assert.Equal(6.000000285882365, ((ValueUnit)result).Value, precision: 3);
                Assert.Contains("GPa", ((ValueUnit)result).Units.Select(u => u.Symbol));
                Assert.Equal("6.000000285882365GPa", ((ValueUnit)result).ToString(CultureInfo.InvariantCulture));
            }
        }

        [Fact]
        public void MoreComplicatedTest_PowerLessThan1_OrganizeUnits()
        {
            using (new AssertionScope())
            {
                var expr = new Expression("Pow([a]/[b],0.3)*c", EvaluateOptions.AllowUnitCalculations);

                expr.AddParameter("a", new ValueUnit(10.07937 * 2,   "MPa"));
                expr.AddParameter("b", new ValueUnit(2,   "MPa"));
                expr.AddParameter("c", new ValueUnit(3,   "GPa"));

                var result = expr.Evaluate() as ValueUnit;
                result.OrganizeUnits();

                Assert.IsType<ValueUnit>(result);
                Assert.Equal(6000000.285882365, ((ValueUnit)result).Value, precision: 3);
                Assert.Contains("kPa", ((ValueUnit)result).Units.Select(u => u.Symbol));
                Assert.Equal("6000000.285882365kPa", ((ValueUnit)result).ToString(CultureInfo.InvariantCulture));
            }
        }
        #endregion Not Lambda

        #region Lambda
        [Fact]
        public void PowFunctionTest()
        {
            var expression = new Expression("Pow([a],[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(10,   "mm"));
            expression.SetParameter("b", 2);
            var sut = expression.ToLambda(typeof(ValueUnit));

            expression.SetParameter("a", new ValueUnit(10,   "cm"));
            expression.SetParameter("b", 3);
            var result = sut.Invoke();
            (result as ValueUnit).RoundValue(0.0001);
            result.Should().Be(new ValueUnit(1000,   "cm^3"));
        }
        [Fact]
        public void PowFunctionTest_WrongArguments_ShouldThrowInvalidOperationException()
        {
            var expression = new Expression("Pow([a],[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(1,   "kg"));
            expression.SetParameter("b", new ValueUnit(3,   "mm"));
            var sut = expression.ToLambda(typeof(ValueUnit));
            Assert.Throws<InvalidOperationException>(() => sut.Invoke());
        }
        [Fact]
        public void PowFunctionTest_WrongArguments_ShouldWork()
        {
            var expression = new Expression("Pow([a],[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(1, "kg"));
            expression.SetParameter("b", new ValueUnit(3));
            var sut = expression.ToLambda(typeof(ValueUnit));
            var result = sut.Invoke();

            Assert.Equal(new ValueUnit(1, "kg^3"), result);
        }
        #endregion Lambda
    }
}
