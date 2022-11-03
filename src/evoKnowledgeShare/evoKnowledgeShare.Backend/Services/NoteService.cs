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
        public IEnumerable<Note> GetNotesByUserId(string id) => myRepository.GetAll().Where(x => x.UserId == id).ToList();
        public IEnumerable<Note> GetNotesByTopicId(int id) => myRepository.GetAll().Where(x => x.TopicId == id).ToList();
        public Note GetNoteByDescription(string description) => myRepository.GetAll().Where(x => x.Description == description).First();
        public Note GetNoteByTitle(string title) => myRepository.GetAll().Where(x => x.Title == title).First();

        //Gets but async 
        public async Task<IEnumerable<Note>> GetAllAsync() => await myRepository.GetAllAsync();
        public async Task<Note> GetNoteByIdAsync(Guid guid) => (await myRepository.GetAllAsync()).Where(x => x.NoteId == guid).First();
        public async Task<IEnumerable<Note>> GetNotesByUserIdAsync(string id) => (await myRepository.GetAllAsync()).Where(x => x.UserId == id).ToList();
        public async Task<IEnumerable<Note>> GetNotesByTopicIdAsync(int id) => (await myRepository.GetAllAsync()).Where(x => x.TopicId == id).ToList();
        public async Task<Note> GetNoteByDescriptionAsync(string description) => (await myRepository.GetAllAsync()).Where(x => x.Description == description).First();
        public async Task<Note> GetNoteByTitleAsync(string title) => (await myRepository.GetAllAsync()).Where(x => x.Title == title).First();

        //Adds
        public void AddNote(Note note) => myRepository.Add(note);
        public void AddNotes(List<Note> notes) => myRepository.AddRange(notes);

        //Adds but async
        public async Task AddNoteAsync(Note note) => await myRepository.AddAsync(note);
        public async Task AddNotesAsync(List<Note> notes) => await myRepository.AddRangeAsync(notes);

    }
}
