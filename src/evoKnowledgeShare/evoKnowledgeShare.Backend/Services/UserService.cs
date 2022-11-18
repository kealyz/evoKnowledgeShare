using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;

namespace evoKnowledgeShare.Backend.Services
{
    public class UserService
    {
        private readonly IRepository<User> myRepository;

        public UserService(IRepository<User> repository)
        {
            myRepository = repository;
        }

        public IEnumerable<User> Get() => myRepository.GetAll();

        public User GetUserById(Guid id) => myRepository.GetAll().Where(x => x.Id == id).First();

        public async Task<User> CreateUserAsync(User user)
        {
            await myRepository.AddAsync(user);
            return await Task.FromResult(user);
        }

        public void Remove(User user) => myRepository.Remove(user);

        public void RemoveUserById(Guid id) => myRepository.RemoveById(id);

        public User Update(User user) => myRepository.Update(user);

        public IEnumerable<User> UpdateRange(IEnumerable<User> users) => myRepository.UpdateRange(users);

    }
}