using Build_IT_DataAccess.SnowLoads.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.SnowLoads.Interfaces
{
    public interface ISnowLoadsDbContext : IDbContext
    {
        DbSet<SnowLoad> SnowLoads { get; set; }
        DbSet<CylindricalRoof> CylindricalRoofs { get; set; }
        DbSet<DriftingAtProjectionsObstructions> DriftingsAtProjectionsObstructions { get; set; }
        DbSet<MultiSpanRoof> MultiSpanRoofs { get; set; }
        DbSet<MonopitchRoof> MonopitchRoofs { get; set; }
        DbSet<PitchedRoof> PitchedRoofs { get; set; }
        DbSet<RoofAbuttingToTallerConstruction> RoofsAbuttingToTallerConstructions { get; set; }
        DbSet<Snowguards> Snowguards { get; set; }
        DbSet<SnowOverhanging> SnowOverhangings { get; set; }
        DbSet<ProjectSnowLoad> ProjectsSnowLoads { get; set; }
    }
}
