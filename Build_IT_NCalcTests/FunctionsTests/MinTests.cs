using Build_IT_NCalc.Units;
using FluentAssertions;
using NCalc;
using System;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class MinTests
    {
        #region Not Lambda
        [Fact]
        public void MinFunctionTest_Returns2_2kN()
        {
            var expr = new Expression("Min([a],[b])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new  ValueUnit(1800,   "N"));
            expr.AddParameter("b", new ValueUnit(2,   "kN"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1.8, ((ValueUnit)result).Value);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }
        #endregion Not Lambda

        #region Lambda
        [Fact]
        public void MinFunctionTest_SameUnits_ShouldCompare()
        {
            var expression = new Expression("Min([a],[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(1,   "m"));
            expression.SetParameter("b", new ValueUnit(3,   "mm"));
            var sut = expression.ToLambda(typeof(ValueUnit));

            expression.SetParameter("a", new ValueUnit(30,   "mm"));
            expression.SetParameter("b", new ValueUnit(1,   "m"));
            var result = sut.Invoke();

            result.Should().Be(new ValueUnit(30,   "mm"));
        }
        [Fact]
        public void MinFunctionTest_DifferentUnits_ShouldThrowArgumentException()
        {
            var expression = new Expression("Min([a],[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(1,   "kg"));
            expression.SetParameter("b", new ValueUnit(3,   "mm"));
            var sut = expression.ToLambda(typeof(ValueUnit));

            Assert.Throws<ArgumentException>(() => sut.Invoke());
        }
        [Fact]
        public void MinFunctionTest_DifferentTypes_ShouldThrowArgumentException()
        {
            var expression = new Expression("Min([a],[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(1,   "kg"));
            expression.SetParameter("b", 1);
            var sut = expression.ToLambda(typeof(ValueUnit));
            Assert.Throws<ArgumentException>(() => sut.Invoke());
        }

        [Fact]
        public void MinFunctionTest_DifferentTypes_ShouldWork()
        {
            var expression = new Expression("Min([a],[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(2));
            expression.SetParameter("b", 1);
            var sut = expression.ToLambda(typeof(ValueUnit));
            var result = sut.Invoke();

            Assert.Equal((ValueUnit)1, result);
        }
        [Fact]
        public void MinFunctionTest_ChangeOfArgumentTypes_ShouldThrowArgumentException()
        {
            var expression = new Expression("Min([a],[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(1,   "m"));
            expression.SetParameter("b", new ValueUnit(3,   "mm"));
            var sut = expression.ToLambda(typeof(ValueUnit));

            expression.SetParameter("a", new ValueUnit(30,   "mm"));
            Assert.Throws<ArgumentException>(() => expression.SetParameter("b", 6));
        }
        #endregion Lambda
    }
}
