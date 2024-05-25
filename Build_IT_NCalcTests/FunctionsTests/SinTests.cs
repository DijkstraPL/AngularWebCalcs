using Build_IT_NCalc.Units;
using NCalc;
using System;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class SinTests
    {
        #region Not Lambda
        [Fact]
        public void SinFunctionTest_NotLambda()
        {
            var expr = new Expression("Sin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,   "deg"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.70711, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void SinFunctionTest_Radians_NotLambda()
        {
            var expr = new Expression("Sin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1,   "rad"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.84147, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void SinFunctionTest_ALotParametersDegreesAfterSimplification_NotLambda()
        {
            var expr = new Expression("Sin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,   "deg"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.70711, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void SinFunctionTest_ALotParametersTooManyAfterSimplification_ThrowsFormatException_NotLambda()
        {
            var expr = new Expression("Sin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,   "deg", "deg"));

            Assert.Throws<FormatException>(() => expr.Evaluate());
        }

        [Fact]
        public void SinFunctionTest_WrongUnit_ThrowsFormatException_NotLambda()
        {
            var expr = new Expression("Sin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,   "kN"));

            Assert.Throws<FormatException>(() => expr.Evaluate());
        }
        #endregion Not Lambda
        #region Lambda
        [Fact]
        public void SinFunctionTest()
        {
            var expr = new Expression("Sin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,   "deg"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.70711, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void SinFunctionTest_Radians()
        {
            var expr = new Expression("Sin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1,   "rad"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.84147, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void SinFunctionTest_ALotParametersDegreesAfterSimplification()
        {
            var expr = new Expression("Sin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,   "deg"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.70711, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void SinFunctionTest_ALotParametersTooManyAfterSimplification_ThrowsFormatException()
        {
            var expr = new Expression("Sin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,   "deg", "deg"));

            var sut = expr.ToLambda<ValueUnit>();
            Assert.Throws<FormatException>(() => sut());
        }

        [Fact]
        public void SinFunctionTest_WrongUnit_ThrowsFormatException()
        {
            var expr = new Expression("Sin([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,   "kN"));

            var sut = expr.ToLambda<ValueUnit>();
            Assert.Throws<FormatException>(() => sut());
        }
        #endregion Lambda
    }
}
