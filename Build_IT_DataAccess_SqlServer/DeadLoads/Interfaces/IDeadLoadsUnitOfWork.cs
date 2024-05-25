using Build_IT_DataAccess.DeadLoads.Repositories.Interfaces;
using Build_IT_DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_DataAccess_SqlServer.DeadLoads.Interfaces
{
    public interface IDeadLoadsUnitOfWork : IUnitOfWork
    {
        IMaterialRepository Materials { get; }
    }
}
