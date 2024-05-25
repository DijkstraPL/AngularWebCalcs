using Build_IT_NCalc.Units;
using Build_IT_ScriptInterpreter.Parameters;
using Build_IT_ScriptInterpreter.Scripts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Scripts
{
    [TestFixture]
    public class ScriptTests
    {
        [Test]
        public void GetParameterByNameTest_Success()
        {
                var valueUnit1 = new ValueUnit(1,  new CustomUnit("kPa", 1));
                var valueUnit2 = new ValueUnit(2,  new CustomUnit("m", 2));

                var parameter1 = new Parameter<ValueUnit>(1, "a", valueUnit1, string.Empty, string.Empty);
                var parameter2 = new Parameter<ValueUnit>(2, "b", valueUnit2, string.Empty, string.Empty);

                var script = new Script("Script1", parameter1, parameter2);

                var parameter = script.GetParameterByName("a");

                Assert.That(parameter, Is.SameAs(parameter1));
        }

        [Test]
        public void CalculateTest_Success()
        {
                var valueUnit1 = new ValueUnit(3, new CustomUnit("kN", 1));
                var valueUnit2 = new ValueUnit(2,  new CustomUnit("m", -2));

                var parameter1 = new InputParameter<ValueUnit>(1, "a", valueUnit1, string.Empty, string.Empty);
                var parameter2 = new InputParameter<ValueUnit>(2, "b", valueUnit2, string.Empty, string.Empty);
                var parameter3 = new Parameter<string>(3, "c", "[a]*[b]", string.Empty, string.Empty);

                var script = new Script("Script1", parameter1, parameter2, parameter3);

                var calculatedParameters = script.CalculateScript();

                var calculatedParameter3 = calculatedParameters.First(p => p.Name == "c");

                Assert.That(calculatedParameter3.CalculatedValue, Is.EqualTo(new ValueUnit(6,  new CustomUnit("kPa"))));
            
        }
    }
}
