using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads.ShearLoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads.ShearLoadResults
{
    [TestFixture]
    public class VerticalDeflectionResultTests
    {
        private VerticalDeflectionResult _verticalDeflectionResult;
        private ISpan _span;

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
                .Returns(new SectionData { MomentOfInteria = 4 });
            span.Setup(s => s.Material).Returns(new MaterialData { YoungModulus = 2 });

            _span = span.Object;

            _verticalDeflectionResult = new VerticalDeflectionResult(continousLoad.Object);
        }

        [Test()]
        public void ShearLoadResults_GetValueForVerticalDeflectionTest_InsideLoadLength_Success()
        {
            var result = _verticalDeflectionResult.GetValue(_span, distanceFromLeftSide: 3, currentLength: 1);

            Assert.That(result, Is.EqualTo(0.005555556).Within(0.000000001));
        }

        [Test()]
        public void ShearLoadResults_GetValueForVerticalDeflectionTest_OutsideLoadLength_Success()
        {
            var result = _verticalDeflectionResult.GetValue(_span, distanceFromLeftSide: 9, currentLength: 1);

            Assert.That(result, Is.EqualTo(18.325).Within(0.000000001));
        }

        [Test()]
        public void ShearLoadResults_GetValueForVerticalDeflectionTest_BeforeLoadLength_Success()
        {
            var result = _verticalDeflectionResult.GetValue(_span, distanceFromLeftSide: 2, currentLength: 1);

            Assert.That(result, Is.EqualTo(0));
        }
    }
}
