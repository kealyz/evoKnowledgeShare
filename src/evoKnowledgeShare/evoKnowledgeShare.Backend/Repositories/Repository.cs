using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Interfaces;

namespace evoKnowledgeShare.Backend.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EvoKnowledgeDbContext myDbContext;

        protected Repository(EvoKnowledgeDbContext dbContext)
        {
            myDbContext = dbContext;
        }

        public abstract void Add(T entity);
        public abstract Task AddAsync(T entity);
        public abstract void AddRange(IEnumerable<T> entities);
        public abstract Task AddRangeAsync(IEnumerable<T> entities);
        public abstract IEnumerable<T> GetAll();
        public abstract Task<IEnumerable<T>> GetAllAsync();
        public abstract T? GetById(int id);
        public abstract Task<T?> GetByIdAsync(int id);
        public abstract IEnumerable<T> GetRangeById(IEnumerable<int> ids);
        public abstract Task<IEnumerable<T>> GetRangeByIdAsync(IEnumerable<int> ids);
        public abstract void Remove(T entity);
        public abstract Task RemoveAsync(T entity);
        public abstract void RemoveById(int id);
        public abstract Task RemoveByIdAsync(int id);
        public abstract void RemoveRange(IEnumerable<T> entities);
        public abstract Task RemoveRangeAsync(IEnumerable<T> entities);
        public abstract void RemoveRangeById(IEnumerable<int> ids);
        public abstract Task RemoveRangeByIdAsync(IEnumerable<int> ids);
        public void SaveChanges()
        {
            myDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await myDbContext.SaveChangesAsync(cancellationToken);
        }
        public abstract void Update(T entity);
        public abstract Task UpdateAsync(T entity);
        public abstract void UpdateRange(IEnumerable<T> entities);
        public abstract Task UpdateRangeAsync(IEnumerable<T> entitites);
    }
}
