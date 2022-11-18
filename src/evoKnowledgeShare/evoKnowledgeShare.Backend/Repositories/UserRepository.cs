using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        { }

        public override async Task<User> AddAsync(User entity)
        {
            await myDbContext.Users.AddAsync(entity);
            await myDbContext.SaveChangesAsync();
            return myDbContext.Users.First(x => Equals(x, entity));
        }

        public override async Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> entities)
        {
            await myDbContext.Users.AddRangeAsync(entities);
            return myDbContext.Users;
        }

        public override IEnumerable<User> GetAll() => myDbContext.Users;

        public override User GetById(Guid id) => myDbContext.Users.First(x => x.Id == id);

        public override IEnumerable<User> GetRangeById(IEnumerable<Guid> ids)
        {
            List<User> users = new List<User>();
            foreach (var id in ids)
            {
                users.Add(myDbContext.Users.First(x => x.Id == id));
            }
            return users;
        }

        public override void Remove(User entity)
        {
            myDbContext.Users.Remove(entity);
            myDbContext.SaveChanges();
        }

        public override void RemoveById(Guid id)
        {
            User? user = myDbContext.Users.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException();
            Remove(user);
        }

        public override void RemoveRange(IEnumerable<User> entities) => myDbContext.Users.RemoveRange(entities);

        public override void RemoveRangeById(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                myDbContext.Users.Remove(myDbContext.Users.First(x => x.Id == id));
            }
        }

        public override User Update(User user_in)
        {
            User user = myDbContext.Users.Update(user_in).Entity;
            myDbContext.SaveChanges();
            return user;
        }

        public override IEnumerable<User> UpdateRange(IEnumerable<User> entitites)
        {
            myDbContext.Users.UpdateRange(entitites);
            return myDbContext.Users;
        }
    }
}
