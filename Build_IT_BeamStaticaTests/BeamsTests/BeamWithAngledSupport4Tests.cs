using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "19.04.07-01")]
    public class BeamWithAngledSupport4Tests
    {
        private Beam _beam;

        [SetUp]
        public void SetUpBeam()
        {
            var material = new MaterialData
            {
                Density = 1,
                YoungModulus = 200,
                ThermalExpansionCoefficient = 0
            };
            var section = new SectionData { Area = 60, MomentOfInteria = 20000 };

            var node1 = new PinNode(angle:90);
            var node2 = new FixedNode();

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

            var shearLoad = ContinuousAngledLoad.Create(
                startPosition: 1, startValue: -20, 
                endPosition: 6, endValue: -100, angle:35);
            span1.ContinousLoads.Add(shearLoad);

            var pointLoad = new ShearLoad(value: 200, position: 9);
            span1.PointLoads.Add(pointLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(102.288).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.NormalForce.Value, Is.EqualTo(69.785).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.ShearForce.Value, Is.EqualTo(45.746).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.BendingMoment.Value, Is.EqualTo(1260.821).Within(0.001));
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0).Within(0.000001));
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection.Value, Is.EqualTo(-867.258480).Within(0.000001));
            Assert.That(_beam.Spans[0].LeftNode.RightRotation.Value, Is.EqualTo(0.111499).Within(0.000001));

            Assert.That(_beam.Spans[0].RightNode.LeftRotation, Is.Null);
            Assert.That(_beam.Spans[0].RightNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 102.288)]
        [TestCase(2, 86.228)]
        [TestCase(5, -17.016)]
        [TestCase(7, -69.785)]
        [TestCase(9, -69.785)]
        [TestCase(10, -69.785)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -22.936)]
        [TestCase(4, -108.128)]
        [TestCase(5, -170.384)]
        [TestCase(7, -245.746)]
        [TestCase(9, -245.746)]
        [TestCase(9.0001, -45.746)]
        [TestCase(10, -45.746)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -10.376)]
        [TestCase(4, -132.703)]
        [TestCase(5, -270.866)]
        [TestCase(7, -723.584)]
        [TestCase(8, -969.330)]
        [TestCase(10, -1260.821)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0.111499)]
        [TestCase(2, 0.111417)]
        [TestCase(5, 0.103635)]
        [TestCase(7, 0.079415)]
        [TestCase(8, 0.058254)]
        [TestCase(10, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -0.164)]
        [TestCase(5, -0.268)]
        [TestCase(7, -0.174)]
        [TestCase(8, -0.116)]
        [TestCase(10, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -867.258)]
        [TestCase(2, -644.281)]
        [TestCase(5, -316.930)]
        [TestCase(7, -130.029)]
        [TestCase(8, -60.683)]
        [TestCase(10, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}

