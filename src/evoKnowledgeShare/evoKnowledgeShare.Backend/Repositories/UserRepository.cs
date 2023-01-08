using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace evoKnowledgeShare.Backend.Repositories
{
    public class UserRepository : Repository<User>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public UserRepository(EvoKnowledgeDbContext dbContext) : base(dbContext)
        { }

        #region Get Section

        /// <inheritdoc/>
        public override IEnumerable<User> GetAll() => myDbContext.Users;

        /// <inheritdoc/>
        public override User GetById(Guid id) => myDbContext.Users.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException();

        /// <inheritdoc/>
        public override IEnumerable<User> GetRangeById(IEnumerable<Guid> ids)
        {
            if(ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            IEnumerable<User> users = myDbContext.Users.Where(x => ids.Any(y => x.Id == y)).ToList();
            return users.Count() == ids.Count() ? users : throw new KeyNotFoundException();
        }

        #endregion Get Section

        #region Add Section

        /// <inheritdoc/>
        public override async Task<User> AddAsync(User entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Id = Guid.NewGuid();
            try
            {
                EntityEntry<User> addedUser = await myDbContext.Users.AddAsync(entity);
                await myDbContext.SaveChangesAsync();
                return addedUser.Entity;
            }
            catch (OperationCanceledException ex)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<User>> AddRangeAsync(IEnumerable<User> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                await myDbContext.Users.AddRangeAsync(entities);
                await myDbContext.SaveChangesAsync();
                return myDbContext.Users.Where(user => entities.Any(entity => entity == user)).ToList();
            }
            catch (OperationCanceledException ex)
            {
                //TODO: Log(ex)
                throw;
            }
        }

        #endregion Add Section

        #region Remove Section

        /// <inheritdoc/>
        public override void Remove(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                myDbContext.Users.Remove(entity);
                myDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new KeyNotFoundException();
            }
        }

        /// <inheritdoc/>
        public override void RemoveById(Guid id)
        {
            User user = myDbContext.Users.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException();
            myDbContext.Users.Remove(user);
            myDbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public override void RemoveRange(IEnumerable<User> entities)
        {
            if(entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            try
            {
                myDbContext.Users.RemoveRange(entities);
                myDbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new KeyNotFoundException();
            }
        }

        /// <inheritdoc/>
        public override void RemoveRangeById(IEnumerable<Guid> ids)
        {
            if(ids == null)
            {
                throw new ArgumentNullException();
            }

            try
            {
                IEnumerable<User> usersToRemove = myDbContext.Users.Where(x => ids.Any(y => x.Id == y)).ToList();

                if (ids.Count() != usersToRemove.Count())
                {
                    throw new KeyNotFoundException();
                }
                foreach (User userToRemove in usersToRemove)
                {
                    myDbContext.Users.Remove(userToRemove);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
        }

        #endregion Remove Section

        #region Update Section

        /// <inheritdoc/>
        public override User Update(User entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException();
            }

            try
            {
                User user = myDbContext.Users.Update(entity).Entity;
                myDbContext.SaveChanges();
                return user;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new KeyNotFoundException();
            }
        }

        /// <inheritdoc/>
        public override IEnumerable<User> UpdateRange(IEnumerable<User> entities)
        {
            if(entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            
            try
            {
                myDbContext.Users.UpdateRange(entities);
                myDbContext.SaveChanges();
                return myDbContext.Users.Where(user => entities.Any(entity => entity == user)).ToList();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new KeyNotFoundException();
            }
        }

        #endregion Update Section
    }
}