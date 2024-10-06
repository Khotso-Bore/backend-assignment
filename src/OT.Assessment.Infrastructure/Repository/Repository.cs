using Microsoft.EntityFrameworkCore;
using OT.Assessment.Domain.Data;
using OT.Assessment.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private OTDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(OTDbContext context) {

            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetSet()
        {
           return _dbSet.AsQueryable();
        }

        public async Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<TEntity> FindOne(Expression<Func<TEntity, bool>> expression)
        {
            var response = await _dbSet.FirstOrDefaultAsync(expression);
            if (response != null)
                return null;

            return response;
        }

        public async Task Update(Expression<Func<TEntity, bool>> expression, TEntity entity)
        {
            var response = await _dbSet.FirstOrDefaultAsync(expression);
            if (response != null)
                return;

            _dbSet.Update(entity);
            return;
        }

        public async Task DeleteOne(Expression<Func<TEntity, bool>> expression)
        {
            var response = await _dbSet.FirstOrDefaultAsync(expression);
            if (response != null)
                return;

            _dbSet.Remove(response);
            return;
        }

        public async Task<TEntity> InsertOne(TEntity entity)
        {
            var response = await _dbSet.AddAsync(entity);
            return entity;

        }
    }
}
