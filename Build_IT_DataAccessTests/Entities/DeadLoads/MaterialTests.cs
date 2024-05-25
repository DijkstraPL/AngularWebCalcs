using Build_IT_DataAccess.DeadLoads.Entities;
using NUnit.Framework;

namespace Build_IT_DataAccessTests.Entities.DeadLoads
{
    [TestFixture]
    public class MaterialTests
    {
        [Test]
        public void ConstructorTest_CollectionsInitialized()
        {
            var material = new Material();

            Assert.That(material.MaterialAdditions, Is.Not.Null);
        }
    }
}
