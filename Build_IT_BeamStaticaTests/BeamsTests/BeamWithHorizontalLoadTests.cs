using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.12-11")]
    public class BeamWithHorizontalLoadTests
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
            var node2 = new FreeNode();

            var nodes = new Node[] { node1, node2 };

            var span1 = new Span(
                leftNode: node1,
                length: 10,
                rightNode: node2,
                material: material,
                section: section,
                includeSelfWeight: false
                );

            var spans = new Span[] { span1 };

            var pointLoad1 = new NormalLoad(value: 100, position: 3);
            span1.PointLoads.Add(pointLoad1);
            var pointLoad2 = new ShearLoad(value: -100, position: 5);
            span1.PointLoads.Add(pointLoad2);

            node2.ConcentratedForces.Add(new NormalLoad(value: 150));

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(-250).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(100).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-500).Within(0.001));

            Assert.That(_beam.Spans[0].RightNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[0].RightNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[0].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.013333).Within(0.000001));
            Assert.That(_beam.Spans[0].RightNode.HorizontalDeflection.Value, Is.EqualTo(0.400).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.VerticalDeflection.Value, Is.EqualTo(-111.111).Within(0.001));
        }

        [Test()]
        [TestCase(0, -250)]
        [TestCase(2, -250)]
        [TestCase(3, -250)]
        [TestCase(3.0001, -150)]
        [TestCase(5, -150)]
        [TestCase(7, -150)]
        [TestCase(10, -150)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedNormalForce = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedNormalForce, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 100)]
        [TestCase(2, 100)]
        [TestCase(3, 100)]
        [TestCase(5, 100)]
        [TestCase(5.0001, 0)]
        [TestCase(7, 0)]
        [TestCase(10, 0)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -500)]
        [TestCase(2, -300)]
        [TestCase(3, -200)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(10, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -0.008533)]
        [TestCase(3, -0.0112)]
        [TestCase(4, -0.0128)]
        [TestCase(5, -0.013333)]
        [TestCase(7, -0.013333)]
        [TestCase(10, -0.013333)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0.111)]
        [TestCase(3, 0.167)]
        [TestCase(4, 0.2)]
        [TestCase(5, 0.233)]
        [TestCase(7, 0.3)]
        [TestCase(10, 0.4)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -9.244)]
        [TestCase(3, -19.2)]
        [TestCase(4, -31.289)]
        [TestCase(5, -44.444)]
        [TestCase(7, -71.111)]
        [TestCase(10, -111.111)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
