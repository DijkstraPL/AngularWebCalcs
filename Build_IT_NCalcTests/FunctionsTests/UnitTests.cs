using Build_IT_NCalc.Units;
using FluentAssertions;
using FluentAssertions.Execution;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class UnitTests
    {
        #region Not Lambda
        [Fact]
        public void UnitFunctionTest_NotLambda()
        {
            using (new AssertionScope())
            {
                var unitExpression = new Expression("Unit(11,'cm')", EvaluateOptions.AllowUnitCalculations);

                var result = unitExpression.Evaluate();

                Assert.IsType<ValueUnit>(result);
                Assert.Equal(11, ((ValueUnit)result).Value);
                Assert.Contains("cm", ((ValueUnit)result).Units.Select(u => u.Symbol));
                Assert.Equal("11cm", ((ValueUnit)result).ToString());
            }
        }
        #endregion Not Lambda

        #region Lambda
        [Fact]
        public void UnitFunctionTest()
        {
            var logicalExpression = Expression.Compile("Unit(2,'mm')*2", true);

            var expression = new Expression(logicalExpression, EvaluateOptions.AllowUnitCalculations);
            var sut = expression.ToLambda(typeof(ValueUnit));

            var result = sut.Invoke();

            result.Should().Be(new ValueUnit(4,  "mm"));
        }

        [Fact]
        public void UnitFunctionTest2()
        {
            var expression = new Expression("Unit(2,'mm')*2", EvaluateOptions.AllowUnitCalculations);

            var sut = expression.ToLambda(typeof(ValueUnit));

            var result = sut.Invoke();

            result.Should().Be(new ValueUnit(4,  "mm"));
        }

        [Fact]
        public void UnitFunctionTest3()
        {
            var expression = new Expression("Unit([a],'mm')*[b]", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", 2);
            expression.SetParameter("b", 2);
            var sut = expression.ToLambda(typeof(ValueUnit));

            var result = sut.Invoke();

            result.Should().Be(new ValueUnit(4,  "mm"));
        }
        #endregion Lambda
    }
}
