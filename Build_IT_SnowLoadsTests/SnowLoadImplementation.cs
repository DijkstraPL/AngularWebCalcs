using Build_IT_SnowLoads;
using Build_IT_SnowLoads.Interfaces;

namespace Build_IT_SnowLoadsUnitTests
{
    public class SnowLoadImplementation : ISnowLoad
    {
        public DesignSituation CurrentDesignSituation { get; set; }
        public bool ExcepctionalSituation { get; set; }
        public double SnowDensity { get; set; }

        public double DefaultCharacteristicSnowLoad { get; set; }

        public double SnowLoadForSpecificReturnPeriod { get; set; }

        public double VariationCoefficient { get; set; }

        public int ReturnPeriod { get; set; }

        public double ExceptionalSnowLoadCoefficient { get; set; }

        public double DesignExceptionalSnowLoadForSpecificReturnPeriod { get; set; }

        public IBuildingSite BuildingSite { get; set; }

        public void CalculateSnowLoad()
        {
            DefaultCharacteristicSnowLoad = 0.9;
        }
    }
}