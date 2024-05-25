using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.WindLoadsCases;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.WindLoadsCasesTesrs
{
    [TestFixture]
    public class WallsWindLoadsTests
    {
        [Test]
        [TestCase(0.5,-1.4)]
        [TestCase(5, -1.260206)]
        [TestCase(15, -1.2)]
        public void GetExternalPressureCoefficientsTest_5HeightToLengthRatio_Success(double area, double expectedResult)
        {
            var building = new Mock<IStructure>();
            building.Setup(b => b.Areas).Returns(new Dictionary<Field, double>()
            {
                {Field.A, area },
                {Field.B, 5 },
                {Field.C, 15 }
            });
            building.Setup(b => b.Height).Returns(60);
            building.Setup(b => b.Length).Returns(10);
            building.Setup(b => b.Width).Returns(10);
            var windLoadData = new Mock<IWindLoadData>();
            var verticalWallOfRectangularBuilding
                = new WallsWindLoads(
                    building.Object, windLoadData.Object);

            var result = verticalWallOfRectangularBuilding.GetExternalPressureCoefficientsMax();

            Assert.That(result[Field.A], Is.EqualTo(expectedResult).Within(0.000001));
        }

        [Test]
        [TestCase(50, -0.7)]
        [TestCase(30, -0.6)]
        [TestCase(10,-0.5)]
        [TestCase(6.25, -0.4)]
        [TestCase(2.5,-0.3)]
        public void GetExternalPressureCoefficientsTest_SmallerThan5HeightToLengthRatio_Success(double height, double expectedResult)
        {
            var building = new Mock<IStructure>();
            building.Setup(b => b.Areas).Returns(new Dictionary<Field, double>()
            {
                {Field.E, 10 }
            });
            building.Setup(b => b.Height).Returns(height);
            building.Setup(b => b.Length).Returns(10);
            building.Setup(b => b.Width).Returns(20);
            var windLoadData = new Mock<IWindLoadData>();
            var verticalWallOfRectangularBuilding
                = new WallsWindLoads(
                    building.Object, windLoadData.Object);

            var result = verticalWallOfRectangularBuilding.GetExternalPressureCoefficientsMax();

            Assert.That(result[Field.E], Is.EqualTo(expectedResult).Within(0.000001));
        }

        [Test]
        [TestCase(50, -2.1)]
        [TestCase(30, -1.8)]
        [TestCase(10, -1.5)]
        [TestCase(6.25, -1.2)]
        [TestCase(2.5, -0.9)]
        public void GetExternalWindPressureAtTest_Success(double height, double expectedResult)
        {
            var building = new Mock<IStructure>();
            building.Setup(b => b.Areas).Returns(new Dictionary<Field, double>()
            {
                {Field.E, 10 }
            });
            building.Setup(b => b.Height).Returns(height);
            building.Setup(b => b.Length).Returns(10);
            building.Setup(b => b.Width).Returns(20);
            var windLoadData = new Mock<IWindLoadData>();
            windLoadData.Setup(wld => wld.GetPeakVelocityPressureAt(height, true)).Returns(3);
            var verticalWallOfRectangularBuilding
                = new WallsWindLoads(
                    building.Object, windLoadData.Object);

            var result = verticalWallOfRectangularBuilding.GetExternalWindPressureMaxAt(height);

            Assert.That(result[Field.E], Is.EqualTo(expectedResult).Within(0.000001));
        }
    }
}
