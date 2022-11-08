namespace evoKnowledgeShare.Backend.Interfaces
{
    public interface IRepository<T>
    {
        #region Add section

        void Add(T entity);
        
        Task AddAsync(T entity);
        
        void AddRange(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);

        #endregion Add section

        #region Get section

        T? GetById(int id);

        IEnumerable<T> GetRangeById(IEnumerable<int> ids);

        IEnumerable<T> GetAll();
        
        #endregion Get section

        #region Remove section

        void Remove(T entity);
        
        void RemoveById(int id);

        void RemoveRange(IEnumerable<T> entities);
        
        void RemoveRangeById(IEnumerable<int> ids);

        #endregion Remove section

        #region Update section

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        #endregion Update section

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
