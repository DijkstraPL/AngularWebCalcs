using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Beams.Interfaces;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads;
using Build_IT_BeamStatica.Loads.ContinuousLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.17-06")]
    public class BeamWithSpanExtendsLoadsTests
    {
        private IBeam _beam;

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
            
            var startLoad = new LoadData(value: -150, position: 3);
            var endLoad = new LoadData(value: -50, position: 8);
            span1.ContinousLoads.Add(SpanExtendLoad.Create(span1, 10 ));

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(4500).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(0).Within(0.001));
            
            Assert.That(_beam.Spans[0].RightNode.NormalForce.Value, Is.EqualTo(-4500).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.ShearForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.BendingMoment.Value, Is.EqualTo(0).Within(0.001));
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation, Is.Null);
            Assert.That(_beam.Spans[0].RightNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 4500)]
        [TestCase(2, 4500)]
        [TestCase(4, 4500)]
        [TestCase(5, 4500)]
        [TestCase(7, 4500)]
        [TestCase(8, 4500)]
        [TestCase(10, 4500)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(10, 0)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(10, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(10, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(10, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(10, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}

