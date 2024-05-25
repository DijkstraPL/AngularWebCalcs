using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Expressions;
using Build_IT_ScriptInterpreter.Expressions.Functions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Expressions.Functions
{
    [TestFixture]
    public class MinnFunctionTests
    {
        private MinnFunction _minnFunction;

        [SetUp]
        public void SetUp()
        {
            _minnFunction = new MinnFunction();
        }

        [Test]
        public void MinnFunctionTest_Name_Success()
        {
            Assert.That(_minnFunction.Name, Is.EqualTo("MINN"));
        }

        [Test]
        public void MaxxFunctionTest_FunctionWithOneParameter_Success()
        {
            var parameters = new NCalc.Expression[1];
            parameters[0] = new NCalc.Expression("5");

            Assert.That(_minnFunction.Function(new NCalc.FunctionArgs() { Parameters = parameters }),
                Is.EqualTo(5));
        }

        [Test]
        public void MaxxFunctionTest_FunctionWithManyParameters_Success()
        {
            var parameters = new NCalc.Expression[3];
            parameters[0] = new NCalc.Expression("5");
            parameters[1] = new NCalc.Expression("10");
            parameters[2] = new NCalc.Expression("7");

            Assert.That(_minnFunction.Function(new NCalc.FunctionArgs() { Parameters = parameters }),
              Is.EqualTo(5));
        }

        [Test]
        public void MinnFunctionTest_UnitsAsParameters_ReturnsTrue()
        {
                var parameters = new Dictionary<string, object>
                {
                    ["a"] = new ValueUnit(1,  "m"),
                    ["b"] = new ValueUnit(11,  "cm")
                };

                var function = ExpressionEvaluator.Create("MINN([a],[b])", parameters);

                var result = function.Evaluate();

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.TypeOf<ValueUnit>());
                    Assert.That(((ValueUnit)result).Value, Is.EqualTo(0.11));
                    Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol).ToList());
                });
        }
    }
}
