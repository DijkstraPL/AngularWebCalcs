using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_WebApplication.Common.Mappings;
using System.Collections.Generic;

namespace Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries
{
    public class CategoryResultResource : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubcategoryResultResource> Subcategories { get; set; }
    }
}
