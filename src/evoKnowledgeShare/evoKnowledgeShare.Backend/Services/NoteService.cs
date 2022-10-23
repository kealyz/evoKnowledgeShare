using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using System;

namespace evoKnowledgeShare.Backend.Services
{
    public class NoteService
    {
        private readonly IRepository<Note> myRepository;

        public NoteService(IRepository<Note> repository)
        {
            myRepository = repository;
        }

        public IEnumerable<Note> GetAll() => myRepository.GetAll();
        public async Task<IEnumerable<Note>> GetAllAsync() =>await myRepository.GetAllAsync();
        public Note getNoteByGuid(Guid guid) => myRepository.GetByGuid(guid);
        public Note getNoteByUserId(string id) => myRepository.GetByUserId(id);
        public Note getNoteByTopicId(int id) => myRepository.GetByTopicId(id);
        public Note getNoteByCreationTime(DateTimeOffset date) => myRepository.GetByCreationTime(date);
        public Note getNoteByDescription(string description) => myRepository.GetByDescription(description);
        public Note getNoteByTitle(string title) => myRepository.GetByTitle(title);

    }
}
