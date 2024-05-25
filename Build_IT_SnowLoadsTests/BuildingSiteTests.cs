using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Enums;
using NUnit.Framework;

namespace Build_IT_SnowLoadsUnitTests
{
    [TestFixture()]
    public class BuildingSiteTests
    {
        #region CalculateExposureCoefficient

        [Test()]
        [Description("Ensure that method for calculation of exposure coefficient work correctly.")]
        public void CalculateExposureCoefficientTest_WindsweptTopography_Success()
        {
            var buildingSite = new BuildingSite( currentTopography: Topographies.Windswept);

            buildingSite.CalculateExposureCoefficient();

            Assert.AreEqual(0.8, buildingSite.ExposureCoefficient, "Exposure coefficient is wrong.");
        }

        [Test()]
        [Description("Ensure that method for calculation of exposure coefficient work correctly.")]
        public void CalculateExposureCoefficientTest_NormalTopography_Success()
        {
            var buildingSite = new BuildingSite(currentTopography: Topographies.Normal);

            buildingSite.CalculateExposureCoefficient();

            Assert.AreEqual(1.0, buildingSite.ExposureCoefficient, "Exposure coefficient is wrong.");
        }

        [Test()]
        [Description("Ensure that method for calculation of exposure coefficient work correctly.")]
        public void CalculateExposureCoefficientTest_ShelteredTopography_Success()
        {
            var buildingSite = new BuildingSite(currentTopography: Topographies.Sheltered);

            buildingSite.CalculateExposureCoefficient();

            Assert.AreEqual(1.2, buildingSite.ExposureCoefficient, "Exposure coefficient is wrong.");
        }

        [Test()]
        [Description("Ensure that method for calculation of exposure coefficient work correctly.")]
        public void CalculateExposureCoefficientTest_NoneTopography_Success()
        {
            var buildingSite = new BuildingSite(currentTopography: Topographies.None);

            buildingSite.CalculateExposureCoefficient();

            Assert.AreEqual(1.2, buildingSite.ExposureCoefficient, "Exposure coefficient is wrong.");
        }
        
        #endregion // CalculateExposureCoefficient
    }
}