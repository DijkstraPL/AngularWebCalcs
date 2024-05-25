using Build_IT_Data.Materials;
using Build_IT_Data.Sections;
using Build_IT_FrameStatica.Frames;
using Build_IT_FrameStatica.Loads.PointLoads;
using Build_IT_FrameStatica.Nodes;
using Build_IT_FrameStatica.Spans;
using Build_IT_Data.Geometry;
using NUnit.Framework;

namespace Build_IT_FrameStaticaTests.FramesTests
{
    [TestFixture]
    [Property("Name", "2019.09.30-02")]
    public class LShapeFrameTests
    {
        private Frame _frame;

        [SetUp]
        public void SetUpFrame()
        {
            var material = new Material(youngModulus: 210, density: 0, thermalExpansionCoefficient: 0);
            var section = new SectionProperties(area: 1500, momentOfInteria: 312500);

            var node1 = new PinNode(new Point(0, 0));
            var node2 = new FreeNode(new Point(6, 0));
            var node3 = new FixedNode(new Point(6, -6));

            var nodes = new Node[] { node1, node2, node3 };

            var span1 = new Span(
                leftNode: node1,
                rightNode: node2,
                material: material,
                section: section,
                 includeSelfWeight: false
                );

            var span2 = new Span(
                leftNode: node2,
                rightNode: node3,
                material: material,
                section: section,
                 includeSelfWeight: false
                );

            var spans = new Span[] { span1, span2 };

            node2.ConcentratedForces.Add(new NormalLoad(value: 5));

            _frame = new Frame(spans, nodes);

            _frame.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[0].LeftNode.VerticalForce.Value, Is.EqualTo(-1.874).Within(0.001));
                Assert.That(_frame.Spans[0].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[0].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].RightNode.HorizontalForce.Value, Is.EqualTo(-5).Within(0.01));
                Assert.That(_frame.Spans[1].RightNode.VerticalForce.Value, Is.EqualTo(1.874).Within(0.001));
                Assert.That(_frame.Spans[1].RightNode.BendingMoment.Value, Is.EqualTo(-18.755).Within(0.001));
            });
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0.2401).Within(0.0001));
                Assert.That(_frame.Spans[0].LeftNode.VerticalDeflection, Is.Null);
                Assert.That(_frame.Spans[0].LeftNode.RightRotation.Value, Is.EqualTo(0.000017).Within(0.000001));

                Assert.That(_frame.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.000034).Within(0.000001));
                Assert.That(_frame.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0.240).Within(0.001));
                Assert.That(_frame.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(-0.00036).Within(0.00001));
                Assert.That(_frame.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.000034).Within(0.00001));

                Assert.That(_frame.Spans[1].RightNode.LeftRotation, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.HorizontalDeflection, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.VerticalDeflection, Is.Null);
            });
        }

        [Test()]
        [TestCase(0, 0, 0)]
        [TestCase(2, 0, 0)]
        [TestCase(3, 0, 0)]
        [TestCase(4, 0, 0)]
        [TestCase(6, 0, 0)]
        [TestCase(0, 1, 1.874)]
        [TestCase(2, 1, 1.874)]
        [TestCase(3, 1, 1.874)]
        [TestCase(4, 1, 1.874)]
        [TestCase(6, 1, 1.874)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, short spanIndex, double result)
        {
            double calculatedNormalForce = _frame.Results.NormalForce.GetValue(position, spanIndex).Value;

            Assert.That(calculatedNormalForce, Is.EqualTo(result).Within(0.001), message: $"Span {spanIndex} at {position}m.");
        }

        [Test()]
        [TestCase(0, 0, -1.874)]
        [TestCase(2, 0, -1.874)]
        [TestCase(3, 0, -1.874)]
        [TestCase(4, 0, -1.874)]
        [TestCase(6, 0, -1.874)]
        [TestCase(0, 1, 5)]
        [TestCase(2, 1, 5)]
        [TestCase(3, 1, 5)]
        [TestCase(4, 1, 5)]
        [TestCase(6, 1, 5)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, short spanIndex, double result)
        {
            double calculatedShear = _frame.Results.Shear.GetValue(position, spanIndex).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"Span {spanIndex} at {position}m.");
        }

        [Test()]
        [TestCase(0, 0, 0)]
        [TestCase(2, 0, -3.748)]
        [TestCase(4, 0, -7.497)]
        [TestCase(6, 0, -11.245)]
        [TestCase(0, 1, -11.245)]
        [TestCase(2, 1, -1.245)]
        [TestCase(4, 1, 8.755)]
        [TestCase(6, 1, 18.754)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, short spanIndex, double result)
        {
            double calculatedMoment = _frame.Results.BendingMoment.GetValue(position, spanIndex).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"Span {spanIndex} at {position}m.");
        }

        [Test()]
        [TestCase(0, 0, 0.000017)]
        [TestCase(2, 0, 0.000011)]
        [TestCase(4, 0, -0.000006)]
        [TestCase(6, 0, -0.000034)]
        [TestCase(0, 1, -0.000034)]
        [TestCase(2, 1, -0.000053)]
        [TestCase(4, 1, -0.000042)]
        [TestCase(6, 1, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, short spanIndex, double result)
        {
            double rotation = _frame.Results.Rotation.GetValue(position, spanIndex).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"Span {spanIndex} at {position}m.");
        }

        [Test()]
        [TestCase(0, 0, 0.2401)]
        [TestCase(2, 0, 0.2401)]
        [TestCase(4, 0, 0.2401)]
        [TestCase(6, 0, 0.2401)]
        [TestCase(0, 1, 0.0004)]
        [TestCase(2, 1, 0.0002)]
        [TestCase(4, 1, 0.0001)]
        [TestCase(6, 1, 0)]
        public void NormalDeflectionAtPositionCalculationsTest_Successful(double position, short spanIndex, double result)
        {
            double deflection = _frame.Results.NormalDeflection.GetValue(position, spanIndex).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.0001), message: $"Span {spanIndex} at {position}m.");
        }

        [Test()]
        [TestCase(0, 0, 0)]
        [TestCase(2, 0, 0.0303)]
        [TestCase(4, 0, 0.0378)]
        [TestCase(6, 0, -0.0004)]
        [TestCase(0, 1, 0.2401)]
        [TestCase(2, 1, 0.1474)]
        [TestCase(4, 1, 0.0470)]
        [TestCase(6, 1, 0)]
        public void ShearDeflectionAtPositionCalculationsTest_Successful(double position, short spanIndex, double result)
        {
            double deflection = _frame.Results.ShearDeflection.GetValue(position, spanIndex).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.0001), message: $"Span {spanIndex} at {position}m.");
        }


        [Test()]
        [TestCase(0, 0, 0.2401)]
        [TestCase(2, 0, 0.2401)]
        [TestCase(4, 0, 0.2401)]
        [TestCase(6, 0, 0.2401)]
        [TestCase(0, 1, 0.2401)]
        [TestCase(2, 1, 0.1474)]
        [TestCase(4, 1, 0.0470)]
        [TestCase(6, 1, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, short spanIndex, double result)
        {
            double deflection = _frame.Results.GetHorizontalDeflection(position, spanIndex).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.0001), message: $"Span {spanIndex} at {position}m.");
        }

        [Test()]
        [TestCase(0, 0, 0)]
        [TestCase(2, 0, 0.0303)]
        [TestCase(4, 0, 0.0378)]
        [TestCase(6, 0, -0.0004)]
        [TestCase(0, 1, -0.0004)]
        [TestCase(2, 1, -0.0002)]
        [TestCase(4, 1, -0.0001)]
        [TestCase(6, 1, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, short spanIndex, double result)
        {
            double deflection = _frame.Results.GetVerticalDeflection(position, spanIndex).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.0001), message: $"Span {spanIndex} at {position}m.");
        }
    }
}