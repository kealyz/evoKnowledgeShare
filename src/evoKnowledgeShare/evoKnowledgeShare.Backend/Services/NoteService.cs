using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Services
{
    public class NoteService
    {
        private readonly IRepository<Note> myRepository;

        public NoteService(IRepository<Note> repository)
        {
            myRepository = repository;
        }

        public IEnumerable<Note> Get() => myRepository.GetAll();

        public async Task<IEnumerable<Note>> GetAsync() => await myRepository.GetAllAsync();
    }
}
