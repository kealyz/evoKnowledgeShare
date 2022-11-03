namespace evoKnowledgeShare.Backend.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        Task AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        T GetById(int id);
        IEnumerable<T> GetRangeById(IEnumerable<int> ids);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        void Remove(T entity);
        Task RemoveAsync(T entity);
        void RemoveById(int id);
        void RemoveRange(IEnumerable<T> entities);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        void RemoveRangeById(IEnumerable<int> ids);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void UpdateRange(IEnumerable<T> entitites);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
