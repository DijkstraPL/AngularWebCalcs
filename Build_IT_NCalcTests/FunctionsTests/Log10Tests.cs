using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class Log10Tests
    {
        #region Not Lambda
        [Fact]
        public void Log10Test_NotLambda()
        {
            var expr = new Expression("Log10([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,   "kN"));

            Assert.Throws<NotSupportedException>(() => expr.Evaluate());
        }
        #endregion Not Lambda
        #region Lambda
        [Fact]
        public void Log10Test()
        {
            var expr = new Expression("Log10([a])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,   "kN"));

            var sut = expr.ToLambda<ValueUnit>();
            Assert.Throws<NotSupportedException>(() => sut());
        }
        #endregion Lambda
    }
}
