using Build_IT_SnowLoads.API;
using NUnit.Framework;

namespace Build_IT_SnowLoadsUnitTests.API
{
    [TestFixture()]
    public class UnitConversionTests
    {
        [Test]
        public void ConvertPercentageToDegreeTest_40Percent()
        {
            double percent = 40;
            double result = UnitConversion.ConvertToDegrees(percent);
            Assert.AreEqual(21.801, result, 0.001);
        }
        
        [Test]
        public void ConvertToRadiansTest_90Degrees()
        {
            double degrees = 90;
            double result = UnitConversion.ConvertToRadians(degrees);

            Assert.AreEqual(1.571, result, 0.001);
        }
    }
}