using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class NoteRepository : Repository<Note>
    {
        public NoteRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        { }

        public override void Add(Note entity)
        {
            myDbContext.Notes.Add(entity);
        }

        public override async Task AddAsync(Note entity)
        {
            await myDbContext.Notes.AddAsync(entity);
        }

        public override void AddRange(IEnumerable<Note> entities)
        {
            myDbContext.Notes.AddRange(entities);
        }

        public override Task AddRangeAsync(IEnumerable<Note> entities)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Note> GetAll()
        {
            return myDbContext.Notes;
        }

        public override Task<IEnumerable<Note>> GetAllAsync()
        {
            IEnumerable<Note> notes = myDbContext.Notes;
            return Task.FromResult(notes);
        }


        public override Note GetById(int id)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override Task<Note?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Note> GetRangeById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override Task<IEnumerable<Note>> GetRangeByIdAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Note entity)
        {
            myDbContext.Remove(entity);
        }

        public override Task RemoveAsync(Note entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoveById(int id)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override Task RemoveByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRange(IEnumerable<Note> entities)
        {
            myDbContext.Notes.RemoveRange(entities);
        }

        public override Task RemoveRangeAsync(IEnumerable<Note> entities)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRangeById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override Task RemoveRangeByIdAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public override void Update(Note entity)
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

        public override Task UpdateAsync(Note entity)
        {
            throw new NotImplementedException();
        }

        public override void UpdateRange(IEnumerable<Note> entities)
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

        public override Task UpdateRangeAsync(IEnumerable<Note> entitites)
        {
            throw new NotImplementedException();
        }
    }
}