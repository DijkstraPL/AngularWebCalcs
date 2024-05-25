using Build_IT_ScriptInterpreter.Expressions.Parameters;
using NUnit.Framework;

namespace Build_IT_ScriptInterpreterTests.UnitTests.Expressions.Parameters
{
    [TestFixture]
    public class PiParameterTests
    {
        [Test]
        public void ParameterDataTest_Success()
        {
            var eParameter = new PiParameter();

            CollectionAssert.Contains(eParameter.Names, "π");
            CollectionAssert.Contains(eParameter.Names, "Pi");
            Assert.That(eParameter.Value, Is.EqualTo( 3.141593).Within(0.000001));
        }
    }
}
