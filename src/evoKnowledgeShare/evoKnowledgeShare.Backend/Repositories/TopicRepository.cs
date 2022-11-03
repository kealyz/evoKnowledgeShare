using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection.Metadata.Ecma335;

namespace evoKnowledgeShare.Backend.Repositories {
    public class TopicRepository : Repository<Topic> {
        public TopicRepository(EvoKnowledgeDbContext dbContext) : base(dbContext) { }

        public override void Add(Topic entity) {
            myDbContext.Topics.Add(entity);
        }

        public override async Task AddAsync(Topic entity) {
            await myDbContext.Topics.AddAsync(entity);
        }

        public override void AddRange(IEnumerable<Topic> entities) {
            for (int i = 0; i < entities.Count(); i++) {
                myDbContext.Topics.Add(entities.ElementAt(i));
            }
        }

        public override IEnumerable<Topic> GetAll() {
            return myDbContext.Topics;
        }

        public override async Task<IEnumerable<Topic>> GetAllAsync() {
            return await myDbContext.Topics.ToListAsync();
        }

        public override Topic GetById(int id) {
            return myDbContext.Topics.Where(x => x.TopicID == id).First();
        }

        public override IEnumerable<Topic> GetRangeById(IEnumerable<int> ids) {
            List<Topic> returnTopics = new List<Topic>();
            foreach (var id in ids) {
                returnTopics.Add(myDbContext.Topics.Where(x => x.TopicID == id).First());
            }
            return returnTopics;
        }

        public override void Remove(Topic entity) {
            myDbContext.Remove(entity);
        }

        public override void RemoveById(int id) {
            myDbContext.Remove(myDbContext.Topics.Where(x => x.TopicID == id).First());
        }

        public override void RemoveRange(IEnumerable<Topic> entities) {
            foreach (var topic in entities) {
                myDbContext.Remove(topic);
            }
        }

        public override void RemoveRangeById(IEnumerable<int> ids) {
            foreach (var id in ids) {
                myDbContext.Remove(myDbContext.Topics.Where(x => x.TopicID == id).First());
            }
        }

        public override async Task SaveChangesAsync(CancellationToken cancellationToken = default) {
            await myDbContext.SaveChangesAsync(cancellationToken);
        }

        public override void Update(Topic entity) {
            myDbContext.Update(entity);
        }

        public override void UpdateRange(IEnumerable<Topic> entitites) {
            foreach (var entity in entitites) {
                myDbContext.Update(entity);
            }
        }
    }
}
