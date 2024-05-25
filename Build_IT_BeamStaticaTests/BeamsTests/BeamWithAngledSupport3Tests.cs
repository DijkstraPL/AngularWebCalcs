using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "19.04.06-03")]
    public class BeamWithAngledSupport3Tests
    {
        private Beam _beam;

        [SetUp]
        public void SetUpBeam()
        {
            var material1 = new MaterialData
            {
                YoungModulus = 400,
                Density = 0,
                ThermalExpansionCoefficient = 0
            };
            var material2 = new MaterialData
            {
                YoungModulus = 200,
                Density = 0,
                ThermalExpansionCoefficient = 0
            };
            var section = new SectionData { Area = 60, MomentOfInteria = 20000 };

            var node1 = new FixedNode();
            var node2 = new PinNode();
            var node3 = new PinNode(angle:22.02);

            var nodes = new Node[] { node1, node2, node3 };

            var span1 = new Span(
                leftNode: node1,
                length: 8,
                rightNode: node2,
                material: material1,
                section: section,
                includeSelfWeight: false
                );
            var span2 = new Span(
                leftNode: node2,
                length: 8,
                rightNode: node3,
                material: material2,
                section: section,
                includeSelfWeight: false
                );

            var spans = new Span[] { span1, span2 };

            var shearLoad = ContinuousShearLoad.Create(0, -6, 8, -6);
            span1.ContinousLoads.Add(shearLoad);

            var pointLoad = new ShearLoad(value: -40, position: 4);
            span2.PointLoads.Add(pointLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(-5.440).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(20.176).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-21.803).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.ShearForce.Value, Is.EqualTo(54.373).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].RightNode.NormalForce.Value, Is.EqualTo(5.440).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.ShearForce.Value, Is.EqualTo(13.451).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[1].LeftNode.LeftRotation.Value, Is.EqualTo(-0.000510).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0.018133).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection, Is.Null);

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(0.002251).Within(0.000001));
            Assert.That(_beam.Spans[1].RightNode.HorizontalDeflection.Value, Is.EqualTo(0.054399).Within(0.000001));
            Assert.That(_beam.Spans[1].RightNode.VerticalDeflection.Value, Is.EqualTo(-0.022001).Within(0.000001));
        }

        [Test()]
        [TestCase(0, -5.440)]
        [TestCase(2, -5.440)]
        [TestCase(5, -5.440)]
        [TestCase(7, -5.440)]
        [TestCase(8, -5.440)]
        [TestCase(8.0001, -5.440)]
        [TestCase(10, -5.440)]
        [TestCase(12, -5.440)]
        [TestCase(15, -5.440)]
        [TestCase(16, -5.440)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 20.176)]
        [TestCase(2, 8.176)]
        [TestCase(4, -3.824)]
        [TestCase(5, -9.824)]
        [TestCase(7, -21.824)]
        [TestCase(8, -27.824)]
        [TestCase(8.0001, 26.549)]
        [TestCase(10, 26.549)]
        [TestCase(12, 26.549)]
        [TestCase(12.0001, -13.451)]
        [TestCase(15, -13.451)]
        [TestCase(16, -13.451)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -21.803)]
        [TestCase(2, 6.549)]
        [TestCase(4, 10.902)]
        [TestCase(5, 4.078)]
        [TestCase(7, -27.57)]
        [TestCase(8, -52.394)]
        [TestCase(10, 0.705)]
        [TestCase(12, 53.803)]
        [TestCase(15, 13.45)]
        [TestCase(16, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -0.000141)]
        [TestCase(5, 0.000227)]
        [TestCase(7, -0.000016)]
        [TestCase(8, -0.000510)]
        [TestCase(10, -0.001802)]
        [TestCase(12, -0.000439)]
        [TestCase(15, 0.002083)]
        [TestCase(16, 0.002251)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0.004)]
        [TestCase(5,0.011)]
        [TestCase(7, 0.0159)]
        [TestCase(8, 0.018)]
        [TestCase(10, 0.027)]
        [TestCase(12, 0.036)]
        [TestCase(15, 0.050)]
        [TestCase(16, 0.054)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -0.259)]
        [TestCase(5, -0.106)]
        [TestCase(7, 0.237)]
        [TestCase(8, 0)]
        [TestCase(10, -2.754)]
        [TestCase(12, -5.438)]
        [TestCase(15, -2.217)]
        [TestCase(16, -0.022)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}

