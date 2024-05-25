using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Exceptional;
using Build_IT_SnowLoads.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.Exceptional
{
    [TestFixture()]
    public class ExceptionalObstructionOnPitchedOrCurvedRoofTests
    {
        [Test()]
        public void ExceptionalObstructionOnPitchedOrCurvedRoofTest_Constructor_Success()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var exceptionalObstructionOnPitchedOrCurvedRoof =
                new ExceptionalObstructionOnPitchedOrCurvedRoof(building.Object, 
                leftWidth: 15, rightWidth: 20, leftHeightDifference: 1, rightHeightDifference: 0.5);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(15, exceptionalObstructionOnPitchedOrCurvedRoof.LeftWidth);
                Assert.AreEqual(20, exceptionalObstructionOnPitchedOrCurvedRoof.RightWidth);
                Assert.AreEqual(1, exceptionalObstructionOnPitchedOrCurvedRoof.LeftHeightDifference);
                Assert.AreEqual(0.5, exceptionalObstructionOnPitchedOrCurvedRoof.RightHeightDifference);
            });
        }

        [Test()]
        public void ExceptionalObstructionOnPitchedOrCurvedRoofTest_Constructor_MinusValues_ThrowsArgumentOutOfRangeException()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);


            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => 
                new ExceptionalObstructionOnPitchedOrCurvedRoof(building.Object,
                leftWidth: -15, rightWidth: 20, leftHeightDifference: 1, rightHeightDifference: 0.5));
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ExceptionalObstructionOnPitchedOrCurvedRoof(building.Object,
                leftWidth: 15, rightWidth: -20, leftHeightDifference: 1, rightHeightDifference: 0.5));
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ExceptionalObstructionOnPitchedOrCurvedRoof(building.Object,
                leftWidth: 15, rightWidth: 20, leftHeightDifference: -1, rightHeightDifference: 0.5));
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                new ExceptionalObstructionOnPitchedOrCurvedRoof(building.Object,
                leftWidth: 15, rightWidth: 20, leftHeightDifference: 1, rightHeightDifference: -0.5));
            });
        }

        [Test()]
        public void ExceptionalObstructionOnPitchedOrCurvedRoofTest_CalculateSnowLoad_ExceptionalDesignSituationBAnnexB()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.B2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(0.5);


            var exceptionalObstructionOnPitchedOrCurvedRoof =
                new ExceptionalObstructionOnPitchedOrCurvedRoof(building.Object,
                leftWidth: 15, rightWidth: 20, leftHeightDifference: 1, rightHeightDifference: 2);

            exceptionalObstructionOnPitchedOrCurvedRoof.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(4, exceptionalObstructionOnPitchedOrCurvedRoof.LeftShapeCoefficient);
                Assert.AreEqual(2, exceptionalObstructionOnPitchedOrCurvedRoof.LeftSnowLoad);
                Assert.AreEqual(5, exceptionalObstructionOnPitchedOrCurvedRoof.RightShapeCoefficient);
                Assert.AreEqual(2.5, exceptionalObstructionOnPitchedOrCurvedRoof.RightSnowLoad);
            });
        }

        [Test()]
        public void ExceptionalObstructionOnPitchedOrCurvedRoofTest_CalculateSnowLoad_NotExceptionalDesignSituationBAnnexB()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(false);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.B1);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(0.5);


            var exceptionalObstructionOnPitchedOrCurvedRoof =
                new ExceptionalObstructionOnPitchedOrCurvedRoof(building.Object,
                leftWidth: 15, rightWidth: 20, leftHeightDifference: 1, rightHeightDifference: 2);
            
            Assert.Throws<ArgumentException>(() => exceptionalObstructionOnPitchedOrCurvedRoof.CalculateSnowLoad());
        }

        [Test()]
        public void ExceptionalObstructionOnPitchedOrCurvedRoofTest_CalculateDriftLength_HeightDifferenceLessThan1()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.B2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(2);

            var exceptionalObstructionOnPitchedOrCurvedRoof =
                new ExceptionalObstructionOnPitchedOrCurvedRoof(building.Object,
                leftWidth: 15, rightWidth: 2, leftHeightDifference: 1, rightHeightDifference: 0.5);

            exceptionalObstructionOnPitchedOrCurvedRoof.CalculateDriftLength();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(5, exceptionalObstructionOnPitchedOrCurvedRoof.LeftDriftLength, 0.0001);
                Assert.AreEqual(2, exceptionalObstructionOnPitchedOrCurvedRoof.RightDriftLength, 0.0001);
            });
        }

        [Test()]
        public void ExceptionalObstructionOnPitchedOrCurvedRoofTest_CalculateDriftLength_HeightDifferenceMoreThan1()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.B2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(0.5);

            var exceptionalObstructionOnPitchedOrCurvedRoof =
                new ExceptionalObstructionOnPitchedOrCurvedRoof(building.Object,
                leftWidth: 15, rightWidth: 20, leftHeightDifference: 1.5, rightHeightDifference: 1.7);

            exceptionalObstructionOnPitchedOrCurvedRoof.CalculateDriftLength();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(15, exceptionalObstructionOnPitchedOrCurvedRoof.LeftDriftLength, 0.0001);
                Assert.AreEqual(20, exceptionalObstructionOnPitchedOrCurvedRoof.RightDriftLength, 0.0001);
            });
        }
    }
}