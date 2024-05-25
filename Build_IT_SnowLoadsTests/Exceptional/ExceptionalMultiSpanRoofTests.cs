using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Exceptional;
using Build_IT_SnowLoads.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.Exceptional
{
    [TestFixture()]
    public class ExceptionalMultiSpanRoofTests
    {
        [Test()]
        public void ExceptionalMultiSpanRoofTest_Constructor()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var exceptionalMultiSpanRoof = new ExceptionalMultiSpanRoof(
                building.Object, leftDriftLength: 10, rightDriftLength: 5, heightInTheLowestPart: 2);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(10, exceptionalMultiSpanRoof.LeftDriftLength);
                Assert.AreEqual(5, exceptionalMultiSpanRoof.RightDriftLength);
                Assert.AreEqual(2, exceptionalMultiSpanRoof.HeightInTheLowestPart);
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
                Assert.Throws<ArgumentOutOfRangeException>(() => new ExceptionalMultiSpanRoof(
                building.Object, leftDriftLength: -10, rightDriftLength: 5, heightInTheLowestPart: 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => new ExceptionalMultiSpanRoof(
                building.Object, leftDriftLength: 10, rightDriftLength: -5, heightInTheLowestPart: 2));
                Assert.Throws<ArgumentOutOfRangeException>(() => new ExceptionalMultiSpanRoof(
                building.Object, leftDriftLength: 10, rightDriftLength: 5, heightInTheLowestPart: -2));
            });
        }

        [Test()]
        public void ExceptionalMultiSpanRoofTest_CalculateSnowLoad_HorizontalDimensionOfThreeSlopes()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.B2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(2);

            var exceptionalMultiSpanRoof = new ExceptionalMultiSpanRoof(
                building.Object, leftDriftLength: 10, rightDriftLength: 5, heightInTheLowestPart: 2);

            exceptionalMultiSpanRoof.CalculateSnowLoad();
            Assert.AreEqual(22.5, exceptionalMultiSpanRoof.HorizontalDimensionOfThreeSlopes,0.0001);
        }

        [Test()]
        public void ExceptionalMultiSpanRoofTest_CalculateSnowLoad_ExceptionalDesignSituationBAnnexB()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(true);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.B2);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(2);

            var exceptionalMultiSpanRoof = new ExceptionalMultiSpanRoof(
                building.Object, leftDriftLength: 10, rightDriftLength: 5, heightInTheLowestPart: 2);
            
            exceptionalMultiSpanRoof.CalculateSnowLoad();
            Assert.AreEqual(4, Math.Round(exceptionalMultiSpanRoof.SnowLoad, 3));
        }

        [Test()]
        public void ExceptionalMultiSpanRoofTest_CalculateSnowLoad_NotExceptionalDesignSituationBAnnexB_ThrowsArgumentException()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);
            snowLoad.Setup(sl => sl.ExcepctionalSituation).Returns(false);
            snowLoad.Setup(sl => sl.CurrentDesignSituation).Returns(DesignSituation.A);
            snowLoad.Setup(sl => sl.SnowLoadForSpecificReturnPeriod).Returns(2);

            var exceptionalMultiSpanRoof = new ExceptionalMultiSpanRoof(
                building.Object, leftDriftLength: 10, rightDriftLength: 5, heightInTheLowestPart: 2);

            Assert.Throws<ArgumentException>(() => exceptionalMultiSpanRoof.CalculateSnowLoad());
        }
    }
}