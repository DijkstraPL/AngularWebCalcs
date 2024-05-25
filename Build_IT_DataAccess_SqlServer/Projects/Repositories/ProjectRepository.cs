using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.Projects.Repositories
{
    public sealed class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public IProjectsDbContext ProjectsDbContext { get; }


        public ProjectRepository(IProjectsDbContext context) : base(context)
        {
            ProjectsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Project>> GetAllForCompanyAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => { return ProjectsDbContext.Projects.Where(p => p.CompanyId == companyId).ToList(); }, cancellationToken);
        }
    }
}
