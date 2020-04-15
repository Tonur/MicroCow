using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Shared.Interfaces;

namespace Shared.Repositories
{
    public class MdbGenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : KeyedBaseModel<IEntity<TKey>, TKey>
        where TKey : class, IEquatable<TKey>
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MdbGenericRepository(IMongoDatabase database)
        {
            if (database.ListCollectionNames().ToEnumerable().All(name => name != typeof(TEntity).Name))
                database.CreateCollectionAsync(typeof(TEntity).Name);
            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<TEntity> GetById(TKey earTag)
        {
            if (await _collection.CountDocumentsAsync(model => model.EarTag == earTag) == 0) 
                return null;
            {
                return _collection.Find<TEntity>(model => model.EarTag == earTag).FirstOrDefault();
            }
        }

        public async Task Create(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update(TKey earTag, TEntity entity)
        {
            await _collection.ReplaceOneAsync(model => model.EarTag == earTag, entity);
        }

        public async Task Delete(TKey earTag)
        {
            await _collection.DeleteOneAsync(model => model.EarTag == earTag);
        }
    }
}
