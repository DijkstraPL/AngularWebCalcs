using Build_IT_DataAccess.Projects.Entites;
using Build_IT_WebApplication.Common.Mappings;
using System;

namespace Build_IT_WebApplication.Projects.Queries
{
    public class ProjectResource : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
