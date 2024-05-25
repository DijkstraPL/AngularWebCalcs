using Build_IT_BeamStatica.Beams;
using Build_IT_BeamStatica.CalculationEngines.Interfaces;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaTests.UnitTests.Beams
{
    [TestFixture]
    public class BeamTests
    {
        private IList<ISpan> _spans;
        private ICollection<INode> _nodes;

        [SetUp]
        public void SetUp()
        {
            var span1 = new Mock<ISpan>();
            span1.SetupProperty(s => s.Number);
            var span2 = new Mock<ISpan>();
            span2.SetupProperty(s => s.Number);
            _spans = new List<ISpan>() { span1.Object, span2.Object };

            short counter = 0;
            var node1 = new Mock<INode>();
            node1.Setup(n => n.DegreesOfFreedom).Returns(1);
            node1.Setup(n => n.SetDisplacementNumeration(ref counter)).Callback(() => counter = 1);
            node1.Setup(n => n.SetReactionNumeration(ref counter)).Callback(() => counter = 1);
            var node2 = new Mock<INode>();
            node2.Setup(n => n.DegreesOfFreedom).Returns(2);
            node2.Setup(n => n.SetDisplacementNumeration(ref counter)).Callback(() => counter = 5);
            node2.Setup(n => n.SetReactionNumeration(ref counter)).Callback(() => counter = 5);
            var node3 = new Mock<INode>();
            node3.Setup(n => n.DegreesOfFreedom).Returns(3);
            node2.Setup(n => n.SetDisplacementNumeration(ref counter)).Callback(() => counter = 7);
            node2.Setup(n => n.SetReactionNumeration(ref counter)).Callback(() => counter = 7);
            _nodes = new List<INode>()
            { node1.Object, node2.Object, node3.Object };
        }

        [Test]
        public void ConstructorTest_Success()
        {
            var beam = new Beam(_spans, _nodes, includeSelfWeight: true);

            Assert.That(beam.Spans, Is.SameAs(_spans));
            Assert.That(beam.Spans.Count, Is.EqualTo(2));
            Assert.That(beam.Nodes, Is.SameAs(_nodes));
            Assert.That(beam.Nodes.Count, Is.EqualTo(3));
            Assert.That(beam.IncludeSelfWeight, Is.True); 
            Assert.That(beam.Results, Is.Not.Null);
            Assert.That(beam.CalculationEngine, Is.Not.Null);
        }

        [Test]
        public void FullConstructorTest_Success()
        {
            var beamCalculationEngine = new Mock<IBeamCalculationEngine>();
            var resultContainer = new Mock<IResultsContainer>();

            var beam = new Beam(
                _spans, 
                _nodes, 
                beamCalculationEngine.Object, 
                resultContainer.Object,  
                includeSelfWeight: true);

            Assert.That(beam.CalculationEngine, Is.SameAs(beamCalculationEngine.Object));
            Assert.That(beam.Results, Is.SameAs(resultContainer.Object));
        }

        [Test]
        public void SetNumeration()
        {
            var beam = new Beam(_spans, _nodes, includeSelfWeight: true);

            beam.SetNumeration();

            Assert.That(beam.NumberOfDegreesOfFreedom, Is.EqualTo(6));
        }
    }
}
