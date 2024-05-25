using Build_IT_DataAccess.Projects.Entites;
using Build_IT_WebApplication.Common.Mappings;
using System;

namespace Build_IT_WebApplication.Companies.Queries
{
    public class CompanyResource : IMapFrom<Company>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
