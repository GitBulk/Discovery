using System;
using System.Linq;
using System.Linq.Expressions;

namespace MultiClients.Infrastructure.Data.Repositories
{
    public interface IRepository<TEntity>
    {
        #region READ
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);

        #endregion
    }
}
