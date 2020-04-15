using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Shared.Repositories
{
    public class EfGenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : KeyedBaseModel<IEntity<TKey>, TKey>
        where TKey : class, IEquatable<TKey>
    {
        private readonly DbContext _dbContext;

        public EfGenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(TKey earTag)
        {
            var idEquals = KeyedBaseModel<TEntity, TKey>.IdEquals(earTag);
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(idEquals);
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TKey earTag, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TKey earTag)
        {
            var entity = await GetById(earTag);
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
