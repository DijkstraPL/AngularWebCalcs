using Build_IT_Data.Materials;
using Build_IT_Data.Sections;
using Build_IT_FrameStatica.Frames;
using Build_IT_FrameStatica.Frames.Interfaces;
using Build_IT_FrameStatica.Loads.PointLoads;
using Build_IT_FrameStatica.Nodes;
using Build_IT_FrameStatica.Nodes.Interfaces;
using Build_IT_FrameStatica.Spans;
using Build_IT_FrameStatica.Spans.Interfaces;
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
    [Property("Name", "2021.09.16-02")]
    public class FrameWithHingeTests
    {
        private Frame _frame;

        [SetUp]
        public void SetUpFrame()
        {
            var material = new Material(youngModulus: 30, density: 0, thermalExpansionCoefficient: 0);
            var section = new SectionProperties(area: 1500, momentOfInteria: 312500);

            var node1 = new FixedNode(new Point(0, 0));
            var node2 = new FreeNode(new Point(0, 10));
            var node3 = new FreeNode(new Point(10, 10));
            var node4 = new SupportedNode(new Point(10, 0));
            var node5 = new FreeNode(new Point(20, 10));
            var node6 = new FixedNode(new Point(20, 0));

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
                leftNode: node4,
                rightNode: node3,
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

            var span5 = new Span(
                leftNode: node6,
                rightNode: node5,
                material: material,
                section: section,
                 includeSelfWeight: false
                );

            var nodes = new List<INode> { node1, node2, node3, node4, node5, node6 };
            var spans = new List<ISpan> { span1, span2, span3, span4, span5 };

            node2.ConcentratedForces.Add(new NormalLoad(20));

            _frame = new Frame(spans, nodes);
            _frame.AddHingeToSpan(span3, IFrame.Side.Right);

            _frame.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.HorizontalForce.Value, Is.EqualTo(-10.016).Within(0.001));
                Assert.That(_frame.Spans[0].LeftNode.VerticalForce.Value, Is.EqualTo(-3.759).Within(0.001));
                Assert.That(_frame.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-62.599).Within(0.001));

                Assert.That(_frame.Spans[0].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[2].LeftNode.HorizontalForce.Value, Is.EqualTo(0).Within(0.001));
                Assert.That(_frame.Spans[2].LeftNode.VerticalForce.Value, Is.EqualTo(0.018).Within(0.001));
                Assert.That(_frame.Spans[2].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[2].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[2].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[2].RightNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[3].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[3].LeftNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[3].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[3].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[3].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[3].RightNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[4].LeftNode.HorizontalForce.Value, Is.EqualTo(-9.984).Within(0.001));
                Assert.That(_frame.Spans[4].LeftNode.VerticalForce.Value, Is.EqualTo(3.740).Within(0.001));
                Assert.That(_frame.Spans[4].LeftNode.BendingMoment.Value, Is.EqualTo(-62.412).Within(0.001));

                Assert.That(_frame.Spans[4].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[4].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[4].RightNode.BendingMoment, Is.Null);
            });
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
                Assert.That(_frame.Spans[0].LeftNode.VerticalDeflection, Is.Null);
                Assert.That(_frame.Spans[0].LeftNode.RightRotation, Is.Null);

                Assert.That(_frame.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.001335).Within(0.000001));
                Assert.That(_frame.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(15.580).Within(0.001));
                Assert.That(_frame.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(0.0083).Within(0.0001));
                Assert.That(_frame.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.001335).Within(0.000001));

                Assert.That(_frame.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(0.000665).Within(0.000001));
                Assert.That(_frame.Spans[1].RightNode.HorizontalDeflection.Value, Is.EqualTo(15.558).Within(0.001));
                Assert.That(_frame.Spans[1].RightNode.VerticalDeflection.Value, Is.EqualTo(0).Within(0.001));

                Assert.That(_frame.Spans[2].LeftNode.HorizontalDeflection, Is.Null);
                Assert.That(_frame.Spans[2].LeftNode.VerticalDeflection, Is.Null);
                Assert.That(_frame.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(-0.001556).Within(0.000001));

                Assert.That(_frame.Spans[5].RightNode.HorizontalDeflection.Value, Is.EqualTo(15.558).Within(0.001));
                Assert.That(_frame.Spans[5].RightNode.VerticalDeflection.Value, Is.EqualTo(0).Within(0.001));
                Assert.That(_frame.Spans[5].RightNode.LeftRotation.Value, Is.EqualTo(0.000665).Within(0.000001));

                Assert.That(_frame.Spans[3].LeftNode.HorizontalDeflection.Value, Is.EqualTo(15.558).Within(0.001));
                Assert.That(_frame.Spans[3].LeftNode.VerticalDeflection.Value, Is.EqualTo(0).Within(0.001));
                Assert.That(_frame.Spans[3].LeftNode.RightRotation.Value, Is.EqualTo(0.000665).Within(0.000001));

                Assert.That(_frame.Spans[3].RightNode.HorizontalDeflection.Value, Is.EqualTo(15.536).Within(0.001));
                Assert.That(_frame.Spans[3].RightNode.VerticalDeflection.Value, Is.EqualTo(-0.008).Within(0.001));
                Assert.That(_frame.Spans[3].RightNode.LeftRotation.Value, Is.EqualTo(-0.001332).Within(0.000001));

                Assert.That(_frame.Spans[4].LeftNode.HorizontalDeflection, Is.Null);
                Assert.That(_frame.Spans[4].LeftNode.VerticalDeflection, Is.Null);
                Assert.That(_frame.Spans[4].LeftNode.RightRotation, Is.Null);

                Assert.That(_frame.Spans[4].RightNode.HorizontalDeflection.Value, Is.EqualTo(15.536).Within(0.001));
                Assert.That(_frame.Spans[4].RightNode.VerticalDeflection.Value, Is.EqualTo(-0.008).Within(0.001));
                Assert.That(_frame.Spans[4].RightNode.LeftRotation.Value, Is.EqualTo(-0.001332).Within(0.000001));
            });
        }
    }
}
