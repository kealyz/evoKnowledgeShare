using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using static System.Net.Mime.MediaTypeNames;

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

        ///<summary>Return all <see cref="Note"/> type data from database.</summary>
        /// <returns>A list of <see cref="Note"/> or empty list</returns>
        public IEnumerable<Note> GetAll() => myRepository.GetAll();

        ///<summary>Return a specific note from database by it's noteId</summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="Note"/> if found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Note GetById(Guid guid) => myRepository.GetById(guid);
        
        ///<summary>Returns all <see cref="Note"/> type datas from database if their userId match with the given.</summary>
        /// <param name="id"></param>
        /// <returns>A list of <see cref="Note"/> if found, else an empty list</returns>
        public IEnumerable<Note> GetRangeByUserId(Guid id)
        {
            IEnumerable<Note> notes = myRepository.GetAll().Where(x => x.UserId == id).ToList();
            if (!notes.Any())
            {
                return Enumerable.Empty<Note>();
            }
            return notes;
            
        }

        ///<summary>Returns all <see cref="Note"/> type datas from database if their topicId match with the given.</summary>
        /// <param name="id"></param>
        /// <returns>A list of <see cref="Note"/> if found, else an empty list</returns>
        public IEnumerable<Note> GetRangeBytTopicId(Guid id)
        {
            IEnumerable<Note> notes = myRepository.GetAll().Where(x => x.TopicId == id).ToList();
            if (!notes.Any())
            {
                return Enumerable.Empty<Note>();
            }
            return notes;
        } 

        ///<summary>Returns a specific <see cref="Note"/> by it's description.</summary>
        /// <param name="description"></param>
        /// <returns>A <see cref="Note"/> if found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Note GetByDescription(string description)
        {
            Note note=myRepository.GetAll().FirstOrDefault(x => x.Description == description) ?? throw new KeyNotFoundException();
            return note;
        }

        ///<summary>Get a specific <see cref="Note"/> by it's title</summary>
        /// <param name="title"></param>
        /// <returns>A <see cref="Note"/> if found</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Note GetByTitle(string title)
        {
            Note note=myRepository.GetAll().FirstOrDefault(x => x.Title == title) ?? throw new KeyNotFoundException();
            return note;
        }
        #endregion Get Section

        #region Add Section

        ///<summary>Adds a <see cref="Note"/> entity to the database.</summary>
        /// <param name="note"></param>
        /// <returns>Task <see cref="Note"/> if added</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Note> AddAsync(Note note, string mdRaw) 
        {
            try {
                await myRepository.AddAsync(note);
                CreateNoteFolder(note.NoteId.ToString(), mdRaw);
                return await Task.FromResult(note);
            } catch (ArgumentException) {
                throw;
            }
        }

        private void CreateNoteFolder(string guid, string mdRaw) {
            // Create Note Folder
            if(!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", guid)))
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", guid));

            // Create Latest Version File
            using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", guid, "LatestVersion.txt"))) 
            {
                writer.WriteLine("1.0.0");
            }

            // Create Version Subfolder
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", guid, "1.0.0"));

            // Create MD File
            using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", guid, "1.0.0", "document.md"))) 
            {
                writer.Write(mdRaw);
            }
        }

        /// <param name="note"></param>
        /// <returns>Task list of <see cref="Note"/> if added</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<Note>> AddRangeAsync(IEnumerable<Note> notes)
        {
            try
            {
                await myRepository.AddRangeAsync(notes);
                return await Task.FromResult(notes);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
        #endregion Add Section

        #region Remove Section

        ///<summary>Removes a <see cref="Note"/> entity from the database.</summary>
        /// <param name="note"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void Remove(Note note) {
            if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", note.NoteId.ToString())))
                Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", note.NoteId.ToString()), true);
            myRepository.Remove(note);
        }

        ///<summary>Removes a specific <see cref="Note"/> from the database identified by it's noteId</summary>
        /// <param name="id"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void RemoveById(Guid id) {
            if(Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", id.ToString())))
                Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", id.ToString()), true);
            myRepository.RemoveById(id);
        }
        #endregion Remove Section

        #region Modify Section

        ///<summary>Updates a <see cref="Note"/> entity in the database to the given one.</summary>
        /// <param name="note"></param>
        /// <returns>A <see cref="Note"/> if modified</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Note Update(Note note, string mdRaw, int incrementSize) {
            UpdateNoteFolder(note.NoteId.ToString(), mdRaw, incrementSize);
            return myRepository.Update(note);
        }

        private void UpdateNoteFolder(string guid, string mdRaw, int incrementSize) {
            // Get Latest Version
            string[] versionRaw;
            int[] version = new int[3];
            string newVersion;

            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", guid, "LatestVersion.txt")))
                return;

            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", guid, "LatestVersion.txt"))) {
                versionRaw = reader.ReadLine()!.Split('.');
                version[0] = int.Parse(versionRaw[0]);
                version[1] = int.Parse(versionRaw[1]);
                version[2] = int.Parse(versionRaw[2]);
            }

            // Update Version
            switch (incrementSize) {
                case 1: version[0]++; version[1] = 0; version[2] = 0; break;
                case 2: version[1]++; version[2] = 0; break;
                case 3: version[2]++; break;
                default:
                    break;
            }

            newVersion = version[0] + "." + version[1] + "." + version[2];

            using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", guid, "LatestVersion.txt"))) {
                writer.WriteLine(newVersion);
            }

            // Create New Version Subfolder
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", guid, newVersion));

            // Create MD File
            using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "NoteTesting", guid, newVersion, "document.md"))) {
                writer.Write(mdRaw);
            }
        }
        #endregion Modify Section
    }
}
