namespace evoKnowledgeShare.Backend.Interfaces
{
    public interface IRepository<T>
    {
        #region Add section
        
        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        #endregion Add section

        #region Get section

        T GetById(Guid id);

        IEnumerable<T> GetRangeById(IEnumerable<Guid> ids);

        IEnumerable<T> GetAll();

        #endregion Get section

        #region Remove section

        void Remove(T entity);

        void RemoveById(Guid id);

        void RemoveRange(IEnumerable<T> entities);

        void RemoveRangeById(IEnumerable<Guid> ids);

        #endregion Remove section

        #region Update section

        T Update(T entity);

        IEnumerable<T> UpdateRange(IEnumerable<T> entities);

        #endregion Update section

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
