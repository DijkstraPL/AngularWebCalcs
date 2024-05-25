using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_DataAccess.SnowLoads.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.SnowLoads.Interfaces;
using System;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.Repositories
{
    public sealed class ProjectSnowLoadRepository : Repository<ProjectSnowLoad>, IProjectSnowLoadRepository
    {
        public ISnowLoadsDbContext  SnowLoadsDbContext { get; }


        public ProjectSnowLoadRepository(ISnowLoadsDbContext context) : base(context)
        {
            SnowLoadsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
