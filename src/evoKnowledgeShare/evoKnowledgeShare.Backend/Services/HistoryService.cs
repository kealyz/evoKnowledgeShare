using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Services
{
    public class HistoryService
    {
        private readonly IRepository<History> myHistoryRepository;

        public HistoryService(IRepository<History> historyRepository)
        {
            myHistoryRepository = historyRepository;
        }

        public async Task<IEnumerable<History>> GetAllAsync() => await myHistoryRepository.GetAllAsync();

        public IEnumerable<History> GetAll() => myHistoryRepository.GetAll();

        public History GetById(Guid id) => myHistoryRepository.GetAll().First(x => x.Id == id);

        public async Task CreateHistory(History history) => await myHistoryRepository.AddAsync(history);
    }
}
