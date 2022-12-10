namespace evoKnowledgeShare.Backend.Interfaces
{
    public interface IRepository<TEntity>
    {
        #region Add section

        ///<summary>It adds a <typeparamref name="TEntity"/> to database (from param)</summary>
        /// <param name="entity"></param>
        /// <returns><typeparamref name="TEntity"/> if added</returns>
        /// <exception cref="ArgumentException"></exception>
        Task<TEntity> AddAsync(TEntity entity);

        ///<summary>Adds a list of <typeparamref name="TEntity"/> to database (from param)</summary>
        /// <param name="entities"></param>
        /// <returns>A list of <typeparamref name="TEntity"/> if added</returns>
        /// <exception cref="ArgumentException"></exception>
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        #endregion Add section

        #region Get section


        ///<summary>Return one <typeparamref name="TEntity"/> type data by it's GUID (from param)</summary>
        /// <param name="id"></param>
        /// <returns><typeparamref name="TEntity"/> If found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        TEntity GetById(Guid id);

        ///<summary>Return all <typeparamref name="TEntity"/> type datas by their GUID (from param)</summary>
        /// <param name="ids"></param>
        /// <returns>A list of <typeparamref name="TEntity"/> if found or an empty list if not found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        IEnumerable<TEntity> GetRangeById(IEnumerable<Guid> ids);

        ///<summary>Return all <typeparamref name="TEntity"/> type datas from the database.</summary>
        /// <returns>All <typeparamref name="TEntity"/> or an empty list</returns>
        IEnumerable<TEntity> GetAll();

        #endregion Get section

        #region Remove section

        ///<summary>Removes<typeparamref name="TEntity"/> (from param) from database.</summary>
        /// <param name="entity"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        void Remove(TEntity entity);

        ///<summary>Removes<typeparamref name="TEntity"/> from database by it's GUID (from param)</summary>
        /// <param name="id"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        void RemoveById(Guid id);

        ///<summary>Removes a list of<typeparamref name="TEntity"/> (from param) from database.</summary>
        /// <param name="entities"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        void RemoveRange(IEnumerable<TEntity> entities);

        ///<summary>Removes a list of<typeparamref name="TEntity"/> from database by their GUID (from param)</summary>
        /// <param name="ids"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        void RemoveRangeById(IEnumerable<Guid> ids);

        #endregion Remove section

        #region Update section

        ///<summary>Updates<typeparamref name="TEntity"/> (from param).</summary>
        /// <param name="entity"></param>
        /// <returns><typeparamref name="TEntity"/> if modified</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        TEntity Update(TEntity entity);

        ///<summary>Updates a list of<typeparamref name="TEntity"/> (from param).</summary>
        /// <param name="entity"></param>
        /// <returns><typeparamref name="TEntity"/> if modified</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);

        #endregion Update section

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
