using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class TopicRepository : Repository<Topic>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public TopicRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        {

        }

        #region Add Section

        /// <inheritdoc/>
        public override async Task<Topic> AddAsync(Topic topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            topic.Id = Guid.NewGuid();
            try
            {
                EntityEntry<Topic> addedTopic = await myDbContext.Topics.AddAsync(topic);
                await myDbContext.SaveChangesAsync();
                return addedTopic.Entity;
            }
            catch (OperationCanceledException)
            {
                //TODO: Log(ex)
                throw;
            }
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Topic>> AddRangeAsync(IEnumerable<Topic> topics)
        {
            if (topics == null)
            {
                throw new ArgumentNullException(nameof(topics));
            }

            try
            {
                await myDbContext.Topics.AddRangeAsync(topics);
                await myDbContext.SaveChangesAsync();
                return myDbContext.Topics.Where(topic => topics.Any(entity => entity == topic));
            }
            catch (OperationCanceledException)
            {
                //TODO: Log(ex)
                throw;
            }
        }

        #endregion Add Section

        #region Get Section

        /// <inheritdoc/>
        public override IEnumerable<Topic> GetAll()
        {
            return myDbContext.Topics;
        }

        /// <inheritdoc/>
        public override Topic GetById(Guid id)
        {
            return myDbContext.Topics.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException();
        }

        /// <inheritdoc/>
        public override IEnumerable<Topic> GetRangeById(IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            IEnumerable<Topic> topics = myDbContext.Topics.Where(x => ids.Any(y => x.Id == y));
            return topics.Any() ? topics : throw new KeyNotFoundException();
        }

        #endregion Get Section

        #region Remove Section

        /// <inheritdoc/>
        public override void Remove(Topic topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            try
            {
                myDbContext.Topics.Remove(topic);
                myDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO: Log(ex)
                throw;
            }
        }

        /// <inheritdoc/>
        public override void RemoveById(Guid id)
        {
            try
            {
                Topic topicToRemove = myDbContext.Topics.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException();
                myDbContext.Topics.Remove(topicToRemove);
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO: Log(ex)
                throw;
            }
        }

        /// <inheritdoc/>
        public override void RemoveRange(IEnumerable<Topic> topics)
        {
            if (topics == null)
            {
                throw new ArgumentNullException(nameof(topics));
            }

            try
            {
                foreach (var topic in topics)
                {
                    myDbContext.Remove(topic);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO: Log(ex)
                throw;
            }
        }

        /// <inheritdoc/>
        public override void RemoveRangeById(IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            try
            {
                IEnumerable<Topic> topicsToRemove = myDbContext.Topics.Where(x => ids.Any(y => x.Id == y)).ToList();

                if (ids.Count() != topicsToRemove.Count())
                {
                    throw new KeyNotFoundException();
                }
                foreach (Topic topicToRemove in topicsToRemove)
                {
                    myDbContext.Topics.Remove(topicToRemove);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO: Log(ex)
                throw;
            }
        }

        #endregion Remove Section

        #region Update Section

        /// <inheritdoc/>
        public override Topic Update(Topic topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException(nameof(topic));
            }

            Topic topicUpdated = myDbContext.Topics.Update(topic).Entity;
            myDbContext.SaveChanges();

            return topicUpdated;
        }

        /// <inheritdoc/>
        public override IEnumerable<Topic> UpdateRange(IEnumerable<Topic> topics)
        {
            if (topics == null)
            {
                throw new ArgumentNullException(nameof(topics));
            }

            myDbContext.Topics.UpdateRange(topics);
            return myDbContext.Topics.Where(topic => topics.Any(entity => entity == topic)).ToList();
        }

        #endregion Update Section
    }
}