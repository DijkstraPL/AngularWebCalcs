using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "19.04.06-01")]
    public class BeamWithAngledSupport2Tests
    {
        private Beam _beam;

        [SetUp]
        public void SetUpBeam()
        {
            var material = new MaterialData
            {
                Density = 0,
                YoungModulus = 200,
                ThermalExpansionCoefficient = 0
            };
            var section = new SectionData { Area = 60, MomentOfInteria = 20000 };

            var node1 = new FixedNode();
            var node2 = new PinNode(angle:22.02);

            var nodes = new Node[] { node1, node2 };

            var span1 = new Span(
                leftNode: node1,
                length: 8,
                rightNode: node2,
                material: material,
                section: section,
                includeSelfWeight: false
                );

            var spans = new Span[] { span1 };
            
            var pointLoad = new ShearLoad(value: -40, position: 4);
            span1.PointLoads.Add(pointLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(-5.054).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(27.503).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-60.026).Within(0.001));
            
            Assert.That(_beam.Spans[0].RightNode.NormalForce.Value, Is.EqualTo(5.054).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.ShearForce.Value, Is.EqualTo(12.497).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(0.001997).Within(0.000001));
            Assert.That(_beam.Spans[0].RightNode.HorizontalDeflection.Value, Is.EqualTo(0.033694).Within(0.000001));
            Assert.That(_beam.Spans[0].RightNode.VerticalDeflection.Value, Is.EqualTo(-0.013627).Within(0.000001));
        }

        [Test()]
        [TestCase(0, -5.054)]
        [TestCase(2, -5.054)]
        [TestCase(5, -5.054)]
        [TestCase(7, -5.054)]
        [TestCase(8, -5.054)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 27.503)]
        [TestCase(2, 27.503)]
        [TestCase(4, 27.503)]
        [TestCase(4.001, -12.497)]
        [TestCase(5, -12.497)]
        [TestCase(7, -12.497)]
        [TestCase(8, -12.497)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -60.026)]
        [TestCase(2, -5.019)]
        [TestCase(4, 49.987)]
        [TestCase(5, 37.49)]
        [TestCase(7, 12.497)]
        [TestCase(8, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -0.001626)]
        [TestCase(5, 0.000592)]
        [TestCase(7, 0.001841)]
        [TestCase(8, 0.001997)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0.008)]
        [TestCase(5, 0.021)]
        [TestCase(7, 0.029)]
        [TestCase(8, 0.034)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -2.085)]
        [TestCase(5, -4.6)]
        [TestCase(7, -1.959)]
        [TestCase(8, -0.014)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}

