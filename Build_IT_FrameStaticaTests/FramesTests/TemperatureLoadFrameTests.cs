using Build_IT_Data.Materials;
using Build_IT_Data.Sections;
using Build_IT_FrameStatica.Frames;
using Build_IT_FrameStatica.Loads.ContinuousLoads;
using Build_IT_FrameStatica.Loads.PointLoads;
using Build_IT_FrameStatica.Nodes;
using Build_IT_FrameStatica.Spans;
using Build_IT_Data.Geometry;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_FrameStaticaTests.FramesTests
{
    [TestFixture]
    [Property("Name", "2021.09.17-01")]
    public class TemperatureLoadFrameTests
    {
        private Frame _frame;

        [SetUp]
        public void SetUpFrame()
        {
            var material = new Material(youngModulus: 30, density: 0, thermalExpansionCoefficient: 0.000010);
            var section = new RectangleSection(width: 300, height: 500);

            var node1 = new PinNode(new Point(0, 0));
            var node2 = new FreeNode(new Point(20, 10));
            var node3 = new FreeNode(new Point(40, 10));
            var node4 = new SleeveNode(new Point(20, 0));
            var node5 = new FixedNode(new Point(50, 0));

            var nodes = new Node[] { node1, node2, node3, node4, node5 };

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

            var span3 = new Span(
                leftNode: node2,
                rightNode: node4,
                material: material,
                section: section,
                 includeSelfWeight: false
                );

            var span4 = new Span(
                leftNode: node3,
                rightNode: node5,
                material: material,
                section: section,
                 includeSelfWeight: false
                );

            var spans = new Span[] { span1, span2, span3, span4 };

            span3.ContinousLoads.Add(AlongTemperatureDifferenceLoad.Create(span3, 15));
            span4.ContinousLoads.Add(UpDownTemperatureDifferenceLoad.Create(span4, 10, 0));

            _frame = new Frame(spans, nodes);

            _frame.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[0].LeftNode.VerticalForce.Value, Is.EqualTo(-0.166).Within(0.001));
                Assert.That(_frame.Spans[0].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[0].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[2].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[2].LeftNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[2].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[2].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[2].RightNode.VerticalForce.Value, Is.EqualTo(0.990).Within(0.001));
                Assert.That(_frame.Spans[2].RightNode.BendingMoment.Value, Is.EqualTo(-1.768).Within(0.001));

                Assert.That(_frame.Spans[3].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[3].LeftNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[3].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[3].RightNode.HorizontalForce.Value, Is.EqualTo(0).Within(0.001));
                Assert.That(_frame.Spans[3].RightNode.VerticalForce.Value, Is.EqualTo(-0.824).Within(0.001));
                Assert.That(_frame.Spans[3].RightNode.BendingMoment.Value, Is.EqualTo(-19.651).Within(0.001));
            });
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.HorizontalDeflection.Value, Is.EqualTo(-0.646).Within(0.001));
                Assert.That(_frame.Spans[0].LeftNode.VerticalDeflection, Is.Null);
                Assert.That(_frame.Spans[0].LeftNode.RightRotation.Value, Is.EqualTo(0.000206).Within(0.000001));

                Assert.That(_frame.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.000188).Within(0.000001));
                Assert.That(_frame.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(-1.394).Within(0.001));
                Assert.That(_frame.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(1.497).Within(0.001));
                Assert.That(_frame.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.000188).Within(0.000001));

                Assert.That(_frame.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(0.000485).Within(0.000001));
                Assert.That(_frame.Spans[1].RightNode.HorizontalDeflection.Value, Is.EqualTo(-1.394).Within(0.001));
                Assert.That(_frame.Spans[1].RightNode.VerticalDeflection.Value, Is.EqualTo(-1.391).Within(0.001));

                Assert.That(_frame.Spans[2].LeftNode.HorizontalDeflection.Value, Is.EqualTo(-1.394).Within(0.001));
                Assert.That(_frame.Spans[2].LeftNode.VerticalDeflection.Value, Is.EqualTo(1.497).Within(0.001));
                Assert.That(_frame.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(-0.000188).Within(0.000001));

                Assert.That(_frame.Spans[2].RightNode.HorizontalDeflection.Value, Is.EqualTo(-2.337).Within(0.001));
                Assert.That(_frame.Spans[2].RightNode.VerticalDeflection, Is.Null);
                Assert.That(_frame.Spans[2].RightNode.LeftRotation, Is.Null);

                Assert.That(_frame.Spans[3].LeftNode.HorizontalDeflection.Value, Is.EqualTo(-1.394).Within(0.001));
                Assert.That(_frame.Spans[3].LeftNode.VerticalDeflection.Value, Is.EqualTo(-1.391).Within(0.001));
                Assert.That(_frame.Spans[3].LeftNode.RightRotation.Value, Is.EqualTo(0.000485).Within(0.000001));

                Assert.That(_frame.Spans[3].RightNode.HorizontalDeflection, Is.Null);
                Assert.That(_frame.Spans[3].RightNode.VerticalDeflection, Is.Null);
                Assert.That(_frame.Spans[3].RightNode.LeftRotation, Is.Null);
            });
        }
    }
}