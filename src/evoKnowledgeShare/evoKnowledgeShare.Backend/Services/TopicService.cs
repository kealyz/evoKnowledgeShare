using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
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
        /// Get every topic from the database.
        /// </summary>
        /// <returns>Every topic from the database. Can be null.</returns>
        public IEnumerable<Topic> GetAll() => myRepository.GetAll();

        /// <summary>
        /// Get a topic by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The topic that matches the id.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public Topic? GetById(Guid id) => myRepository.GetById(id);

        /// <summary>
        /// Gets every topic that matches the given title.
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Every topic that matches the title.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Topic> GetByTitle(string title) => myRepository.GetAll().Where(x => x.Title == title).ToList();

        /// <summary>
        /// Get a range of topics by id.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>The topics that match the id given.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Topic> GetRangeById(IEnumerable<Guid> ids) => myRepository.GetRangeById(ids);
        #endregion Get Section

        #region Add Section
        /// <summary>
        /// Adds a topic async to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The topic added to the database.</returns>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Topic> AddAsync(TopicDTO topicDTO) {
            Topic topic = new Topic(Guid.Empty, topicDTO.Title);
            return await myRepository.AddAsync(topic); 
        }
        #endregion Add Section

        #region Remove Section
        /// <summary>
        /// Remove a topic.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public void Remove(Topic entity) => myRepository.Remove(entity);

        /// <summary>
        /// Removes a topic by an id.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public void RemoveById(Guid id) => myRepository.RemoveById(id);

        /// <summary>
        /// Removes of a range of topics.
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public void RemoveRange(IEnumerable<Topic> entities) => myRepository.RemoveRange(entities);

        /// <summary>
        /// Removes a range of topics by ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public void RemoveRangeById(IEnumerable<Guid> ids) => myRepository.RemoveRangeById(ids);
        #endregion Remove Section

        #region Update Section
        /// <summary>
        /// Updates one topic.
        /// </summary>
        /// <param name="topic_in"></param>
        /// <returns>The updated topic in the database.</returns>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        public Topic Update(Topic entity) => myRepository.Update(entity);

        /// <summary>
        /// Updates a range of topics.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>The topic that has been updated in the database.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<Topic> UpdateRange(IEnumerable<Topic> entities) => myRepository.UpdateRange(entities);
        #endregion Remove Section
    }
}