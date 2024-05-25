using Build_IT_Data.Materials;
using Build_IT_Data.Sections;
using Build_IT_FrameStatica.Frames;
using Build_IT_FrameStatica.Loads.ContinuousLoads;
using Build_IT_FrameStatica.Loads.PointLoads;
using Build_IT_FrameStatica.Nodes;
using Build_IT_FrameStatica.Spans;
using Build_IT_Data.Geometry;
using NUnit.Framework;
using System.Linq;

namespace Build_IT_FrameStaticaTests.FramesTests
{
    [TestFixture]
    [Property("Name", "2019.10.01-01")]
    public class FrameWithDifferentDirectionsAndRandomOrderTests
    {
        private Frame _frame;

        [SetUp]
        public void SetUpFrame()
        {
            var material = new Material(youngModulus: 30, density: 0, thermalExpansionCoefficient: 0);
            var section = new SectionProperties(area: 1500, momentOfInteria: 312500);

            var node4 = new FixedNode(new Point(0, 0));
            var node1 = new FreeNode(new Point(0, 5));
            var node3 = new FreeNode(new Point(10, 4));
            var node2 = new FixedNode(new Point(5, 0));

            var nodes = new Node[] { node1, node2, node3, node4 };

            var span2 = new Span(
                leftNode: node1,
                rightNode: node4,
                material: material,
                section: section,
                 includeSelfWeight: false
                );

            var span1 = new Span(
                leftNode: node1,
                rightNode: node3,
                material: material,
                section: section,
                 includeSelfWeight: false
                );

            var span3 = new Span(
                leftNode: node2,
                rightNode: node3,
                material: material,
                section: section,
                 includeSelfWeight: false
                );
            
            var spans = new Span[] { span1, span2, span3 };

              node1.ConcentratedForces.Add(new BendingMoment(20));
              node3.ConcentratedForces.Add(new NormalLoad(100));

            span1.ContinousLoads.Add(ContinuousShearLoad.Create(
                   startPosition: 0, startValue: -10, endPosition: span1.Length, endValue: -10));

             span2.PointLoads.Add(new ShearLoad(position: 3, value: 20));

            _frame = new Frame(spans, nodes);

            _frame.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[0].LeftNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[0].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[0].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].RightNode.HorizontalForce.Value, Is.EqualTo(-73.974).Within(0.001));
                Assert.That(_frame.Spans[1].RightNode.VerticalForce.Value, Is.EqualTo(30.642).Within(0.001));
                Assert.That(_frame.Spans[1].RightNode.BendingMoment.Value, Is.EqualTo(-270.049).Within(0.001));

                Assert.That(_frame.Spans[2].LeftNode.HorizontalForce.Value, Is.EqualTo(-36.026).Within(0.001));
                Assert.That(_frame.Spans[2].LeftNode.VerticalForce.Value, Is.EqualTo(69.358).Within(0.001));
                Assert.That(_frame.Spans[2].LeftNode.BendingMoment.Value, Is.EqualTo(-298.162).Within(0.001));

