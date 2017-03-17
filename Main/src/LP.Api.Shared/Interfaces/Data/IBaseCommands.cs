using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LP.Api.Shared.Interfaces.Data
{
    public interface IBaseCommands
    {
        #region Generic Commands

        T GetById<T>(int id) where T : class;
        Task<T> GetByIdAsync<T>(int id) where T : class;
        Task<T> GetByIdWithIncludesAsync<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes)
            where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        Task<IQueryable<T>> GetAllAsync<T>() where T : class;
        void Add<T>(T entity) where T : class;

        void Update<T1, T2>(T1 existingEntity, T2 updatedEntity)
            where T1 : class
            where T2 : class;

        void Update<T1>(T1 entity) where T1 : class;

        void Delete<T>(T entity) where T : class;
        bool Exists<T>(Func<T, bool> deleg) where T : class;

        IQueryable<T> GetConditional<T>(Expression<Func<T, bool>> filters) where T : class;

        IQueryable<T> GetConditionalWithIncludes<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes)
            where T : class;

        Task<IQueryable<T>> GetWithIncludesAsync<T>(params Expression<Func<T, object>>[] includes) where T : class;

        Task<IQueryable<T>> GetConditionalAsync<T>(Expression<Func<T, bool>> filters) where T : class;

        Task<IQueryable<T>> GetConditionalWithIncludesAsync<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes) where T : class;
        void SaveChanges();
        Task SaveChangesAsync();

        IEnumerable<TResponse> ExecuteStoredProcedure<TResponse, TProcedure>(TProcedure procedure) where TProcedure : class;
        #endregion Generic Commands
    }
}
