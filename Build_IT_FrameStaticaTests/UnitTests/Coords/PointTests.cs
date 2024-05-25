using Build_IT_Data.Geometry;
using NUnit.Framework;

namespace Build_IT_FrameStaticaTests.UnitTests.Coords
{
    [TestFixture]
    public class PointTests
    {
        [Test]
        public void ConstructorTest_Success()
        {
            var point = new Point(5,1);

            Assert.That(point.X, Is.EqualTo(5));
            Assert.That(point.Y, Is.EqualTo(1));
        }

        [Test]
        public void DistanceBetweenPointsTest_Success()
        {
            var point1 = new Point(5, 1);
            var point2 = new Point(10, 5);

            Assert.That(point1.DistanceTo(point2),Is.EqualTo(6.403).Within(0.001));
            Assert.That(point2.DistanceTo(point1),Is.EqualTo(6.403).Within(0.001));
        }
    }
}
