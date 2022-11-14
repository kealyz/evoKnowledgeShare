using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class NoteRepository : Repository<Note>
    {
        public NoteRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        { }

        public override async Task AddAsync(Note entity)
        {
            await myDbContext.Notes.AddAsync(entity);
        }

        public override async Task AddRangeAsync(IEnumerable<Note> entities)
        {
            await myDbContext.Notes.AddRangeAsync(entities);
        }

        public override IEnumerable<Note> GetAll()
        {
            return myDbContext.Notes;
        }

        public override Note GetById(Guid id)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override IEnumerable<Note> GetRangeById(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override void Remove(Note entity)
        {
            myDbContext.Notes.Remove(entity);
            myDbContext.SaveChanges();
        }

        public override void RemoveById(Guid id)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override void RemoveRange(IEnumerable<Note> entities)
        {
            myDbContext.Notes.RemoveRange(entities);
        }

        public override void RemoveRangeById(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override Note Update(Note note_in)
        {
            Note note = myDbContext.Notes.Update(note_in).Entity;
            myDbContext.SaveChanges();
            return note;
        }

        public override IEnumerable<Note> UpdateRange(IEnumerable<Note> entities)
        {
            foreach (var entity in entities)
            {
                foreach (var item in myDbContext.Notes)
                {
                    if (item.NoteId == entity.NoteId)
                    {
                        item.Title = entity.Title;
                        item.TopicId = entity.TopicId;
                        item.UserId = entity.UserId;
                        item.CreatedAt = entity.CreatedAt;
                        item.Description = entity.Description;
                    }
                }
            }
        }
    }
}