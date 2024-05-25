using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace Build_IT_DataAccess_SqlServer.Projects.Repositories
{
    public sealed class DeadLoadLayerRepository : Repository<DeadLoadLayer>, IDeadLoadLayerRepository
    {
        public IProjectsDbContext ProjectsDbContext { get; }


        public DeadLoadLayerRepository(IProjectsDbContext context) : base(context)
        {
            ProjectsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
