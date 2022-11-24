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
            entity.Id = Guid.NewGuid();
            await myDbContext.Histories.AddAsync(entity);
            await myDbContext.SaveChangesAsync();
            return myDbContext.Histories.FirstOrDefault(x => x.Id == entity.Id);
        }

        public override Task<IEnumerable<History>> AddRangeAsync(IEnumerable<History> entities)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Get Section
        public override IEnumerable<History> GetAll()
        {
            return myDbContext.Histories;
        }

        public override History GetById(Guid id)
        {
            return myDbContext.Histories.FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<History> GetRangeById(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Remove Section
        public override void Remove(History entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoveById(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRange(IEnumerable<History> entities)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRangeById(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update Section
        public override History Update(History entity)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<History> UpdateRange(IEnumerable<History> entities)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
