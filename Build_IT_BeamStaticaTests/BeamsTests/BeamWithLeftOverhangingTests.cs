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
    [TestFixture(Description = "18.12.12-12")]
    public class BeamWithLeftOverhangingTests
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

            var node1 = new FreeNode();
            var node2 = new SupportedNode();
            var node3 = new SupportedNode();
            var node4 = new FixedNode();

            var nodes = new Node[] { node1, node2, node3, node4 };

            var span1 = new Span(
                leftNode: node1,
                length: 3,
                rightNode: node2,
                material: material,
                section: section1,
                includeSelfWeight: false
                );

            var span2 = new Span(
                leftNode: node2,
                length: 4,
                rightNode: node3,
                material: material,
                section: section1,
                includeSelfWeight: false
                );

            var span3 = new Span(
                leftNode: node3,
                length: 10,
                rightNode: node4,
                material: material,
                section: section2, 
                includeSelfWeight: false
                );

            var spans = new Span[] { span1, span2, span3 };

            node1.ConcentratedForces.Add(new ShearLoad(value: -200));
            node3.ConcentratedForces.Add(new BendingMoment(value: 90));

            var startLoad = new LoadData(value: -30, position: 2);
            var endLoad = new LoadData(value: -30, position: 6);
            span3.ContinousLoads.Add(ContinuousShearLoad.Create(startLoad, endLoad));

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }
        
        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {            
            Assert.That(_beam.Spans[0].LeftNode.ShearForce, Is.Null);
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[1].LeftNode.ShearForce.Value, Is.EqualTo(309.876).Within(0.001));
            Assert.That(_beam.Spans[1].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].LeftNode.ShearForce.Value, Is.EqualTo(-46.501).Within(0.001));
            Assert.That(_beam.Spans[2].LeftNode.BendingMoment, Is.Null);

            Assert.That(_beam.Spans[2].RightNode.ShearForce.Value, Is.EqualTo(56.626).Within(0.001));
            Assert.That(_beam.Spans[2].RightNode.BendingMoment.Value, Is.EqualTo(156.751).Within(0.001));
        }

        [Test()]
        public void NodeDisplacementsCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.VerticalDeflection.Value, Is.EqualTo(-17.574).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.RightRotation.Value, Is.EqualTo(0.007024).Within(0.000001));

            Assert.That(_beam.Spans[0].RightNode.LeftRotation.Value, Is.EqualTo(0.003526).Within(0.000001));
            Assert.That(_beam.Spans[1].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[1].LeftNode.RightRotation.Value, Is.EqualTo(0.003526).Within(0.000001));

            Assert.That(_beam.Spans[1].RightNode.LeftRotation.Value, Is.EqualTo(-0.002387).Within(0.000001));
            Assert.That(_beam.Spans[2].LeftNode.VerticalDeflection, Is.Null);
            Assert.That(_beam.Spans[2].LeftNode.RightRotation.Value, Is.EqualTo(-0.002387).Within(0.000001));

            Assert.That(_beam.Spans[2].RightNode.LeftRotation, Is.Null);
            Assert.That(_beam.Spans[2].RightNode.VerticalDeflection, Is.Null);
        }

        [Test()]
        [TestCase(0, -200)]
        [TestCase(1.5, -200)]
        [TestCase(3, -200)]
        [TestCase(3.01, 109.876)]
        [TestCase(5, 109.876)]
        [TestCase(7, 109.876)]
        [TestCase(7.01, 63.374)]
        [TestCase(9, 63.374)]
        [TestCase(10, 33.374)]
        [TestCase(11, 3.374)]
        [TestCase(12, -26.626)]
        [TestCase(13, -56.626)]
        [TestCase(15, -56.626)]
        [TestCase(17, -56.626)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(3, -600)]
        [TestCase(7, -160.496)]
        [TestCase(7.00001, -70.496)]
        [TestCase(9, 56.253)]
        [TestCase(12, 111.376)]
        [TestCase(13, 69.75)]
        [TestCase(17, -156.752)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0.007024)]
        [TestCase(3, 0.003526)]
        [TestCase(7, -0.002387)]
        [TestCase(9, -0.002539)]
        [TestCase(12, 0.000863)]
        [TestCase(13, 0.001856)]
        [TestCase(17, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -17.574)]
        [TestCase(2, -4.562)]
        [TestCase(7, 0)]
        [TestCase(5, 2.956)]
        [TestCase(9, -5.376)]
        [TestCase(11, -8.565)]
        [TestCase(13, -6.933)]
        [TestCase(15, -2.539)]
        [TestCase(17, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}
