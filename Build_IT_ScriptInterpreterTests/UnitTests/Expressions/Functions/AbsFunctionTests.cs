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
    public class AbsFunctionTests
    {
        [Test]
        public void AbsFunction_NegativeParameter_Unit_Success()
        {
                var parameters = new Dictionary<string, object>
                {
                    ["a"] = new ValueUnit(-1,  "m"),
                };

                var function = ExpressionEvaluator.Create("Abs([a])", parameters);

                var result = function.Evaluate();

                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.TypeOf<ValueUnit>());
                    Assert.That(((ValueUnit)result).Value, Is.EqualTo(1));
                    Assert.Contains("m", ((ValueUnit)result).Units.Select(u => u.Symbol).ToList());
                });
        }
    }
}
