using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetSet();
        Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FindOne(Expression<Func<TEntity, bool>> expression);
        Task Update(Expression<Func<TEntity, bool>> expression, TEntity entity);
        Task DeleteOne(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> InsertOne(TEntity entity);
        Task InsertMany(List<TEntity> entities);
    }
}
