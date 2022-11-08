using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        { }

        public override void Add(User entity) => myDbContext.Users.Add(entity);

        public override async Task AddAsync(User entity) => await myDbContext.Users.AddAsync(entity);

        public override void AddRange(IEnumerable<User> entities) => myDbContext.Users.AddRange(entities);

        public override Task AddRangeAsync(IEnumerable<User> entities) => myDbContext.Users.AddRangeAsync(entities);

        public override IEnumerable<User> GetAll() => myDbContext.Users;

        public override async Task<IEnumerable<User>> GetAllAsync() => myDbContext.Users;

        public override User GetById(int id) => myDbContext.Users.First(x => x.Id == id);

        public override async Task<User?> GetByIdAsync(int id) => myDbContext.Users.First(x => x.Id == id);

        public override IEnumerable<User> GetRangeById(IEnumerable<int> ids)
        {
            List<User> users = new List<User>();
            foreach (var id in ids)
            {
                users.Add(myDbContext.Users.First(x => x.Id == id));
            }
            return users;
        }

        public override async Task<IEnumerable<User>> GetRangeByIdAsync(IEnumerable<int> ids) => throw new NotSupportedException();
        /*{
            IEnumerable<User> users = new List<User>();
            foreach (var id in ids)
            {
                users.Append(myDbContext.Users.First(x => x.Id == id));
            }
            return users;
        }*/

        public override void Remove(User entity) => myDbContext.Users.Remove(entity);

        public override Task RemoveAsync(User entity)
        {
            myDbContext.Users.Remove(entity);
            return Task.CompletedTask;
        }

        public override void RemoveById(int id) => myDbContext.Users.Remove(myDbContext.Users.First(x => x.Id == id));

        public override Task RemoveByIdAsync(int id)
        {
            myDbContext.Users.Remove(myDbContext.Users.First(x => x.Id == id));
            return Task.CompletedTask;
        }

        public override void RemoveRange(IEnumerable<User> entities) => myDbContext.Users.RemoveRange(entities);

        public override Task RemoveRangeAsync(IEnumerable<User> entities)
        {
            foreach (var entity in entities)
            {
                myDbContext.Users.Remove(entity);
            }
            return Task.CompletedTask;
        }

        public override void RemoveRangeById(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                myDbContext.Users.Remove(myDbContext.Users.First(x => x.Id == id));
            }
        }

        public override Task RemoveRangeByIdAsync(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                myDbContext.Users.Remove(myDbContext.Users.First(x => x.Id == id));
            }
            return Task.CompletedTask;
        }

        public override void Update(User entity) => myDbContext.Users.Update(entity);

        public override Task UpdateAsync(User entity)
        {
            myDbContext.Users.Update(entity);
            return Task.CompletedTask;
        }

        public override void UpdateRange(IEnumerable<User> entitites) => myDbContext.Users.UpdateRange(entitites);

        public override Task UpdateRangeAsync(IEnumerable<User> entitites)
        {
            myDbContext.Users.UpdateRange(entitites);
            return Task.CompletedTask;
        }
    }
}
