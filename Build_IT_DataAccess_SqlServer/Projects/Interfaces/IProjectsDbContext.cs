using Build_IT_DataAccess.DeadLoads.Entities;
using Build_IT_DataAccess.Projects.Entites;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.Projects.Interfaces
{
    public interface IProjectsDbContext : IDbContext
    {
         DbSet<Project> Projects { get; set; }
         DbSet<ProjectDeadLoad> ProjectDeadLoad { get; set; }
         DbSet<DeadLoadLayer> DeadLoadLayers { get; set; }
        DbSet<DeadLoad> DeadLoads { get; set; }
        DbSet<Designer> Designers { get; set; }
         DbSet<Claim> Claims { get; set; }
         DbSet<ParameterInput> ParameterInputs { get; set; }
         DbSet<ProjectDesignerClaim> ProjectDesignerClaims { get; set; }
         DbSet<ProjectScript> ProjectScriptInput { get; set; }
         DbSet<UserCompany> UserCompanies { get; set; }
         DbSet<Company>  Companies { get; set; }

    }
}
