using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using System;

namespace Build_IT_DataAccess_SqlServer.Projects.Repositories
{
    public sealed class ParameterInputRepository : Repository<ParameterInput>, IParameterInputRepository
    {
        public IProjectsDbContext ProjectsDbContext { get; }


        public ParameterInputRepository(IProjectsDbContext context) : base(context)
        {
            ProjectsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
