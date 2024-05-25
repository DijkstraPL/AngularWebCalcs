using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads.AlongTemperatureDifferenceResult;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.UnitTests.Loads.ContinousLoads.AlongTemperatureDifferenceResult
{
    [TestFixture()]
    public class HorizontalDeflectionResultTests
    {
        [Test()]
        public void AlongTemperatureDifferenceResult_GetValueForHorizontalDeflectionTest_Success()
        {
            var span = new Mock<ISpan>();
            span.Setup(s => s.Material).Returns(new MaterialData { ThermalExpansionCoefficient = 2 });

            var continousLoad = new Mock<IContinousLoad>();
            continousLoad.Setup(cl => cl.StartPosition.Value).Returns(0);
            continousLoad.Setup(cl => cl.EndPosition.Value).Returns(3);

            var horizontalDeflectionResult = new HorizontalDeflectionResult(continousLoad.Object);

           var result =  horizontalDeflectionResult.GetValue(span.Object, distanceFromLeftSide: 4, currentLength: 1);

            Assert.That(result, Is.EqualTo(-1800));
        }
    }
}