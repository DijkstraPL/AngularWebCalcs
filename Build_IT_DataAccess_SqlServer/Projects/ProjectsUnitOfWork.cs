using Build_IT_DataAccess.DeadLoads.Repositories.Interfaces;
using Build_IT_DataAccess.Projects.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.Projects
{
    public sealed class ProjectsUnitOfWork : IProjectsUnitOfWork
    {
        private readonly IProjectsDbContext _context;

        public ProjectsUnitOfWork(
            IProjectsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CompleteAsync()
        {
            return await CompleteAsync(CancellationToken.None);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
