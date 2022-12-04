using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class NoteRepository : Repository<Note>
    {
        public NoteRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        { }

        #region Get Section

        /// <inheritdoc/>
        public override IEnumerable<Note> GetAll()
        {
            IEnumerable<Note> notes = myDbContext.Notes;
            if (notes == null)
            {
                throw new KeyNotFoundException();
            }
            return notes;
        }

        
        ///<inheritdoc/>
        public override Note GetById(Guid id)
        {
            Note? note=myDbContext.Notes.FirstOrDefault(x => x.NoteId == id);
            if (note == null)
            {
                throw new KeyNotFoundException();
            }
            return note;
        }

        /// <inheritdoc/>
        public override IEnumerable<Note> GetRangeById(IEnumerable<Guid> guids)
        {
            IEnumerable<Note> notes = myDbContext.Notes.Where(x => guids.Any(y => x.NoteId == y));
            if (notes == null)
            {
                throw new KeyNotFoundException();
            }
            return notes;
        }

        #endregion Get Section
        #region Add Section

        /// <inheritdoc/>
        public override async Task<Note> AddAsync(Note note)
        {
            await myDbContext.Notes.AddAsync(note);
            myDbContext.SaveChanges();
            return myDbContext.Notes.First(x => x.NoteId == note.NoteId);
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Note>> AddRangeAsync(IEnumerable<Note> notes)
        {
            List<Note> resultNotes = new List<Note>();
            foreach (Note note in notes)
                resultNotes.Add(await AddAsync(note));
            return resultNotes;
        }
        #endregion Add Section
        #region Remove Section

        /// <inheritdoc/>
        public override void Remove(Note note)
        {
            myDbContext.Notes.Remove(note);
            myDbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public override void RemoveById(Guid id)
        {
            myDbContext.Notes.Remove(myDbContext.Notes.First(x=>x.NoteId==id));
            myDbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public override void RemoveRange(IEnumerable<Note> entities)
        {
            myDbContext.Notes.RemoveRange(entities);
            myDbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public override void RemoveRangeById(IEnumerable<Guid> ids)
        {
            foreach (Guid id in ids)
            {
                var note = myDbContext.Notes.FirstOrDefault(x => x.NoteId == id);
                if (note!=null)
                {
                    myDbContext.Notes.Remove(note);
                }
            }
            myDbContext.SaveChanges();
        }
        #endregion Remove Section
        #region Modify Section

        /// <inheritdoc/>
        public override Note Update(Note note)
        {
            Note resultNote = myDbContext.Notes.Update(note).Entity;
            myDbContext.SaveChanges();
            return resultNote;
        }

        /// <inheritdoc/>
        public override IEnumerable<Note> UpdateRange(IEnumerable<Note> notes)
        {
            List<Note> resultNotes=new();
            foreach (Note note in notes)
            {
                resultNotes.Add(myDbContext.Notes.Update(note).Entity);
            }
            myDbContext.SaveChanges();
            return resultNotes;
        }
        #endregion Modify Section
    }
}