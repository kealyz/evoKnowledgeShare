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
            if (!notes.Any())
            {
                return Enumerable.Empty<Note>();
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
            if (!notes.Any())
            {
                return Enumerable.Empty<Note>();
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
            return await Task.FromResult(note);
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Note>> AddRangeAsync(IEnumerable<Note> notes)
        {
            List<Note> resultNotes = new();
            foreach (Note note in notes)
            {
                resultNotes.Add(await AddAsync(note));
            }
            myDbContext.SaveChanges();
            return resultNotes;
            
        }
        #endregion Add Section
        #region Remove Section

        /// <inheritdoc/>
        public override void Remove(Note note)
        {
            try
            {
                myDbContext.Notes.Remove(note);
                myDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException();
            }
        }

        /// <inheritdoc/>
        public override void RemoveById(Guid id)
        {
            try
            {
                Note note = myDbContext.Notes.FirstOrDefault(x => x.NoteId == id) ?? throw new KeyNotFoundException();
                myDbContext.Notes.Remove(note);
                myDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException();
            }
        }

        /// <inheritdoc/>
        public override void RemoveRange(IEnumerable<Note> entities)
        {
            try
            {
                myDbContext.Notes.RemoveRange(entities);
                myDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException();
            }
        }

        /// <inheritdoc/>
        public override void RemoveRangeById(IEnumerable<Guid> ids)
        {
            try
            {
                foreach (Guid id in ids)
                {
                    Note? note = myDbContext.Notes.FirstOrDefault(x => x.NoteId == id) ?? throw new KeyNotFoundException();
                    myDbContext.Notes.Remove(note);
                }
                myDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException();
            }
        }
        #endregion Remove Section
        #region Modify Section

        /// <inheritdoc/>
        public override Note Update(Note note)
        {
            try
            {
                Note resultNote = myDbContext.Notes.Update(note).Entity;
                myDbContext.SaveChanges();
                return resultNote;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException();
            }
        }

        /// <inheritdoc/>
        public override IEnumerable<Note> UpdateRange(IEnumerable<Note> notes)
        {
            try
            {
                List<Note> resultNotes = new();
                foreach (Note note in notes)
                {
                    resultNotes.Add(myDbContext.Notes.Update(note).Entity);
                }
                myDbContext.SaveChanges();
                return resultNotes;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException();
            }
        }
        #endregion Modify Section
    }
}