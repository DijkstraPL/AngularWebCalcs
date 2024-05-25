using Build_IT_NCalc.Units;
using FluentAssertions;
using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class ErrorTests
    {
        [Fact]
        public void ErrorFunctionTest_NotLambda()
        {
            var expression = new Expression("Error([a])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", "Error message");

            Assert.Throws<CalculationException>(() => expression.Evaluate());
        }
        #region Lambda
        [Fact]
        public void ErrorFunctionTest()
        {
            var expression = new Expression("Error([a])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", "Error message");
            var sut = expression.ToLambda(typeof(void));

            Assert.Throws<CalculationException>(() => sut.Invoke());
        }

        [Fact]
        public void IfWithErrorFunctionTest_NoErrorRaised()
        {
            var expression = new Expression("if(1>2,Error([a]),[b])", EvaluateOptions.AllowUnitCalculations);
            expression.SetParameter("a", "Error message");
            expression.SetParameter("b", new ValueUnit(1,   "mm"));
            var sut = expression.ToLambda(typeof(ValueUnit));

            var result = sut.Invoke();
            result.Should().Be(new ValueUnit(1,   "mm"));
        }
        [Fact]
        public void IfWithErrorFunctionTest_ErrorRaised()
        {
            var expression = new Expression("if(2>1,Error([a]),[b])", EvaluateOptions.AllowUnitCalculations );
            expression.SetParameter("a", "Error message");
            expression.SetParameter("b", new ValueUnit(1,   "mm"));
            var sut = expression.ToLambda(typeof(ValueUnit));

            Assert.Throws<CalculationException>(() => sut.Invoke());
        }
        #endregion Lambda 
    }
}
