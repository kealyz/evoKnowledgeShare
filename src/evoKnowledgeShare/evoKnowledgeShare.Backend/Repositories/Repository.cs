using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Interfaces;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EvoKnowledgeDbContext myDbContext;

        public Repository(EvoKnowledgeDbContext dbContext)
        {
            myDbContext = dbContext;
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetRangeById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void RemoveRangeById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<T> entitites)
        {
            throw new NotImplementedException();
        }

        Task IRepository<T>.RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        Task IRepository<T>.UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
