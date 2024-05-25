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
    [TestFixture(Description = "18.12.12-03")]
    public class BeamWithVerticalDisplacementAtNode1Tests
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
            var section1 = new CustomSectionData(
                new List<Point>
                {
                    new Point(0,0),
                    new Point(300,0),
                    new Point(300,700),
                    new Point(0,700),
                });
            var section2 = new CustomSectionData(
                new List<Point>
                {
                    new Point(0,0),
                    new Point(300,0),
                    new Point(300,500),
                    new Point(0,500),
                });

            var node1 = new SleeveNode();
            var node2 = new PinNode();
            var node3 = new PinNode();
            var node4 = new FixedNode();

            var nodes = new Node[] { node1, node2, node3, node4 };

            var span1 = new Span(
                leftNode: node1,
                length: 7,
                rightNode: node2,
                material: material1,
                section: section1,
                includeSelfWeight: false
                );

            var span2 = new Span(
                leftNode: node2,
                length: 3,
                rightNode: node3,
                material: material1,
                section: section2,
                includeSelfWeight: false
                );

            var span3 = new Span(
                leftNode: node3,
                length: 5,
                rightNode: node4,
                material: material2,
                section: section2,
                includeSelfWeight: false
                );

            var spans = new Span[] { span1, span2, span3 };

            node2.ConcentratedForces.Add(new NormalLoad(value: 200));
            node3.ConcentratedForces.Add(new VerticalDisplacement(value: -10));

            var pointLoad = new ShearLoad(value: -100, position: 2.5);
            span3.PointLoads.Add(pointLoad);

            var startLoad = new LoadData(value: -10, position: 0);
            var endLoad = new LoadData(value: -10, position: 7);
            span1.ContinousLoads.Add( ContinuousShearLoad.Create(startLoad, endLoad));

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(-21.193).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(90.283).Within(0.001));

            Assert.That(_beam.Spans[1].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.ShearForce.Value, Is.EqualTo(297.418).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.ShearForce.Value, Is.EqualTo(-294.409).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].RightNode.NormalForce.Value, Is.EqualTo(-200).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.ShearForce.Value, Is.EqualTo(188.183).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.BendingMoment.Value, Is.EqualTo(375.306).Within(0.001));
        }
        
        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0)]
        [TestCase(5, 0)]
        [TestCase(7, 0)]
        [TestCase(7.0001, 200)]
        [TestCase(8, 200)]
        [TestCase(10, 200)]
        [TestCase(12, 200)]
        [TestCase(16, 200)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedNormalForce = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedNormalForce, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -21.193)]
        [TestCase(2, -41.193)]
        [TestCase(5, -71.192)]
        [TestCase(7, -91.193)]
        [TestCase(7.0001, 206.226)]
        [TestCase(8, 206.226)]
        [TestCase(10, 206.226)]
        [TestCase(10.0001,-88.183)]
        [TestCase(12, -88.183)]
        [TestCase(12.5, -88.183)]
        [TestCase(12.5001, -188.183)]
        [TestCase(15, -188.183)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 90.283)]
        [TestCase(2, 27.898)]
        [TestCase(5, -140.68)]
        [TestCase(7, -303.066)]
        [TestCase(8, -96.840)]
        [TestCase(10, 315.611)]
        [TestCase(12, 139.244)]
        [TestCase(12.5, 95.153)]
        [TestCase(15, -375.306)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0.000485)]
        [TestCase(5, -0.000085)]
        [TestCase(7, -0.001784)]
        [TestCase(8, -0.003917)]
        [TestCase(10, -0.001583)]
        [TestCase(12, 0.002828)]
        [TestCase(15, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0.335)]
        [TestCase(2, 0.335)]
        [TestCase(5, 0.335)]
        [TestCase(7, 0.335)]
        [TestCase(8, 0.291)]
        [TestCase(10, 0.202)]
        [TestCase(12, 0.121)]
        [TestCase(15, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, 0.566)]
        [TestCase(5, 1.658)]
        [TestCase(7, 0)]
        [TestCase(8, -3.034)]
        [TestCase(10, -10)]
        [TestCase(12, -8.186)]
        [TestCase(15, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
