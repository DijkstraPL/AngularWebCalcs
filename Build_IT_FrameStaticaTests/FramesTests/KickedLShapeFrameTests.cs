using Build_IT_Data.Materials;
using Build_IT_Data.Sections;
using Build_IT_FrameStatica.Frames;
using Build_IT_FrameStatica.Loads.ContinuousLoads;
using Build_IT_FrameStatica.Nodes;
using Build_IT_FrameStatica.Spans;
using Build_IT_Data.Geometry;
using NUnit.Framework;
using System.Linq;

namespace Build_IT_FrameStaticaTests.FramesTests
{
    [TestFixture]
    [Property("Name", "2019.09.30-03")]
    public class KickedLShapeFrameTests
    {
        private Frame _frame;
               
        [SetUp]
        public void SetUpFrame()
        {
            var material = new Material(youngModulus: 210, density: 0, thermalExpansionCoefficient: 0);
            var section = new SectionProperties(area: 1500, momentOfInteria: 312500);

            var node1 = new FixedNode(new Point(0, 0));
            var node2 = new FreeNode(new Point(6, 4.5));
            var node3 = new FixedNode(new Point(12, 4.5));

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

            span2.ContinousLoads.Add(ContinuousShearLoad.Create(
                    startPosition: 0, startValue: -3, endPosition: 6, endValue: -3));

            _frame = new Frame(spans, nodes);

            _frame.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_frame.Spans[0].LeftNode.HorizontalForce.Value, Is.EqualTo(11.045).Within(0.001));
                Assert.That(_frame.Spans[0].LeftNode.VerticalForce.Value, Is.EqualTo(7.503).Within(0.001));
                Assert.That(_frame.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(1.382).Within(0.001));

                Assert.That(_frame.Spans[0].RightNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[0].RightNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].LeftNode.HorizontalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.VerticalForce, Is.Null);
                Assert.That(_frame.Spans[1].LeftNode.BendingMoment, Is.Null);

                Assert.That(_frame.Spans[1].RightNode.HorizontalForce.Value, Is.EqualTo(-11.045).Within(0.001));
                Assert.That(_frame.Spans[1].RightNode.VerticalForce.Value, Is.EqualTo(10.497).Within(0.001));
                Assert.That(_frame.Spans[1].RightNode.BendingMoment.Value, Is.EqualTo(12.290).Within(0.001));
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

                Assert.That(_frame.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.00001099).Within(0.0000001));
                Assert.That(_frame.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0.002104).Within(0.000001));
                Assert.That(_frame.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(-0.008098).Within(0.000001));
                Assert.That(_frame.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.00001099).Within(0.0000001));

                Assert.That(_frame.Spans[1].RightNode.LeftRotation, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.HorizontalDeflection, Is.Null);
                Assert.That(_frame.Spans[1].RightNode.VerticalDeflection, Is.Null);
            });
        }

        [TestCaseSource(typeof(DataSplitter), nameof(DataSplitter.SplitData), new object[] { (short)0, "2019.09.30-03_0-4" })]
        [TestCaseSource(typeof(DataSplitter), nameof(DataSplitter.SplitData), new object[] { (short)1, "2019.09.30-03_1-5" })]
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

        [Test()]
        [TestCase(0, 0, 0)]
        [TestCase(2, 0, 0.000002)]
        [TestCase(4, 0, 0.000001)]
        [TestCase(6, 0, -0.000005)]
        [TestCase(0, 1, -0.000011)]
        [TestCase(2, 1, -0.000004)]
        [TestCase(4, 1, 0.000012)]
        [TestCase(6, 1, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, short spanIndex, double result)
        {
            double rotation = _frame.Results.Rotation.GetValue(position, spanIndex).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"Span {spanIndex} at {position}m.");
        }
    }
}