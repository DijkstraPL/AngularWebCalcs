using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using System;

namespace Build_IT_DataAccess_SqlServer.Projects.Repositories
{
    public sealed class DesignerRepository : Repository<Designer>, IDesignerRepository
    {
        public IProjectsDbContext ProjectsDbContext { get; }


        public DesignerRepository(IProjectsDbContext context) : base(context)
        {
            ProjectsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
