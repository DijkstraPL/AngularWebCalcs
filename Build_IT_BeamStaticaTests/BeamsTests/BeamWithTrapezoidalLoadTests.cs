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
    [TestFixture(Description = "18.12.12-19")]
    public class BeamWithTrapezoidalLoadTests
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
            var node4 = new FixedNode();

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
            node2.ConcentratedForces.Add(new BendingMoment(value: 150));

            var startLoad1 = new LoadData(value: 0, position: 0);
            var endLoad1 = new LoadData(value: -30, position: 2.5);
            var startLoad2 = new LoadData(value: -30, position: 2.5);
            var endLoad2 = new LoadData(value: -30, position: 7.5);
            var startLoad3 = new LoadData(value: -30, position: 7.5);
            var endLoad3 = new LoadData(value: 0, position: 10);
            span3.ContinousLoads.Add( ContinuousShearLoad.Create(startLoad1, endLoad1));
            span3.ContinousLoads.Add( ContinuousShearLoad.Create(startLoad2, endLoad2));
            span3.ContinousLoads.Add( ContinuousShearLoad.Create(startLoad3, endLoad3));

            var pointLoad = new ShearLoad(value: -150, position: 3);
            span1.PointLoads.Add(pointLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(208.866).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-632.293).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].LeftNode.ShearForce.Value, Is.EqualTo(256.780).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].RightNode.ShearForce.Value, Is.EqualTo(109.354).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.BendingMoment.Value, Is.EqualTo(212.170).Within(0.001));
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.007564).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(-48.396).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.007564).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(0.003884).Within(0.000001));
            Assert.That(_beam.Spans[2].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(0.003884).Within(0.000001));

            Assert.That(_beam.Spans[2].RightNode.LeftRotation, Is.Null);
            Assert.That(_beam.Spans[2].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 208.866)]
        [TestCase(1, 208.866)]
        [TestCase(3, 208.866)]
        [TestCase(3.00001, 58.866)]
        [TestCase(6, 58.866)]
        [TestCase(6.00001, -141.134)]
        [TestCase(8, -141.134)]
        [TestCase(10, -141.134)]
        [TestCase(10.00001, 115.646)]
        [TestCase(11, 109.646)]
        [TestCase(12, 91.645)]
        [TestCase(13, 63.146)]
        [TestCase(15, 3.146)]
        [TestCase(19, -103.355)]
        [TestCase(20, -109.354)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -632.293)]
        [TestCase(1, -423.426)]
        [TestCase(3, -5.694)]
        [TestCase(6, 170.905)]
        [TestCase(6.00001, 320.904)]
        [TestCase(8, 38.638)]
        [TestCase(10, -243.629)]
        [TestCase(11, -129.983)]
        [TestCase(12, -28.337)]
        [TestCase(13, 49.559)]
        [TestCase(15, 115.850)]
        [TestCase(19, -104.816)]
        [TestCase(20, -212.170)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, -0.005631)]
        [TestCase(3, -0.010208)]
        [TestCase(6, -0.007564)]
        [TestCase(8, 0.019068)]
        [TestCase(10, 0.003884)]
        [TestCase(11, -0.009917)]
        [TestCase(12, -0.015669)]
        [TestCase(13, -0.014705)]
        [TestCase(15, -0.000971)]
        [TestCase(19, 0.011703)]
        [TestCase(20, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, -3.001)]
        [TestCase(3, -20.324)]
        [TestCase(6, -48.396)]
        [TestCase(8, -29.922)]
        [TestCase(10, 0)]
        [TestCase(11, -3.719)]
        [TestCase(12, -17.141)]
        [TestCase(13, -32.809)]
        [TestCase(15, -50.122)]
        [TestCase(19, -6.515)]
        [TestCase(20, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
