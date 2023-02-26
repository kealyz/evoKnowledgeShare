﻿using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.Backend.Services
{
    public class UserService
    {
        private readonly IRepository<User> myRepository;

        public UserService(IRepository<User> repository)
        {
            myRepository = repository;
        }

        #region Get Section

        /// <summary>
        /// Returns every <see cref="User"/> type object from the database.
        /// </summary>
        /// <returns>A list of <see cref="User"/> objects or an empty list</returns>
        public IEnumerable<User> GetAll() => myRepository.GetAll();

        /// <summary>
        /// Returns a <see cref="User"/> by its <see cref="Guid"/> primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="User"/> object, if it has been found by its id</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public User GetUserById(Guid id) => myRepository.GetById(id);

        /// <summary>
        /// Returns a <see cref="User"/> by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>A <see cref="User"/> object, if it has been found by its username</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public User? GetUserByUserName(string username)
        {
            if(username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }
            return myRepository.GetAll().FirstOrDefault(x => x.UserName.Equals(username)) ?? throw new KeyNotFoundException();
        }
        /// <summary>
        /// Returns every <see cref="User"/> by their ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>A list of <see cref="User"/> objects, if they have been found by their ids</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public IEnumerable<User> GetUserRangeById(IEnumerable<Guid> ids) => myRepository.GetRangeById(ids);

        #endregion Get Section

        /// <summary>
        /// Adds a <see cref="User"/> entity to the database.async
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Task <see cref="User"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task<User> CreateUserAsync(UserDTO userDTO)
        {
            User user = new User(Guid.NewGuid(), userDTO.UserName, userDTO.FirstName, userDTO.LastName);
            return await myRepository.AddAsync(user);
        }

        #region Remove Section

        /// <summary>
        /// Removes a <see cref="User"/> entity from the database.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public void Remove(User user) => myRepository.Remove(user);

        /// <summary>
        /// Removes a <see cref="User"/> entity by its id from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public void RemoveUserById(Guid id) => myRepository.RemoveById(id);

        #endregion Remove Section

        #region Update Section

        /// <summary>
        /// Updates specific <see cref="User"/> entity in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A <see cref="User"/> object if modified</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public User Update(User user) => myRepository.Update(user);

        /// <summary>
        /// Updates specific list of <see cref="User"/> entities in the database.
        /// </summary>
        /// <param name="users"></param>
        /// <returns>A list of <see cref="User"/> objects if modified</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DbUpdateConcurrencyException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public IEnumerable<User> UpdateRange(IEnumerable<User> users) => myRepository.UpdateRange(users);

        #endregion Update Section
    }
}