using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources
{
    public class DriftingAtProjectionsObstructionsResource : BaseSnowLoadRoofResource, IMapFrom<DriftingAtProjectionsObstructions>
    {
        public double DriftLength { get; set; }
        public double ObstructionHeight { get; set; }
    }
}
