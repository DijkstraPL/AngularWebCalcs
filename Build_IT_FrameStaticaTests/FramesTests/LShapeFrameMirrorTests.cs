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
    [Property("Name", "2019.09.30-01")]
    public class LShapeFrameInsertedInOppositeDirectionTests
    {
        private Frame _frame;

        [SetUp]
        public void SetUpFrame()
        {
            var material = new Material(youngModulus: 210, density: 0, thermalExpansionCoefficient: 0);
            var section = new SectionProperties(area: 1500, momentOfInteria: 312500);

            var node1 = new FixedNode(new Point(0, -6));
            var node2 = new FreeNode(new Point(0, 0));
            var node3 = new PinNode(new Point(6, 0));

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

            node2.ConcentratedForces.Add(new NormalLoad(value: -5));

            _frame = new Frame(spans, nodes);

            _frame.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.HorizontalForce.Value, Is.EqualTo(5).Within(0.01));
                Assert.That(_frame.Spans[0].LeftNode.VerticalForce.Value, Is.EqualTo(1.874).Within(0.001));
                Assert.That(_frame.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(18.755).Within(0.001));
                
                Assert.That(_frame.Spans[0].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.BendingMoment, Is.Null);
                
                Assert.That(_frame.Spans[1].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.VerticalForce.Value, Is.EqualTo(-1.874).Within(0.001));
                Assert.That(_frame.Spans[1].RightNode.BendingMoment, Is.Null);
         });
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.LeftRotation, Is.Null);
                Assert.That(_frame.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
                Assert.That(_frame.Spans[0].LeftNode.VerticalDeflection, Is.Null);

                Assert.That(_frame.Spans[1].LeftNode.LeftRotation.Value, Is.EqualTo(0.000034).Within(0.000001));
                Assert.That(_frame.Spans[0].RightNode.HorizontalDeflection.Value, Is.EqualTo(-0.240).Within(0.001));
                Assert.That(_frame.Spans[0].RightNode.VerticalDeflection.Value, Is.EqualTo(-0.0004).Within(0.0001));
                Assert.That(_frame.Spans[0].RightNode.RightRotation.Value, Is.EqualTo(0.000034).Within(0.000001));

                Assert.That(_frame.Spans[1].RightNode.HorizontalDeflection.Value, Is.EqualTo(-0.240).Within(0.001));
                Assert.That(_frame.Spans[1].RightNode.VerticalDeflection, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.RightRotation.Value, Is.EqualTo(-0.000017).Within(0.000001));

            });
        }

        //[Test()]
        //[TestCase(0, 0)]
        //[TestCase(2, 0)]
        //[TestCase(3, 0)]
        //[TestCase(4, 0)]
        //[TestCase(6, 0)]
        //public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        //{
        //    double calculatedShear = _frame.Results.NormalForce.GetValue(position).Value;

        //    Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        //}

        //[Test()]
        //[TestCase(0, -1.871)]
        //[TestCase(2, -1.871)]
        //[TestCase(3, -1.871)]
        //[TestCase(4, -1.871)]
        //[TestCase(6, -1.871)]
        //public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        //{
        //    double calculatedShear = _frame.Results.Shear.GetValue(position).Value;

        //    Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        //}
    }
}