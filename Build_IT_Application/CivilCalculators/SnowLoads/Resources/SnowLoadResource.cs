using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_WebApplication.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.CivilCalculators.SnowLoads.Resources
{
    public class SnowLoadResource : IMapFrom<SnowLoad>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SnowLoadRoofId { get; set; }
        //public BaseSnowLoadRoofResource SnowLoadRoof { get; set; }
    }
}
