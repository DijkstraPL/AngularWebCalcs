using Build_IT_NCalc.Units;
using NCalc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
   public  class LogTests
    {
        #region Not Lambda
        [Fact]
        public void LogTest_NotLambda()
        {
            var expr = new Expression("Log([a],[b])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,   "kN"));
            expr.AddParameter("b", new ValueUnit(2,   "kN"));

            Assert.Throws<NotSupportedException>(() => expr.Evaluate());
        }
        #endregion Not Lambda
        #region Lambda
        [Fact]
        public void LogTest()
        {
            var expr = new Expression("Log([a],[b])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,   "kN"));
            expr.AddParameter("b", new ValueUnit(2,   "kN"));

            var sut = expr.ToLambda<ValueUnit>();
            Assert.Throws<NotSupportedException>(() => sut());
        }
        #endregion Lambda
    }
}
