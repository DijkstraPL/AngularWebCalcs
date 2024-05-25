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
    [TestFixture(Description = "18.12.12-04")]
    public class BeamWithDoubleHinge1Tests
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
            var node2 = new Hinge();
            var node3 = new Hinge();
            var node4 = new SupportedNode();
            var node5 = new SupportedNode();

            var nodes = new Node[] { node1, node2, node3, node4, node5 };

            var span1 = new Span(
                leftNode: node1,
                length: 5,
                rightNode: node2,
                material: material,
                section: section, 
                includeSelfWeight: false
                );

            var span2 = new Span(
                leftNode: node2,
                length: 7,
                rightNode: node3,
                material: material,
                section: section,
                includeSelfWeight: false
                );

            var span3 = new Span(
                leftNode: node3,
                length: 5,
                rightNode: node4,
                material: material,
                section: section,
                includeSelfWeight: false
                );

            var span4 = new Span(
                leftNode: node4,
                length: 3,
                rightNode: node5,
                material: material,
                section: section,
                includeSelfWeight: false
                );

            var spans = new Span[] { span1, span2, span3, span4 };

            node3.ConcentratedForces.Add(new ShearLoad(value: 100));

            var startLoad1 = new LoadData(value: -15, position: 1);
            var endLoad1 = new LoadData(value: -25, position: 5);
            span1.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad1, endLoad1));

            var startLoad2 = new LoadData(value: -25, position: 0);
            var endLoad2 = new LoadData(value: 25, position: 4);
            span3.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad2, endLoad2));

            var pointLoad1 = new ShearLoad(value: -150, position: 3.4);
            span2.PointLoads.Add(pointLoad1);

            var pointLoad2 = new ShearLoad(value: -50, position: 1.5);
            span4.PointLoads.Add(pointLoad2);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(157.143).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-639.048).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[3].LeftNode.NormalForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[3].LeftNode.ShearForce.Value, Is.EqualTo(-25.159).Within(0.001));
            Assert.That(_beam.Spans[3].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[3].RightNode.NormalForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[3].RightNode.ShearForce.Value, Is.EqualTo(48.016).Within(0.001));
            Assert.That(_beam.Spans[3].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.015121).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(-52.220).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(0.003955).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(0.013747).Within(0.000001));
            Assert.That(_beam.Spans[2].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.VerticalDeflection.Value, Is.EqualTo(10.064).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(-0.002522).Within(0.000001));

            Assert.That(_beam.Spans[2].RightNode.LeftRotation.Value, Is.EqualTo(-0.001037).Within(0.000001));
            Assert.That(_beam.Spans[3].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[3].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[3].LeftNode.RightRotation.Value, Is.EqualTo(-0.001037).Within(0.000001));
            
            Assert.That(_beam.Spans[3].RightNode.LeftRotation.Value, Is.EqualTo(0.000668).Within(0.000001));
            Assert.That(_beam.Spans[3].RightNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[3].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(10, 0)]
        [TestCase(12, 0)]
        [TestCase(15, 0)]
        [TestCase(20, 0)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 157.143)]
        [TestCase(2, 140.893)]
        [TestCase(4, 100.893)]
        [TestCase(5, 77.143)]
        [TestCase(7, 77.143)]
        [TestCase(8.3999, 77.143)]
        [TestCase(8.4001, -72.857)]
        [TestCase(10, -72.857)]
        [TestCase(12, -72.857)]
        [TestCase(12.0001, 27.140)]
        [TestCase(14, 2.143)]
        [TestCase(17, 27.143)]
        [TestCase(17.0001, 1.984)]
        [TestCase(19, -48.016)]
        [TestCase(20, -48.016)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -639.048)]
        [TestCase(2, -332.679)]
        [TestCase(4, -89.226)]
        [TestCase(5, 0)]
        [TestCase(7, 154.286)]
        [TestCase(8.4, 262.286)]
        [TestCase(10, 145.714)]
        [TestCase(12, 0)]
        [TestCase(14, 20.952)]
        [TestCase(17, 69.048)]
        [TestCase(19, 48.015)]
        [TestCase(20, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -0.010308)]
        [TestCase(4, -0.014667)]
        [TestCase(4.9999, -0.015121)]
        [TestCase(5, 0.003955)]
        [TestCase(5.0001, 0.003955)]
        [TestCase(7, 0.005601)]
        [TestCase(8, 0.007658)]
        [TestCase(10, 0.012193)]
        [TestCase(11.9999, 0.013747)]
        [TestCase(12, -0.002522)]
        [TestCase(12.0001, -0.002522)]
        [TestCase(14, -0.002210)]
        [TestCase(17, -0.001037)]
        [TestCase(19, 0.000412)]
        [TestCase(20, 0.000668)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(10, 0)]
        [TestCase(12, 0)]
        [TestCase(15, 0)]
        [TestCase(20, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -11.405)]
        [TestCase(4, -37.247)]
        [TestCase(5, -52.220)]
        [TestCase(7, -43.213)]
        [TestCase(8, -36.652)]
        [TestCase(10, -16.394)]
        [TestCase(12, 10.064)]
        [TestCase(14, 5.263)]
        [TestCase(17, 0)]
        [TestCase(19, -0.583)]
        [TestCase(20, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
