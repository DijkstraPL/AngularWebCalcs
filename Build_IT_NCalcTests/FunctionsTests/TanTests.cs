using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class TanTests
    {
        #region Not Lambda
        [Fact]
        public void TanFunctionTest_NotLambda()
        {
            var expr = new Expression("Tan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,  "deg"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void TanFunctionTest_Radians_NotLambda()
        {
            var expr = new Expression("Tan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1,  "rad"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1.55740772, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void TanFunctionTest_ALotParametersDegreesAfterSimplification_NotLambda()
        {
            var expr = new Expression("Tan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,  "deg"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void TanFunctionTest_ALotParametersTooManyAfterSimplification_ThrowsFormatException_NotLambda()
        {
            var expr = new Expression("Tan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,  "deg", "deg"));

            Assert.Throws<FormatException>(() => expr.Evaluate());
        }

        [Fact]
        public void TanFunctionTest_WrongUnit_ThrowsFormatException_NotLambda()
        {
            var expr = new Expression("Tan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,  "kN"));

            Assert.Throws<FormatException>(() => expr.Evaluate());
        }
        #endregion Not Lambda
        #region Lambda
        [Fact]
        public void TanFunctionTest()
        {
            var expr = new Expression("Tan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,  "deg"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void TanFunctionTest_Radians()
        {
            var expr = new Expression("Tan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1,  "rad"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1.55740772, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void TanFunctionTest_ALotParametersDegreesAfterSimplification()
        {
            var expr = new Expression("Tan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45, "deg"));

            var sut = expr.ToLambda<ValueUnit>();
            var result = sut();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(1, ((ValueUnit)result).Value, 5);
            Assert.Empty(((ValueUnit)result).Units);
        }

        [Fact]
        public void TanFunctionTest_ALotParametersTooManyAfterSimplification_ThrowsFormatException()
        {
            var expr = new Expression("Tan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,  "deg", "deg"));

            var sut = expr.ToLambda<ValueUnit>();
            Assert.Throws<FormatException>(() => sut());
        }

        [Fact]
        public void TanFunctionTest_WrongUnit_ThrowsFormatException()
        {
            var expr = new Expression("Tan([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(45,  "kN"));

            var sut = expr.ToLambda<ValueUnit>();
            Assert.Throws<FormatException>(() => sut());
        }
        #endregion Lambda
    }
}