                Assert.That(_frame.Spans[2].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[2].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[2].RightNode.BendingMoment, Is.Null);
            });
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.HorizontalDeflection.Value, Is.EqualTo(20.528).Within(0.001));
                Assert.That(_frame.Spans[0].LeftNode.VerticalDeflection.Value, Is.EqualTo(-0.034).Within(0.001));
                Assert.That(_frame.Spans[0].LeftNode.RightRotation.Value, Is.EqualTo(-0.005499).Within(0.000001));

                Assert.That(_frame.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.003600).Within(0.000001));
                Assert.That(_frame.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(20.528).Within(0.001));
                Assert.That(_frame.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(-0.034).Within(0.0001));
                Assert.That(_frame.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.005499).Within(0.000001));

                Assert.That(_frame.Spans[1].RightNode.LeftRotation, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.HorizontalDeflection, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.VerticalDeflection, Is.Null);

                Assert.That(_frame.Spans[2].LeftNode.HorizontalDeflection, Is.Null);
                Assert.That(_frame.Spans[2].LeftNode.VerticalDeflection, Is.Null);
                Assert.That(_frame.Spans[2].LeftNode.RightRotation, Is.Null);

                Assert.That(_frame.Spans[2].RightNode.HorizontalDeflection.Value, Is.EqualTo(18.360).Within(0.001));
                Assert.That(_frame.Spans[2].RightNode.VerticalDeflection.Value, Is.EqualTo(-22.985).Within(0.001));
                Assert.That(_frame.Spans[2].RightNode.LeftRotation.Value, Is.EqualTo(-0.00360).Within(0.000001));

            });
        }

       // TODO: [TestCaseSource(typeof(DataSplitter), nameof(DataSplitter.SplitData), new object[] { (short)0, "2019.10.01-01_0-23" })]
        [TestCaseSource(typeof(DataSplitter), nameof(DataSplitter.SplitData), new object[] { (short)1, "2019.10.01-01_1-22" })]
       // [TestCaseSource(typeof(DataSplitter), nameof(DataSplitter.SplitData), new object[] { (short)2, "2019.10.01-01_2-24" })]
        public void CalculationsAreCorrectTest_Successful(short spanNumber, Data data)
        {
            if (data.LastOne)
                data.Position = _frame.Spans.First(s => s.Number == spanNumber).Length;
            double calculatedNormalForce = _frame.Results.NormalForce.GetValue(data.Position, spanNumber).Value;
            double calculatedShear = _frame.Results.Shear.GetValue(data.Position, spanNumber).Value;
            double calculatedMoment = _frame.Results.BendingMoment.GetValue(data.Position, spanNumber).Value;
            double calculatedHorizontalDeflection = _frame.Results.NormalDeflection.GetValue(data.Position, spanNumber).Value;
            double calculatedVerticalDeflection = _frame.Results.ShearDeflection.GetValue(data.Position, spanNumber).Value;
            
            Assert.Multiple(() =>
            {
                if (data.HasTwoValues)
                {
                    Assert.That(calculatedNormalForce, Is.GreaterThanOrEqualTo(data.MinMaxFx[0]).Within(0.001)
                        .And.LessThanOrEqualTo(data.MinMaxFx[1]).Within(0.001)
                        .Or.EqualTo((data.MinMaxFx[0] + data.MinMaxFx[1]) / 2).Within(0.01),
                        message: $"Normal force - span {spanNumber} at {data.Position}m."); ;
                    Assert.That(calculatedShear, Is.GreaterThanOrEqualTo(data.MinMaxFz[0]).Within(0.001)
                        .And.LessThanOrEqualTo(data.MinMaxFz[1]).Within(0.001)
                        .Or.EqualTo((data.MinMaxFz[0] + data.MinMaxFz[1]) / 2).Within(0.01),
                        message: $"Shear force - span {spanNumber} at {data.Position}m.");
                    Assert.That(calculatedMoment, Is.GreaterThanOrEqualTo(data.MinMaxMy[0]).Within(0.001)
                        .And.LessThanOrEqualTo(data.MinMaxMy[1]).Within(0.001)
                        .Or.EqualTo((data.MinMaxMy[0] + data.MinMaxMy[1]) / 2).Within(0.01),
                        message: $"Bending moment force - span {spanNumber} at {data.Position}m.");
                    Assert.That(calculatedHorizontalDeflection, Is.GreaterThanOrEqualTo(data.MinMaxUx[0]).Within(0.001)
                        .And.LessThanOrEqualTo(data.MinMaxUx[1]).Within(0.001)
                        .Or.EqualTo((data.MinMaxUx[0] + data.MinMaxUx[1]) / 2).Within(0.01),
                        message: $"Horizontal deflection - span {spanNumber} at {data.Position}m.");
                    Assert.That(calculatedVerticalDeflection, Is.GreaterThanOrEqualTo(data.MinMaxUz[0]).Within(0.001)
                        .And.LessThanOrEqualTo(data.MinMaxUz[1]).Within(0.001)
                        .Or.EqualTo((data.MinMaxUz[0] + data.MinMaxUz[1]) / 2).Within(0.01),
                        message: $"Vertical deflection - span {spanNumber} at {data.Position}m.");
                }
                else
                {
                    Assert.That(calculatedNormalForce, Is.EqualTo(data.Fx).Within(0.001), message: $"Normal force - span {spanNumber} at {data.Position}m.");
                    Assert.That(calculatedShear, Is.EqualTo(data.Fz).Within(0.001), message: $"Shear force - span {spanNumber} at {data.Position}m.");
                    Assert.That(calculatedMoment, Is.EqualTo(data.My).Within(0.001), message: $"Bending moment - span {spanNumber} at {data.Position}m.");
                    Assert.That(calculatedHorizontalDeflection, Is.EqualTo(data.Ux).Within(0.001), message: $"Horizontal deflection - span {spanNumber} at {data.Position}m.");
                    Assert.That(calculatedVerticalDeflection, Is.EqualTo(data.Uz).Within(0.001), message: $"Vertical deflection - span {spanNumber} at {data.Position}m.");
                }
            });
        }
    }
}