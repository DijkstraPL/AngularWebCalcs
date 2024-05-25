using Build_IT_BeamStatica.Loads.ContinuousLoads.SpanExtendLoadResult;
using Build_IT_BeamStatica.Loads.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads.SpanExtendLoadResult
{
    [TestFixture()]
    public class HorizontalDeflectionResultTests
    {
        [Test()]
        public void SpanExtendLoadResult_GetValueForHorizontalDeflectionTest_Success()
        {
            var continousLoad = new Mock<IContinousLoad>();
            continousLoad.Setup(cl => cl.StartPosition.Value).Returns(0);
            continousLoad.Setup(cl => cl.EndPosition.Value).Returns(3);

            var horizontalDeflectionResult = new HorizontalDeflectionResult(continousLoad.Object);

           var result =  horizontalDeflectionResult.GetValue(null, distanceFromLeftSide: 4, currentLength: 1);

            Assert.That(result, Is.EqualTo(-0.09));
        }
    }
}