using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

namespace evoKnowledgeShare.UnitTests
{
    public class UserServiceTests
    {
        private UserService myUserService;
        private readonly Mock<IRepository<User>> myRepositoryMock = new Mock<IRepository<User>>();
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            stopwatch.Start();
            myUserService = new UserService(myRepositoryMock.Object);
            stopwatch.Stop();
            Console.WriteLine("One time setup took {0} ms.", stopwatch.ElapsedMilliseconds);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllUsers_ShouldReturnAll()
        {
            // Arrange
            stopwatch.Restart();
            stopwatch.Start();
            myRepositoryMock.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<User>()
                {
                    new User(1,"TestUser","User","UserLastName"),
                    new User(2,"TestUser2","User2","UserLastName2")
                };
            });
            stopwatch.Stop();
            Console.WriteLine("Mock setup took {0} ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            stopwatch.Start();
            // Action
            var users = myUserService.Get();
            stopwatch.Stop();
            Console.WriteLine("Get users took {0} ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            stopwatch.Start();
            // Assert
            Assert.That(users.Count, Is.EqualTo(2));
            stopwatch.Stop();
            Console.WriteLine("Assertion took {0} ms.", stopwatch.ElapsedMilliseconds);
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnAll()
        {
            // Arrange
            stopwatch.Restart();
            stopwatch.Start();
            myRepositoryMock.Setup(x => x.GetAllAsync()).Returns(() =>
            {
                Task.FromResult(() =>
                {
                    return new List<User>()
                    {
                        new User(1,"TestUser","User","UserLastName"),
                        new User(2,"TestUser2","User2","UserLastName2")
                    };
                });
            });
            stopwatch.Stop();
            Console.WriteLine("Mock setup took {0} ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            stopwatch.Start();
            // Action
            var users = await myUserService.GetAsync();
            stopwatch.Stop();
            Console.WriteLine("Get users took {0} ms.", stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            stopwatch.Start();
            // Assert
            Assert.That(users.Count, Is.EqualTo(2));
            stopwatch.Stop();
            Console.WriteLine("Assertion took {0} ms.", stopwatch.ElapsedMilliseconds);
        }
    }
}