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

        public IEnumerable<History> GetAll() => myHistoryRepository.GetAll();

        public History GetById(Guid id) => myHistoryRepository.GetById(id);

        public async Task<History> CreateHistory(History history) => await myHistoryRepository.AddAsync(history);
    }
}