using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Interfaces;

namespace evoKnowledgeShare.Backend.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EvoKnowledgeDbContext myDbContext;

        public Repository(EvoKnowledgeDbContext dbContext)
        {
            myDbContext = dbContext;
        }

        public abstract void Add(T entity);

        public abstract Task AddAsync(T entity);

        public abstract void AddRange(IEnumerable<T> entities);

        public abstract IEnumerable<T> GetAll();

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract T GetById(int id);

        public abstract IEnumerable<T> GetRangeById(IEnumerable<int> ids);

        public abstract void Remove(T entity);

        public abstract void RemoveById(int id);

        public abstract void RemoveRange(IEnumerable<T> entities);

        public abstract void RemoveRangeById(IEnumerable<int> ids);

        public void SaveChanges()
        {
            myDbContext.SaveChanges();
        }

        public abstract Task SaveChangesAsync(CancellationToken cancellationToken = default);

        public abstract void Update(T entity);

        public abstract void UpdateRange(IEnumerable<T> entitites);
    }
}
