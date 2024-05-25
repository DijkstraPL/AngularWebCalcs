using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Spans;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Build_IT_BeamStaticaTests.UnitTests.Spans
{
    [TestFixture]
    public class SpanTests
    {
        private INode _leftNode;
        private INode _rightNode;
        private MaterialData _material;
        private SectionData _section;

        [SetUp]
        public void SetUp()
        {
            var leftNode = new Mock<INode>();
            var rightNode = new Mock<INode>();
            var material = new MaterialData();
            var section = new SectionData();

            section.Area = 3;
            section.MomentOfInteria =11;
            material.YoungModulus=5;
            material.Density=7;
            leftNode.Setup(ln => ln.HorizontalMovementNumber).Returns(0);
            leftNode.Setup(ln => ln.VerticalMovementNumber).Returns(1);
            leftNode.Setup(ln => ln.LeftRotationNumber).Returns(2);
            leftNode.Setup(ln => ln.RightRotationNumber).Returns(3);
            leftNode.Setup(ln => ln.ConcentratedForces).Returns(new List<INodeLoad>());
            rightNode.Setup(rn => rn.HorizontalMovementNumber).Returns(4);
            rightNode.Setup(rn => rn.VerticalMovementNumber).Returns(5);
            rightNode.Setup(rn => rn.LeftRotationNumber).Returns(6);
            rightNode.Setup(rn => rn.RightRotationNumber).Returns(7);
            rightNode.Setup(rn => rn.ConcentratedForces).Returns(new List<INodeLoad>());
            
            _leftNode = leftNode.Object;
            _rightNode = rightNode.Object;
            _material = material;
            _section = section;

        }

        [Test]
        public void SpanCreationTest_Properties_Success()
        {
            var span = new Span(
                _leftNode, length: 7, _rightNode,
                _material, _section, includeSelfWeight: true);

            Assert.That(span.LeftNode, Is.EqualTo(_leftNode));
            Assert.That(span.RightNode, Is.EqualTo(_rightNode));
            Assert.That(span.Length, Is.EqualTo(7));
            Assert.That(span.Material, Is.EqualTo(_material));
            Assert.That(span.Section, Is.EqualTo(_section));
            Assert.That(span.IncludeSelfWeight, Is.EqualTo(true));
            Assert.That(span.ContinousLoads, Is.Not.Null);
            Assert.That(span.PointLoads, Is.Not.Null);
        }

        [Test]
        public void SpanCreationTest_Properties_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => 
            new Span(null, 7, _rightNode, _material, _section, false));
            Assert.Throws<ArgumentNullException>(() =>
            new Span(_leftNode, 7, null, _material, _section, false));
            Assert.Throws<ArgumentNullException>(() =>
            new Span(_leftNode, 7, _rightNode, null, _section, false));
            Assert.Throws<ArgumentNullException>(() =>
            new Span(_leftNode, 7, _rightNode, _material, null, false));
        }
        
    }
}
