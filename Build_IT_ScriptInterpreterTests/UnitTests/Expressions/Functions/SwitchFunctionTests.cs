using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Expressions;
using Build_IT_ScriptInterpreter.Expressions.Functions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Expressions.Functions
{
    [TestFixture]
    public class SwitchFunctionTests
    {
        private SwitchFunction _switchFunction;

        [SetUp]
        public void SetUp()
        {
            _switchFunction = new SwitchFunction();
        }

        [Test]
        public void SwitchFunctionTest_Name_Success()
        {
            Assert.That(_switchFunction.Name, Is.EqualTo("SWITCH"));
        }

        [Test]
        public void SwitchFunctionTest_FunctionWithOneParameter_ReturnsNull()
        {
            var parameters = new NCalc.Expression[1];
            parameters[0] = new NCalc.Expression("1");

            Assert.IsNull(_switchFunction.Function(new NCalc.FunctionArgs()
            { Parameters = parameters }));
        }

        [Test]
        public void SwitchFunctionTest_FunctionWithManyParameters_Success()
        {
            var parameters = new NCalc.Expression[7];
            parameters[0] = new NCalc.Expression("'a'");
            parameters[1] = new NCalc.Expression("'b'");
            parameters[2] = new NCalc.Expression("7");
            parameters[3] = new NCalc.Expression("'c'");
            parameters[4] = new NCalc.Expression("1");
            parameters[5] = new NCalc.Expression("'a'");
            parameters[6] = new NCalc.Expression("3");

            Assert.That(_switchFunction.Function(new NCalc.FunctionArgs() { Parameters = parameters }),
              Is.EqualTo(3));
        }


        [Test]
        public void SwitchFunctionTest_UnitsAsParameters_ReturnsOne()
        {
                var parameters = new Dictionary<string, object>
                {
                    ["a"] = new ValueUnit(1,  "m"),
                    ["b"] = new ValueUnit(100,  "cm"),
                    ["c"] = new ValueUnit(11,  "cm"),
                };

                var function = ExpressionEvaluator.Create("SWITCH([a],[b],1,[c],2)", parameters);

                var result = function.Evaluate();

                Assert.That(result, Is.EqualTo(1));
        }
    }
}