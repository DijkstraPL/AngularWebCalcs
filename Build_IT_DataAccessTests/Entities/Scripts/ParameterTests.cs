using Build_IT_DataAccess.ScriptInterpreter.Entities;
using NUnit.Framework;

namespace Build_IT_DataAccessTests.Entities.Scripts
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void ConstructorTest_CollectionsInitialized()
        {
            var parameter = new Parameter();

            Assert.Multiple(() =>
            {
                Assert.That(parameter.ValueOptions, Is.Not.Null);
                Assert.That(parameter.ParameterFigures, Is.Not.Null);
                Assert.That(parameter.ParametersTranslations, Is.Not.Null);
            });
        }
    }
}
