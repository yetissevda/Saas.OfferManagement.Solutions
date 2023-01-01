using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
namespace Saas.Entities.Generic;

public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>, IEntityRepositoryAsync<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
{
    #region sync

    public void Add(TEntity entity)
    {
        //unit of work kendisinde dahil
        using var context = new TContext();
        var addedcontext = context.Entry(entity);
        addedcontext.State = EntityState.Added;
        context.SaveChanges();//  SaveChanges();
        //gonderilen entity i context e abone ettik.
        //ister update ister delete ne yapacaksan 
    }

    public void Delete(TEntity entity)
    {
        using var context = new TContext();
        //gonderilen entity i context e abone ettik.ister update ister delete ne yapacaksan 
        var deletedEntity = context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        context.SaveChanges();
    }

    public void Update(TEntity entity, object key)
    {
        using var context = new TContext();
        var exist = context.Set<TEntity>().Find(key);

        if (exist != null)
        {
            context.Entry(exist).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }
        //gonderilen entity i context e abone ettik.ister update ister delete ne yapacaksan 
        //var updatedEntity = context.Entry(entity);
        //updatedEntity.State = EntityState.Modified;
        //context.SaveChanges();
    }
    public virtual TEntity? Get(Expression<Func<TEntity, bool>> filter)
    {
        using var context = new TContext();
        return context.Set<TEntity>().SingleOrDefault(filter);
        
    }
    public List<TEntity> GetList(Expression<Func<TEntity, bool>>? filter = null)
    {
        using var context = new TContext();
        return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
    }

    #endregion

    #region async


    public virtual async Task<ICollection<TEntity>> GetAllAsync()
    {
        using var context = new TContext();
        return await context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(Guid id)
    {
        using var context = new TContext();
        return await context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<TEntity> AddAsyn(TEntity t)
    {
        using var context = new TContext();
        context.Set<TEntity>().Add(t);
        await context.SaveChangesAsync();
        return t;

    }

    public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> match)
    {
        using var context = new TContext();
        return await context.Set<TEntity>().SingleOrDefaultAsync(match);
    }

    public virtual async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
    {
        using var context = new TContext();
        return await context.Set<TEntity>().Where(match).ToListAsync();
    }

    public virtual async Task<int> DeleteAsyn(TEntity entity)
    {
        using var context = new TContext();
        context.Set<TEntity>().Remove(entity);
        return await context.SaveChangesAsync();
    }

    public virtual async Task<TEntity> UpdateAsyn(TEntity? t, object key)
    {
        using var context = new TContext();
        if (t == null)
            return null;
        TEntity? exist = await context.Set<TEntity>().FindAsync(key);
        if (exist != null)
        {
            context.Entry(exist).CurrentValues.SetValues(t);
            await context.SaveChangesAsync();
        }
        return exist;
    }


    public virtual async Task<int> CountAsync()
    {
        using var context = new TContext();
        return await context.Set<TEntity>().CountAsync();
    }

    public virtual async Task<int> SaveAsync()
    {
        using var context = new TContext();
        return await context.SaveChangesAsync();
    }

    public virtual async Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        using var context = new TContext();
        return await context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    //alternatif kullanim bicimi
    //public virtual async Task<TEntity?> GetById(int id)
    //{
    //    using var _context = new TContext();
    //    DbSet<TEntity> dbSet = _context.Set<TEntity>();
    //    return await dbSet.FindAsync(id);
    //}

    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        using var context = new TContext();
        if (!this._disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
            this._disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion

    #region raw Sql
    private class PropertyMapp
    {
        public string? Name { get; set; }
        public Type? Type { get; set; }

        public bool IsSame(PropertyMapp? mapp)
        {
            if (mapp == null)
            {
                return false;
            }
            bool same = mapp.Name == Name && mapp.Type == Type;
            return same;
        }
    }
    /*
     * https://entityframeworkcore.com/knowledge-base/35631903/raw-sql-query-without-dbset---entity-framework-core
     */
    public IEnumerable<T> FromSqlQuery<T>(string query, Func<DbDataReader, T> map, params object[] parameters)
    {
        using var context = new TContext();
        using var command = context.Database.GetDbConnection().CreateCommand();
        if (command.Connection != null && command.Connection.State != ConnectionState.Open)
        {
            command.Connection.Open();
        }
        var currentTransaction = context.Database.CurrentTransaction;
        if (currentTransaction != null)
        {
            command.Transaction = currentTransaction.GetDbTransaction();
        }
        command.CommandText = query;
        if (parameters.Any())
        {
            command.Parameters.AddRange(parameters);
        }

        using var result = command.ExecuteReader();
        while (result.Read())
        {
            yield return map(result);
        }


    }

    public IEnumerable<T> FromSqlQuery<T>(string query, params object[] parameters) where T : new()
    {
        using var context = new TContext();
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
        List<PropertyMapp> entityFields = (from PropertyInfo aProp in typeof(T).GetProperties(flags)
                                           select new PropertyMapp
                                           {
                                               Name = aProp.Name,
                                               Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType
                                           }).ToList();
        List<PropertyMapp> dbDataReaderFields = new List<PropertyMapp>();
        List<PropertyMapp>? commonFields = null;

        using var command = context.Database.GetDbConnection().CreateCommand();
        if (command.Connection != null && command.Connection.State != ConnectionState.Open)
        {
            command.Connection.Open();
        }
        var currentTransaction = context.Database.CurrentTransaction;
        if (currentTransaction != null)
        {
            command.Transaction = currentTransaction.GetDbTransaction();
        }
        command.CommandText = query;
        if (parameters.Any())
        {
            command.Parameters.AddRange(parameters);
        }


        using var result = command.ExecuteReader();
        while (result.Read())
        {
            if (commonFields == null)
            {
                for (int i = 0; i < result.FieldCount; i++)
                {
                    dbDataReaderFields.Add(new PropertyMapp { Name = result.GetName(i), Type = result.GetFieldType(i) });
                }
                commonFields = entityFields.Where(x => dbDataReaderFields.Any(d => d.IsSame(x))).Select(x => x).ToList();
            }

            var entity = new T();
            foreach (var aField in commonFields)
            {
                if (aField.Name != null)
                {
                    PropertyInfo? propertyInfos = entity.GetType().GetProperty(aField.Name);
                    var value = (result[aField.Name] == DBNull.Value) ? null : result[aField.Name]; //if field is nullable
                    propertyInfos?.SetValue(entity, value, null);
                }
            }
            yield return entity;
        }


    }
    #endregion
}
