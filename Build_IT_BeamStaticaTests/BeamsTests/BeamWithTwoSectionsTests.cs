using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.12-22")]
   public  class BeamWithTwoSectionsTests
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
            var section1 = new CustomSectionData(
                new List<Point>
                {
                    new Point(0,0),
                    new Point(300,0),
                    new Point(300,500),
                    new Point(0,500),
                });
            var section2 = new CustomSectionData(
                new List<Point>
                {
                    new Point(0,0),
                    new Point(300,0),
                    new Point(300,700),
                    new Point(0,700),
                });

            var node1 = new FixedNode();
            var node2 = new FreeNode();
            var node3 = new FixedNode();

            var nodes = new Node[] { node1, node2, node3 };

            var span1 = new Span(
                leftNode: node1,
                length: 10,
                rightNode: node2,
                material: material,
                section: section1,
                includeSelfWeight: false
                );

            var span2 = new Span(
                leftNode: node2,
                length: 10,
                rightNode: node3,
                material: material,
                section: section2,
                includeSelfWeight: false
                );
            
            var spans = new Span[] { span1, span2 };

            var pointLoad = new ShearLoad(value: -100, position: 5);
            span1.PointLoads.Add(pointLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(80.290).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-257.196).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].RightNode.ShearForce.Value, Is.EqualTo(19.710).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.BendingMoment.Value, Is.EqualTo(151.387).Within(0.001));
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(0.002054).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(-16.655).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(0.002054).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation, Is.Null);
            Assert.That(_beam.Spans[1].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 80.290)]
        [TestCase(5, 80.290)]
        [TestCase(5.01, -19.710)]
        [TestCase(10, -19.710)]
        [TestCase(10.01, -19.710)]
        [TestCase(15, -19.710)]
        [TestCase(20, -19.710)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -257.196)]
        [TestCase(3, -16.324)]
        [TestCase(5, 144.256)]
        [TestCase(10, 45.708)]
        [TestCase(15, -52.839)]
        [TestCase(20, -151.387)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(3, -0.004376)]
        [TestCase(5, -0.003012)]
        [TestCase(9, 0.001461)]
        [TestCase(10, 0.002054)]
        [TestCase(11, 0.002193)]
        [TestCase(17, 0.001421)]
        [TestCase(20, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -4.345)]
        [TestCase(5, -16.450)]
        [TestCase(9, -18.430)]
        [TestCase(10, -16.655)]
        [TestCase(11, -14.525)]
        [TestCase(13, -10.038)]
        [TestCase(15, -5.760)]
        [TestCase(20, 0)]
        public void DeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
