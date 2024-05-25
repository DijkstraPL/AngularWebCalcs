using Build_IT_SnowLoads.Interfaces;

namespace Build_IT_SnowLoadsUnitTests
{
    public class BuildingImplementation : IBuilding
    {
        public double ThermalCoefficient { get; set; }

        public double InternalTemperature { get; set; }

        public double TempreatureDifference { get; set; }

        public double OverallHeatTransferCoefficient { get; set; }

        public ISnowLoad SnowLoad { get; set; }
        public SnowLoadImplementation SnowLoadImplementation 
            => SnowLoad as SnowLoadImplementation;

        public void CalculateThermalCoefficient()
        {
            ThermalCoefficient = 0.9;
        }
        
        public static BuildingImplementation CreateBuilding()
        {
            var buildingSite = new BuildingSiteImplementation(1);
            var snowLoad = new SnowLoadImplementation()
            {
                BuildingSite = buildingSite,
                SnowLoadForSpecificReturnPeriod = 0.9,
                SnowDensity = 2
            };
            var building = new BuildingImplementation()
            {
                SnowLoad = snowLoad,
                ThermalCoefficient = 1
            };
            return building;
        }
    }
}