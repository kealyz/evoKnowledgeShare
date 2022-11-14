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

        //Gets
        public IEnumerable<Note> GetAll() => myRepository.GetAll();
        public Note GetNoteById(Guid guid) => myRepository.GetAll().Where(x=>x.NoteId==guid).First();
        public IEnumerable<Note> GetByUserId(string id) => myRepository.GetAll().Where(x => x.UserId == id).ToList();
        public IEnumerable<Note> GetByTopicId(int id) => myRepository.GetAll().Where(x => x.TopicId == id).ToList();
        public Note GetByDescription(string description) => myRepository.GetAll().Where(x => x.Description == description).First();
        public Note GetByTitle(string title) => myRepository.GetAll().Where(x => x.Title == title).First();

        //Adds but async
        public async Task AddAsync(Note note) => await myRepository.AddAsync(note);
        public async Task AddRangeAsync(IEnumerable<Note> notes) => await myRepository.AddRangeAsync(notes);

        //Remove
        public void Remove(Note note) => myRepository.Remove(note);
        public void RemoveById(Guid id) => myRepository.Remove(myRepository.GetAll().Where(x => x.NoteId == id).First());

        //Modify
        public Note Update(Note note) => myRepository.Update(note);
    }
}
