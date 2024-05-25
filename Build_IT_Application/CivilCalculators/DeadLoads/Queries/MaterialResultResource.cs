using AutoMapper;
using Build_IT_Data.Units.Enums;
using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries.GetAllMaterialsForSubcategory;
using Build_IT_WebApplication.Common.Mappings;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_WebApplication.CivilCalculators.DeadLoads.Queries
{
    public class MaterialResultResource : IMapFrom<Material>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double MinimumDensity { get; set; }
        public double MaximumDensity { get; set; }
        public LoadUnit Unit { get; set; }
        public string DocumentName { get; set; }
        public string Comments { get; set; }
        public int SubcategoryId { get; set; }
        public SubcategoryResultResource Subcategory  { get; set; }
        public List<MaterialAdditionResult> MaterialAdditions { get; set; }

        public void Mapping(Profile profile)
        {
            var map = profile.CreateMap<Material, MaterialResultResource>();
            map.ForMember(res => res.MaterialAdditions, opt => opt.MapFrom(x => x.MaterialAdditions.Select(ma => ma.Addition).ToList()));
        }
    }
}
