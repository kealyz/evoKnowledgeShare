using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        { }

        #region Get Section

        public override IEnumerable<User> GetAll() => myDbContext.Users;
        public override User GetById(Guid id) => myDbContext.Users.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException();
        public override IEnumerable<User> GetRangeById(IEnumerable<Guid> ids)
        {
            List<User> resultUsers = new();
            resultUsers.AddRange(myDbContext.Users.Where(x => ids.Any(y => x.Id.Equals(y))));

            if (resultUsers.Count() != ids.Count())
                throw new KeyNotFoundException();
            return resultUsers;

        }

        #endregion Get Section

        #region Add Section

        public override async Task<User> AddAsync(User entity)
        {
            try
            {
                await myDbContext.Users.AddAsync(entity);
                await myDbContext.SaveChangesAsync();
                return await Task.FromResult(entity);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("An item with the same key has already been added.");
            }
        }

        public override async Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> entities)
        {
            try
            {
                List<User> resultUsers = new();
                foreach (User entity in entities)
                {
                    resultUsers.Add(await AddAsync(entity));
                }
                await myDbContext.SaveChangesAsync();
                return resultUsers;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("An item with the same key has already been added.");
            }
        }

        #endregion Add Section

        #region Remove Section

        public override void Remove(User entity)
        {
            try
            {
                myDbContext.Users.Remove(entity);
                myDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException();
            }
        }

        public override void RemoveById(Guid id)
        {
            User user = myDbContext.Users.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException();
            myDbContext.Remove(user);
            myDbContext.SaveChanges();
        }

        public override void RemoveRange(IEnumerable<User> entities)
        {
            try
            {
                myDbContext.Users.RemoveRange(entities);
                myDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException();
            }
        }

        public override void RemoveRangeById(IEnumerable<Guid> ids)
        {
            foreach (Guid id in ids)
            {
                User? user = myDbContext.Users.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException();
                myDbContext.Users.Remove(user);
            }
            myDbContext.SaveChanges();
        }

        #endregion Remove Section

        #region Update Section

        public override User Update(User user_in)
        {
            try
            {
                User user = myDbContext.Users.Update(user_in).Entity;
                myDbContext.SaveChanges();
                return user;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException();
            }
        }

        public override IEnumerable<User> UpdateRange(IEnumerable<User> entities)
        {
            try
            {
                myDbContext.Users.UpdateRange(entities);
                myDbContext.SaveChanges();
                return myDbContext.Users.Where(user => entities.Any(entity => entity == user));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException();
            }
        }

        #endregion Update Section
    }
}