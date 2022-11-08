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

        #region Add section

        public abstract void Add(T entity);

        public abstract Task AddAsync(T entity);

        public abstract void AddRange(IEnumerable<T> entities);

        public abstract Task AddRangeAsync(IEnumerable<T> entities);

        #endregion Add section

        #region Get section

        public abstract IEnumerable<T> GetAll();

        public abstract T? GetById(int id);

        public abstract IEnumerable<T> GetRangeById(IEnumerable<int> ids);

        #endregion Get section

        #region Remove section

        public abstract void Remove(T entity);

        public abstract void RemoveById(int id);

        public abstract void RemoveRange(IEnumerable<T> entities);

        public abstract void RemoveRangeById(IEnumerable<int> ids);

        #endregion Remove section

        #region Update section

        public abstract void Update(T entity);

        public abstract void UpdateRange(IEnumerable<T> entities);

        #endregion Update section

        public void SaveChanges()
        {
            myDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await myDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
