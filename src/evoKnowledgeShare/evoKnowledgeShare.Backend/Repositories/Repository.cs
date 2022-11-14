using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Interfaces;

namespace evoKnowledgeShare.Backend.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EvoKnowledgeDbContext myDbContext;

        internal Repository(EvoKnowledgeDbContext dbContext)
        {
            myDbContext = dbContext;
        }

        #region Add section

        public abstract Task<T> AddAsync(T entity);

        public abstract Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        #endregion Add section

        #region Get section

        public abstract IEnumerable<T> GetAll();

        public abstract T GetById(Guid id);

        public abstract IEnumerable<T> GetRangeById(IEnumerable<Guid> ids);

        #endregion Get section

        #region Remove section

        public abstract void Remove(T entity);

        public abstract void RemoveById(Guid id);

        public abstract void RemoveRange(IEnumerable<T> entities);

        public abstract void RemoveRangeById(IEnumerable<Guid> ids);

        #endregion Remove section

        #region Update section

        public abstract T Update(T entity);

        public abstract IEnumerable<T> UpdateRange(IEnumerable<T> entities);

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
