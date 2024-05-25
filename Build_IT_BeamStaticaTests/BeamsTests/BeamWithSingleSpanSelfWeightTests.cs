using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.25-01")]
    public class BeamWithSingleSpanSelfWeightTests
    {
        private Beam _beam;

        [SetUp]
        public void SetUpBeam()
        {
            var material = new MaterialData
            {
                Density = 2500,
                YoungModulus = 30,
                ThermalExpansionCoefficient = 0.000010
            };
            var section = new CustomSectionData(
                new List<Point>
                {
                    new Point(0,0),
                    new Point(300,0),
                    new Point(300,500),
                    new Point(0,500),
                });

            var node1 = new FixedNode();
            var node2 = new FixedNode();

            var nodes = new Node[] { node1, node2 };

            var span1 = new Span(
                leftNode: node1,
                length: 10,
                rightNode: node2,
                material: material,
                section: section,
                includeSelfWeight: true
                );

            var spans = new Span[] { span1 };

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(18.398).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-30.663).Within(0.001));

            Assert.That(_beam.Spans[0].RightNode.ShearForce.Value, Is.EqualTo(18.398).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.BendingMoment.Value, Is.EqualTo(30.663).Within(0.001));
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation, Is.Null);
            Assert.That(_beam.Spans[0].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 18.398)]
        [TestCase(3, 7.359)]
        [TestCase(5, 0)]
        [TestCase(7, -7.359)]
        [TestCase(10, -18.398)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -30.663)]
        [TestCase(3, 7.972)]
        [TestCase(5, 15.331)]
        [TestCase(7, 7.972)]
        [TestCase(10, -30.663)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, -0.000235)]
        [TestCase(3, -0.000275)]
        [TestCase(5, 0)]
        [TestCase(7, 0.000275)]
        [TestCase(10, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, -0.132)]
        [TestCase(3, -0.721)]
        [TestCase(5, -1.022)]
        [TestCase(7, -0.721)]
        [TestCase(10, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
