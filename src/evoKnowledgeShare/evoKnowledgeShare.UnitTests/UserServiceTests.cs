using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Moq;

namespace evoKnowledgeShare.UnitTests
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
        public void GetUserById_ShouldReturnSpecificUser()
        {
            var userid = myUserService.GetNoteById(1);

            Assert.That(userid.Id.Equals(1));
            
        }

        [Test] 
        public async Task CreateUser_ShouldCreateAnUser()
        {
            var newUser = new User(3, "Test3Admin", "Bot", "AI");

            await myUserService.AddAsync(newUser);

            var users = await myUserService.GetAsync();

            Assert.That(users.Where(x => x.Id == newUser.Id).Equals(newUser));
        }

        [Test]
        public void RemoveUser_ShouldRemoveSpecificUser()
        {
            var specificuser = new User(1, "TestUser", "User", "UserLastName");

            myUserService.RemoveUser(specificuser);

            var users = myUserService.Get();

            Assert.That(users.Any(x => x.Equals(specificuser)), Is.False);
        }

        [Test]
        public void ModifyUsername_ShouldModifyUsername()
        {

        }


    }
}