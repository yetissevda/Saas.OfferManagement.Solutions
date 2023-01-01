using System.Data.Common;
using System.Linq.Expressions;

namespace Saas.Entities.Generic;

/// <summary>
/// </summary>
/// <typeparam name="T">
/// t refarans tipinde olmali Ientityden 
/// implemente edilis newlenebilir 
/// her sey gonderilebilir buraya</typeparam>
public interface IEntityRepository<T> where T : class, IEntity, new()
{
    T? Get(Expression<Func<T, bool>> filter);
    List<T> GetList(Expression<Func<T, bool>>? filter = null);
    void Add(T entity);
    void Update(T entity, object key);
    void Delete(T entity);

    IEnumerable<T> FromSqlQuery<T>(string query, Func<DbDataReader, T> map, params object[] parameters);
    public IEnumerable<T> FromSqlQuery<T>(string query, params object[] parameters) where T : new();
}