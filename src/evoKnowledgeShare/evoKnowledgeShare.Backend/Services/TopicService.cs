using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;

using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.Backend.Services
{
    public class TopicService
    {
        private readonly IRepository<Topic> myRepository;

        public TopicService(IRepository<Topic> repository)
        {
            myRepository = repository;
        }

        #region Get Section

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Empty list if not found.</returns>
        public IEnumerable<Topic> GetAll() => myRepository.GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The topic that matches the id.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public Topic GetById(Guid id) => myRepository.GetById(id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public IEnumerable<Topic> GetByTitle(string title)
        {
            if(title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            IEnumerable<Topic> topics = myRepository.GetAll().Where(x => x.Title == title).ToList();
            return topics.Any() ? topics : throw new KeyNotFoundException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>Empty list if not found.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public IEnumerable<Topic> GetRangeById(IEnumerable<Guid> ids) => myRepository.GetRangeById(ids);

        #endregion Get Section

        #region Add Section

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The topics that has been added to the database.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<Topic> AddAsync(TopicDTO topicDTO) {
            Topic topic = new Topic(Guid.NewGuid(), topicDTO.Title);
            return await myRepository.AddAsync(topic); 
        }

        #endregion Add Section

        #region Remove Section

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public void Remove(Topic entity) => myRepository.Remove(entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public void RemoveById(Guid id) => myRepository.RemoveById(id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public void RemoveRange(IEnumerable<Topic> entities) => myRepository.RemoveRange(entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public void RemoveRangeById(IEnumerable<Guid> ids) => myRepository.RemoveRangeById(ids);

        #endregion Remove Section

        #region Update Section

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic_in"></param>
        /// <returns>The updated topic in the database.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Topic Update(Topic entity) => myRepository.Update(entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>The topic that has been updated in the database.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Topic> UpdateRange(IEnumerable<Topic> entities) => myRepository.UpdateRange(entities);

        #endregion Update Section
    }
}