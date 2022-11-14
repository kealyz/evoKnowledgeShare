using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class TopicRepository : Repository<Topic>
    {
        public TopicRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task AddAsync(Topic entity)
        {
            await myDbContext.Topics.AddAsync(entity);
        }

        public override async Task AddRangeAsync(IEnumerable<Topic> entities)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Topic> GetAll()
        {
            return myDbContext.Topics;
        }

        public override Topic GetById(Guid id)
        {
            return myDbContext.Topics.FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Topic> GetRangeById(IEnumerable<Guid> ids)
        {
            return myDbContext.Topics.Where(x => ids.Any(y => x.Id == y));
        }

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

        public override Topic Update(Topic topic_in)
        {
            Topic topic = myDbContext.Topics.Update(topic_in).Entity;
            myDbContext.SaveChanges();
            return topic;
        }

        public override IEnumerable<Topic> UpdateRange(IEnumerable<Topic> entities)
        {
            foreach (var entity in entities)
            {
                myDbContext.Update(entity);
            }
        }
    }
}