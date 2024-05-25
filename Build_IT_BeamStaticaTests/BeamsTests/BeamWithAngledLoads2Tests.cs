using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture(Description = "18.12.14-02")]
    public class BeamWithAngledLoads2Tests
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

            var node1 = new SupportedNode();
            var node2 = new PinNode();
            var node3 = new FixedNode();

            var nodes = new Node[] { node1, node2, node3 };

            var span1 = new Span(
                leftNode: node1,
                length: 10,
                rightNode: node2,
                material: material,
                section: section,
                includeSelfWeight: false
                );

            var span2 = new Span(
                leftNode: node2,
                length: 10,
                rightNode: node3,
                material: material,
                section: section,
                includeSelfWeight: false
                );


            var spans = new Span[] { span1, span2 };

            node2.ConcentratedForces.Add(new AngledLoad(value: -100, angle: 30));
            
            var pointLoad1 = new AngledLoad(value: -200, position: 5, angle: -45);
            span1.PointLoads.Add(pointLoad1);
            
            var pointLoad2 = new AngledLoad(value: -200, position: 5, angle: -60);
            span2.PointLoads.Add(pointLoad2);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(-124.367).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(50.201).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.ShearForce.Value, Is.EqualTo(239.837).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].RightNode.NormalForce.Value, Is.EqualTo(-140.259).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.ShearForce.Value, Is.EqualTo(37.986).Within(0.001));
            Assert.That(_beam.Spans[1].RightNode.BendingMoment.Value, Is.EqualTo(84.953).Within(0.001));
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation.Value, Is.EqualTo(-0.005782).Within(0.000001));

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(0.002136).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0.119).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(0.002136).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation, Is.Null);
            Assert.That(_beam.Spans[1].RightNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, -124.367)]
        [TestCase(2, -124.367)]
        [TestCase(5, -124.367)]
        [TestCase(5.0001, 17.054)]
        [TestCase(7, 17.054)]
        [TestCase(10, 17.054)]
        [TestCase(10.0001, -32.946)]
        [TestCase(12, -32.946)]
        [TestCase(15, -32.946)]
        [TestCase(15.0001, 140.259)]
        [TestCase(20, 140.259)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 50.201)]
        [TestCase(2, 50.201)]
        [TestCase(5, 50.201)]
        [TestCase(5.0001, -91.22)]
        [TestCase(7, -91.22)]
        [TestCase(10, -91.22)]
        [TestCase(10.0001, 62.014)]
        [TestCase(12, 62.014)]
        [TestCase(15, 62.014)]
        [TestCase(15.0001, -37.986)]
        [TestCase(20, -37.986)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 100.402)]
        [TestCase(5, 251.006)]
        [TestCase(7, 68.566)]
        [TestCase(10, -205.094)]
        [TestCase(12, -81.066)]
        [TestCase(15, 104.976)]
        [TestCase(20, -84.953)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -0.005782)]
        [TestCase(2, -0.004711)]
        [TestCase(5, 0.000912)]
        [TestCase(7, 0.004320)]
        [TestCase(10, 0.002136)]
        [TestCase(12, -0.000917)]
        [TestCase(15, -0.000534)]
        [TestCase(20, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0.055)]
        [TestCase(5, 0.138)]
        [TestCase(7, 0.131)]
        [TestCase(10, 0.119)]
        [TestCase(12, 0.134)]
        [TestCase(15, 0.156)]
        [TestCase(17, 0.094)]
        [TestCase(20, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -10.850)]
        [TestCase(5, -17.754)]
        [TestCase(7, -11.874)]
        [TestCase(10, 0)]
        [TestCase(12, 0.778)]
        [TestCase(15, -2.886)]
        [TestCase(17, -2.254)]
        [TestCase(20, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        public void CheckMaxNormalForce_Successful()
        {
            var result = _beam.Results.NormalForce.GetMaxValue();

            Assert.That(result.Value, Is.EqualTo(140.259).Within(0.001), message: $"At {result.Position}m.");
            Assert.That(result.Position, Is.EqualTo(15.01).Within(0.001), message: $"At {result.Position}m.");
        }

        [Test()]
        public void CheckMinNormalForce_Successful()
        {
            var result = _beam.Results.NormalForce.GetMinValue();

            Assert.That(result.Value, Is.EqualTo(-124.367).Within(0.001), message: $"At {result.Position}m.");
            Assert.That(result.Position, Is.EqualTo(0).Within(0.001), message: $"At {result.Position}m.");
        }

        [Test()]
        public void CheckMaxShear_Successful()
        {
            var result = _beam.Results.Shear.GetMaxValue();

            Assert.That(result.Value, Is.EqualTo(62.014).Within(0.001), message: $"At {result.Position}m.");
            Assert.That(result.Position, Is.EqualTo(10.01).Within(0.001), message: $"At {result.Position}m.");
        }

        [Test()]
        public void CheckMinShear_Successful()
        {
            var result = _beam.Results.Shear.GetMinValue();

            Assert.That(result.Value, Is.EqualTo(-91.220).Within(0.001), message: $"At {result.Position}m.");
            Assert.That(result.Position, Is.EqualTo(5.01).Within(0.001), message: $"At {result.Position}m.");
        }

        [Test()]
        public void CheckMaxBendingMoment_Successful()
        {
            var result = _beam.Results.BendingMoment.GetMaxValue();

            Assert.That(result.Value, Is.EqualTo(251.006).Within(0.001), message: $"At {result.Position}m.");
            Assert.That(result.Position, Is.EqualTo(5).Within(0.001), message: $"At {result.Position}m.");
        }

        [Test()]
        public void CheckMinBendingMoment_Successful()
        {
            var result = _beam.Results.BendingMoment.GetMinValue();

            Assert.That(result.Value, Is.EqualTo(-205.094).Within(0.001), message: $"At {result.Position}m.");
            Assert.That(result.Position, Is.EqualTo(10).Within(0.001), message: $"At {result.Position}m.");
        }

        [Test()]
        public void CheckMaxVerticalDisplacement_Successful()
        {
            var result = _beam.Results.VerticalDeflection.GetMaxValue();

            Assert.That(result.Value, Is.EqualTo(1.178).Within(0.001), message: $"At {result.Position}m.");
            Assert.That(result.Position, Is.EqualTo(11.19).Within(0.001), message: $"At {result.Position}m.");
        }

        [Test()]
        public void CheckMinVerticalDisplacement_Successful()
        {
            var result = _beam.Results.VerticalDeflection.GetMinValue();

            Assert.That(result.Value, Is.EqualTo(-17.913).Within(0.001), message: $"At {result.Position}m.");
            Assert.That(result.Position, Is.EqualTo(4.65).Within(0.001), message: $"At {result.Position}m.");
        }
    }
}

