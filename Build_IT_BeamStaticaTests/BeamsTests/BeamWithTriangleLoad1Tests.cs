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
    [TestFixture(Description = "18.12.12-20")]
    public class BeamWithTriangleLoad1Tests
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

            var startLoad = new LoadData(value: -30, position: 0);
            var endLoad = new LoadData(value: 0, position: 6);
            span1.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad, endLoad));

            var pointLoad = new ShearLoad(value: -150, position: 5);
            span3.PointLoads.Add(pointLoad);

            _beam = new Beam(spans, nodes , includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(155.952).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-352.225).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].LeftNode.ShearForce.Value, Is.EqualTo(231.318).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].RightNode.ShearForce.Value, Is.EqualTo(52.730).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.001240).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(-21.565).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.001240).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(-0.014456).Within(0.000001));
            Assert.That(_beam.Spans[2].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(-0.014456).Within(0.000001));

            Assert.That(_beam.Spans[2].RightNode.LeftRotation.Value, Is.EqualTo(0.041950).Within(0.000001));
            Assert.That(_beam.Spans[2].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 155.952)]
        [TestCase(1, 128.452)]
        [TestCase(3, 88.452)]
        [TestCase(6, 65.952)]
        [TestCase(6.00001, -134.048)]
        [TestCase(8, -134.048)]
        [TestCase(10, -134.048)]
        [TestCase(10.00001, 97.270)]
        [TestCase(15, 97.270)]
        [TestCase(15.00001, -52.730)]
        [TestCase(20, -52.730)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -352.225)]
        [TestCase(1, -210.439)]
        [TestCase(3, 3.132)]
        [TestCase(6, 223.489)]
        [TestCase(8, -44.607)]
        [TestCase(10, -312.702)]
        [TestCase(10.00001, -222.702)]
        [TestCase(15, 263.649)]
        [TestCase(20, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, -0.002976)]
        [TestCase(2, -0.004578)]
        [TestCase(3, -0.005045)]
        [TestCase(5, -0.003269)]
        [TestCase(6, -0.001240)]
        [TestCase(8, 0.012011)]
        [TestCase(10, -0.014456)]
        [TestCase(15, -0.006874)]
        [TestCase(20, 0.041950)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, -1.614)]
        [TestCase(2, -5.495)]
        [TestCase(3, -10.393)]
        [TestCase(5, -19.252)]
        [TestCase(6, -21.565)]
        [TestCase(8, -4.174)]
        [TestCase(10, 0)]
        [TestCase(15, -128.379)]
        [TestCase(20, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
