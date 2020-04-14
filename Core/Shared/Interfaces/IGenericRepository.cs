using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IGenericRepository<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(TKey id);

        Task Create(TEntity entity);

        Task Update(TKey id, TEntity entity);

        Task Delete(TKey id);
    }
}
