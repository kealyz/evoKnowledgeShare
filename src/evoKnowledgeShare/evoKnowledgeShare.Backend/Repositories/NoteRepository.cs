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
            foreach (var item in entities)
            {
                myDbContext.Notes.Add(item);
            }
        }

        public override IEnumerable<Note> GetAll()
        {
            return myDbContext.Notes;
        }

        public override async Task<IEnumerable<Note>> GetAllAsync()
        {
            return myDbContext.Notes;
        }


        public override Note GetById(int id)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }


        public override IEnumerable<Note> GetRangeById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override void Remove(Note entity)
        {
            myDbContext.Remove(entity);
        }

        public override void RemoveById(int id)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override void RemoveRange(IEnumerable<Note> entities)
        {
            foreach (var item in entities)
            {
                myDbContext.Notes.Remove(item);
            }
        }

        public override void RemoveRangeById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
            //Ez ide haszontalan?
        }

        public override async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await myDbContext.SaveChangesAsync();
        }

        public override void Update(Note entity)
        {
            foreach (var item in myDbContext.Notes)
            {
                if (item.NoteId==entity.NoteId)
                {
                    item.Title = entity.Title;
                    item.TopicId = entity.TopicId;
                    item.UserId = entity.UserId;
                    item.CreatedAt = entity.CreatedAt;
                    item.Description = entity.Description;
                }
            }
        }

        public override void UpdateRange(IEnumerable<Note> entitites)
        {
            foreach (var entity in entitites)
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
