using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.GlobalComponents.Encryption;
using System.Linq.Expressions;


namespace PRJ.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RepositoryContext _db;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(
           RepositoryContext db,
           ILogger logger
       )
        {
            _db = db;
            _logger = logger;
            this.dbSet = db.Set<T>();
        }


        #region Get
        /// <summary>
        ///     The get all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        public virtual IQueryable<T> GetAll()
        {
            return this.dbSet.Select(e => e);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="cacheTime">
        /// The cache time.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public virtual IEnumerable<T> GetAll(TimeSpan cacheTime)
        {
            return this.dbSet.Select(e => e); //.FromCache(CachePolicy.WithDurationExpiration(cacheTime));
        }

        /// <summary>
        ///     The get all async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public virtual Task<List<T>> GetAllAsync()
        {
            return this.dbSet.Select(e => e).ToListAsync();
        }
        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T? GetById(object id)
        {
            var _get = this.dbSet.Find(id);

            if (_get != null)
                return _get;

            return null;
        }
        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<T?> GetByIdAsync(object id)
        {
            var _get = await this.dbSet.FindAsync(id);
            if (_get != null)
            {
                return _get;
            }

            return null;
        }
        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T? GetEncryptById(string id)
        {
            var _get = this.dbSet.Find(int.Parse(EncryptQueryURL.Decrypt(id.Replace(" ", "+"))));

            if (_get != null)
                return _get;

            return null;
        }
        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<T?> GetEncryptByIdAsync(string? id)
        {
            if (id is not null)
            {
                var _get = await this.dbSet.FindAsync(int.Parse(EncryptQueryURL.Decrypt(id.Replace(" ", "+"))));
                if (_get != null)
                {
                    return _get;
                }

            }

            return null;
        }
        /// <summary>
        /// The get by query.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public virtual IQueryable<T> GetByQuery(Expression<Func<T, bool>> filter)
        {
            return this.dbSet.Where(filter);
        }
        /// <summary>
        ///     The count.
        /// </summary>
        /// <returns>
        ///     The <see cref="long" />.
        /// </returns>
        public virtual long Count()
        {
            return this.dbSet.Count();
        }
        /// <summary>
        /// The count.
        /// </summary>
        /// <param name="whereExpression">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public virtual long Count(Expression<Func<T, bool>> whereExpression)
        {
            return this.dbSet.Count(whereExpression);
        }
        /// <summary>
        ///     The count async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public virtual async Task<int> CountAsync()
        {
            return await this._db.Set<T>().CountAsync();
        }
        /// <summary>
        /// The count async.
        /// </summary>
        /// <param name="whereExpression">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await this._db.Set<T>().CountAsync(whereExpression);
        }
        /// <summary>
        /// Used to execute raw sql queries to used DbSet 
        /// </summary>
        /// <param name="query">Query to be executed</param>
        /// <param name="parameters">Parameters to be passed to query</param>
        /// <returns></returns>
        //public virtual IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        //{
        //    return dbSet.FromSqlRaw(query, parameters).ToList();
        //}
        public virtual async Task<IEnumerable<T>> GetWithRawSqlAsync(string query, params object[] parameters)
        {
            return await dbSet.FromSqlRaw(query, parameters).ToListAsync();
        }
        /// <summary>
        /// Get paged result with option include, filter and order 
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total Count</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="filter">filter</param>
        /// <param name="includeProperties">Included Properties</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetWithPagination(int pageIndex, int pageSize, out int totalCount,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>>? filter = null,
          string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            totalCount = query.Count();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query).Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                throw new Exception();
            }

            return query.ToList();
        }

        /// <summary>
        /// Get paged reslut with option include, filter and order async
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total Count</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="filter">filter</param>
        /// <param name="includeProperties">Included Properties</param>
        /// <returns></returns>
        public virtual Task<List<T>> GetWithPaginationAsync(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
           Expression<Func<T, bool>>? filter = null,
         string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            totalCount = query.Count();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);

            }

            if (orderBy != null)
            {
                query = orderBy(query).Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                throw new Exception();
            }

            return query.ToListAsync();
        }

        /// <summary>
        /// Get items with paging and dynamic filters
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total Count</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="filters">Filters</param>
        /// <param name="includeProperties">included Properties</param>
        /// <returns>List of items</returns>
        public virtual IEnumerable<T> GetWithPaginationDynmicFilter(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
          IEnumerable<Expression<Func<T, bool>>>? filters = null,
         string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            totalCount = query.Count();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);

            }

            if (orderBy != null)
            {
                query = orderBy(query).Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                throw new Exception();
            }

            return query.ToList();
        }

        /// <summary>
        /// Get items with paging and dynamic filters
        /// </summary>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total Count</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="anyFilters">Any Filters</param>
        /// <param name="filters">Filters</param>
        /// <param name="includeProperties">Included Properties</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetWithPaginationDynmicFilter(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, IEnumerable<Expression<Func<T, bool>>> anyFilters,
          IEnumerable<Expression<Func<T, bool>>>? filters = null,
         string includeProperties = "")
        {
            IQueryable<T> query = _db.Set<T>();

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            if (anyFilters != null && anyFilters.Count() > 0)
            {
                query = query.Where(anyFilters.FirstOrDefault());
                foreach (var anyfilter in anyFilters)
                {
                    query = query.Union(query.Where(anyfilter));
                }
            }

            totalCount = query.Count();
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);

            }

            if (orderBy != null)
            {
                query = orderBy(query).Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                throw new Exception();
            }

            return query.ToList();
        }
        #endregion

        //    public static IOrderedQueryable<T> AddOrdering<T, TKey>(
        //this IQueryable<T> source,
        //Expression<Func<T, TKey>> keySelector,
        //bool descending)
        //    {
        //        IOrderedQueryable<T> ordered = source as IOrderedQueryable<T>;
        //        // If it's not ordered yet, use OrderBy/OrderByDescending.
        //        if (ordered == null)
        //        {
        //            return descending ? source.OrderByDescending(keySelector)
        //                              : source.OrderBy(keySelector);
        //        }
        //        // Already ordered, so use ThenBy/ThenByDescending
        //        return descending ? ordered.ThenByDescending(keySelector)
        //                          : ordered.ThenBy(keySelector);
        //    }

        #region Add
        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }
        /// <summary>
        /// The add async.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<bool> AddAsync(T t)
        {

            this._db.Set<T>().Add(t);
            if (await this._db.SaveChangesAsync() > 0) return true;

            return false;
        }
        #endregion

        #region Update
        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        public virtual void Update(T t)
        {
            EntityEntry dbEntityEntry = this._db.Entry(t);
            this._db.Set<T>().Attach(t);
            dbEntityEntry.State = EntityState.Modified;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="t">
        /// The t.
        /// </param>
        public virtual void Update(object id, T t)
        {
            var obj = this.GetById(id);
            this._db.Entry(obj).CurrentValues.SetValues(t);
        }
        #endregion

        #region Delete
        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Delete(T entity)
        {
            var deletedEntity = this.dbSet.Remove(entity);

            return this._db.Entry(deletedEntity).State == EntityState.Deleted;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            this.dbSet.RemoveRange(entities);
        }
        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<bool> DeleteAsync(T t)
        {
            this._db.Entry(t).State = EntityState.Deleted;
            return await this.SaveAsync();
        }

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool DeleteById(int id)
        {
            var entity = this.GetById(id);

            return this.Delete(entity);
        }

        public virtual void DeleteByParentId(Expression<Func<T, bool>> filter)
        {
            foreach (var item in this.dbSet.Where(filter))
                this.dbSet.Remove(item);
        }


        //public virtual void DeleteByIds(List<int> idsToDelete)
        //{
        //    //List<int> idsToDelete = new List<int> { 2, 4 };

        //    //list.RemoveAll(o => idsToDelete.Contains(o.Id));
        //    var entitiesToDelete = this.dbSet.Where(e => idsToDelete.Contains(e.));

        //    dbContext.RemoveRange(entitiesToDelete);
        //    this.dbSet.RemoveRange(o => idsToDelete.Contains(o.Id));
        //    foreach (var item in this.dbSet.Where(filter))
        //        this.dbSet.Remove(item);
        //}

        #endregion

        #region Exist
        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Exists(T entity)
        {
            return this.dbSet.Any(e => e == entity);
        }

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Exists(object id)
        {
            return this.dbSet.Find(id) != null;
        }

        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<bool> ExistsAsync(T entity)
        {
            return await this.dbSet.AnyAsync(e => e == entity);
        }
        public virtual async Task<bool> ExistsByQueryAsync(Expression<Func<T, bool>> filter)
        {
            return await this.dbSet.AnyAsync(filter);
        }

        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<bool> ExistsAsync(object id)
        {
            return await this.dbSet.FindAsync(id) != null;
        }
        #endregion

        #region Save / Insert Or Update
        /// <summary>
        ///     The save async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public virtual async Task<bool> SaveAsync()
        {
            return await this._db.SaveChangesAsync() > 0;
        }
        public virtual void InsertOrUpdate(T t, Expression<Func<T, bool>> predicate)
        {
            var exists = this.dbSet.Where(predicate).Any();
            if (exists)
            {
                this.Update(t);
            }
            else
            {
                this.Add(t);
            }
        }
        #endregion

    }
}


