using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;

namespace evoKnowledgeShare.Backend.Services
{
    public class HistoryService
    {
        private readonly IRepository<History> myHistoryRepository;

        public HistoryService(IRepository<History> historyRepository)
        {
            myHistoryRepository = historyRepository;
        }

        /// <summary>
        /// Return all <see cref="History"/> entity from database.
        /// </summary>
        /// <returns>A list of <see cref="History"/> or empty list</returns>
        public IEnumerable<History> GetAll() => myHistoryRepository.GetAll();

        /// <summary>
        /// Return a specific history entity from database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="History"/> entity </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public History GetById(Guid id) => myHistoryRepository.GetById(id);

        /// <summary>
        /// Create <see cref="History"/> entity in the database
        /// </summary>
        /// <param name="history"></param>
        /// <returns>Task <see cref="History"/> if added</returns>
        /// <exception cref="ArgumentException">Argument Exception</exception>
        public async Task<History> CreateHistory(History history) => await myHistoryRepository.AddAsync(history);
    }
}