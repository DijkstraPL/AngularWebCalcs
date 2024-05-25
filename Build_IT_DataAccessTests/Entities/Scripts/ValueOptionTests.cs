using Build_IT_DataAccess.ScriptInterpreter.Entities;
using NUnit.Framework;

namespace Build_IT_DataAccessTests.Entities.Scripts
{
    [TestFixture]
    public class ValueOptionTests
    {
        [Test]
        public void ConstructorTest_CollectionsInitialized()
        {
            var valueOption = new ValueOption();

            Assert.That(valueOption.ValueOptionsTranslations, Is.Not.Null);
        }
    }
}
