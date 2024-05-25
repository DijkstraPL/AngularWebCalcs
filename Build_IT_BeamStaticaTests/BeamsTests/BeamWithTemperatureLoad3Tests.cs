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
    [TestFixture(Description = "18.12.13-03")]
    public class BeamWithTemperatureLoad3Tests
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

            var node1 = new FreeNode();
            var node2 = new PinNode();
            var node3 = new PinNode();
            var node4 = new SupportedNode();

            var nodes = new Node[] { node1, node2, node3, node4 };

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
                length: 10,
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

            var upDownTemperatureLoad = UpDownTemperatureDifferenceLoad.Create(
                span3, upperTemperature: -20, lowerTemperature: 5);
            span3.ContinousLoads.Add(upDownTemperatureLoad);

            var alongTemperatureLoad1 = AlongTemperatureDifferenceLoad.Create(
                span1, temperatureDifference: -50);
            span1.ContinousLoads.Add(alongTemperatureLoad1);

            var alongTemperatureLoad2 = AlongTemperatureDifferenceLoad.Create(
                span2, temperatureDifference: 10);
            span2.ContinousLoads.Add(alongTemperatureLoad2);

            var startLoad = new LoadData(value: -50, position: 0);
            var endLoad = new LoadData(value: -50, position: 10);
            span2.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad, endLoad));

            var pointLoad = new ShearLoad(value: -100, position: 2);
            span3.PointLoads.Add(pointLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.ShearForce.Value, Is.EqualTo(202.790).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.ShearForce.Value, Is.EqualTo(451.631).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].RightNode.NormalForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.ShearForce.Value, Is.EqualTo(-54.421).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection.Value, Is.EqualTo(1.5).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection.Value, Is.EqualTo(69.146).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.RightRotation.Value, Is.EqualTo(-0.013829).Within(0.000001));

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.013829).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(-1).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.013829).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(0.005436).Within(0.000001));
            Assert.That(_beam.Spans[2].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(0.005436).Within(0.000001));

            Assert.That(_beam.Spans[2].RightNode.LeftRotation.Value, Is.EqualTo(-0.001453).Within(0.000001));
            Assert.That(_beam.Spans[2].RightNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(10, 0)]
        [TestCase(12, 0)]
        [TestCase(15, 0)]
        [TestCase(17, 0)]
        [TestCase(20, 0)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(5.00001, 202.790)]
        [TestCase(7, 102.790)]
        [TestCase(10, -47.21)]
        [TestCase(12,-147.21)]
        [TestCase(15, -297.210)]
        [TestCase(15.0001, 154.421)]
        [TestCase(17, 154.421)]
        [TestCase(17.0001, 54.421)]
        [TestCase(20, 54.421)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 305.579)]
        [TestCase(10, 388.948)]
        [TestCase(12, 194.527)]
        [TestCase(15, -472.104)]
        [TestCase(17, -163.263)]
        [TestCase(20, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -0.013829)]
        [TestCase(2, -0.013829)]
        [TestCase(5, -0.013829)]
        [TestCase(7, -0.010214)]
        [TestCase(10, 0.002098)]
        [TestCase(12, 0.008678)]
        [TestCase(15, 0.005436)]
        [TestCase(17, -0.000341)]
        [TestCase(20, -0.001453)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 1.5)]
        [TestCase(2, 0.5)]
        [TestCase(5, -1)]
        [TestCase(7, -0.8)]
        [TestCase(10, -0.5)]
        [TestCase(12, -0.3)]
        [TestCase(15, 0)]
        [TestCase(17, 0)]
        [TestCase(20, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 69.146)]
        [TestCase(2,41.488)]
        [TestCase(5, 0)]
        [TestCase(7, -25.130)]
        [TestCase(10, -37.971)]
        [TestCase(12, -26.504)]
        [TestCase(15, 0)]
        [TestCase(17, 3.997)]
        [TestCase(20, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
