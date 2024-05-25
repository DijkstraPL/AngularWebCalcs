using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources
{
    public class SnowguardsResource : BaseSnowLoadRoofResource, IMapFrom<Snowguards>
    {
        public double SnowLoadOnRoofValue { get; set; }
        public double Width { get; set; }
        public double Slope { get; set; }
    }
}
