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

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public abstract IEnumerable<T> GetAll();

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract T? GetById(int id);

        public abstract IEnumerable<T> GetRangeById(IEnumerable<int> ids);

        public abstract void Remove(T entity);

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public abstract void RemoveById(int id);

        public abstract void RemoveRange(IEnumerable<T> entities);

        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public abstract void RemoveRangeById(IEnumerable<int> ids);

        public void SaveChanges()
        {
            myDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await myDbContext.SaveChangesAsync(cancellationToken);
        }

        public abstract void Update(T entity);

        public abstract void UpdateRange(IEnumerable<T> entities);
    }
}
