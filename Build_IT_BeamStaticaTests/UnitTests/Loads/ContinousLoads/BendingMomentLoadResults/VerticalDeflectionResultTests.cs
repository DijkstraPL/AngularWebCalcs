using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads.BendingMomentLoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads.BendingMomentLoadResults
{
    [TestFixture()]
    public class VerticalDeflectionResultTests
    {
        [Test()]
        public void BendingMomentLoadResults_GetValueForVerticalDeflectionTest_Success()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Section)
                .Returns(new SectionData { MomentOfInteria = 4 });
            span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus = 2 });
            
            var continousLoad = new Mock<IContinousLoad>();
            continousLoad.Setup(cl => cl.StartPosition.Value).Returns(5);

            var verticalDeflectionResult = new VerticalDeflectionResult(continousLoad.Object);

            var result = verticalDeflectionResult.GetValue(span.Object, distanceFromLeftSide: 7, currentLength:  1);

            Assert.That(result, Is.EqualTo(22.5));
        }
    }
}
