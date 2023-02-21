using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Interfaces;

namespace evoKnowledgeShare.Backend.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly EvoKnowledgeDbContext myDbContext;

        internal Repository(EvoKnowledgeDbContext dbContext)
        {
            myDbContext = dbContext;
        }

        #region Add section

        /// <inheritdoc/>
        public abstract Task<TEntity> AddAsync(TEntity entity);

        /// <inheritdoc/>
        public abstract Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        #endregion Add section

        #region Get section

        /// <inheritdoc/>
        public abstract IEnumerable<TEntity> GetAll();

        
        /// <inheritdoc/>
        public abstract TEntity GetById(Guid id);

        /// <inheritdoc/>
        public abstract IEnumerable<TEntity> GetRangeById(IEnumerable<Guid> ids);

        #endregion Get section

        #region Remove section

        /// <inheritdoc/>
        public abstract void Remove(TEntity entity);

        /// <inheritdoc/>
        public abstract void RemoveById(Guid id);

        /// <inheritdoc/>
        public abstract void RemoveRange(IEnumerable<TEntity> entities);

        /// <inheritdoc/>
        public abstract void RemoveRangeById(IEnumerable<Guid> ids);

        #endregion Remove section

        #region Update section

        /// <inheritdoc/>
        public abstract TEntity Update(TEntity entity);

        /// <inheritdoc/>
        public abstract IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);

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
