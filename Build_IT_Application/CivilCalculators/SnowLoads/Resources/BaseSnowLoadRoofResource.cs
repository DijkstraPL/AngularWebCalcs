using Build_IT_Data.Models.SnowLoads.Enums;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources
{
    public abstract class BaseSnowLoadRoofResource 
    {
        public int Id { get; set; }

        public int SnowLoadId { get; set; }
        public SnowLoadResource SnowLoad { get; set; }
        public double AltitudeAboveSea { get; set; }
        public Zones CurrentZone { get; set; }
        public Topographies CurrentTopography { get; set; }

        public double ThermalCoefficient { get; set; }
        public double OverallHeatTransferCoefficient { get; set; }
        public double InternalTemperature { get; set; }
        public double TempreatureDifference { get; set; }
    }
}
