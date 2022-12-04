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

        #region Get Section

        /// <returns>A list of <see cref="Note"/></returns>
        public IEnumerable<Note> GetAll() => myRepository.GetAll();

        /// <param name="id"></param>
        /// <returns><see cref="Note"/> If found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Note GetById(Guid guid) => myRepository.GetById(guid);
        
        /// <param name="id"></param>
        /// <returns>A list of <see cref="Note"/> if found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public IEnumerable<Note> GetByUserId(Guid id) => myRepository.GetAll().Where(x => x.UserId == id).ToList();

        /// <param name="id"></param>
        /// <returns>A list of <see cref="Note"/> if found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public IEnumerable<Note> GetByTopicId(int id) => myRepository.GetAll().Where(x => x.TopicId == id).ToList();


        /// <param name="description"></param>
        /// <returns>A <see cref="Note"/> if found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Note GetByDescription(string description) => myRepository.GetAll().FirstOrDefault(x => x.Description == description);

        /// <param name="title"></param>
        /// <returns>A <see cref="Note"/> if found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Note GetByTitle(string title) => myRepository.GetAll().Where(x => x.Title == title).First();
        #endregion Get Section

        #region Add Section

        /// <param name="note"></param>
        /// <returns>Task <see cref="Note"/> if added</returns>
        public async Task<Note> AddAsync(Note note)
        {
            await myRepository.AddAsync(note);
            return await Task.FromResult(note);
        }

        /// <param name="note"></param>
        /// <returns>Task list of <see cref="Note"/> if added</returns>
        public async Task<IEnumerable<Note>> AddRangeAsync(IEnumerable<Note> notes)
        {
            await myRepository.AddRangeAsync(notes);
            return await Task.FromResult(notes);
        }
        #endregion Add Section

        #region Remove Section


        /// <param name="note"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void Remove(Note note) => myRepository.Remove(note);

        /// <param name="id"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void RemoveById(Guid id) => myRepository.RemoveById(id);
        #endregion Remove Section

        #region Modify Section


        /// <param name="note"></param>
        /// <returns>A <see cref="Note"/> if modified</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Note Update(Note note) => myRepository.Update(note);
        #endregion Modify Section
    }
}
