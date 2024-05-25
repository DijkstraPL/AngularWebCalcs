using Build_IT_WindLoads.BuildingData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.BuildingDataTests
{
    [TestFixture]
    public class WallTests
    {
        [Test]
        public void ConstructorTest_Success()
        {
            var wall = new Wall(length:1, 
                leftHeight: 2,middleHeight: 3, rightHeight: 4, 
                edgeDistance: 5, Wall.Rotation.Crosswind);

            Assert.That(wall.Length, Is.EqualTo(1));
            Assert.That(wall.LeftHeight, Is.EqualTo(2));
            Assert.That(wall.MiddleHeight, Is.EqualTo(3));
            Assert.That(wall.RightHeight, Is.EqualTo(4));
            Assert.That(wall.EdgeDistance, Is.EqualTo(5));
            Assert.That(wall.CurrentRotation, Is.EqualTo(Wall.Rotation.Crosswind));
        }

        [Test]
        public void AreasTest_CrosswindWithOneField_Success()
        {
            var wall = new Wall(length: 1,
                leftHeight: 2, middleHeight: 3, rightHeight: 4,
                edgeDistance: 5, Wall.Rotation.Crosswind);

            Assert.That(wall.Areas.Count, Is.EqualTo(1));
            Assert.That(wall.Areas[Field.A], Is.EqualTo(3));
        }

        [Test]
        public void AreasTest_CrosswindWithTwoFields_Success()
        {
            var wall = new Wall(length: 4,
                leftHeight: 2, middleHeight: 3, rightHeight: 4,
                edgeDistance: 5, Wall.Rotation.Crosswind);

            Assert.That(wall.Areas.Count, Is.EqualTo(2));
            Assert.That(wall.Areas[Field.A], Is.EqualTo(2.25));
            Assert.That(wall.Areas[Field.B], Is.EqualTo(9.75));
        }

        [Test]
        public void AreasTest_CrosswindWithThreeFields_Success()
        {
            var wall = new Wall(length: 10,
                leftHeight: 2, middleHeight: 3, rightHeight: 4,
                edgeDistance: 5, Wall.Rotation.Crosswind);

            Assert.That(wall.Areas.Count, Is.EqualTo(3));
            Assert.That(wall.Areas[Field.A], Is.EqualTo(2.1));
            Assert.That(wall.Areas[Field.B], Is.EqualTo(10.4));
            Assert.That(wall.Areas[Field.C], Is.EqualTo(17.5));
        }

        [Test]
        public void AreasTest_UpWind_Success()
        {
            var wall = new Wall(length: 10,
                leftHeight: 2, middleHeight: 3, rightHeight: 4,
                edgeDistance: 5, Wall.Rotation.UpWind);

            Assert.That(wall.Areas.Count, Is.EqualTo(1));
            Assert.That(wall.Areas[Field.D], Is.EqualTo(30));
        }

        [Test]
        public void AreasTest_DownWind_Success()
        {
            var wall = new Wall(length: 10,
                leftHeight: 2, middleHeight: 3, rightHeight: 4,
                edgeDistance: 5, Wall.Rotation.DownWind);

            Assert.That(wall.Areas.Count, Is.EqualTo(1));
            Assert.That(wall.Areas[Field.E], Is.EqualTo(30));
        }
    }
}
