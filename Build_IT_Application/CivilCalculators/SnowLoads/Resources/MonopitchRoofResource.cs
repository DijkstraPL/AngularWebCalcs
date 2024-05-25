using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources
{
    public class MonopitchRoofResource : BaseSnowLoadRoofResource, IMapFrom<MonopitchRoof>
    {
        public double Slope { get; set; }
        public bool SnowFences { get; set; }
    }
}
