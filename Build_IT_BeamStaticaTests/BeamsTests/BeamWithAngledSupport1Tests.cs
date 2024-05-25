using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.18-02")]
    public class BeamWithAngledSupport1Tests
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
            var node2 = new PinNode(angle:30);

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
            
            var startLoad = new LoadData(value: -50, position: 0);
            var endLoad = new LoadData(value: -50, position: 10);
            span1.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad, endLoad));

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(-108.231).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(312.539).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-625.391).Within(0.001));
            
            Assert.That(_beam.Spans[0].RightNode.NormalForce.Value, Is.EqualTo(108.231).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.ShearForce.Value, Is.EqualTo(187.461).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(0.011090).Within(0.000001));
            Assert.That(_beam.Spans[0].RightNode.HorizontalDeflection.Value, Is.EqualTo(0.240513).Within(0.000001));
            Assert.That(_beam.Spans[0].RightNode.VerticalDeflection.Value, Is.EqualTo(-0.138860).Within(0.000001));
        }

        [Test()]
        [TestCase(0, -108.231)]
        [TestCase(2, -108.231)]
        [TestCase(5, -108.231)]
        [TestCase(7, -108.231)]
        [TestCase(10, -108.231)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 312.539)]
        [TestCase(2, 212.539)]
        [TestCase(5, 62.539)]
        [TestCase(7, -37.461)]
        [TestCase(10, -187.461)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -625.391)]
        [TestCase(2, -100.312)]
        [TestCase(5, 312.305)]
        [TestCase(7, 337.383)]
        [TestCase(10, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -0.007385)]
        [TestCase(5, -0.002793)]
        [TestCase(7, 0.004492)]
        [TestCase(10, 0.011090)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0.048)]
        [TestCase(5, 0.120)]
        [TestCase(7, 0.168)]
        [TestCase(10, 0.241)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -9.252)]
        [TestCase(5, -27.821)]
        [TestCase(7, -26.212)]
        [TestCase(10, -0.139)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}

