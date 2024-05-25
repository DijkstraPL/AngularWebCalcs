using Build_IT_SnowLoads;
using Build_IT_SnowLoads.BuildingTypes;
using Build_IT_SnowLoads.Enums;
using Build_IT_SnowLoads.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.BuildingTypes
{
    [TestFixture()]
    public class PitchedRoofTests
    {
        [Test()]
        public void PitchedRoofTest_Constructor_Success()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var leftRoof = new Mock<IMonopitchRoof>();
            leftRoof.Setup(lr => lr.Slope).Returns(45);
            var rightRoof = new Mock<IMonopitchRoof>();
            rightRoof.Setup(rr => rr.Slope).Returns(15);

            var pitchedRoof = new PitchedRoof(building.Object,
                leftRoof.Object, rightRoof.Object);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(45, pitchedRoof.LeftRoof.Slope);
                Assert.AreEqual(15, pitchedRoof.RightRoof.Slope);
            });
        }

        [Test()]
        public void PitchedRoofTest_CalculateSnowLoad_InvokeCalculationOnRoofSides()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var leftRoof = new Mock<IMonopitchRoof>();
            leftRoof.Setup(lr => lr.Slope).Returns(45);
            leftRoof.Setup(lr => lr.SnowLoadOnRoofValue).Returns(2);
            var rightRoof = new Mock<IMonopitchRoof>();
            rightRoof.Setup(rr => rr.Slope).Returns(15);
            rightRoof.Setup(rr => rr.SnowLoadOnRoofValue).Returns(3);

            var pitchedRoof = new PitchedRoof(building.Object,
                leftRoof.Object, rightRoof.Object);

            pitchedRoof.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                leftRoof.Verify(lr => lr.CalculateSnowLoad());
                rightRoof.Verify(rr => rr.CalculateSnowLoad());
            });
        }

        [Test()]
        public void PitchedRoofTest_CalculateSnowLoad_Success()
        {
            var building = new Mock<IBuilding>();
            var snowLoad = new Mock<ISnowLoad>();
            building.Setup(b => b.SnowLoad).Returns(snowLoad.Object);

            var leftRoof = new Mock<IMonopitchRoof>();
            leftRoof.Setup(lr => lr.Slope).Returns(45);
            leftRoof.Setup(lr => lr.SnowLoadOnRoofValue).Returns(2);
            var rightRoof = new Mock<IMonopitchRoof>();
            rightRoof.Setup(rr => rr.Slope).Returns(15);
            rightRoof.Setup(rr => rr.SnowLoadOnRoofValue).Returns(3);

            var pitchedRoof = new PitchedRoof(building.Object,
                leftRoof.Object, rightRoof.Object);

            pitchedRoof.CalculateSnowLoad();

            Assert.Multiple(() =>
            {
                Assert.That(pitchedRoof.LeftRoofCasesSnowLoad[1], Is.EqualTo(2));
                Assert.That(pitchedRoof.LeftRoofCasesSnowLoad[2], Is.EqualTo(1));
                Assert.That(pitchedRoof.LeftRoofCasesSnowLoad[3], Is.EqualTo(2));
                Assert.That(pitchedRoof.RightRoofCasesSnowLoad[1], Is.EqualTo(3));
                Assert.That(pitchedRoof.RightRoofCasesSnowLoad[2], Is.EqualTo(3));
                Assert.That(pitchedRoof.RightRoofCasesSnowLoad[3], Is.EqualTo(1.5));

            });
        }

    }
}