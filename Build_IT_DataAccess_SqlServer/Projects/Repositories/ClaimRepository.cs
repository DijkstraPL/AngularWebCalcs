using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.Projects.Repositories
{
    public sealed class ClaimRepository : Repository<Claim>, IClaimRepository
    {
        public IProjectsDbContext ProjectsDbContext  { get; }


        public ClaimRepository(IProjectsDbContext context) : base(context)
        {
            ProjectsDbContext = context ?? throw new ArgumentNullException(nameof(context));    
        }

        public async Task<IEnumerable<Claim>> GetClaimsForUserAndProject(string userId, int projectId, CancellationToken cancellationToken)
        {
            var claimsDesignerProject = ProjectsDbContext.ProjectDesignerClaims.Where(pdc => pdc.ProjectId == projectId && pdc.DesignerId == userId);
            return await Context.Set<Claim>().Where(c => claimsDesignerProject.Any(pdc => pdc.ClaimId == c.Id)).ToListAsync(cancellationToken);
        }

        public Task<Claim> FindAsync(string claimName, CancellationToken cancellationToken)
        {
            return  Context.Set<Claim>().FirstAsync(c => c.Name == claimName, cancellationToken);
        }
    }
}
