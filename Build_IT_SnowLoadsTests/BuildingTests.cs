using Build_IT_SnowLoads;
using NUnit.Framework;
using System;

namespace Build_IT_SnowLoadsUnitTests
{
    [TestFixture()]
    public class BuildingTests
    {
        #region InternalTemperature

        [Test()]
        [Description("Test for setting the internal temperatue")]
        public void BuildingTest_InternalTemperatureLessThan5_Success()
        {
            var building = new Building(new SnowLoadImplementation(),
                internalTemperature: 3, overallHeatTransferCoefficient: 0);

            Assert.AreEqual(0, building.InternalTemperature, "Tempreature shouldn't be less or equal 5. If it is it should be equal to 0.");
        }

        [Test()]
        [Description("Test for setting the internal temperatue")]
        public void BuildingTest_InternalTemperatureMoreThan5LessThan18_Success()
        {
            var building = new Building(new SnowLoadImplementation(), 
                internalTemperature: 8,overallHeatTransferCoefficient: 0);

            Assert.AreEqual(8, building.InternalTemperature, "Tempreature should be equal to the selected value.");
        }

        [Test()]
        [Description("Test for setting the internal temperatue")]
        public void BuildingTest_InternalTemperatureMoreThan18_Success()
        {
            var building = new Building(new SnowLoadImplementation(), 
                internalTemperature: 28, overallHeatTransferCoefficient:0);

            Assert.AreEqual(18, building.InternalTemperature, "Tempreature shouldn't be more than 18.");
        }

        #endregion // InternalTemperature

        #region Constructors

        [Test()]
        [Description("Test if constructor for building class is proper.")]
        public void BuildingTest_Constructor_Success()
        {
            var building = new Building(new SnowLoadImplementation());
            Assert.NotNull(building.SnowLoad, "SnowLoad should be set.");
        }

        [Test()]
        [Description("Test if constructor for building class is proper.")]
        public void BuildingTest_ConstructorWithTemperatureAndOverallHeatTransferCoefficient_Success()
        {
            var building = new Building(new SnowLoadImplementation(), 12, 3);
            Assert.NotNull(building.SnowLoad, "SnowLoad should be set.");
            Assert.AreEqual(12,building.InternalTemperature, "Tempreature should be equal to the selected value.");
            Assert.AreEqual(3, building.OverallHeatTransferCoefficient, "OverallHeatTransferCoefficient should be equal to the selected value.");
        }

        #endregion // Constructors

        #region CalculateThermalCoefficient

        [Test()]
        [Description("Test for thermalCoefficient calculations.")]
        public void CalculateThermalCoefficientTest_Success()
        {
            var building = new Building(new SnowLoadImplementation() { SnowLoadForSpecificReturnPeriod= 2 }, 12, 3);
            building.CalculateThermalCoefficient();

            Assert.AreEqual(7, building.TempreatureDifference, "Temperature difference differ from proper value.");
            Assert.AreEqual(0.681, Math.Round(building.ThermalCoefficient,3), "ThermalCoefficient differ from proper value.");
        }

        #endregion // CalculateThermalCoefficient
    }
}