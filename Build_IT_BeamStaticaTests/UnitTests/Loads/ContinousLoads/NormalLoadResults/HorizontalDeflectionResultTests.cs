using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads.NormalLoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads.NormalLoadResults
{
    [TestFixture()]
    public class HorizontalDeflectionResultTests
    {
        private ISpan _span;
        private HorizontalDeflectionResult _horizontalDeflectionResult;

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

            var span = new Mock<ISpan>();
            span.Setup(s => s.Section)
                .Returns(new SectionData { Area = 5 });
            span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus = 2 });
            
            _span = span.Object;
            _horizontalDeflectionResult = new HorizontalDeflectionResult(continousLoad.Object);
        }

        [Test()]
        public void NormalLoadResults_GetValueForHorizontalDeflectionTest_InsideLoadLength_Success()
        {
            var result = _horizontalDeflectionResult.GetValue(_span, distanceFromLeftSide: 4, currentLength: 1);

            Assert.That(result, Is.EqualTo(0.2444444).Within(0.000001));
        }

        [Test()]
        public void NormalLoadResults_GetValueForHorizontalDeflectionTest_OutsideLoadLength_Success()
        {
            var result = _horizontalDeflectionResult.GetValue(_span, distanceFromLeftSide: 9, currentLength: 1);

            Assert.That(result, Is.EqualTo(4.2).Within(0.000001));
        }

        [Test()]
        public void NormalLoadResults_GetValueForHorizontalDeflectionTest_BeforeLoadLength_Success()
        {
                var result = _horizontalDeflectionResult.GetValue(_span, distanceFromLeftSide: 1, currentLength: 0);

            Assert.That(result, Is.EqualTo(0).Within(0.000001));
        }
    }
}