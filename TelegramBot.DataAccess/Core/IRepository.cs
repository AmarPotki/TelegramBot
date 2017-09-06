using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TelegramBot.Business.Domain.Core;
namespace TelegramBot.DataAccess.Core{
    public interface IRepository<TEntity> where TEntity : class, IEntity{
      
            #region Regular

            int Count(Expression<Func<TEntity, bool>> predicate = null);

            int NonQuery(string query, params object[] args);

            TEntity One(
                long id,
                params string[] includes);

            TEntity One(
                Expression<Func<TEntity, bool>> predicate,
                params string[] includes);

            TValue Query<TValue>(
                Func<IQueryable<TEntity>, TValue> query,
                params string[] includes);

            void Remove(TEntity instance);

            void Remove(long id);

            TEntity Save(TEntity instance);

            #endregion

            #region Async

            Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

            Task<List<TValue>> NonQueryAsync<TValue>(string query, params object[] args);
            Task<int> NonQExecuteueryAsync(string query, params object[] args);

            Task<TEntity> OneAsync(
                long id,
                params string[] includes);

            Task<TEntity> OneAsync(
                Expression<Func<TEntity, bool>> predicate,
                params string[] includes);

            Task<TValue> QueryAsync<TValue>(
                Func<IQueryable<TEntity>, Task<TValue>> query,
                params string[] includes);


            Task<int> RemoveAsync(TEntity instance);

            Task<int> RemoveAsync(long id);

            Task<int> SaveAsync(TEntity instance);
            Task<int> InsertManyToManyRelationshipAsync<TProperty>(TEntity instance, IEnumerable<TProperty> newValues) where TProperty : class, IEntity;
            Task<int> UpdateWithAttachAsync(TEntity instance, params string[] includes);
            Task<int> DeleteWithAttachAsync(TEntity instance);
            Task<int> RemoveAnObjectFromManyToManyRelationship<TProperty>(TEntity instance, long id, string relationName) where TProperty : class, IEntity;
            Task<int> RemoveAnObjectFromManyToManyRelationship<TProperty>(TEntity instance, long[] ids, string relationName)
                where TProperty : class, IEntity;
            Task<int> UpdateManyToManyRelationship<TProperty>(TEntity instance, IList<TProperty> newValues,
                string relationName) where TProperty : class, IEntity;

            #endregion

        }
    }