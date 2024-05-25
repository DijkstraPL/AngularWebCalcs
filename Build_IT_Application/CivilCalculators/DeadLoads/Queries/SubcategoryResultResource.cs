using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_WebApplication.Common.Mappings;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries
{
    public class SubcategoryResultResource : IMapFrom<Subcategory>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DocumentName { get; set; }
        public int CategoryId { get; set; }
        public CategoryResultResource Category  { get; set; }
        public List<MaterialResultResource> Materials { get; set; }

    }
}
