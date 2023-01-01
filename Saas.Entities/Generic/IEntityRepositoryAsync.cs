using System.Linq.Expressions;

namespace Saas.Entities.Generic
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// t refarans tipinde olmali Ientityden 
    /// implemente edilis newlenebilir 
    /// her sey gonderilebilir buraya</typeparam>
    public interface IEntityRepositoryAsync<T> where T : class, IEntity, new()
    {
        Task<T> AddAsyn(T t);
        Task<int> CountAsync();
        Task<int> DeleteAsyn(T entity);
        void Dispose();
        Task<ICollection<T>> FindAllAsync(Expression<Func<T,bool>> match);
        Task<T?> FindAsync(Expression<Func<T,bool>> match);
        Task<ICollection<T>> FindByAsync(Expression<Func<T,bool>> predicate);
        Task<ICollection<T>> GetAllAsync();
        Task<T?> GetAsync(Guid id);
        Task<int> SaveAsync();
        Task<T> UpdateAsyn(T? t,object key);
      
    }
}
