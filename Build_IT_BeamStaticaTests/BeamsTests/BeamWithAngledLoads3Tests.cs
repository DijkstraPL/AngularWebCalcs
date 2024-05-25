using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.17-01")]
    public class BeamWithAngledLoads3Tests
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
            var node2 = new Hinge();
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

            node2.ConcentratedForces.Add(new AngledLoad(value: -100, angle: 30));
            
            var pointLoad1 = new AngledLoad(value: -200, position: 5, angle: -45);
            span1.PointLoads.Add(pointLoad1);
            
            var pointLoad2 = new AngledLoad(value: -200, position: 5, angle: -60);
            span2.PointLoads.Add(pointLoad2);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(-124.367).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(178.251).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-1075.399).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].RightNode.NormalForce.Value, Is.EqualTo(-140.259).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.ShearForce.Value, Is.EqualTo(149.773).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.BendingMoment.Value, Is.EqualTo(997.734).Within(0.001));
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.0384984).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0.119).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(-288.083).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(0.039879 ).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation, Is.Null);
            Assert.That(_beam.Spans[1].RightNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, -124.367)]
        [TestCase(2, -124.367)]
        [TestCase(5, -124.367)]
        [TestCase(5.0001, 17.054)]
        [TestCase(7, 17.054)]
        [TestCase(10, 17.054)]
        [TestCase(10.0001, -32.946)]
        [TestCase(12, -32.946)]
        [TestCase(15, -32.946)]
        [TestCase(15.0001, 140.259)]
        [TestCase(20, 140.259)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 178.251)]
        [TestCase(2, 178.251)]
        [TestCase(5, 178.251)]
        [TestCase(5.0001, 36.829)]
        [TestCase(7, 36.829)]
        [TestCase(10, 36.829)]
        [TestCase(10.0001, -49.773)]
        [TestCase(12, -49.773)]
        [TestCase(15, -49.773)]
        [TestCase(15.0001, -149.773)]
        [TestCase(20, -149.773)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -1075.399)]
        [TestCase(2, -718.898)]
        [TestCase(5, -184.146)]
        [TestCase(7, -110.488)]
        [TestCase(10, 0)]
        [TestCase(12, -99.547)]
        [TestCase(15, -248.867)]
        [TestCase(20, -997.734)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -0.019139)]
        [TestCase(5, -0.033588)]
        [TestCase(7, -0.0367306)]
        [TestCase(9.9999, -0.038498)]
        [TestCase(10, 0.039879)]
        [TestCase(10.0001, 0.039879)]
        [TestCase(12, 0.038817)]
        [TestCase(15, 0.033243)]
        [TestCase(20, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0.055)]
        [TestCase(5, 0.138)]
        [TestCase(7, 0.131)]
        [TestCase(10, 0.119)]
        [TestCase(12, 0.134)]
        [TestCase(15, 0.156)]
        [TestCase(17, 0.094)]
        [TestCase(20, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -20.407)]
        [TestCase(5, -103.775)]
        [TestCase(7, -174.356)]
        [TestCase(10, -288.083)]
        [TestCase(12, -209.033)]
        [TestCase(15, -99.748)]
        [TestCase(17, -40.702)]
        [TestCase(20, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}

