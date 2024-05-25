using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources
{
    public class RoofAbuttingToTallerConstructionResource : BaseSnowLoadRoofResource, IMapFrom<RoofAbuttingToTallerConstruction>
    {
        public double WidthOfUpperBuilding { get; set; }
        public bool WidthOfLowerBuilding { get; set; }
        public double HeightDifference { get; set; }
        public bool SlopeOfHigherRoof { get; set; }
        public bool SnowFencesOnHigherRoof { get; set; }
    }
}
