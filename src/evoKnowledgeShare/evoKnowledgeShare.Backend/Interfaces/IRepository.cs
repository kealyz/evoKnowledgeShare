namespace evoKnowledgeShare.Backend.Interfaces
{
    public interface IRepository<TEntity>
    {
        #region Add section

        /// <param name="entity"></param>
        /// <returns><typeparamref name="TEntity"/> if added</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <param name="entities"></param>
        /// <returns>A list of <typeparamref name="TEntity"/> if added</returns>
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        #endregion Add section

        #region Get section

        
        /// <param name="id"></param>
        /// <returns><typeparamref name="TEntity"/> If found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        TEntity GetById(Guid id);

        /// <param name="ids"></param>
        /// <returns>A list of <typeparamref name="TEntity"/> if found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        IEnumerable<TEntity> GetRangeById(IEnumerable<Guid> ids);

        /// <returns>All <typeparamref name="TEntity"/></returns>
        ///<exception cref="KeyNotFoundException"></exception>
        IEnumerable<TEntity> GetAll();

        #endregion Get section

        #region Remove section

        /// <param name="entity"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        void Remove(TEntity entity);

        /// <param name="id"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        void RemoveById(Guid id);

        /// <param name="entities"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <param name="ids"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        void RemoveRangeById(IEnumerable<Guid> ids);

        #endregion Remove section

        #region Update section


        /// <param name="entity"></param>
        /// <returns><typeparamref name="TEntity"/> if modified</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        TEntity Update(TEntity entity);

        /// <param name="entity"></param>
        /// <returns><typeparamref name="TEntity"/> if modified</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);

        #endregion Update section

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
