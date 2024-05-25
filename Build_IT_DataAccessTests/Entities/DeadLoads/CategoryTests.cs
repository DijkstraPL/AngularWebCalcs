using Build_IT_DataAccess.DeadLoads.Entities;
using NUnit.Framework;

namespace Build_IT_DataAccessTests.Entities.DeadLoads
{
    [TestFixture]
    public class CategoryTests
    {
        [Test]
        public void ConstructorTest_CollectionsInitialized()
        {
            var category = new Category();

            Assert.That(category.Subcategories, Is.Not.Null);
        }
    }
}
