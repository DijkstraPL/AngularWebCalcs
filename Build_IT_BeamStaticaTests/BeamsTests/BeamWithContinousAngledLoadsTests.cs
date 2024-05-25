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
    [TestFixture(Description = "18.12.17-05")]
    public class BeamWithContinousAngledLoadsTests
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
            span1.ContinousLoads.Add(ContinuousAngledLoad.Create(startLoad, endLoad, 30));

            _beam = new Beam(spans, nodes, includeSelfWeight: false);

            _beam.CalculationEngine.Calculate();
        }

        [Test()]
        public void NodeForcesCalculationsTest_Successful()
        {
            Assert.That(_beam.Spans[0].LeftNode.NormalForce.Value, Is.EqualTo(122.917).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.ShearForce.Value, Is.EqualTo(212.284).Within(0.001));
            Assert.That(_beam.Spans[0].LeftNode.BendingMoment.Value, Is.EqualTo(-496.702).Within(0.001));
            
            Assert.That(_beam.Spans[0].RightNode.NormalForce.Value, Is.EqualTo(127.083).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.ShearForce.Value, Is.EqualTo(220.728).Within(0.001));
            Assert.That(_beam.Spans[0].RightNode.BendingMoment.Value, Is.EqualTo(502.836).Within(0.001));
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
        [TestCase(0, 122.917)]
        [TestCase(2, 122.917)]
        [TestCase(4, 52.917)]
        [TestCase(5, -7.083)]
        [TestCase(7, -97.083)]
        [TestCase(8, -127.083)]
        [TestCase(10, -127.083)]
        public void NormalForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.NormalForce.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 212.284)]
        [TestCase(2, 212.284)]
        [TestCase(4, 91.041)]
        [TestCase(5, -12.882)]
        [TestCase(7, -168.767)]
        [TestCase(8, -220.728)]
        [TestCase(10, -220.728)]
        public void ShearForceAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedShear = _beam.Results.Shear.GetValue(position).Value;

            Assert.That(calculatedShear, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, -496.702)]
        [TestCase(2, -72.133)]
        [TestCase(4, 290.371)]
        [TestCase(5, 328.007)]
        [TestCase(7, 134.811)]
        [TestCase(8, -61.380)]
        [TestCase(10, -502.836)]
        public void BendingMomentAtPositionCalculationsTest_Successful(double position, double result)
        {
            double calculatedMoment = _beam.Results.BendingMoment.GetValue(position).Value;

            Assert.That(calculatedMoment, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -0.006068)]
        [TestCase(4, -0.003301)]
        [TestCase(5, 0.000089)]
        [TestCase(7, 0.005580)]
        [TestCase(8, 0.006018)]
        [TestCase(10, 0)]
        public void RotationAtPositionCalculationsTest_Successful(double position, double result)
        {
            double rotation = _beam.Results.Rotation.GetValue(position).Value;

            Assert.That(rotation, Is.EqualTo(result).Within(0.000001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -0.055)]
        [TestCase(4, -0.101)]
        [TestCase(5, -0.106)]
        [TestCase(7, -0.082)]
        [TestCase(8, -0.056)]
        [TestCase(10, 0)]
        public void HorizontalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.HorizontalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }

        [Test()]
        [TestCase(0, 0)]
        [TestCase(2, -7.577)]
        [TestCase(4, -18.288)]
        [TestCase(5, -19.927)]
        [TestCase(7, -13.562)]
        [TestCase(8, -7.588)]
        [TestCase(10, 0)]
        public void VerticalDeflectionAtPositionCalculationsTest_Successful(double position, double result)
        {
            double deflection = _beam.Results.VerticalDeflection.GetValue(position).Value;

            Assert.That(deflection, Is.EqualTo(result).Within(0.001), message: $"At {position}m.");
        }
    }
}

