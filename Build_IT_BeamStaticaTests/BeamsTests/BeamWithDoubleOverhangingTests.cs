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
    [TestFixture(Description = "18.12.12-06")]
    public class BeamWithDoubleOverhangingTests
    {
        private Beam _beam;

        [SetUp]
        public void SetUpBeam()
        {
            var material = new MaterialData
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

            var node1 = new FreeNode();
            var node2 = new SupportedNode();
            var node3 = new SupportedNode();
            var node4 = new SupportedNode();
            var node5 = new FreeNode();

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
                length: 5,
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
                length: 5,
                rightNode: node5,
                material: material,
                section: section,
                includeSelfWeight: false
                );

            var spans = new Span[] { span1, span2, span3, span4 };

            node5.ConcentratedForces.Add(new BendingMoment(value: -50));

            var startLoad1 = new LoadData(value: -10, position: 0);
            var endLoad1 = new LoadData(value: -30, position: 5);
            span1.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad1, endLoad1));

            var startLoad2 = new LoadData(value: -10, position: 0);
            var endLoad2 = new LoadData(value: -10, position: 5);
            span4.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad2, endLoad2));

            var startLoad3 = new LoadData(value: 0, position: 0);
            var endLoad3 = new LoadData(value: -20, position: 5);
            span4.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad3, endLoad3));
            
            var pointLoad1 = new ShearLoad(value: -150, position: 4);
            span2.PointLoads.Add(pointLoad1);

            var pointLoad2 = new ShearLoad(value: -100, position: 2);
            span3.PointLoads.Add(pointLoad2);

            var pointLoad3 = new ShearLoad(value: -50, position: 4);
            span3.PointLoads.Add(pointLoad3);

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].LeftNode.NormalForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.ShearForce.Value, Is.EqualTo(171.367).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].LeftNode.NormalForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.ShearForce.Value, Is.EqualTo(100.6).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[3].LeftNode.NormalForce.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[3].LeftNode.ShearForce.Value, Is.EqualTo(228.033).Within(0.001));
            Assert.That(_beam.Spans[3].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[3].RightNode.NormalForce, Is.Null);
            Assert.That(_beam.Spans[3].RightNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[3].RightNode.BendingMoment, Is.Null);
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.HorizontalDeflection.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection.Value, Is.EqualTo(-22.694).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.RightRotation.Value, Is.EqualTo(0.005246).Within(0.000001));

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(0.002215).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(0.002215).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(0.000038).Within(0.000001));
            Assert.That(_beam.Spans[2].LeftNode.HorizontalDeflection , Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(0.000038).Within(0.000001));

            Assert.That(_beam.Spans[2].RightNode.LeftRotation.Value, Is.EqualTo(-0.001978).Within(0.000001));
            Assert.That(_beam.Spans[3].LeftNode.HorizontalDeflection, Is.Null);
            Assert.That(_beam.Spans[3].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[3].LeftNode.RightRotation.Value, Is.EqualTo(-0.001978).Within(0.000001));

            Assert.That(_beam.Spans[3].RightNode.LeftRotation.Value, Is.EqualTo(-0.004605).Within(0.000001));
            Assert.That(_beam.Spans[3].RightNode.HorizontalDeflection.Value, Is.EqualTo(0).Within(0.001));
            Assert.That(_beam.Spans[3].RightNode.VerticalDeflection.Value, Is.EqualTo(-22.519).Within(0.001));
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
        [TestCase(0, 0)]
        [TestCase(3, -48.00)]
        [TestCase(5, -100)]
        [TestCase(5.01, 71.367)]
        [TestCase(7, 71.367)]
        [TestCase(9, 71.367)]
        [TestCase(9.01, -78.633)]
        [TestCase(10, -78.633)]
        [TestCase(10.01, 21.967)]
        [TestCase(11, 21.967)]
        [TestCase(12, 21.967)]
        [TestCase(12.01, -78.033)]
        [TestCase(14, -78.033)]
        [TestCase(14.01, -128.033)]
        [TestCase(15, -128.033)]
        [TestCase(15.00001, 100)]
        [TestCase(17, 72.000)]
        [TestCase(20, 0)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(3, -63)]
        [TestCase(5, -208.333)]
        [TestCase(7, -65.6)]
        [TestCase(9, 77.133)]
        [TestCase(10, -1.5)]
        [TestCase(11, 20.467)]
        [TestCase(12, 42.433)]
        [TestCase(14, -113.633)]
        [TestCase(15, -241.667)]
        [TestCase(17, -67)]
        [TestCase(20, 50)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0.005246)]
        [TestCase(3, 0.004679)]
        [TestCase(5, 0.002215)]
        [TestCase(7, -0.000441)]
        [TestCase(9, -0.000329)]
        [TestCase(10, 0.000038)]
        [TestCase(11, 0.000130)]
        [TestCase(12, 0.000435)]
        [TestCase(14, -0.000256)]
        [TestCase(15, -0.001978)]
        [TestCase(17, -0.004881)]
        [TestCase(20, -0.004605)]
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
        [TestCase(0, -22.694)]
        [TestCase(3, -7.362)]
        [TestCase(5, 0)]
        [TestCase(7, 1.313)]
        [TestCase(9, 0.082)]
        [TestCase(10, 0)]
        [TestCase(11, 0.066)]
        [TestCase(12, 0.330)]
        [TestCase(14, 1.014)]
        [TestCase(15, 0)]
        [TestCase(17, -7.426)]
        [TestCase(20, -22.519)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
