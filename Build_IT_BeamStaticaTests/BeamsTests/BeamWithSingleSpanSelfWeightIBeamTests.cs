using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.25-02")]
    public class BeamWithSingleSpanSelfWeightIBeamTests
    {
        private Beam _beam;

        [SetUp]
        public void SetUpBeam()
        {
            var material = new MaterialData
            {
                Density = 7850,
                YoungModulus = 210,
                ThermalExpansionCoefficient = 0.000012
            };
            var section = new CustomSectionData(
                GetPoints(width: 91, height: 180, flangeWidth: 8, webWidth: 5.3, radius: 9));

            var node1 = new FixedNode();
            var node2 = new FixedNode();

            var nodes = new Node[] { node1, node2 };

            var span1 = new Span(
                leftNode: node1,
                length: 10,
                rightNode: node2,
                material: material,
                section: section,
                includeSelfWeight: true
                );

            var spans = new Span[] { span1 };

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(0.923).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-1.539).Within(0.001));

            Assert.That(_beam.Spans[0].RightNode.ShearForce.Value, Is.EqualTo(0.923).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.BendingMoment.Value, Is.EqualTo(1.539).Within(0.001));
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation, Is.Null);
            Assert.That(_beam.Spans[0].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 0.923)]
        [TestCase(3, 0.369)]
        [TestCase(5, 0)]
        [TestCase(7, -0.369)]
        [TestCase(10, -0.923)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -1.539)]
        [TestCase(3, 0.4)]
        [TestCase(5, 0.769)]
        [TestCase(7, 0.4)]
        [TestCase(10, -1.539)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, -0.0004)]
        [TestCase(3, -0.000467)]
        [TestCase(5, 0)]
        [TestCase(7, 0.000467)]
        [TestCase(10, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1, -0.225)]
        [TestCase(3, -1.225)]
        [TestCase(5, -1.736)]
        [TestCase(7, -1.225)]
        [TestCase(10, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        private List<Point> GetPoints(double width, double height, double webWidth, double flangeWidth, double radius)
        {
            var points = new List<Point>();
            points.Add(new Point(0, 0));
            points.Add(new Point(width, 0));
            points.Add(new Point(width, flangeWidth));
            for (int i = 0; i < 7; i++)
            {
                points.Add(new Point(
                    width / 2 + webWidth / 2 + radius - radius * Math.Sin(Math.PI * 15 * i / 180),
                    flangeWidth + radius - radius * Math.Cos(Math.PI * 15 * i / 180)
                    ));
            }
            for (int i = 0; i < 7; i++)
            {
                points.Add(new Point(
                    width / 2 + webWidth / 2 + radius - radius * Math.Cos(Math.PI * 15 * i / 180),
                    height - flangeWidth - radius + radius * Math.Sin(Math.PI * 15 * i / 180)
                    ));
            }

            points.Add(new Point(width, height - flangeWidth));
            points.Add(new Point(width, height));
            points.Add(new Point(0, height));
            points.Add(new Point(0, height - flangeWidth));

            for (int i = 0; i < 7; i++)
            {
                points.Add(new Point(
                    width / 2 - webWidth / 2 - radius + radius * Math.Sin(Math.PI / 180 * 15 * i),
                    height - flangeWidth - radius + radius * Math.Cos(Math.PI / 180 * 15 * i)
                    ));
            }
            for (int i = 0; i < 7; i++)
            {
                points.Add(new Point(
                    width / 2 - webWidth / 2 - radius + radius * Math.Cos(Math.PI / 180 * 15 * i),
                    flangeWidth + radius - radius * Math.Sin(Math.PI / 180 * 15 * i)
                    ));
            }

            points.Add(new Point(0, flangeWidth));

            return points;
        }
    }
}
