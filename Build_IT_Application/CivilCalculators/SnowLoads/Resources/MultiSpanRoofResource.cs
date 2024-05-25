using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources
{
    public class MultiSpanRoofResource : BaseSnowLoadRoofResource, IMapFrom<MultiSpanRoof>
    {
        public double LeftSlope { get; set; }
        public bool LeftSnowFences { get; set; }
        public double RightSlope { get; set; }
        public bool RightSnowFences { get; set; }
    }
}
