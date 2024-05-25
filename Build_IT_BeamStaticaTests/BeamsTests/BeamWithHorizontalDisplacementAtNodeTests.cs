using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.17-02")]
    public class BeamWithHorizontalDisplacementAtNodeTests
    {
        private Beam _beam;

        [SetUp]
        public void SetUpBeam()
        {
            var material = new MaterialData
            {
                Density = 2400,
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
            var node2 = new SupportedNode();
            var node3 = new FixedNode();

            var nodes = new Node[] { node1, node2, node3 };

            var span1 = new Span(
                leftNode: node1,
                length: 10,
                rightNode: node2,
                material: material,
                section: section,
                includeSelfWeight: false
                );

            var span2 = new Span(
                leftNode: node2,
                length: 10,
                rightNode: node3,
                material: material,
                section: section,
                includeSelfWeight: false
                );
            
            var spans = new Span[] { span1, span2 };

            node2.ConcentratedForces.Add(new HorizontalDisplacement(value: 10));
            
            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(-4500).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(0).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.NormalForce.Value, Is.EqualTo(9000).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.ShearForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].RightNode.NormalForce.Value, Is.EqualTo(-4500).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.ShearForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.BendingMoment.Value, Is.EqualTo(0).Within(0.001));
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(0).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(0).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation, Is.Null);
            Assert.That(_beam.Spans[1].RightNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, -4500)]
        [TestCase(2, -4500)]
        [TestCase(5, -4500)]
        [TestCase(7, -4500)]
        [TestCase(8, -4500)]
        [TestCase(10, -4500)]
        [TestCase(10.0001, 4500)]
        [TestCase(12, 4500)]
        [TestCase(16, 4500)]
        [TestCase(20, 4500)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedNormalForce = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedNormalForce, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(10, 0)]
        [TestCase(12, 0)]
        [TestCase(16, 0)]
        [TestCase(20, 0)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(10, 0)]
        [TestCase(12, 0)]
        [TestCase(16, 0)]
        [TestCase(20, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(10, 0)]
        [TestCase(12, 0)]
        [TestCase(15, 0)]
        [TestCase(20, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 2)]
        [TestCase(5, 5)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(10, 10)]
        [TestCase(12, 8)]
        [TestCase(15, 5)]
        [TestCase(20, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(10, 0)]
        [TestCase(12, 0)]
        [TestCase(15, 0)]
        [TestCase(20, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
