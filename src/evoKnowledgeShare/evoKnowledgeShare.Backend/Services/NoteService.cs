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

        //Gets 
        public IEnumerable<Note> GetAll() => myRepository.GetAll();
        public Note getNoteById(Guid guid) => myRepository.GetAll().Where(x=>x.NoteId==guid).First();
        public Note getNoteByUserId(string id) => myRepository.GetAll().Where(x => x.UserId == id).First();
        public Note getNoteByTopicId(int id) => myRepository.GetAll().Where(x => x.TopicId == id).First();
        public Note getNoteByCreationTime(DateTimeOffset date) => myRepository.GetAll().Where(x => x.CreatedAt == date).First();
        public Note getNoteByDescription(string description) => myRepository.GetAll().Where(x => x.Description == description).First();
        public Note getNoteByTitle(string title) => myRepository.GetAll().Where(x => x.Title == title).First();

        //Gets but async 
        public async Task<IEnumerable<Note>> GetAllAsync() => await myRepository.GetAllAsync();
        public async Task<Note> getNoteByIdAsync(Guid guid) => (await myRepository.GetAllAsync()).Where(x => x.NoteId == guid).First();
        public async Task<Note> getNoteByUserIdAsync(string id) => (await myRepository.GetAllAsync()).Where(x => x.UserId == id).First();
        public async Task<Note> getNoteByTopicIdAsync(int id) => (await myRepository.GetAllAsync()).Where(x => x.TopicId == id).First();
        public async Task<Note> getNoteByCreationTimeAsync(DateTimeOffset date) => (await myRepository.GetAllAsync()).Where(x => x.CreatedAt == date).First();
        public async Task<Note> getNoteByDescriptionAsync(string description) => (await myRepository.GetAllAsync()).Where(x => x.Description == description).First();
        public async Task<Note> getNoteByTitleAsync(string title) => (await myRepository.GetAllAsync()).Where(x => x.Title == title).First();

    }
}
