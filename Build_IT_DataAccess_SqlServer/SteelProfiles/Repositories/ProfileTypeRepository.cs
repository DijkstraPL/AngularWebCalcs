using Build_IT_DataAccess.SteelProfiles.Entities;
using Build_IT_DataAccess.SteelProfiles.Repositories.Interfaces;
using Build_IT_DataAccess_SqlServer.SteelProfiles.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer.SteelProfiles.Repositories
{
    public class ProfileTypeRepository : Repository<ProfileType>, IProfileTypeRepository
    {
        public ISteelProfilesDbContext SteelProfilesContext
            => Context as ISteelProfilesDbContext;

        public ProfileTypeRepository(ISteelProfilesDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ProfileType>> GetAllProfileTypesAsync()
        {
            return await SteelProfilesContext.ProfileTypes
                .Include(pt => pt.Parameters)
                .Include(pt => pt.SectionPoints)
                .Include(pt => pt.SteelProfiles)
                .ThenInclude(sp => sp.ParametersValues)
                .ToListAsync();
        }
    }
}
