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

        public async Task<IEnumerable<User>> GetAsync() => await myRepository.GetAllAsync();

        public User GetUserById(int id) => myRepository.GetAll().Where(x => x.Id == id).First();

        public void CreateUser(User user) => myRepository.Add(user);

        public async Task CreateUserAsync(User user) => await myRepository.AddAsync(user);
        
        public void RemoveUser(User user) => myRepository.Remove(user);

        public void RemoveUserById(int id) => myRepository.RemoveById(id);

        public void Update(User user) => myRepository.Update(user);

    }
}