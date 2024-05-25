using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources
{
    public class CylindricalRoofResource : BaseSnowLoadRoofResource, IMapFrom<CylindricalRoof>
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double DriftLength { get; set; }
    }
}
