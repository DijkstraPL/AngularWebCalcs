using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using System;

namespace Build_IT_DataAccess_SqlServer.Projects.Repositories
{
    public sealed class ProjectDeadLoadRepository : Repository<ProjectDeadLoad>, IProjectDeadLoadRepository
    {
        public IProjectsDbContext ProjectsDbContext { get; }


        public ProjectDeadLoadRepository(IProjectsDbContext context) : base(context)
        {
            ProjectsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
