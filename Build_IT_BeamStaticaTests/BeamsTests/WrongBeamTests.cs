using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.PointLoads;
using Build_IT_BeamStatica.Nodes;
using Build_IT_BeamStatica.Spans;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.BeamsTests
{
    [TestFixture]
    public class WrongBeamTests
    {
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

                var node1 = new FreeNode();
                var node2 = new PinNode();

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

                var pointLoad1 = new ShearLoad(value: -200, position: 5);
                span1.PointLoads.Add(pointLoad1);

                _beam = new Beam(spans, nodes, includeSelfWeight: false);
            }

            [Test()]
            public void NodeForcesCalculationsTest_ThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => _beam.CalculationEngine.Calculate());
            }
        }
    }
}
