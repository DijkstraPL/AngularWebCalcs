using Build_IT_DataAccess.Projects.Entites;
using Build_IT_WebApplication.Common.Mappings;

namespace Build_IT_WebApplication.Claims.Queries
{
    public class ClaimResource : IMapFrom<Claim>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
