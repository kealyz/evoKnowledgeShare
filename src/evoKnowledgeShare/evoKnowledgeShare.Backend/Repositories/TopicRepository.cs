using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class TopicRepository : Repository<Topic>
    {
        public TopicRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        {
        }

        public override void Add(Topic entity)
        {
            myDbContext.Topics.Add(entity);
        }

        public override async Task AddAsync(Topic entity)
        {
            await myDbContext.Topics.AddAsync(entity);
        }

        public override void AddRange(IEnumerable<Topic> entities)
        {
            foreach (var entity in entities)
            {
                myDbContext.Topics.Add(entity);
            }
        }

        public override Task AddRangeAsync(IEnumerable<Topic> entities)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Topic> GetAll()
        {
            return myDbContext.Topics;
        }

        public override Task<IEnumerable<Topic>> GetAllAsync()
        {
            IEnumerable<Topic> topics = myDbContext.Topics;
            return Task.FromResult(topics);
        }

        public override Topic? GetById(int id)
        {
            return myDbContext.Topics.FirstOrDefault(x => x.Id == id);
        }

        public override Task<Topic?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Topic> GetRangeById(IEnumerable<int> ids)
        {
            return myDbContext.Topics.Where(x => ids.Any(y => x.Id == y));
        }

        public override Task<IEnumerable<Topic>> GetRangeByIdAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Topic entity)
        {
            myDbContext.Topics.Remove(entity);
        }

        public override Task RemoveAsync(Topic entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoveById(int id)
        {
            var topicToRemove = myDbContext.Topics.FirstOrDefault(x => x.Id == id);
            if (topicToRemove is not null)
            {
                myDbContext.Topics.Remove(topicToRemove);
            }
        }

        public override Task RemoveByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRange(IEnumerable<Topic> entities)
        {
            foreach (var topic in entities)
            {
                myDbContext.Remove(topic);
            }
        }

        public override Task RemoveRangeAsync(IEnumerable<Topic> entities)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRangeById(IEnumerable<int> ids)
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

        public override Task RemoveRangeByIdAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public override void Update(Topic entity)
        {
            myDbContext.Update(entity);
        }

        public override Task UpdateAsync(Topic entity)
        {
            throw new NotImplementedException();
        }

        public override void UpdateRange(IEnumerable<Topic> entities)
        {
            foreach (var entity in entities)
            {
                myDbContext.Update(entity);
            }
        }

        public override Task UpdateRangeAsync(IEnumerable<Topic> entitites)
        {
            throw new NotImplementedException();
        }
    }
}