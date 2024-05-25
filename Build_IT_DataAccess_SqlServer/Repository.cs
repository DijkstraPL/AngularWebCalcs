using Build_IT_DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Build_IT_DataAccess_SqlServer
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IDbContext Context;

        public Repository(IDbContext context)
        {
            Context = context;
        }

        public virtual ValueTask<TEntity> GetAsync(int id, CancellationToken cancellationToken = default) 
        {
            return Context.Set<TEntity>().FindAsync(new object[] {id}, cancellationToken);
        }

        public virtual  IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public virtual  IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate); 
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Context.Set<TEntity>().AddAsync(entity, cancellationToken); 
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
