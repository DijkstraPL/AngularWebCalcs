using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources
{
    public class SnowOverhangingResource : BaseSnowLoadRoofResource, IMapFrom<SnowOverhanging>
    {
        public double SnowLayerDepth { get; set; }
        public bool SnowFences { get; set; }
        public double Slope { get; set; }
    }
}
