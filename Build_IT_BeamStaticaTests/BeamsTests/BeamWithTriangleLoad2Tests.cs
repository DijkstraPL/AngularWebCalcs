using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.12-21")]
    public class BeamWithTriangleLoad2Tests
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
                    new Point(200,0),
                    new Point(200,300),
                    new Point(0,300),
                });

            var node1 = new FixedNode();
            var node2 = new FreeNode();
            var node3 = new SupportedNode();
            var node4 = new SupportedNode();

            var nodes = new Node[] { node1, node2, node3, node4 };

            var span1 = new Span(
                leftNode: node1,
                length: 6,
                rightNode: node2,
                material: material,
                section: section1,
                includeSelfWeight: false
                );

            var span2 = new Span(
                leftNode: node2,
                length: 4,
                rightNode: node3,
                material: material,
                section: section2,
                includeSelfWeight: false
                );

            var span3 = new Span(
                leftNode: node3,
                length: 10,
                rightNode: node4,
                material: material,
                section: section2,
                includeSelfWeight: false
                );

            var spans = new Span[] { span1, span2, span3 };
            
            node2.ConcentratedForces.Add(new ShearLoad(value: -200));
            node3.ConcentratedForces.Add(new BendingMoment(value: 90));

            var startLoad = new LoadData(value: 0, position: 0);
            var endLoad = new LoadData(value: -30, position: 6);
            span1.ContinousLoads.Add( ContinuousShearLoad.Create(startLoad, endLoad));

            var pointLoad = new ShearLoad(value: -150, position: 5);
            span3.PointLoads.Add(pointLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(143.997).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-423.671).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].LeftNode.ShearForce.Value, Is.EqualTo(244.374).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].RightNode.ShearForce.Value, Is.EqualTo(51.630).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.002348).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(-29.506).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.002348).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(-0.011740).Within(0.000001));
            Assert.That(_beam.Spans[2].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(-0.011740).Within(0.000001));

            Assert.That(_beam.Spans[2].RightNode.LeftRotation.Value, Is.EqualTo(0.040592).Within(0.000001));
            Assert.That(_beam.Spans[2].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 143.997)]
        [TestCase(1, 141.497)]
        [TestCase(3, 121.497)]
        [TestCase(6, 53.997)]
        [TestCase(6.00001, -146.003)]
        [TestCase(8, -146.003)]
        [TestCase(10, -146.003)]
        [TestCase(10.00001, 98.370)]
        [TestCase(15, 98.370)]
        [TestCase(15.00001, -51.630)]
        [TestCase(20, -51.630)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -423.671)]
        [TestCase(1, -280.507)]
        [TestCase(3, -14.181)]
        [TestCase(6, 260.309)]
        [TestCase(8, -31.698)]
        [TestCase(10, -323.705)]
        [TestCase(10.00001, -233.704)]
        [TestCase(15, 258.148)]
        [TestCase(20, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, -0.003753)]
        [TestCase(2, -0.006002)]
        [TestCase(3, -0.006826)]
        [TestCase(5, -0.004785)]
        [TestCase(6, -0.002348)]
        [TestCase(8, 0.014587)]
        [TestCase(10, -0.011740)]
        [TestCase(15, -0.007213)]
        [TestCase(20, 0.040592)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, -2.004)]
        [TestCase(2, -7.005)]
        [TestCase(3, -13.532)]
        [TestCase(5, -25.879)]
        [TestCase(6, -29.506)]
        [TestCase(8, -10.057)]
        [TestCase(10, 0)]
        [TestCase(15, -123.285)]
        [TestCase(20, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
