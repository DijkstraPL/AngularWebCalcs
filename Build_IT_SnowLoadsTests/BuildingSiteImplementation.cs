using Build_IT_SnowLoads.Enums;
using Build_IT_SnowLoads.Interfaces;

namespace Build_IT_SnowLoadsUnitTests
{
    public class BuildingSiteImplementation : IBuildingSite
    {
        public double AltitudeAboveSea { get; set; }
        public Zones CurrentZone { get; set; }
        public Topographies CurrentTopography { get; set; }

        public double ExposureCoefficient { get; private set; }


        public BuildingSiteImplementation(double exposureCoefficient)
        {
            ExposureCoefficient = exposureCoefficient;
        }

        public void CalculateExposureCoefficient()
        {
            ExposureCoefficient = 1;
        }
    }
}