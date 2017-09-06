using TelegramBot.Business.Domain.Core;
using TelegramBot.Common.Extensions;
namespace TelegramBot.DataAccess.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IDataContextFactory _dataContextFactory;

        public RepositoryBase(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

        #region Async

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                return predicate == null ? set.Count() : set.Count(predicate);
            }
        }
        public int NonQuery(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetContext()) { return db.Database.ExecuteSqlCommand(query, args); }
        }
        public TEntity One(long id, params string[] includes)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                if (includes == null || includes.Any()) { return db.CreateSet<TEntity>().Find(id); }
                IQueryable<TEntity> set = db.CreateSet<TEntity>().AsNoTracking();
                includes.ForEach(include => set = set.Include(include));
                return set.FirstOrDefault(entity => entity.Id == id);
            }
        }
        public TEntity One(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>().AsNoTracking();
                includes.ForEach(include => set = set.Include(include));
                return set.FirstOrDefault(predicate);
            }
        }
        public TValue Query<TValue>(Func<IQueryable<TEntity>, TValue> query, params string[] includes)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                return query.Invoke(set);
            }
        }
        public void Remove(TEntity instance)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                db.CreateSet<TEntity>().Remove(instance);
                db.SaveChanges();
            }
        }
        public void Remove(long id)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                var set = db.CreateSet<TEntity>();
                var instance = set.Find(id);
                if (instance == null) { return; }
                set.Remove(instance);
                db.SaveChanges();
            }
        }
        public TEntity Save(TEntity instance)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                if (instance.Id == 0)
                {
                    db.CreateSet<TEntity>().Add(instance);
                }
                else
                {
                    db.MarkAsModified(instance);
                }
                db.SaveChanges();
                return instance;
            }
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                if (predicate == null) { return await set.CountAsync(); }
                return await set.CountAsync(predicate);
            }
        }
        public async Task<int> NonQueryAsync(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                var token = new CancellationToken();
                return await db.Database.ExecuteSqlCommandAsync(query, token, args);
            }
        }
        public async Task<TEntity> OneAsync(long id, params string[] includes)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                if (includes == null || !includes.Any()) { return await db.CreateSet<TEntity>().FindAsync(id); }
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                return await set.FirstOrDefaultAsync(entity => entity.Id == id);
            }
        }
        public async Task<TEntity> OneAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                IQueryable<TEntity> set = db.CreateSet<TEntity>();
                includes.ForEach(include => set = set.Include(include));
                if (predicate == null) { return await set.FirstOrDefaultAsync(); }
                return await set.FirstOrDefaultAsync(predicate);
            }
        }
        public async Task<int> NonQExecuteueryAsync(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                var token = new CancellationToken();
                return await db.Database.ExecuteSqlCommandAsync(query, token, args);
            }
        }
        public async Task<List<TValue>> NonQueryAsync<TValue>(string query, params object[] args)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                var token = new CancellationToken();
                return await db.Database.SqlQuery<TValue>(query, token, args).ToListAsync();
            }
        }
        public async Task<TValue> QueryAsync<TValue>(Func<IQueryable<TEntity>, Task<TValue>> query,
            params string[] includes)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                try
                {
                    IQueryable<TEntity> set = db.CreateSet<TEntity>();
                    includes.ForEach(include => set = set.Include(include));
                    var value = await query.Invoke(set);
                    return value;
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
        public async Task<int> RemoveAsync(TEntity instance)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                db.CreateSet<TEntity>().Remove(instance);
                return await db.SaveChangesAsync();
            }
        }
        public async Task<int> RemoveAsync(long id)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                var set = db.CreateSet<TEntity>();
                var instance = set.Find(id);
                if (instance == null) { return await new Task<int>(() => 0); }
                set.Remove(instance);
                return await db.SaveChangesAsync();
            }
        }
        public async Task<int> SaveAsync(TEntity instance)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                if (instance.Id == 0)
                {
                    db.CreateSet<TEntity>().Add(instance);
                }
                else
                {
                    db.MarkAsModified(instance);
                }
                return await db.SaveChangesAsync();
            }
        }
        public async Task<int> UpdateWithAttachAsync(TEntity instance, params string[] includes)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                var set = db.CreateSet<TEntity>().Attach(instance);
                includes.ForEach(include => db.Entry(set).Property(include).IsModified = true);
                // db.MarkAsModified(instance);
                return await db.SaveChangesAsync();
            }
        }
        public async Task<int> SaveAsync(TEntity instance, params string[] parameters)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                if (instance.Id == 0)
                {
                    db.CreateSet<TEntity>().Add(instance);
                }
                else
                {
                    db.MarkAsModified(instance);
                }
                return await db.SaveChangesAsync();
            }
        }
        public async Task<int> DeleteWithAttachAsync(TEntity instance)
        {
            using (var db = _dataContextFactory.GetContext())
            {
                db.CreateSet<TEntity>().Attach(instance);
                db.CreateSet<TEntity>().Remove(instance);
                return await db.SaveChangesAsync();
            }
        }
        // save new instance with list of Relation
        public async Task<int> InsertManyToManyRelationshipAsync<TProperty>(TEntity instance,
            IEnumerable<TProperty> newValues)
            where TProperty : class, IEntity
        {
            using (var db = _dataContextFactory.GetContext())
            {
                var secondarySet = db.CreateSet<TProperty>();
                newValues.ForEach(entity => secondarySet.Attach(entity));
                db.CreateSet<TEntity>().Add(instance);
                return await db.SaveChangesAsync();
            }
        }
        public async Task<int> RemoveAnObjectFromManyToManyRelationship<TProperty>(TEntity instance, long id,
            string relationName)
            where TProperty : class, IEntity
        {
            using (var db = _dataContextFactory.GetContext())
            {
                db.MarkAsModified(instance);
                var value = instance.GetType().GetProperty(relationName).GetValue(instance);
                if (value != null)
                {
                    if (value is HashSet<TProperty>)
                    {
                        var list = (HashSet<TProperty>)value;
                        list.Remove(list.First(t => t.Id == id));
                    }
                    else if (value is Collection<TProperty>)
                    {
                        var list = (Collection<TProperty>)value;
                        list.Remove(list.First(t => t.Id == id));
                    }
                    return await db.SaveChangesAsync();
                }
            }
            return 0;
        }
        public async Task<int> RemoveAnObjectFromManyToManyRelationship<TProperty>(TEntity instance, long[] ids,
            string relationName)
            where TProperty : class, IEntity
        {
            using (var db = _dataContextFactory.GetContext())
            {
                db.MarkAsModified(instance);
                var value = instance.GetType().GetProperty(relationName).GetValue(instance);
                if (value != null)
                {
                    if (value is HashSet<TProperty>)
                    {
                        var list = (HashSet<TProperty>)value;
                        list.RemoveWhere(t => ids.Contains(t.Id));
                    }
                    else if (value is Collection<TProperty>)
                    {
                        // bayd eslah beshe 
                        var list = (Collection<TProperty>)value;
                        list.Remove(list.First(t => ids.Contains(t.Id)));
                    }
                    return await db.SaveChangesAsync();
                }
            }
            return 0;
        }
        public async Task<int> UpdateManyToManyRelationship<TProperty>(TEntity instance, IList<TProperty> newValues,
            string relationName)
            where TProperty : class, IEntity
        {
            using (var db = _dataContextFactory.GetContext())
            {
                var secondarySet = db.CreateSet<TProperty>();
                foreach (var entity in newValues)
                {
                    if (db.Entry(entity).State == EntityState.Detached) secondarySet.Attach(entity);
                }
                db.MarkAsModified(instance);
                instance.GetType().GetProperty(relationName).SetValue(instance, newValues);
                return await db.SaveChangesAsync();
            }
        }

        #endregion
    }
}