using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.DynamicCharacteristics;
using Moq;
using NUnit.Framework;

namespace Build_IT_WindLoadsTests.UnitTests.DynamicCharacteristicsTests
{
    [TestFixture]
    public class FundamentalFlexuralFrequencyForHeightAbove50Tests
    {
        [Test]
        public void GetFactorTest_Success()
        {
            var building = new Mock<IStructure>();
            building.Setup(b => b.Height).Returns(50);
            var fundamentalFlexuralFrequency =
                new FundamentalFlexuralFrequencyForHeightAbove50(building.Object);

            Assert.That(fundamentalFlexuralFrequency.GetFactor(), Is.EqualTo(0.92));
        }
    }
}
