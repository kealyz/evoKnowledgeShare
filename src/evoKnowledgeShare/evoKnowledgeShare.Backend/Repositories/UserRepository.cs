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

        public override User GetById(int id) => myDbContext.Users.First(x => x.Id == id);

        public override IEnumerable<User> GetRangeById(IEnumerable<int> ids)
        {
            List<User> users = new List<User>();
            foreach (var id in ids)
            {
                users.Add(myDbContext.Users.First(x => x.Id == id));
            }
            return users;
        }

        public override void Remove(User entity) => myDbContext.Users.Remove(entity);

        public override void RemoveById(int id) => myDbContext.Users.Remove(myDbContext.Users.First(x => x.Id == id));

        public override void RemoveRange(IEnumerable<User> entities) => myDbContext.Users.RemoveRange(entities);

        public override void RemoveRangeById(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                myDbContext.Users.Remove(myDbContext.Users.First(x => x.Id == id));
            }
        }

        public override void Update(User entity) => myDbContext.Users.Update(entity);

        public override void UpdateRange(IEnumerable<User> entitites) => myDbContext.Users.UpdateRange(entitites);
    }
}
