using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.13-02")]
    public class BeamWithTemperatureLoad2Tests
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

            var node1 = new FixedNode();
            var node2 = new FreeNode();
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
                length: 5,
                rightNode: node3,
                material: material2,
                section: section,
                includeSelfWeight: false
                );

            var spans = new Span[] { span1, span2 };

            var upDownTemperatureLoad = UpDownTemperatureDifferenceLoad.Create(
                span1, upperTemperature: 20, lowerTemperature: 0);
            span1.ContinousLoads.Add(upDownTemperatureLoad);

            var alongTemperatureLoad = AlongTemperatureDifferenceLoad.Create(
                span1, temperatureDifference: 10);
            span1.ContinousLoads.Add(alongTemperatureLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(309.375).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(-3.345).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(50.169).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].RightNode.NormalForce.Value, Is.EqualTo(-309.375).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.ShearForce.Value, Is.EqualTo(3.345).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.000432).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0.313).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(0.811).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.000432).Within(0.000001));
            
            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(-0.000027).Within(0.000001));
            Assert.That(_beam.Spans[1].RightNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 309.375)]
        [TestCase(2, 309.375)]
        [TestCase(5, 309.375)]
        [TestCase(10, 309.375)]
        [TestCase(12, 309.375)]
        [TestCase(15, 309.375)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -3.345)]
        [TestCase(2, -3.345)]
        [TestCase(5, -3.345)]
        [TestCase(10, -3.345)]
        [TestCase(12, -3.345)]
        [TestCase(15, -3.345)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 50.169)]
        [TestCase(2, 43.480)]
        [TestCase(5, 33.445)]
        [TestCase(10, 16.722)]
        [TestCase(12, 10.034)]
        [TestCase(15, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, 0.000117)]
        [TestCase(2, 0.000199)]
        [TestCase(3, 0.000245)]
        [TestCase(4, 0.000255)]
        [TestCase(5, 0.000230)]
        [TestCase(6, 0.000169)]
        [TestCase(7, 0.000072)]
        [TestCase(8, -0.000061)]
        [TestCase(9, -0.000229)]
        [TestCase(10, -0.000432)]
        [TestCase(12, -0.000173)]
        [TestCase(15, -0.000027)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0.063)]
        [TestCase(5, 0.156)]
        [TestCase(10, 0.313)]
        [TestCase(12, 0.188)]
        [TestCase(15, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, 0.062)]
        [TestCase(2, 0.223)]
        [TestCase(3, 0.448)]
        [TestCase(4, 0.701)]
        [TestCase(5, 0.946)]
        [TestCase(6, 1.148)]
        [TestCase(7, 1.271)]
        [TestCase(8, 1.28)]
        [TestCase(9, 1.138)]
        [TestCase(10, 0.811)]
        [TestCase(12, 0.227)]
        [TestCase(15, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
