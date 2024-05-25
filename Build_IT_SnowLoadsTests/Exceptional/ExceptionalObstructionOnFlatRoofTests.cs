using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Exceptional;
using Build_IT_SnowLoads.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.Exceptional
{
    [TestFixture()]
    public class ExceptionalObstructionOnFlatRoofTests
    {
        [Test()]
        public void ExceptionalObstructionOnFlatRoofTest_Constructor_Success()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var exceptionalObstructionOnFlatRoof =
                new ExceptionalObstructionOnFlatRoof(building.Object, leftWidth: 15, rightWidth: 20,
                leftHeightDifference: 1, rightHeightDifference: 0.5);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(15, exceptionalObstructionOnFlatRoof.LeftWidth);
                Assert.AreEqual(20, exceptionalObstructionOnFlatRoof.RightWidth);
                Assert.AreEqual(1, exceptionalObstructionOnFlatRoof.LeftHeightDifference);
                Assert.AreEqual(0.5, exceptionalObstructionOnFlatRoof.RightHeightDifference);
            });
        }

        [Test()]
        public void ExceptionalMultiSpanRoofTest_Constructor_MinusValues_ThrowsArgumentOutOfRangeException()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);


            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new ExceptionalObstructionOnFlatRoof(
                    building.Object, leftWidth: -15, rightWidth: 20,
                    leftHeightDifference: 1, rightHeightDifference: 0.5));
                Assert.Throws<ArgumentOutOfRangeException>(() => new ExceptionalObstructionOnFlatRoof(
                    building.Object, leftWidth: 15, rightWidth: -20,
                leftHeightDifference: 1, rightHeightDifference: 0.5));
                Assert.Throws<ArgumentOutOfRangeException>(() => new ExceptionalObstructionOnFlatRoof(
                    building.Object, leftWidth: 15, rightWidth: 20,
                leftHeightDifference: -1, rightHeightDifference: 0.5));
                Assert.Throws<ArgumentOutOfRangeException>(() => new ExceptionalObstructionOnFlatRoof(
                    building.Object, leftWidth: 15, rightWidth: 20,
                leftHeightDifference: -1, rightHeightDifference: -0.5));
            });
        }

        [Test()]
        public void ExceptionalMultiSpanRoofTest_Constructor_LargerThan2HeightDifferences_ThrowsArgumentOutOfRangeException()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);


            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => new ExceptionalObstructionOnFlatRoof(
                    building.Object, leftWidth: 15, rightWidth: 20,
                    leftHeightDifference: 3, rightHeightDifference: 0.5));
                Assert.Throws<ArgumentOutOfRangeException>(() => new ExceptionalObstructionOnFlatRoof(
                    building.Object, leftWidth: 15, rightWidth: 20,
                leftHeightDifference: 1, rightHeightDifference: 3));
            });
        }

        [Test()]
        public void ExceptionalObstructionOnFlatRoofTest_CalculateDriftLength_HeightDifferenceLessThan1()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.B2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(2);

            var exceptionalObstructionOnFlatRoof =
                new ExceptionalObstructionOnFlatRoof(building.Object, leftWidth: 15, rightWidth: 2,
                leftHeightDifference: 1, rightHeightDifference: 0.5);

            exceptionalObstructionOnFlatRoof.CalculateDriftLength();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(5, exceptionalObstructionOnFlatRoof.LeftDriftLength, 0.0001);
                Assert.AreEqual(2, exceptionalObstructionOnFlatRoof.RightDriftLength, 0.0001);
            });
        }

        [Test()]
        public void ExceptionalObstructionOnFlatRoofTest_CalculateDriftLength_HeightDifferenceMoreThan1()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.B2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(2);

            var exceptionalObstructionOnFlatRoof =
                new ExceptionalObstructionOnFlatRoof(building.Object, leftWidth: 15, rightWidth: 2,
                leftHeightDifference: 1.5, rightHeightDifference: 1.7);

            exceptionalObstructionOnFlatRoof.CalculateDriftLength();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(15, exceptionalObstructionOnFlatRoof.LeftDriftLength, 0.0001);
                Assert.AreEqual(2, exceptionalObstructionOnFlatRoof.RightDriftLength, 0.0001);
            });
        }

        [Test()]
        public void ExceptionalMultiSpanRoofTest_CalculateSnowLoad_ExceptionalDesignSituationBAnnexB()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.B2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(0.5);

            var exceptionalObstructionOnFlatRoof =
                new ExceptionalObstructionOnFlatRoof(building.Object, leftWidth: 15, rightWidth: 2,
                leftHeightDifference: 1, rightHeightDifference: 2);

            exceptionalObstructionOnFlatRoof.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(4, exceptionalObstructionOnFlatRoof.LeftShapeCoefficient, 0.0001);
                Assert.AreEqual(2, exceptionalObstructionOnFlatRoof.LeftSnowLoad, 0.0001);
                Assert.AreEqual(5, exceptionalObstructionOnFlatRoof.RightShapeCoefficient, 0.0001);
                Assert.AreEqual(2.5, exceptionalObstructionOnFlatRoof.RightSnowLoad, 0.0001);
            });
        }

        [Test()]
        public void ExceptionalObstructionOnFlatRoofTest_CalculateSnowLoad_NotExceptionalDesignSituationBAnnexB_ThrowsArgumentException()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(false);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.A);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(2);

            var exceptionalObstructionOnFlatRoof =
                new ExceptionalObstructionOnFlatRoof(building.Object, leftWidth: 15, rightWidth: 2,
                leftHeightDifference: 1, rightHeightDifference: 0.5);

            Assert.Throws<ArgumentException>(() => exceptionalObstructionOnFlatRoof.CalculateSnowLoad());
        }
    }
}