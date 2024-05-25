using Build_IT_SnowLoads.API;
using NUnit.Framework;

namespace Build_IT_SnowLoadsUnitTests.API
{
    [TestFixture()]
    public class SnowLoadCalcTests
    {
        #region CalculateSnowLoad

        [Test]
        public void CalculateSnowLoadTest_Success()
        {
            double shapeCoefficient = 1.5;
            double exposureCoefficient = 1.2;
            double thermalCoefficient = 0.8;
            double snowLoad = 0.9;

            double result = SnowLoadCalc.CalculateSnowLoad(shapeCoefficient, exposureCoefficient, thermalCoefficient, snowLoad);

            Assert.AreEqual(1.296, result);
        }

        #endregion // CalculateSnowLoad

        #region CalculateSnowLoadForAnnexB

        [Test]
        public void CalculateSnowLoadForAnnexBTest_Success()
        {
            double shapeCoefficient = 2;
            double snowLoad = 3;

            double result = SnowLoadCalc.CalculateSnowLoadForAnnexB(shapeCoefficient, snowLoad);

            Assert.AreEqual(6, result);
        }

        #endregion // CalculateSnowLoadForAnnexB
    }
}