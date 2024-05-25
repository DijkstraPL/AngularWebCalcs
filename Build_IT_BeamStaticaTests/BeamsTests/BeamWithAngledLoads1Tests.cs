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
    [TestFixture(Description = "18.12.14-01")]
    public class BeamWithAngledLoads1Tests
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

            var node1 = new TelescopeNode();
            var node2 = new PinNode();
            var node3 = new Hinge();
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

            node3.ConcentratedForces.Add(new AngledLoad(value: -100, angle: 30));
            node2.ConcentratedForces.Add(new VerticalDisplacement(value: -10));

            var pointLoad1 = new AngledLoad(value: -200, position: 1.5, angle: -45);
            span1.PointLoads.Add(pointLoad1);

            var startLoad = new LoadData(value: -20, position: 0);
            var endLoad = new LoadData(value: -20, position: 7);
            span2.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad, endLoad));

            var upDownTemperatureLoad = UpDownTemperatureDifferenceLoad.Create( // TODO: Check Hinge - something wrong
                span3, upperTemperature: 5, lowerTemperature: 0);
            span3.ContinousLoads.Add(upDownTemperatureLoad);

            var alongTemperatureLoad = AlongTemperatureDifferenceLoad.Create(
                span3, temperatureDifference: 10);
            span3.ContinousLoads.Add(alongTemperatureLoad);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(39.387).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-884.086).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.ShearForce.Value, Is.EqualTo(368.024).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].RightNode.NormalForce.Value, Is.EqualTo(-130.809).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.ShearForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection.Value, Is.EqualTo(33.285).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.RightRotation, Is.Null);

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(-0.029988).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.HorizontalDeflection.Value, Is.EqualTo(-0.073).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(-0.029988).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(-0.064815).Within(0.000001));
            Assert.That(_beam.Spans[2].LeftNode.HorizontalDeflection.Value, Is.EqualTo(-0.355).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.VerticalDeflection.Value, Is.EqualTo(-389.558).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(0.078162).Within(0.000001));

            Assert.That(_beam.Spans[2].RightNode.LeftRotation.Value, Is.EqualTo(0.077662).Within(0.000001));
            Assert.That(_beam.Spans[2].RightNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, 39.387)]
        [TestCase(1.5, 39.387)]
        [TestCase(1.5001, 180.808)]
        [TestCase(3, 180.808)]
        [TestCase(3.0001, 180.808)]
        [TestCase(4, 180.808)]
        [TestCase(7, 180.808)]
        [TestCase(9.9999, 180.808)]
        [TestCase(10, 180.808)]
        [TestCase(10.0001, 130.808)]
        [TestCase(12, 130.808)]
        [TestCase(15, 130.808)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1.5, 0)]
        [TestCase(1.5001, -141.421)]
        [TestCase(3, -141.421)]
        [TestCase(3.0001, 226.601)]
        [TestCase(4, 206.603)]
        [TestCase(7, 146.603)]
        [TestCase(9.9999, 86.605)]
        [TestCase(10, 86.603)]
        [TestCase(10.0001, 0)]
        [TestCase(12, 0)]
        [TestCase(15, 0)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -884.086)]
        [TestCase(1.5, -884.086)]
        [TestCase(3, -1096.218)]
        [TestCase(4, -879.615)]
        [TestCase(7, -349.808)]
        [TestCase(9.99999, 0)]
        [TestCase(10, 0)]
        [TestCase(10.0001, 0)]
        [TestCase(12, 0)]
        [TestCase(15, 0)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1.5, -0.014145)]
        [TestCase(3, -0.029988)]
        [TestCase(4, -0.040508)]
        [TestCase(7, -0.059699)]
        [TestCase(9.9999, -0.064815)]
        [TestCase(10, 0.078162)]
        [TestCase(10.0001, 0.078162)]
        [TestCase(12, 0.077962)]
        [TestCase(15, 0.077662)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(1.5, -0.013)]
        [TestCase(3, -0.073)]
        [TestCase(4, -0.114)]
        [TestCase(7, -0.234)]
        [TestCase(10, -0.355)]
        [TestCase(12, -0.213)]
        [TestCase(15, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 33.285)]
        [TestCase(1.5, 22.676)]
        [TestCase(3, -10)]
        [TestCase(4, -45.440)]
        [TestCase(7, -199.988)]
        [TestCase(10, -389.558)]
        [TestCase(12, -233.435)]
        [TestCase(15, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}

