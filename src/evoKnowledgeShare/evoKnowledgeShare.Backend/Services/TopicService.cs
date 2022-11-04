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

        public async Task<IEnumerable<Topic>> GetAllAsync() => await myRepository.GetAllAsync();

        public Topic? GetById(int id) => myRepository.GetById(id);

        public IEnumerable<Topic> GetByTitle(string title) => myRepository.GetAll().Where(x => x.Title == title).ToList();

        public IEnumerable<Topic> GetRangeById(IEnumerable<int> ids) => myRepository.GetRangeById(ids);

        public void Add(Topic topic) => myRepository.Add(topic);

        public async Task AddAsync(Topic topic) => await myRepository.AddAsync(topic);

        public void AddRange(IEnumerable<Topic> range) => myRepository.AddRange(range);

        public void Remove(Topic entity) => myRepository.Remove(entity);

        public void RemoveById(int id) => myRepository.RemoveById(id);

        public void RemoveRange(IEnumerable<Topic> entities) => myRepository.RemoveRange(entities);

        public void RemoveRangeById(IEnumerable<int> ids) => myRepository.RemoveRangeById(ids);

        public void Update(Topic entity) => myRepository.Update(entity);

        public void UpdateRange(IEnumerable<Topic> entities) => myRepository.UpdateRange(entities);

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) => myRepository.SaveChangesAsync(cancellationToken);
    }
}