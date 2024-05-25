using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Exceptional;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.Exceptional
{
    [TestFixture()]
    public class ExceptionalSnowBehindParapetTests
    {
        [Test()]
        [Description("Check constructor for the ExceptionalSnowBehindParapet.")]
        public void ExceptionalSnowBehindParapetTest_Constructor_Success()
        {
            var building = BuildingImplementation.CreateBuilding();

            var exceptionalSnowBehindParapet =
                new ExceptionalSnowBehindParapet(building, 20, 1);

            Assert.IsNotNull(exceptionalSnowBehindParapet,
                "ExceptionalSnowBehindParapet should be created.");
            Assert.AreEqual(20, exceptionalSnowBehindParapet.Width,
                "Width should be set at construction time.");
            Assert.AreEqual(1, exceptionalSnowBehindParapet.HeightDifference,
                "Height should be set at construction time.");
        }

        [Test()]
        [Description("Check calculations of snow loads for the ExceptionalSnowBehindParapet.")]
        public void ExceptionalSnowBehindParapetTest_CalculateSnowLoad_Success()
        {
            var building = BuildingImplementation.CreateBuilding();
            building.SnowLoadImplementation.ExcepctionalSituation = true;
            building.SnowLoadImplementation.CurrentDesignSituation = DesignSituation.B2;

            var exceptionalSnowBehindParapet =
                new ExceptionalSnowBehindParapet(building, 20, 1);

            exceptionalSnowBehindParapet.CalculateSnowLoad();

            Assert.AreEqual(2, Math.Round(exceptionalSnowBehindParapet.SnowLoad, 3),
                "Snow load for roof is not calculated properly.");
        }

        [Test()]
        [Description("Check calculations of drift length for the ExceptionalSnowBehindParapet.")]
        public void ExceptionalSnowBehindParapetTest_CalculateDriftLength_Success()
        {
            var building = BuildingImplementation.CreateBuilding();

            var exceptionalSnowBehindParapet =
                new ExceptionalSnowBehindParapet(building, 20, 1);

            exceptionalSnowBehindParapet.CalculateDriftLength();
            Assert.AreEqual(5, Math.Round(exceptionalSnowBehindParapet.DriftLength, 3),
                "Drift length for roof is not calculated properly.");
        }
    }
}