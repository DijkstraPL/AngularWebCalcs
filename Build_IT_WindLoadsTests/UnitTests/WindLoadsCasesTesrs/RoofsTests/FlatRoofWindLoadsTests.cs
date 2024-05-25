using Build_IT_WindLoads;
using Build_IT_WindLoads.BuildingData;
using Build_IT_WindLoads.BuildingData.Interfaces;
using Build_IT_WindLoads.WindLoadsCases;
using Build_IT_WindLoads.WindLoadsCases.Roofs;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_WindLoadsTests.UnitTests.WindLoadsCasesTesrs
{
    [TestFixture]
    public class FlatRoofWindLoadsTests
    {
        [Test]
        [TestCase(0.5,-2.5)]
        [TestCase(5, -2.010721)]
        [TestCase(15, -1.8)]
        public void GetExternalPressureCoefficientsTest_Success(
            double area, double expectedResult)
        {
            var building = new Mock<IFlatRoofBuilding>();
            building.Setup(b => b.Areas).Returns(new Dictionary<Field, double>()
            {
                {Field.F, area },
                {Field.G, 5 },
                {Field.H, 15 },
                {Field.I, 15 }
            });
            building.Setup(b => b.Height).Returns(60);
            building.Setup(b => b.Length).Returns(10);
            building.Setup(b => b.Width).Returns(10);
            var windLoadData = new Mock<IWindLoadData>();
            var flatRoofWindLoads
                = new FlatRoofWindLoads(
                    building.Object, windLoadData.Object);

            var result = flatRoofWindLoads.GetExternalPressureCoefficientsMax();

            Assert.That(result[Field.F], Is.EqualTo(expectedResult).Within(0.000001));
        }

        [Test]
        [TestCase(50, -5.4)]
        [TestCase(30, -5.4)]
        [TestCase(10, -5.4)]
        [TestCase(6.25, -5.4)]
        [TestCase(2.5, -5.4)]
        public void GetExternalWindPressureAtTest_Success(double height, double expectedResult)
        {
            var building = new Mock<IFlatRoofBuilding>();
            building.Setup(b => b.Areas).Returns(new Dictionary<Field, double>()
            {
                {Field.F, 10 },
                {Field.G, 5 },
                {Field.H, 15 },
                {Field.I, 15 }
            });
            building.Setup(b => b.Height).Returns(height);
            building.Setup(b => b.Length).Returns(10);
            building.Setup(b => b.Width).Returns(20);
            var windLoadData = new Mock<IWindLoadData>();
            windLoadData.Setup(wld => wld.GetPeakVelocityPressureAt(height, true)).Returns(3);
            var flatRoofWindLoads
                = new FlatRoofWindLoads(
                    building.Object, windLoadData.Object);

            var result = flatRoofWindLoads.GetExternalWindPressureMaxAt(height);

            Assert.That(result[Field.F], Is.EqualTo(expectedResult).Within(0.000001));
        }
    }
}
