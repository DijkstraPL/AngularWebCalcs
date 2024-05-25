using Build_IT_Data.Materials.Intefaces;
using Build_IT_Data.Sections.Interfaces;
using Build_IT_FrameStatica.Nodes.Interfaces;
using Build_IT_FrameStatica.Spans;
using Build_IT_Data.Geometry;
using Moq;
using NUnit.Framework;

namespace Build_IT_FrameStaticaTests.UnitTests.Spans
{
    [TestFixture]
    public class SpanTests
    {
        [Test]
        public void GetAngleTest_Returns0()
        {
            var material = Mock.Of<IMaterial>();
            var section = Mock.Of<ISection>();
            var leftNode = new Mock<INode>();
            var rightNode = new Mock<INode>();
            leftNode.Setup(ln => ln.Position).Returns(new Point(0, 6));
            rightNode.Setup(rn => rn.Position).Returns(new Point(6, 6));

            var span = new Span(leftNode.Object, rightNode.Object, material, section, includeSelfWeight: false);

            var result = span.GetAngle();

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetAngleTest_MinusAngle_ReturnsMinus45()
        {
            var material = Mock.Of<IMaterial>();
            var section = Mock.Of<ISection>();
            var leftNode = new Mock<INode>();
            var rightNode = new Mock<INode>();
            leftNode.Setup(ln => ln.Position).Returns(new Point(2, 2));
            rightNode.Setup(rn => rn.Position).Returns(new Point(1, 3));

            var span = new Span(leftNode.Object, rightNode.Object, material, section, includeSelfWeight: false);

            var result = span.GetAngle();

            Assert.That(result, Is.EqualTo(-45));
        }

        [Test]
        public void GetAngleTest_MinusAngle_ReturnsMinus90()
        {
            var material = Mock.Of<IMaterial>();
            var section = Mock.Of<ISection>();
            var leftNode = new Mock<INode>();
            var rightNode = new Mock<INode>();
            leftNode.Setup(ln => ln.Position).Returns(new Point(0, 6));
            rightNode.Setup(rn => rn.Position).Returns(new Point(0, 0));

            var span = new Span(leftNode.Object, rightNode.Object, material, section, includeSelfWeight: false);

            var result = span.GetAngle();

            Assert.That(result, Is.EqualTo(-90));
        }

        [Test]
        public void GetAngleTest_Returns45()
        {
            var material = Mock.Of<IMaterial>();
            var section = Mock.Of<ISection>();
            var leftNode = new Mock<INode>();
            var rightNode = new Mock<INode>();
            leftNode.Setup(ln => ln.Position).Returns(new Point(1, 1));
            rightNode.Setup(rn => rn.Position).Returns(new Point(2, 2));

            var span = new Span(leftNode.Object, rightNode.Object, material, section, includeSelfWeight: false);

            var result = span.GetAngle();

            Assert.That(result, Is.EqualTo(45));
        }
    }
}
