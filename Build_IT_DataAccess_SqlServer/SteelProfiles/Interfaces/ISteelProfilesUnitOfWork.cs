using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.SteelProfiles.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_DataAccess_SqlServer.SteelProfiles.Interfaces
{
    public interface ISteelProfilesUnitOfWork : IUnitOfWork
    {
        IProfileTypeRepository ProfileTypes { get; }
    }
}
