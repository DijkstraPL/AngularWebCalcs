using Build_IT_DataAccess.Projects.Entites;
using Build_IT_DataAccess.Projects.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.Projects.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.Projects.Repositories
{
    public sealed class DeadLoadRepository : Repository<DeadLoad>, IDeadLoadRepository
    {
        public IProjectsDbContext ProjectsDbContext { get; }


        public DeadLoadRepository(IProjectsDbContext context) : base(context)
        {
            ProjectsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<DeadLoad>> GetAllForProjectAsync(int projectId, CancellationToken cancellationToken = default)
        {
            var projectDeadLoads = await ProjectsDbContext.ProjectDeadLoad
                .Include(pdl => pdl.DeadLoad)
                .ThenInclude(dl => dl.DeadLoadLayers)
                .ThenInclude(dll => dll.Material)
                .ThenInclude(m => m.MaterialAdditions)
                .Where(pdl => pdl.ProjectId == projectId)
                .Select(pdl => pdl.DeadLoad)
                .ToListAsync();
            return projectDeadLoads;
        }

        public async Task AddProjectMapping(ProjectDeadLoad projectDeadLoad, CancellationToken cancellationToken)
        {
            await ProjectsDbContext.ProjectDeadLoad.AddAsync(projectDeadLoad, cancellationToken);
        }

        public async Task<IEnumerable<DeadLoadLayer>> GetLayers(int deadLoadId, CancellationToken cancellationToken = default)
        {
            var deadLoadLayers = await ProjectsDbContext.DeadLoadLayers
                .Where(pdl => pdl.DeadLoadId == deadLoadId)
                .ToListAsync();
            return deadLoadLayers;
        }

        public void RemoveLayers(int deadLoadId)
        {
            var deadLoadLayers =  ProjectsDbContext.DeadLoadLayers.Where(dll => dll.DeadLoadId == deadLoadId);
            ProjectsDbContext.DeadLoadLayers.RemoveRange(deadLoadLayers);
        }

        public async Task AddLayersAsync(IEnumerable<DeadLoadLayer> deadLoadLayers, CancellationToken cancellationToken)
        {
            await ProjectsDbContext.DeadLoadLayers.AddRangeAsync(deadLoadLayers, cancellationToken);
        }

        public async Task AddLayerAsync(DeadLoadLayer deadLoadLayer, CancellationToken cancellationToken)
        {
            await ProjectsDbContext.DeadLoadLayers.AddAsync(deadLoadLayer, cancellationToken);
        }
    }
}
