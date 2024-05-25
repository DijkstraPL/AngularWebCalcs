using Build_IT_BeamStatica.Nodes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaTests.UnitTests.Nodes
{
    [TestFixture]
    public class FreeNodeTests
    {
        private FreeNode _freeNode;

        [SetUp]
        public void SetUp()
        {
            _freeNode = new FreeNode();
        }

        [Test]
        public void PropertiesTest_Success()
        {
            Assert.That(_freeNode.DegreesOfFreedom, Is.EqualTo(3));

            Assert.That(_freeNode.NormalForce, Is.Null);
            Assert.That(_freeNode.ShearForce, Is.Null);
            Assert.That(_freeNode.BendingMoment, Is.Null);

            Assert.That(_freeNode.HorizontalDeflection, Is.Not.Null);
            Assert.That(_freeNode.VerticalDeflection, Is.Not.Null);
            Assert.That(_freeNode.LeftRotation, Is.Not.Null);
            Assert.That(_freeNode.RightRotation, Is.Not.Null);

            Assert.That(Is.ReferenceEquals(_freeNode.LeftRotation,_freeNode.RightRotation));

            Assert.That(_freeNode.ConcentratedForces, Is.Not.Null);
        }

        [Test]
        public void SetDisplacementNumerationTest_Success()
        {
            short currentCounter = 0;

            _freeNode.SetDisplacementNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(3));

            Assert.That(_freeNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_freeNode.VerticalMovementNumber, Is.EqualTo(1));
            Assert.That(_freeNode.LeftRotationNumber, Is.EqualTo(2));
            Assert.That(_freeNode.RightRotationNumber, Is.EqualTo(2));
        }

        [Test]
        public void SetReactionNumerationTest_Success()
        {
            short currentCounter = 0;

            _freeNode.SetReactionNumeration(ref currentCounter);

            Assert.That(currentCounter, Is.EqualTo(0));

            Assert.That(_freeNode.HorizontalMovementNumber, Is.EqualTo(0));
            Assert.That(_freeNode.VerticalMovementNumber, Is.EqualTo(0));
            Assert.That(_freeNode.LeftRotationNumber, Is.EqualTo(0));
            Assert.That(_freeNode.RightRotationNumber, Is.EqualTo(0));
        }
    }
}
