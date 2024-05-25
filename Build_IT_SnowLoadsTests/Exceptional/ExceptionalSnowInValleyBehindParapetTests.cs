using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Exceptional;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests.Exceptional
{
    [TestFixture()]
    public class ExceptionalSnowInValleyBehindParapetTests
    {
        [Test()]
        [Description("Check constructor for the ExceptionalSnowInValleyBehindParapet.")]
        public void ExceptionalSnowInValleyBehindParapetTest_Constructor_Success()
        {
            var building = BuildingImplementation.CreateBuilding();

            var eceptionalSnowInValleyBehindParapet =
                new ExceptionalSnowInValleyBehindParapet(building, 20, 1);

            Assert.IsNotNull(eceptionalSnowInValleyBehindParapet,
                "ExceptionalSnowBehindParapetAtEaves should be created.");
            Assert.AreEqual(20, eceptionalSnowInValleyBehindParapet.Width,
                "Width should be set at construction time.");
            Assert.AreEqual(1, eceptionalSnowInValleyBehindParapet.HeightDifference,
                "Height should be set at construction time.");
        }

        [Test()]
        [Description("Check calculations of snow loads for the ExceptionalSnowInValleyBehindParapet.")]
        public void ExceptionalSnowInValleyBehindParapetTest_CalculateSnowLoad_Success()
        {
            var building = BuildingImplementation.CreateBuilding();
            building.SnowLoadImplementation.ExcepctionalSituation = true;
            building.SnowLoadImplementation.CurrentDesignSituation = DesignSituation.B2;

            var eceptionalSnowInValleyBehindParapet =
                new ExceptionalSnowInValleyBehindParapet(building, 20, 1);

            eceptionalSnowInValleyBehindParapet.CalculateSnowLoad();

            Assert.AreEqual(2, Math.Round(eceptionalSnowInValleyBehindParapet.SnowLoad, 3),
                "Snow load for roof is not calculated properly.");
        }

        [Test()]
        [Description("Check calculations of drift length for the ExceptionalSnowInValleyBehindParapet.")]
        public void ExceptionalSnowInValleyBehindParapetTest_CalculateDriftLength_Success()
        {
            var building = BuildingImplementation.CreateBuilding();

            var eceptionalSnowInValleyBehindParapet =
                new ExceptionalSnowInValleyBehindParapet(building, 20, 1);

            eceptionalSnowInValleyBehindParapet.CalculateDriftLength();
            Assert.AreEqual(5, Math.Round(eceptionalSnowInValleyBehindParapet.DriftLength, 3),
                "Drift length for roof is not calculated properly.");
        }
    }
}