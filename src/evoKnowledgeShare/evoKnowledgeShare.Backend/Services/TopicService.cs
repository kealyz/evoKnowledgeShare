using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;

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
        public IEnumerable<Topic> GetAll() => myRepository.GetAll();

        public Topic? GetById(Guid id) => myRepository.GetById(id);

        public IEnumerable<Topic> GetByTitle(string title) => myRepository.GetAll().Where(x => x.Title == title).ToList();

        public IEnumerable<Topic> GetRangeById(IEnumerable<Guid> ids) => myRepository.GetRangeById(ids);
        #endregion Get Section

        #region Add Section
        public async Task AddAsync(Topic topic) => await myRepository.AddAsync(topic);
        #endregion Add Section

        #region Remove Section
        public void Remove(Topic entity) => myRepository.Remove(entity);

        public void RemoveById(Guid id) => myRepository.RemoveById(id);

        public void RemoveRange(IEnumerable<Topic> entities) => myRepository.RemoveRange(entities);

        public void RemoveRangeById(IEnumerable<Guid> ids) => myRepository.RemoveRangeById(ids);
        #endregion Remove Section

        #region Update Section
        public Topic Update(Topic entity) => myRepository.Update(entity);

        public IEnumerable<Topic> UpdateRange(IEnumerable<Topic> entities) => myRepository.UpdateRange(entities);
        #endregion Remove Section
    }
}