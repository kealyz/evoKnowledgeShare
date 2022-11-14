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

        public IEnumerable<Topic> GetAll() => myRepository.GetAll();

        public Topic? GetById(Guid id) => myRepository.GetById(id);

        public IEnumerable<Topic> GetByTitle(string title) => myRepository.GetAll().Where(x => x.Title == title).ToList();

        public IEnumerable<Topic> GetRangeById(IEnumerable<Guid> ids) => myRepository.GetRangeById(ids);

        public async Task AddAsync(Topic topic) => await myRepository.AddAsync(topic);

        public void Remove(Topic entity) => myRepository.Remove(entity);

        public void RemoveById(Guid id) => myRepository.RemoveById(id);

        public void RemoveRange(IEnumerable<Topic> entities) => myRepository.RemoveRange(entities);

        public void RemoveRangeById(IEnumerable<Guid> ids) => myRepository.RemoveRangeById(ids);

        public Topic Update(Topic entity) => myRepository.Update(entity);

        public IEnumerable<Topic> UpdateRange(IEnumerable<Topic> entities) => myRepository.UpdateRange(entities);
    }
}