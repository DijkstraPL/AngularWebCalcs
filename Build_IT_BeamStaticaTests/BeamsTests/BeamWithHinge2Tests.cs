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
    [TestFixture(Description = "18.12.12-08")]
    public class BeamWithHinge2Tests
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
            var node3 = new SupportedNode();
            var node4 = new SupportedNode();

            var nodes = new Node[] { node1, node2, node3, node4 };

            var span1 = new Span(
                leftNode: node1,
                length: 3,
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

            var spans = new Span[] { span1, span2, span3 };

            var startLoad1 = new LoadData(value: -10, position: 0);
            var endLoad1 = new LoadData(value: -10, position: 3);
            span1.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad1, endLoad1));

            var startLoad2 = new LoadData(value: -10, position: 0);
            var endLoad2 = new LoadData(value: -10, position: 7);
            span2.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad2, endLoad2));

            var startLoad3 = new LoadData(value: -10, position: 0);
            var endLoad3 = new LoadData(value: -10, position: 5);
            span3.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad3, endLoad3));

            var pointLoad = new ShearLoad(value: -100, position: 2.5);
            span3.PointLoads.Add(pointLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(50.976).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-107.927).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].LeftNode.NormalForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.ShearForce.Value, Is.EqualTo(143.659).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].RightNode.NormalForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.ShearForce.Value, Is.EqualTo(55.366).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.001487).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection.Value, Is.EqualTo(-3.094).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(0.000139).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(-0.000477).Within(0.000001));
            Assert.That(_beam.Spans[2].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(-0.000477).Within(0.000001));

            Assert.That(_beam.Spans[2].RightNode.LeftRotation.Value, Is.EqualTo(0.001350).Within(0.000001));
            Assert.That(_beam.Spans[2].RightNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(10, 0)]
        [TestCase(12, 0)]
        [TestCase(15, 0)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 50.976)]
        [TestCase(2, 30.976)]
        [TestCase(3, 20.976)]
        [TestCase(5, 0.976)]
        [TestCase(7, -19.024)]
        [TestCase(10, -49.024)]
        [TestCase(10.00001, 94.634)]
        [TestCase(12, 74.635)]
        [TestCase(12.5, 69.634)]
        [TestCase(12.50001, -30.366)]
        [TestCase(14, -45.365)]
        [TestCase(15, -55.366)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -107.927)]
        [TestCase(2, -25.975)]
        [TestCase(3, 0)]
        [TestCase(5, 21.951)]
        [TestCase(7, 3.902)]
        [TestCase(10, -98.171)]
        [TestCase(12, 71.098)]
        [TestCase(12.5, 107.165)]
        [TestCase(14, 50.366)]
        [TestCase(15, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2,-0.001357)]
        [TestCase(2.99999, -0.001487)]
        [TestCase(3, 0.000139)]
        [TestCase(3.00001, 0.000139)]
        [TestCase(5, 0.000444)]
        [TestCase(7, 0.000791)]
        [TestCase(10, -0.000477)]
        [TestCase(12, -0.000695)]
        [TestCase(14, 0.001072)]
        [TestCase(15, 0.001350)]
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
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -1.649)]
        [TestCase(3, -3.094)]
        [TestCase(5, -2.588)]
        [TestCase(7, -1.288)]
        [TestCase(10, 0)]
        [TestCase(12, -1.773)]
        [TestCase(14, -1.256)]
        [TestCase(15, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
