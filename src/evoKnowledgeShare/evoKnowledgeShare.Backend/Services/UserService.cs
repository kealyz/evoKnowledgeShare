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

        public User GetNoteById(int id) => myRepository.GetAll().Where(x => x.Id == id).First();

        public async Task AddAsync(User user){}

        public async Task CreateUserAsync(User user)
        {
            await myRepository.AddAsync(user);
        }
        public void RemoveUser(User user)
        {
            myRepository.Remove(user);
        }
        public async Task CreateUserAsync(User user) => await myRepository.AddAsync(user);
    }
}