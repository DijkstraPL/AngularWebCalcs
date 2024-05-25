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
    [TestFixture(Description = "18.12.12-18")]
    public class BeamWithTelescopeSupportTests
    {
        private Beam _beam;

        [SetUp]
        public void SetUpBeam()
        {
            var material1 = new MaterialData
            {
                Density = 2400,
                YoungModulus = 30,
                ThermalExpansionCoefficient = 0.000010
            };
            var material2 = new MaterialData
            {
                Density = 2400,
                YoungModulus = 33,
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

            var node1 = new TelescopeNode();
            var node2 = new SupportedNode();
            var node3 = new SupportedNode();

            var nodes = new Node[] { node1, node2, node3 };

            var span1 = new Span(
                leftNode: node1,
                length: 10,
                rightNode: node2,
                material: material1,
                section: section,
                includeSelfWeight: false
                );

            var span2 = new Span(
                leftNode: node2,
                length: 10,
                rightNode: node3,
                material: material2,
                section: section,
                includeSelfWeight: false
                );

            var spans = new Span[] { span1, span2 };

            var startLoad1 = new LoadData(value: -20, position: 0);
            var endLoad1 = new LoadData(value: -20, position: 10);
            span1.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad1, endLoad1));

            var startLoad2 = new LoadData(value: -20, position: 0);
            var endLoad2 = new LoadData(value: -20, position: 10);
            span2.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad2, endLoad2));

            var pointLoad = new ShearLoad(value: -100, position: 5);
            span1.PointLoads.Add(pointLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(642.442).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.ShearForce.Value, Is.EqualTo(485.756).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].RightNode.ShearForce.Value, Is.EqualTo(14.244).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection.Value, Is.EqualTo(-231.525).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(0.019638).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(0.019638).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(-0.005779).Within(0.000001));
            Assert.That(_beam.Spans[1].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -40)]
        [TestCase(5, -100)]
        [TestCase(5.00001, -200)]
        [TestCase(10, -300)]
        [TestCase(10.0001, 185.754)]
        [TestCase(12, 145.756)]
        [TestCase(15, 85.756)]
        [TestCase(20, -14.244)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 642.442)]
        [TestCase(2, 602.442)]
        [TestCase(5, 392.442)]
        [TestCase(10, -857.558)]
        [TestCase(12, -526.047)]
        [TestCase(15, -178.779)]
        [TestCase(20, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0.013421)]
        [TestCase(5, 0.029819)]
        [TestCase(10, 0.019638)]
        [TestCase(12, 0.006351)]
        [TestCase(15, -0.003465)]
        [TestCase(20, -0.005779)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -231.525)]
        [TestCase(2, -217.961)]
        [TestCase(5, -151.421)]
        [TestCase(10, 0)]
        [TestCase(12, 24.917)]
        [TestCase(15, 26.721)]
        [TestCase(20, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
