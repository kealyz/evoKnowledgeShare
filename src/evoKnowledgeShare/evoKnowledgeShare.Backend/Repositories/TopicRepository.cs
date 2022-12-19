using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class TopicRepository : Repository<Topic>
    {
        public TopicRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        {

        }

        #region Add Section
        public override async Task<Topic> AddAsync(Topic entity) {
            Guid newEntityGuid = Guid.NewGuid();
            entity.Id = newEntityGuid;
            await myDbContext.Topics.AddAsync(entity);
            return myDbContext.Topics.First(x => x.Id == newEntityGuid);
        }

        public override async Task<IEnumerable<Topic>> AddRangeAsync(IEnumerable<Topic> entities) {
            await myDbContext.Topics.AddRangeAsync(entities);
            return myDbContext.Topics.Where(topic => entities.Any(entity => entity == topic));
        }
        #endregion Add Section

        #region Get Section
        public override IEnumerable<Topic> GetAll() 
        {
            return myDbContext.Topics;
        }

        public override Topic GetById(Guid id) 
        {
            return myDbContext.Topics.First(x => x.Id == id);
        }

        public override IEnumerable<Topic> GetRangeById(IEnumerable<Guid> ids) 
        {
            return myDbContext.Topics.Where(x => ids.Any(y => x.Id == y));
        }
        #endregion Get Section

        #region Remove Section
        public override void Remove(Topic entity)
        {
            myDbContext.Topics.Remove(entity);
            myDbContext.SaveChanges();
        }

        public override void RemoveById(Guid id)
        {
            var topicToRemove = myDbContext.Topics.FirstOrDefault(x => x.Id == id);
            if (topicToRemove is not null)
            {
                myDbContext.Topics.Remove(topicToRemove);
            }
        }

        public override void RemoveRange(IEnumerable<Topic> entities)
        {
            foreach (var topic in entities)
            {
                myDbContext.Remove(topic);
            }
        }

        public override void RemoveRangeById(IEnumerable<Guid> ids)
        {
            foreach(var id in ids)
            {
                var topicToRemove = myDbContext.Topics.FirstOrDefault(x => x.Id == id);
                if (topicToRemove is not null)
                {
                    myDbContext.Remove(topicToRemove);
                }
            }
        }
        #endregion Remove Section

        #region Update Section
        public override Topic Update(Topic topic_in)
        {
            Topic topic = myDbContext.Topics.Update(topic_in).Entity;
            myDbContext.SaveChanges();
            return topic;
        }

        public override IEnumerable<Topic> UpdateRange(IEnumerable<Topic> entities)
        {
            myDbContext.Topics.UpdateRange(entities);
            return myDbContext.Topics.Where(topic => entities.Any(entity => entity == topic));
        }
        #endregion Update Section
    }
}