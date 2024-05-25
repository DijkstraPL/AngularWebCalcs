using Build_IT_DataAccess.DeadLoads.Entities;
using NUnit.Framework;

namespace Build_IT_DataAccessTests.DeadLoads
{
    [TestFixture]
    public class SubcategoryTests
    {
        [Test]
        public void ConstructorTest_CollectionsInitialized()
        {
            var subcategory = new Subcategory();

            Assert.That(subcategory.Materials, Is.Not.Null);
        }
    }
}
