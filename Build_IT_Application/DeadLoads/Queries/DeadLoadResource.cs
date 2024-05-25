using Build_IT_DataAccess.Projects.Entites;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.DeadLoads.Queries
{
    public class DeadLoadResource : IMapFrom<DeadLoad>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
