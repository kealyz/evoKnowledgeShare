using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class HistoryRepository : Repository<History>
    {
        public HistoryRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        {

        }

        #region Add Section
        public override async Task<History> AddAsync(History entity)
        {
            try
            {
                await myDbContext.Histories.AddAsync(entity);
                myDbContext.SaveChanges();
                return await Task.FromResult(entity);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public override async Task<IEnumerable<History>> AddRangeAsync(IEnumerable<History> entities)
        {
            try
            {
                List<History> histories = new();
                foreach (History entity in entities)
                {
                    histories.Add(await AddAsync(entity));
                }
                myDbContext.SaveChanges();
                return histories;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
        #endregion

        #region Get Section
        public override IEnumerable<History> GetAll()
        {
            IEnumerable<History> history = myDbContext.Histories;
            if (!history.Any())
            {
                return Enumerable.Empty<History>();
            }
            return history;
        }

        public override History GetById(Guid id)
        {
            try {
                History? history = myDbContext.Histories.FirstOrDefault(x => x.Id == id);
                return history;
            } catch (Exception) {
                throw new KeyNotFoundException();
            }
            
        }

        public override IEnumerable<History> GetRangeById(IEnumerable<Guid> ids)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region Remove Section
        public override void Remove(History entity)
        {
            throw new NotSupportedException();
        }

        public override void RemoveById(Guid id)
        {
            throw new NotSupportedException();
        }

        public override void RemoveRange(IEnumerable<History> entities)
        {
            throw new NotSupportedException();
        }

        public override void RemoveRangeById(IEnumerable<Guid> ids)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region Update Section
        public override History Update(History entity)
        {
            throw new NotSupportedException();
        }

        public override IEnumerable<History> UpdateRange(IEnumerable<History> entities)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
