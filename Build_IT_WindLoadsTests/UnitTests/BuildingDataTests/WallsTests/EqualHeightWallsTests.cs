using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Roofs;
using Build_IT_WindLoads.BuildingData.Walls;
using Moq;
using NUnit.Framework;

namespace Build_IT_WindLoadsTests.UnitTests.BuildingDataTests.WallsTests
{
    [TestFixture]
    public class EqualHeightWallsTests
    {
        [Test]
        public void ConstructorTest_Walls_Success()
        {
            var building = new EqualHeightWalls(length: 10, width: 5, height: 3);

            Assert.That(building.Length, Is.EqualTo(10));
            Assert.That(building.Width, Is.EqualTo(5));
            Assert.That(building.Height, Is.EqualTo(3));
            Assert.That(building.Areas.Count, Is.GreaterThan(0));
        }

        [Test]
        public void ConstructorTest_Walls_Rotated_Success()
        {
            var building = new EqualHeightWalls(length: 10, width: 5, height: 3, 
                EqualHeightWalls.Rotation.Degrees_90);

            Assert.That(building.Length, Is.EqualTo(5));
            Assert.That(building.Width, Is.EqualTo(10));
            Assert.That(building.Height, Is.EqualTo(3));
            Assert.That(building.Areas.Count, Is.GreaterThan(0));
        }
        
        [Test]
        [TestCase(3, 5)]
        [TestCase(1, 2)]
        public void EdgeDistanceTest_Walls_WidthIsSmaller_Success(double height, double expectedResult)
        {
            var building = new EqualHeightWalls(length: 10, width: 5, height: height);

            Assert.That(building.EdgeDistance, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AreasTest_LargeWall_Success()
        {
            var building = new EqualHeightWalls(length: 15, width: 10, height: 5);

            Assert.That(building.Areas[Field.A], Is.EqualTo(10));
            Assert.That(building.Areas[Field.B], Is.EqualTo(40));
            Assert.That(building.Areas[Field.C], Is.EqualTo(25));
            Assert.That(building.Areas[Field.D], Is.EqualTo(50));
            Assert.That(building.Areas[Field.E], Is.EqualTo(50));
        }
        
        [Test]
        public void AreasTest_MediumWall_Success()
        {
            var building = new EqualHeightWalls(length: 10, width: 15, height: 5);

            Assert.That(building.Areas[Field.A], Is.EqualTo(10));
            Assert.That(building.Areas[Field.B], Is.EqualTo(40));
            Assert.That(building.Areas[Field.D], Is.EqualTo(75));
            Assert.That(building.Areas[Field.E], Is.EqualTo(75));
            Assert.That(building.Areas.ContainsKey(Field.C), Is.False);
        }

        [Test]
        public void AreasTest_SmallWall_Success()
        {
            var building = new EqualHeightWalls(length: 1, width: 15, height: 5);

            Assert.That(building.Areas[Field.A], Is.EqualTo(5));
            Assert.That(building.Areas[Field.D], Is.EqualTo(75));
            Assert.That(building.Areas[Field.E], Is.EqualTo(75));
            Assert.That(building.Areas.ContainsKey(Field.B), Is.False);
            Assert.That(building.Areas.ContainsKey(Field.C), Is.False);
        }

        [Test]
        public void GetReferenceHeightTest_Walls_Success()
        {
            var building = new EqualHeightWalls(length: 10, width: 15, height: 10);

            Assert.That(building.GetReferenceHeight(), Is.EqualTo(6));
        }
    }
}
