using Build_IT_DataAccess.SteelProfiles.Entities;
using NUnit.Framework;

namespace Build_IT_DataAccessTests.Entities.SteelProfiles
{
    [TestFixture]
    public class ProfileTypeTests
    {
        [Test]
        public void ConstructorTest_CollectionsInitialized()
        {
            var profileType = new ProfileType();

            Assert.Multiple(() =>
            {
                Assert.That(profileType.Parameters, Is.Not.Null);
                Assert.That(profileType.SectionPoints, Is.Not.Null);
                Assert.That(profileType.SteelProfiles, Is.Not.Null);
            });
        }
    }
}
