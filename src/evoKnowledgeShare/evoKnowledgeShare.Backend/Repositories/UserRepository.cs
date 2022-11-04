using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        { }

        public override void Add(User entity)
        {
            myDbContext.Users.Add(entity);
        }

        public override Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public override void AddRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public override Task AddRangeAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<User> GetAll()
        {
            return myDbContext.Users;
        }

        public override Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<User> GetRangeById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<User>> GetRangeByIdAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public override void Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveRangeAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRangeById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveRangeByIdAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public override void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public override void UpdateRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateRangeAsync(IEnumerable<User> entitites)
        {
            throw new NotImplementedException();
        }
    }
}
