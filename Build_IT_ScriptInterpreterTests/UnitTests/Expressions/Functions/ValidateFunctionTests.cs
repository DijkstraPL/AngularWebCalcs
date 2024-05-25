using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Expressions;
using Build_IT_ScriptInterpreter.Expressions.Functions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Expressions.Functions
{
    [TestFixture]
    public class ValidateFunctionTests
    {
        private ValidateFunction _validateFunction;

        [SetUp]
        public void SetUp()
        {
            _validateFunction = new ValidateFunction();
        }

        [Test]
        public void ValidateFunctionTest_Name_Success()
        {
            Assert.That(_validateFunction.Name, Is.EqualTo("VALIDATE"));
        }

        [Test]
        public void ValidateFunctionTest_FunctionWithOneParameter_Success()
        {
            var parameters = new NCalc.Expression[1];
            parameters[0] = new NCalc.Expression("5>10");

            Assert.That(_validateFunction.Function(new NCalc.FunctionArgs() { Parameters = parameters }),
                Is.EqualTo(false));
        }

        [Test]
        public void ValidateFunctionTest_FunctionWithManyParameters_Success()
        {
            var parameters = new NCalc.Expression[3];
            parameters[0] = new NCalc.Expression("5<10");
            parameters[1] = new NCalc.Expression("1>2");
            parameters[2] = new NCalc.Expression("1<0");

            Assert.That(_validateFunction.Function(new NCalc.FunctionArgs() { Parameters = parameters }),
              Is.EqualTo(true));
        }

        [Test]
        public void ValidateFunctionTest_UnitsAsParameters_ReturnsFalse()
        {
                var parameters = new Dictionary<string, object>
                {
                    ["a"] = new ValueUnit(11,  "cm"),
                    ["b"] = new ValueUnit(1,  "m")
                };

                var function = ExpressionEvaluator.Create("Validate([a]>[b])", parameters);

                var result = function.Evaluate();

                Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void ValidateFunctionTest_UnitsAsParameters_ReturnsTrue()
        {
                var parameters = new Dictionary<string, object>
                {
                    ["a"] = new ValueUnit(1,  "m"),
                    ["b"] = new ValueUnit(11,  "cm")
                };

                var function = ExpressionEvaluator.Create("Validate([a]>[b])", parameters);

                var result = function.Evaluate();

                Assert.That(result, Is.EqualTo(true));
        }
    }
}
