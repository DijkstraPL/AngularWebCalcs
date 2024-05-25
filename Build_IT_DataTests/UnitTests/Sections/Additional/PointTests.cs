using Build_IT_Data.Sections.Additional;
using NUnit.Framework;

namespace Build_IT_DataTests.UnitTests.Sections.Additional
{
    [TestFixture]
    public class PointTests
    {
        [Test()]
        public void ConstructorTest_Success()
        {
            var point = new Point(1, 2);

            Assert.That(point.X, Is.EqualTo(1));
            Assert.That(point.Y, Is.EqualTo(2));
        }

        [Test()]
        public void PlusOperatorTest_Success()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = point1 + point2;
            Assert.That(point3.X, Is.EqualTo(3));
            Assert.That(point3.Y, Is.EqualTo(5));
        }

        [Test()]
        public void MinusOperatorTest_Success()
        {
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 3);
            var point3 = point2 - point1;
            Assert.That(point3.X, Is.EqualTo(1));
            Assert.That(point3.Y, Is.EqualTo(1));
        }
    }
}
