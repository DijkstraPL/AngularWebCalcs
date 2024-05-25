using Build_IT_DataAccess.ScriptInterpreter.Entities;
using NUnit.Framework;

namespace Build_IT_DataAccessTests.Entities.Scripts
{
    [TestFixture]
    public class ScriptTests
    {
        [Test]
        public void ConstructorTest_CollectionsInitialized()
        {
            var script = new Script();

            Assert.Multiple(() =>
            {
                Assert.That(script.Tags, Is.Not.Null);
                Assert.That(script.Parameters, Is.Not.Null);
                Assert.That(script.ScriptTranslations, Is.Not.Null);
            });
        }
    }
}
