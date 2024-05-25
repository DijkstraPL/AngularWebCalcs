using Build_IT_DataAccess.SteelProfiles.Entities;
using Microsoft.EntityFrameworkCore;

namespace Build_IT_DataAccess_SqlServer.SteelProfiles.Interfaces
{
    public interface ISteelProfilesDbContext : IDbContext
    {
        DbSet<ProfileType> ProfileTypes { get; set; }
    }
}
