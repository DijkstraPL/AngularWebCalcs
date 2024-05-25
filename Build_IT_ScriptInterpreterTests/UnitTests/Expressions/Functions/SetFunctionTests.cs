using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Expressions;
using Build_IT_ScriptInterpreter.Expressions.Functions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Expressions.Functions
{
    [TestFixture]
    public class SetFunctionTests
    {
        private SetFunction _setFunction;

        [SetUp]
        public void SetUp()
        {
            _setFunction = new SetFunction();
        }

        [Test]
        public void SetFunctionTest_Name_Success()
        {

            Assert.That(_setFunction.Name, Is.EqualTo("SET"));
        }
        
        [Test]
        public void SetFunctionTest_FunctionWithOneParameter_Success()
        {
            var parameters = new NCalc.Expression[2];
            parameters[0] = new NCalc.Expression("'a'");
            parameters[1] = new NCalc.Expression("8");

            var functionArgs = new NCalc.FunctionArgs() { Parameters = parameters, MainExpression = new NCalc.Expression("_") };

            Assert.That(_setFunction.Function(functionArgs), Is.EqualTo(true));
            Assert.That(functionArgs.MainExpression.Parameters.ContainsKey("a"));
            Assert.That(functionArgs.MainExpression.Parameters["a"], Is.EqualTo(8));
        }

        [Test]
        public void SetFunctionTest_FunctionWithManyParameters_Success()
        {
            var parameters = new NCalc.Expression[4];
            parameters[0] = new NCalc.Expression("'a'");
            parameters[1] = new NCalc.Expression("8");
            parameters[2] = new NCalc.Expression("'b'");
            parameters[3] = new NCalc.Expression("1");

            var functionArgs = new NCalc.FunctionArgs() { Parameters = parameters, MainExpression = new NCalc.Expression("_") };

            Assert.That(_setFunction.Function(functionArgs), Is.EqualTo(true));
            Assert.That(functionArgs.MainExpression.Parameters.ContainsKey("a"));
            Assert.That(functionArgs.MainExpression.Parameters["a"], Is.EqualTo(8));
            Assert.That(functionArgs.MainExpression.Parameters.ContainsKey("b"));
            Assert.That(functionArgs.MainExpression.Parameters["b"], Is.EqualTo(1));
        }

        [Test]
        public void SetFunctionTest_FunctionWithTextParameter_Success()
        {
            var parameters = new NCalc.Expression[2];
            parameters[0] = new NCalc.Expression("'a'");
            parameters[1] = new NCalc.Expression("'par'");

            var functionArgs = new NCalc.FunctionArgs() { Parameters = parameters, MainExpression = new NCalc.Expression("_") };

            Assert.That(_setFunction.Function(functionArgs), Is.EqualTo(true));
            Assert.That(functionArgs.MainExpression.Parameters.ContainsKey("a"));
            Assert.That(functionArgs.MainExpression.Parameters["a"], Is.EqualTo("par"));
        }


        [Test]
        public void SetFunctionTest_FunctionWithUnitParameter_Success()
        {
                var parameters = new Dictionary<string, object>
                {
                    ["a"] = new ValueUnit(11,  "cm"),
                };

                var function = ExpressionEvaluator.Create("SET('c',[a])", parameters);
                function.Evaluate();
                var function2 = ExpressionEvaluator.Create("[c]*2", function.Parameters);
                var result = function2.Evaluate();

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.TypeOf<ValueUnit>());
                    Assert.That(((ValueUnit)result).Value, Is.EqualTo(22));
                    Assert.Contains("cm", ((ValueUnit)result).Units.Select(u => u.Symbol).ToList());
                });           
        }
    }
}
