using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Diagnostics;
using LP.Api.Shared.Interfaces.Data;
using LP.Data.Context;

namespace LP.Data.Commands
{
    public class BaseCommands : IBaseCommands
    {
        private readonly LearningPlatformCodeFirstContext _askDatabase;

        public BaseCommands()
        {
            _askDatabase = new LearningPlatformCodeFirstContext();// askDatabase;
        }

        #region Generic Commands

        public T GetById<T>(int id)
            where T : class
        {
            return _askDatabase.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync<T>(int id)
            where T : class
        {
            return await _askDatabase.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithIncludesAsync<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            var queriable = _askDatabase.Set<T>().AsQueryable();
            return await includes.Aggregate(
                queriable,
                (current, include) => current.Include(include)).Where(filters).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAll<T>()
            where T : class
        {
            return _askDatabase.Set<T>();
        }

        public async Task<IQueryable<T>> GetAllAsync<T>()
            where T : class
        {
            return await Task.FromResult(_askDatabase.Set<T>());
        }

        public void Add<T>(T entity)
            where T : class
        {
            _askDatabase.Set<T>().Add(entity);
            _askDatabase.Entry(entity).State = EntityState.Added;
        }

        public void Update<T1, T2>(T1 existingEntity, T2 updatedEntity)
            where T1 : class
            where T2 : class
        {
            _askDatabase.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            _askDatabase.Entry(existingEntity).State = EntityState.Modified;

   
        }

        public void Update<T1>(T1 entity) where T1 : class
        {

            _askDatabase.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity)
            where T : class
        {
            _askDatabase.Set<T>().Remove(entity);
            _askDatabase.Entry(entity).State = EntityState.Deleted;
        }

        public bool Exists<T>(Func<T, bool> deleg)
            where T : class
        {
            return _askDatabase.Set<T>().Any(deleg);
        }

        public IQueryable<T> GetConditional<T>(Expression<Func<T, bool>> filters)
            where T : class
        {
            return _askDatabase.Set<T>().Where(filters).AsQueryable();
        }


        public IQueryable<T> GetConditionalWithIncludes<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            var queriable = _askDatabase.Set<T>().AsQueryable();
            return includes.Aggregate(
                queriable,
                (current, include) => current.Include(include))
                .Where(filters);
        }

        public async Task<IQueryable<T>> GetWithIncludesAsync<T>(params Expression<Func<T, object>>[] includes)
            where T : class
        {
            var queriable = _askDatabase.Set<T>().AsQueryable();
            return await Task.FromResult(includes.Aggregate(
                queriable,
                (current, include) => current.Include(include)));
        }

        public async Task<IQueryable<T>> GetConditionalAsync<T>(Expression<Func<T, bool>> filters)
            where T : class
        {
            return await Task.FromResult(_askDatabase.Set<T>().Where(filters).AsQueryable());
        }

        public async Task<IQueryable<T>> GetConditionalWithIncludesAsync<T>(Expression<Func<T, bool>> filters, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            var queriable = await Task.FromResult(_askDatabase.Set<T>().AsQueryable());
            return await  Task.FromResult(includes.Aggregate(
                queriable,
                (current, include) => current.Include(include))
                .Where(filters));
        }

        public void SaveChanges()
        {
            try
            {
                _askDatabase.SaveChanges();
            }
            catch (DbEntityValidationException dbex)
            {
                LogDbEntityValidationException(dbex);
                throw;
            }
            catch (DbUpdateException upex)
            {
                LogDbUpdateException(upex);
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _askDatabase.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbex)
            {
                LogDbEntityValidationException(dbex);
                throw;
            }
            catch (DbUpdateException upex)
            {
                LogDbUpdateException(upex);
                throw;
            }
        }

        private static void LogDbEntityValidationException(DbEntityValidationException dbEntityValidationException)
        {
            foreach (var eve in dbEntityValidationException.EntityValidationErrors)
            {
                Debug.WriteLine(@"Entity of type {0} in state {1} has the following validation errors:",
                    eve.Entry.Entity.GetType().Name,
                    eve.Entry.State);
                
                foreach (var ve in eve.ValidationErrors)
                {
                    Debug.WriteLine("- Property: {0}, Error: {1}",
                        ve.PropertyName,
                        ve.ErrorMessage);
                }
            }
        }

        private static void LogDbUpdateException(DbUpdateException dbUpdateException)
        {
            foreach (var eve in dbUpdateException.Entries)
            {
                Debug.WriteLine(@"Entity of type {0} in state {1} has the following validation errors:",
                    eve.Entity.GetType().Name,
                    eve.State);
            }
        }

   
        public IEnumerable<TResponse> ExecuteStoredProcedure<TResponse, TProcedure>(TProcedure procedure) where TProcedure : class
        {
            var procedureType = procedure.GetType();
            var procedureName = GetProcedureName(procedureType.Name);
            var procedureParameters = procedureType.GetProperties();
            var parameters = new List<SqlParameter>();
            var inputParameters = new List<string>();

            foreach (var property in procedureParameters)
            {
                inputParameters.Add("@" + property.Name);

                var parameterValue = property.GetValue(procedure) ?? DBNull.Value;

                var param = new SqlParameter { ParameterName = "@" + property.Name, Value = parameterValue };

                parameters.Add(param);
            }

            var executeSqlCommand = string.Format("Exec {0} {1}", procedureName, string.Join(",", inputParameters));

            return _askDatabase.Database.SqlQuery<TResponse>(executeSqlCommand, parameters.ToArray()).ToList();
        }

        private static string GetProcedureName(string procedureName)
        {
            const string arguments = "Arguments";

            var dboProcedureName = procedureName;

            if (procedureName.EndsWith(arguments)) dboProcedureName = procedureName.Substring(0, procedureName.IndexOf(arguments));

            return string.Format("[dbo].[{0}]", dboProcedureName);
        }

        #endregion Generic Commands



    }
}
