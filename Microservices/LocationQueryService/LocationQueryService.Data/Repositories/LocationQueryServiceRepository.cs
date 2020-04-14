using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocationQueryService.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace LocationQueryService.Data.Repositories
{
    public class LocationQueryServiceRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly LocationQueryServiceContext _dbContext;

        public LocationQueryServiceRepository(LocationQueryServiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(TKey earTag)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EarTag.Equals(earTag));
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
