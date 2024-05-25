using Build_IT_NCalc.Units;
using NCalc;
using System;
using Xunit;

namespace Build_IT_NCalcTests.FunctionsTests
{
    public class IEEERemainderTests
    {
        #region Not Lambda
        [Fact]
        public void IEEERemainderFunctionTest_NotLambda()
        {
            var expr = new Expression("IEEERemainder([a],[b])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,   "kN", "m^-1"));
            expr.AddParameter("b", new ValueUnit(1.6,   "kN", "m^-1"));

            Assert.Throws<NotSupportedException>(() => expr.Evaluate());
        }
        #endregion Not Lambda
        #region Lambda
        [Fact]
        public void IEEERemainderFunctionTest()
        {
            var expr = new Expression("IEEERemainder([a],[b])", EvaluateOptions.AllowUnitCalculations);

            expr.AddParameter("a", new ValueUnit(1.8,   "kN", "m^-1"));
            expr.AddParameter("b", new ValueUnit(1.6,   "kN", "m^-1"));

            var sut = expr.ToLambda<ValueUnit>();
            Assert.Throws<NotSupportedException>(() => sut());
        }
        #endregion Lambda
    }
}
