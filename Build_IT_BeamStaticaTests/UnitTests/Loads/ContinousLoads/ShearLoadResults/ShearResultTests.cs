using Build_IT_BeamStatica.Loads.ContinuousLoads.ShearLoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads.ShearLoadResults
{
    [TestFixture()]
    public class ShearResultTests
    {
        private ShearResult _shearResult;

        [SetUp]
        public void SetUpData()
        {
            var continousLoad = new Mock<IContinousLoad>();
            continousLoad.Setup(cl => cl.StartPosition.Value).Returns(1);
            continousLoad.Setup(cl => cl.StartPosition.Position).Returns(1);
            continousLoad.Setup(cl => cl.EndPosition.Value).Returns(3);
            continousLoad.Setup(cl => cl.EndPosition.Position).Returns(7);
            continousLoad.Setup(cl => cl.Length)
                .Returns(continousLoad.Object.EndPosition.Position - continousLoad.Object.StartPosition.Position);

            _shearResult = new ShearResult(continousLoad.Object);
        }

        [Test()]
        public void ShearLoadResults_GetValueForShearForceTest_InsideLoadLength_Success()
        {
            var result = _shearResult.GetValue(distanceFromLoadStartPosition: 2);

            Assert.That(result, Is.EqualTo(2.666667).Within(0.000001));
        }

        [Test()]
        public void ShearLoadResults_GetValueForShearForceTest_OutsideLoadLength_Success()
        {
            var result = _shearResult.GetValue(distanceFromLoadStartPosition: 8);

            Assert.That(result, Is.EqualTo(12).Within(0.000001));
        }
    }
}