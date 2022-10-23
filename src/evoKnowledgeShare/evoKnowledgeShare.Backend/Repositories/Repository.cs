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

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public T GetByCreationTime(DateTimeOffset date)
        {
            throw new NotImplementedException();
        }

        public T GetByDescription(string description)
        {
            throw new NotImplementedException();
        }

        public T GetByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public T GetByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public T GetByTopicId(int id)
        {
            throw new NotImplementedException();
        }

        public T GetByUserId(string id)
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

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<T> entities)
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

        public void UpdateRange(IEnumerable<T> entitites)
        {
            throw new NotImplementedException();
        }
    }
}
