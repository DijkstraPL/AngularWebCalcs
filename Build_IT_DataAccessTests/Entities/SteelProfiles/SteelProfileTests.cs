using Build_IT_DataAccess.SteelProfiles.Entities;
using NUnit.Framework;

namespace Build_IT_DataAccessTests.Entities.SteelProfiles
{
    [TestFixture]
    public class SteelProfileTests
    {
        [Test]
        public void ConstructorTest_CollectionsInitialized()
        {
            var steelProfile = new SteelProfile();

            Assert.That(steelProfile.ParametersValues, Is.Not.Null);
        }
    }
}
