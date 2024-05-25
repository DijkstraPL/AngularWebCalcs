using Build_IT_NCalc.Units;
using FluentAssertions;
using NCalc;
using System.Linq;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class IfTests
    {
        #region Not Lambda
        [Fact]
        public void IfFunctionTest_True()
        {
            var expr = new Expression("if([a]>[b],[c],[d]-[a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,  "kN"));
            expr.AddParameter("b", new ValueUnit(2,  "kN"));
            expr.AddParameter("c", new ValueUnit(100,  "mm"));
            expr.AddParameter("d", new ValueUnit(2000,  "N"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(0.2, ((ValueUnit)result).Value, 3);
            Assert.Contains("kN", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void IfFunctionTest_False()
        {
            var expr = new Expression("if([a]>[b],[c],[d]-[a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(3,  "kN"));
            expr.AddParameter("b", new ValueUnit(2,  "kN"));
            expr.AddParameter("c", new ValueUnit(100,  "mm"));
            expr.AddParameter("d", new ValueUnit(2000,  "N"));

            var result = expr.Evaluate();
            Assert.IsType<ValueUnit>(result);
            Assert.Equal(100, ((ValueUnit)result).Value, 3);
            Assert.Contains("mm", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }

        [Fact]
        public void UnitOfMeasure_IfFunction()
        {
            var expr = new Expression("if(a>b,1,c)", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", expr.ValueUnit(1, "m"));
            expr.AddParameter("b", expr.ValueUnit(2, "m"));
            expr.AddParameter("c", expr.ValueUnit(3, "kg"));

            var result = expr.Evaluate();

            Assert.IsType<ValueUnit>(result);
            Assert.Equal(3, ((ValueUnit)result).Value);
            Assert.Contains("kg", ((ValueUnit)result).Units.Select(u => u.Symbol));
        }
        #endregion Not Lambda

        #region  Lambda
        [Theory]
        [InlineData(3, "mm", 2, "kg")]
        [InlineData(3, "m", 2, "t")]
        public void IfFunctionTest(double val, string unit, double expectedValue, string expectedUnit)
        {
            var expression = new Expression("if([a]>[b],[c],[d])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", new ValueUnit(1,  "m"));
            expression.SetParameter("b", new ValueUnit(val,  unit));
            expression.SetParameter("c", new ValueUnit(2,  "kg"));
            expression.SetParameter("d", new ValueUnit(2,  "t"));

            var sut = expression.ToLambda(typeof(ValueUnit));
            var result = sut.Invoke();
            result.Should().Be(new ValueUnit(expectedValue,  expectedUnit));
        }
        #endregion Lambda
    }
}
