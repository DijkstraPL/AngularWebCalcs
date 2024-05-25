using Build_IT_DataAccess.SnowLoads.Entities;
using Build_IT_DataAccess.SnowLoads.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.SnowLoads.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.Repositories
{
    public class SnowLoadRepository : Repository<SnowLoad>, ISnowLoadRepository
    {
        public ISnowLoadsDbContext SnowLoadsDbContext { get; }

        public SnowLoadRepository(ISnowLoadsDbContext context) : base(context)
        {
            SnowLoadsDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<SnowLoad>> GetAllSnowLoadsAsync(int projectId, CancellationToken cancellationToken)
        {
            var snowLoads = SnowLoadsDbContext.ProjectsSnowLoads.Where(sl => sl.ProjectId == projectId);
            return await SnowLoadsDbContext.SnowLoads.Where(sl => snowLoads.Any(s => s.SnowLoadId == sl.Id)).Include(sl => sl.SnowLoadRoof).ToListAsync(cancellationToken);
        }

        public async Task AddProjectMapping(ProjectSnowLoad projectEntityMapping, CancellationToken cancellationToken)
        {
            await SnowLoadsDbContext.ProjectsSnowLoads.AddAsync(projectEntityMapping);
        }

        public async Task AddRoof(MonopitchRoof roof)
        {
            await SnowLoadsDbContext.MonopitchRoofs.AddAsync(roof);
        }

        public async Task AddRoof(PitchedRoof roof)
        {
            await SnowLoadsDbContext.PitchedRoofs.AddAsync(roof);
        }

        public async Task AddRoof(MultiSpanRoof roof)
        {
            await SnowLoadsDbContext.MultiSpanRoofs.AddAsync(roof);
        }

        public async Task AddRoof(CylindricalRoof roof)
        {
            await SnowLoadsDbContext.CylindricalRoofs.AddAsync(roof);
        }

        public async Task AddRoof(DriftingAtProjectionsObstructions roof)
        {
            await SnowLoadsDbContext.DriftingsAtProjectionsObstructions.AddAsync(roof);
        }

        public async Task AddRoof(RoofAbuttingToTallerConstruction roof)
        {
            await SnowLoadsDbContext.RoofsAbuttingToTallerConstructions.AddAsync(roof);
        }

        public async Task AddRoof(Snowguards snowguards)
        {
            await SnowLoadsDbContext.Snowguards.AddAsync(snowguards);
        }

        public async Task AddRoof(SnowOverhanging snowOverhanging)
        {
            await SnowLoadsDbContext.SnowOverhangings.AddAsync(snowOverhanging);
        }
    }
}
