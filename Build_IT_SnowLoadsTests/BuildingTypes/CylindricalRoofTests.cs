using Build_IT_SnowLoads.BuildingTypes;
using Build_IT_SnowLoads.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.BuildingTypes
{
    [TestFixture()]
    public class CylindricalRoofTests
    {
        [Test()]
        public void CylindricalRoofTest_Constructor_MinusValues()
        {
            var building = new Mock<IBuilding>();
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(()
                    => new CylindricalRoof(building.Object, -20, 10));
                Assert.Throws<ArgumentOutOfRangeException>(()
                    => new CylindricalRoof(building.Object, 20, -10));
            });
        }
        
        [Test()]
        public void CylindricalRoofTest_Constructor()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var cylindricalRoof = new CylindricalRoof(building.Object, 20, 10);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(20, cylindricalRoof.Width);
                Assert.AreEqual(10, cylindricalRoof.Height);
            });
        }

        [Test()]
        public void CylindricalRoofTest_CalculateSnowLoad_ShapeCoefficientLessThan2()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var cylindricalRoof = new CylindricalRoof(building.Object, 20, 2);

            cylindricalRoof.CalculateSnowLoad();

            Assert.That( cylindricalRoof.ShapeCoefficient, Is.EqualTo(1.2).Within(0.0001));
        }

        [Test()]
        public void CylindricalRoofTest_CalculateSnowLoad_ShapeCoefficientMoreThan2()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var cylindricalRoof = new CylindricalRoof(building.Object, 20, 5);

            cylindricalRoof.CalculateSnowLoad();

            Assert.That(cylindricalRoof.ShapeCoefficient, Is.EqualTo(2).Within(0.0001));
        }

        [Test()]
        public void CylindricalRoofTest_CalculateSnowLoad_NotExceptional()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(5);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var cylindricalRoof = new CylindricalRoof(building.Object, 20, 2);
            
            cylindricalRoof.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(24, cylindricalRoof.RoofCasesSnowLoad[1], 0.0001);
                Assert.AreEqual(18, cylindricalRoof.RoofCasesSnowLoad[2], 0.0001);
                Assert.AreEqual(36, cylindricalRoof.RoofCasesSnowLoad[3], 0.0001);
            });
        }

        [Test()]
        public void CylindricalRoofTest_CalculateSnowLoad_Exceptional()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            var buildingSite = new Mock<IBuildingSite>();

            building.Setup(b => b.ThermalCoefficient).Returns(3);
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.BuildingSite).Returns(buildingSite.Object);
            snowLoad.Setup(sl => sl.DesignExceptionalSnowLoadForSpecificReturnPeriod).Returns(7);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            buildingSite.Setup(bs => bs.ExposureCoefficient).Returns(2);

            var cylindricalRoof = new CylindricalRoof(building.Object, 20, 2);

            cylindricalRoof.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(33.6, cylindricalRoof.RoofCasesSnowLoad[1], 0.0001);
                Assert.AreEqual(25.2, cylindricalRoof.RoofCasesSnowLoad[2], 0.0001);
                Assert.AreEqual(50.4, cylindricalRoof.RoofCasesSnowLoad[3], 0.0001);
            });
        }

        [Test()]
        public void CylindricalRoofTest_CalculateDriftLength_Success()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var cylindricalRoof = new CylindricalRoof(building.Object, 20, 10);

            cylindricalRoof.CalculateDriftLength();
            Assert.AreEqual(17.321, Math.Round(cylindricalRoof.DriftLength, 3));
        }
    }
}