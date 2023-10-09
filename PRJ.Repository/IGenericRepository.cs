using System.Linq.Expressions;

namespace PRJ.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        #region Get

        /// <summary>
        ///     The get all.
        /// </summary>
        /// <returns>
        ///     The <see cref="IQueryable" />.
        /// </returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="cacheTime">
        /// The cache time.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> GetAll(TimeSpan cacheTime);

        /// <summary>
        ///     The get all async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<List<T>> GetAllAsync();
        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T GetById(object id);
        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<T> GetByIdAsync(object id);
        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T GetEncryptById(string id);
        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<T> GetEncryptByIdAsync(string? id);
        /// <summary>
        /// The get by query.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
		IQueryable<T> GetByQuery(Expression<Func<T, bool>> filter);
        /// <summary>
        ///     The count.
        /// </summary>
        /// <returns>
        ///     The <see cref="long" />.
        /// </returns>
        long Count();

        /// <summary>
        /// The count.
        /// </summary>
        /// <param name="whereExpression">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        long Count(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        ///     The count async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<int> CountAsync();

        /// <summary>
        /// The count async.
        /// </summary>
        /// <param name="whereExpression">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<int> CountAsync(Expression<Func<T, bool>> whereExpression);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="query"></param>
        ///// <param name="parameters"></param>
        ///// <returns></returns>
        //IEnumerable<T> GetWithRawSql(string query, params object[] parameters);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="query"></param>
        ///// <param name="parameters"></param>
        ///// <returns></returns>
        Task<IEnumerable<T>> GetWithRawSqlAsync(string query, params object[] parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="orderBy"></param>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<T> GetWithPagination(int pageIndex, int pageSize, out int totalCount,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>>? filter = null,
          string includeProperties = "");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="orderBy"></param>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<List<T>> GetWithPaginationAsync(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
           Expression<Func<T, bool>>? filter = null,
         string includeProperties = "");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="orderBy"></param>
        /// <param name="filters"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<T> GetWithPaginationDynmicFilter(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
          IEnumerable<Expression<Func<T, bool>>>? filters = null,
         string includeProperties = "");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="orderBy"></param>
        /// <param name="anyFilters"></param>
        /// <param name="filters"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IEnumerable<T> GetWithPaginationDynmicFilter(int pageIndex, int pageSize, out int totalCount,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, IEnumerable<Expression<Func<T, bool>>> anyFilters,
          IEnumerable<Expression<Func<T, bool>>>? filters = null,
         string includeProperties = "");
        #endregion

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
        void Add(T entity);

        /// <summary>
        /// The add async.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> AddAsync(T t);
        #endregion

        #region Update
        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        void Update(T t);
        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="t">
        /// The t.
        /// </param>
        void Update(object id, T t);
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
        bool Delete(T entity);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> DeleteAsync(T t);

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool DeleteById(int id);
        void DeleteByParentId(Expression<Func<T, bool>> filter);
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
        bool Exists(T entity);
        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Exists(object id);
        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> ExistsByQueryAsync(Expression<Func<T, bool>> filter);
        Task<bool> ExistsAsync(T entity);
        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> ExistsAsync(object id);
        #endregion

    }
}