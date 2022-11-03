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

        public async Task CreateUserAsync(User user) => await myRepository.AddAsync(user);
    }
}