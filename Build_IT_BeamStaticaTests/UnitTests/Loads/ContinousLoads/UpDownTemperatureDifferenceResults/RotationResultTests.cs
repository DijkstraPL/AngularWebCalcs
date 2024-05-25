using Build_IT_BeamStatica.Loads.ContinuousLoads.UpDownTemperatureDifferenceResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads.UpDownTemperatureDifferenceResults
{
    [TestFixture()]
    public class RotationResultTests
    {
        [Test()]
        public void SpanExtendLoadResult_GetValueForRotationTest_Success()
        {
            var continousLoad = new Mock<IContinousLoad>();
            continousLoad.Setup(cl => cl.StartPosition.Value).Returns(0);
            continousLoad.Setup(cl => cl.EndPosition.Value).Returns(3);

            var span = new Mock<ISpan>();
            span.Setup(s => s.Section).Returns(new Build_IT_BeamStatica.Data.SectionData { SolidHeight = 3 });

            var rotationResult = new RotationResult(continousLoad.Object);

           var result = rotationResult.GetValue(span.Object, distanceFromLeftSide: 4, currentLength: 1);

            Assert.That(result, Is.EqualTo(-0.0003));
        }
    }
}