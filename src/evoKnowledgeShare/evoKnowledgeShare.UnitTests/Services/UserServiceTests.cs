using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

namespace evoKnowledgeShare.UnitTests.Services
{
    public class UserServiceTests
    {
        private UserService myUserService;
        private Mock<IRepository<User>> myRepositoryMock;
        readonly System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        [SetUp]
        public void Setup()
        {
            myRepositoryMock = new Mock<IRepository<User>>(MockBehavior.Strict);
            myUserService = new UserService(myRepositoryMock.Object);
            myRepositoryMock.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<User>()
                {
                    new User(1,"TestUser","User","UserLastName"),
                    new User(2,"TestUser2","User2","UserLastName2")
                };
            });

            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(() =>
            {
                return new List<User>()
                {
                    new User(1,"TestUser","User","UserLastName"),
                    new User(2,"TestUser2","User2","UserLastName2")
                };
            });

            myRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(() =>
            {
                return new User(1, "Helo", "szia", "");
            });
        }

        [Test]
        public void GetAllUsers_ShouldReturnAll()
        {
            // Arrange

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

        [Test]
        public async Task UserService_CreateUserAsync_CreatesNewUser()
        {
            var user = new User(1, "Lajos", "Lali", "L");
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            await myUserService.CreateUserAsync(user);

            myRepositoryMock.Verify(x => x.AddAsync(It.Is<User>(y => y.Equals(user))), Times.Once);
        }
    }
}