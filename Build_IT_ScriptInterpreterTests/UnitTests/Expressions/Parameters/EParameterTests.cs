using Build_IT_ScriptInterpreter.Expressions.Parameters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Expressions.Parameters
{
    [TestFixture]
    public class EParameterTests
    {
        [Test]
        public void ParameterDataTest_Success()
        {
            var eParameter = new EParameter();

            CollectionAssert.Contains(eParameter.Names, "e");
            Assert.That(eParameter.Value, Is.EqualTo( 2.718282).Within(0.000001));
        }
    }
}
