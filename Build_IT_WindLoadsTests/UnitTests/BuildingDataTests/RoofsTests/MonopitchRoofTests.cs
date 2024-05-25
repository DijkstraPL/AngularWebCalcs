using Build_IT_WindLoads.BuildingData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.BuildingDataTests.RoofsTests
{
    [TestFixture]
    public class MonopitchRoofTests
    {
        [Test]
        public void ConstrucorTest_Success()
        {
            var monopitchRoof = new MonopitchRoof(length:1, width:2, 
                maxHeight:4, minHeight:3,
                MonopitchRoof.Rotation.Degrees_0);

            Assert.That(monopitchRoof.MaxHeight, Is.EqualTo(4));
            Assert.That(monopitchRoof.MinHeight, Is.EqualTo(3));
            Assert.That(monopitchRoof.Height, Is.EqualTo(4));
            Assert.That(monopitchRoof.CurrentRotation,
                Is.EqualTo(MonopitchRoof.Rotation.Degrees_0));
            Assert.That(monopitchRoof.Angle, Is.EqualTo(45).Within(0.000001));
        }

        [Test]
        public void ConstrucorTest_Degrees0_Success()
        {
            var monopitchRoof = new MonopitchRoof(length: 1, width: 2,
                maxHeight: 4, minHeight: 3,
                MonopitchRoof.Rotation.Degrees_0);

            Assert.That(monopitchRoof.Length, Is.EqualTo(1));
            Assert.That(monopitchRoof.Width, Is.EqualTo(2));
        }

        [Test]
        public void ConstrucorTest_Degrees180_Success()
        {
            var monopitchRoof = new MonopitchRoof(length: 1, width: 2,
                maxHeight: 4, minHeight: 3,
                MonopitchRoof.Rotation.Degrees_180);

            Assert.That(monopitchRoof.Length, Is.EqualTo(1));
            Assert.That(monopitchRoof.Width, Is.EqualTo(2));
        }

        [Test]
        public void ConstrucorTest_Degrees90_Success()
        {
            var monopitchRoof = new MonopitchRoof(length: 1, width: 2,
                maxHeight: 4, minHeight: 3,
                MonopitchRoof.Rotation.Degrees_90);

            Assert.That(monopitchRoof.Length, Is.EqualTo(2));
            Assert.That(monopitchRoof.Width, Is.EqualTo(1));
        }

        [Test]
        public void AngleTest_0Degrees_Success()
        {
            var monopitchRoof = new MonopitchRoof(length: 1, width: 2,
                maxHeight: 2, minHeight: 1,
                MonopitchRoof.Rotation.Degrees_0);

            Assert.That(monopitchRoof.Angle, Is.EqualTo(45));
        }

        [Test]
        public void AngleTest_90Degrees_Success()
        {
            var monopitchRoof = new MonopitchRoof(length: 1, width: 2,
                maxHeight: 2, minHeight: 1,
                MonopitchRoof.Rotation.Degrees_90);

            Assert.That(monopitchRoof.Angle, Is.EqualTo(45));
        }

        [Test]
        public void AngleInRadiansTest_Success()
        {
            var monopitchRoof = new MonopitchRoof(length: 1, width: 2,
                maxHeight: 2, minHeight: 1,
                MonopitchRoof.Rotation.Degrees_0);

            Assert.That(monopitchRoof.AngleInRadians, Is.EqualTo(0.785).Within(0.001));
        }

        [Test]
        public void GetReferenceHeightTest_Success()
        {
            var monopitchRoof = new MonopitchRoof(length: 1, width: 2,
                maxHeight: 4, minHeight: 3,
                MonopitchRoof.Rotation.Degrees_0);

            var result = monopitchRoof.GetReferenceHeight();

            Assert.That(result, Is.EqualTo(2.4));
        }

        [Test]
        [TestCase(3, 5)]
        [TestCase(1, 2)]
        public void EdgeDistanceTest_Roof_Success(double height, double expectedResult)
        {
            var building = new MonopitchRoof(length: 10, width: 5, 
                maxHeight: height, minHeight: height-2, 
                rotation: MonopitchRoof.Rotation.Degrees_0);

            Assert.That(building.EdgeDistance, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AreasTest_Roof_Rotation0_Success()
        {
            var building = new MonopitchRoof(length: 10, width: 5,
                maxHeight: 3, minHeight:1,
                rotation: MonopitchRoof.Rotation.Degrees_0);

            Assert.That(building.Areas[Field.F], Is.EqualTo(0.637377).Within(0.000001));
            Assert.That(building.Areas[Field.G], Is.EqualTo(1.274755).Within(0.000001));
            Assert.That(building.Areas[Field.H], Is.EqualTo(48.440685).Within(0.000001));
            Assert.That(building.Areas.ContainsKey(Field.I), Is.False);
            Assert.That(building.Areas.ContainsKey(Field.J), Is.False);
        }

        [Test]
        public void AreasTest_Roof_Rotation180_Success()
        {
            var building = new MonopitchRoof(length: 10, width: 5,
                maxHeight: 3, minHeight: 1,
                rotation: MonopitchRoof.Rotation.Degrees_180);

            Assert.That(building.Areas[Field.F], Is.EqualTo(0.637377).Within(0.000001));
            Assert.That(building.Areas[Field.G], Is.EqualTo(1.274755).Within(0.000001));
            Assert.That(building.Areas[Field.H], Is.EqualTo(48.440685).Within(0.000001));
            Assert.That(building.Areas.ContainsKey(Field.I), Is.False);
            Assert.That(building.Areas.ContainsKey(Field.J), Is.False);
        }

        [Test]
        public void AreasTest_Roof_Rotation90_Success()
        {
            var building = new MonopitchRoof(length: 10, width: 5,
                maxHeight: 5, minHeight: 3,
                rotation: MonopitchRoof.Rotation.Degrees_90);

            Assert.That(building.Areas[Field.Fup], Is.EqualTo(2.549510).Within(0.000001));
            Assert.That(building.Areas[Field.Flow], Is.EqualTo(2.549510).Within(0.000001));
            Assert.That(building.Areas[Field.G], Is.EqualTo(5.099020).Within(0.000001));
            Assert.That(building.Areas[Field.H], Is.EqualTo(40.792156).Within(0.000001));
            Assert.That(building.Areas[Field.I], Is.EqualTo(0));
            Assert.That(building.Areas.ContainsKey(Field.J), Is.False);
        }
    }
}
