using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Roofs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.BuildingDataTests.RoofsTests
{
    [TestFixture]
    public class DuopitchRoofTests
    {
        [Test]
        public void ConstrucorTest_Success()
        {
            var duopitchRoof = new DuopitchRoof(length:1, width:2, 
                outerHeight:3, middleHeight:4,
                DuopitchRoof.Rotation.Degrees_0);

            Assert.That(duopitchRoof.MiddleHeight, Is.EqualTo(4));
            Assert.That(duopitchRoof.OuterHeight, Is.EqualTo(3));
            Assert.That(duopitchRoof.Height, Is.EqualTo(4));
            Assert.That(duopitchRoof.CurrentRotation,
                Is.EqualTo(DuopitchRoof.Rotation.Degrees_0));
            Assert.That(duopitchRoof.Angle, Is.EqualTo(63.434949).Within(0.000001));
        }

        [Test]
        public void ConstrucorTest_Degrees0_Success()
        {
            var duopitchRoof = new DuopitchRoof(length: 1, width: 2,
                outerHeight: 3, middleHeight: 4,
                DuopitchRoof.Rotation.Degrees_0);

            Assert.That(duopitchRoof.Length, Is.EqualTo(1));
            Assert.That(duopitchRoof.Width, Is.EqualTo(2));
        }
        
        [Test]
        public void ConstrucorTest_Degrees90_Success()
        {
            var duopitchRoof = new DuopitchRoof(length: 1, width: 2,
                outerHeight: 3, middleHeight: 4,
                DuopitchRoof.Rotation.Degrees_90);

            Assert.That(duopitchRoof.Length, Is.EqualTo(2));
            Assert.That(duopitchRoof.Width, Is.EqualTo(1));
        }

        [Test]
        public void AngleTest_0Degrees_Success()
        {
            var duopitchRoof = new DuopitchRoof(length: 2, width: 2,
                outerHeight: 3, middleHeight: 4,
                DuopitchRoof.Rotation.Degrees_0);

            Assert.That(duopitchRoof.Angle, Is.EqualTo(45));
        }

        [Test]
        public void AngleTest_90Degrees_Success()
        {
            var duopitchRoof = new DuopitchRoof(length: 2, width: 2,
                outerHeight: 3, middleHeight: 4,
                DuopitchRoof.Rotation.Degrees_90);

            Assert.That(duopitchRoof.Angle, Is.EqualTo(45));
        }

        [Test]
        public void AngleInRadiansTest_Success()
        {
            var duopitchRoof = new DuopitchRoof(length: 2, width: 2,
                outerHeight: 3, middleHeight: 4,
                DuopitchRoof.Rotation.Degrees_0);
            
            Assert.That(duopitchRoof.AngleInRadians, Is.EqualTo(0.785).Within(0.001));
        }

        [Test]
        public void GetReferenceHeightTest_Success()
        {
            var duopitchRoof = new DuopitchRoof(length: 1, width: 2,
                outerHeight: 3, middleHeight: 4,
                DuopitchRoof.Rotation.Degrees_0);

            var result = duopitchRoof.GetReferenceHeight();

            Assert.That(result, Is.EqualTo(2.4));
        }

        [Test]
        [TestCase(3, 5)]
        [TestCase(1, 2)]
        public void EdgeDistanceTest_Roof_Success(double height, double expectedResult)
        {
            var building = new DuopitchRoof(length: 10, width: 5,
                outerHeight: height, middleHeight: height-2, 
                rotation: DuopitchRoof.Rotation.Degrees_0);

            Assert.That(building.EdgeDistance, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AreasTest_Roof_Rotation0_Success()
        {
            var building = new DuopitchRoof(length: 10, width: 5,
                outerHeight: 3, middleHeight: 1,
                rotation: DuopitchRoof.Rotation.Degrees_0);

            Assert.That(building.Areas[Field.F], Is.EqualTo(0.673146).Within(0.000001));
            Assert.That(building.Areas[Field.G], Is.EqualTo(1.346291).Within(0.000001));
            Assert.That(building.Areas[Field.H], Is.EqualTo(24.233242).Within(0.000001));
            Assert.That(building.Areas[Field.I], Is.EqualTo(24.233242).Within(0.000001));
            Assert.That(building.Areas[Field.J], Is.EqualTo(2.692582).Within(0.000001));
        }

        [Test]
        public void AreasTest_Roof_Rotation90_Success()
        {
            var building = new DuopitchRoof(length: 10, width: 5,
                outerHeight: 6, middleHeight: 4,
                rotation: DuopitchRoof.Rotation.Degrees_90);

            Assert.That(building.Areas[Field.F], Is.EqualTo(2.692582).Within(0.000001));
            Assert.That(building.Areas[Field.G], Is.EqualTo(2.692582).Within(0.000001));
            Assert.That(building.Areas[Field.H], Is.EqualTo(21.540659).Within(0.000001));
            Assert.That(building.Areas[Field.I], Is.EqualTo(0));
            Assert.That(building.Areas.ContainsKey(Field.J), Is.False);
        }
    }
}
