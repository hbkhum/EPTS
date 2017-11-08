using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core
{
    public abstract class GenericRepositoryEntities<T> : IGenericRepositoryEntities<T> where T : class
    {
        private readonly DbContext _entities;
        protected IDbSet<T> Dbset;

        /// <summary>
        /// This a Contructor Generic Repository
        /// </summary>
        /// <param name="entities">Is a element from database</param>
        protected GenericRepositoryEntities(DbContext entities)
        {
            _entities = entities;
            Dbset = entities.Set<T>();
            //
        }

        public async Task<IEnumerable<T>> GetAllAsync(string whereValue,  string orderBy)
        {
            if (whereValue == "") whereValue = "true";
            if (orderBy == "") orderBy = "true";
            var task = Task.Run(() =>
            {
                var result = Dbset
                    .Where(whereValue)
                    .OrderBy(orderBy);
                    //.Skip((pageIndex - 1) * pageSize)
                    //.Take(pageSize);
                return result;
            });
            return (IEnumerable<T>) await task;
        }
        /// <summary>
        /// Get all elements from database type T
        /// </summary>
        /// <returns>Return a object type T</returns>
        //public Task<IEnumerable<T>> GetAll()
        //{
        //    throw new NotImplementedException();
        //}





        /// <summary>
        /// Custom search using expression Lambda
        /// </summary>
        /// <param name="predicate">Expretion type Lambda</param>
        /// <returns>Return a List type T</returns>
        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            var task = Task.Run(() => Dbset.Where(predicate).ToList());
            return await task;

        }

        /// <summary>
        /// Add to element to database
        /// </summary>
        /// <param name="entity">Is a class type T</param>
        /// <returns>Return element to add</returns>
        public   void Add(T entity)
        {
            try
            {
                Dbset.Add(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Edit element of database
        /// </summary>
        /// <param name="entity">Is a class type T</param>
        public void Edit(T entity)
        {
            try
            {
                _entities.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete element of database
        /// </summary>
        /// <param name="entity">Is a class type T</param>
        /// <returns>Return a object type T</returns>
        public void Delete(T entity)
        {
            try
            {
                Dbset.Remove(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Save element of database
        /// </summary>
        public void Save()
        {
            try
            {
                _entities.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Call stored procedure from database
        /// </summary>
        /// <param name="storedProcedureName">stored Procedure Name</param>
        /// <param name="parameters">parameters</param>
        /// <returns>Return a List type T</returns>
        public IEnumerable<T> StoredProcedure(string storedProcedureName, params object[] parameters)
        {
            return _entities.Database.SqlQuery<T>(storedProcedureName, parameters).AsEnumerable();
        }
        /// <summary>
        /// Call stored procedure from database
        /// </summary>
        /// <param name="storedProcedureName">stored Procedure Name</param>
        /// <returns>Return a List type T</returns>
        public IEnumerable<T> StoredProcedure(string storedProcedureName)
        {
            return _entities.Database.SqlQuery<T>(storedProcedureName);
        }
    }

}
