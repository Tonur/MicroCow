using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IGenericRepository<TEntity, in TKey>
        where TEntity : KeyedBaseModel<IEntity<TKey>, TKey>
        where TKey : class, IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(TKey earTag);

        Task Create(TEntity entity);

        Task Update(TKey earTag, TEntity entity);

        Task Delete(TKey earTag);
    }

    public abstract class KeyedBaseModel<TEntity, TKey>
    {
        [IgnoreDataMember]
        public static readonly Type EntityType = typeof(TEntity);
        [IgnoreDataMember]
        public static readonly Type KeyType = typeof(TKey);
        [IgnoreDataMember]
        public static readonly PropertyInfo KeyProperty = EntityType.GetProperty(nameof(EarTag), BindingFlags.Public | BindingFlags.Instance);

        public static Expression<Func<TEntity, bool>> IdEquals(TKey key)
        {
            var parameter = Expression.Parameter(EntityType, "x"); // x => 
            var property = Expression.Property(parameter, KeyProperty); // x => x.Id
            var constant = Expression.Constant(key, KeyType); // earTag
            var equal = Expression.Equal(property, constant); // x => x.Id == earTag

            return Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
        }

        public abstract TKey EarTag { get; set; }
    }
}
