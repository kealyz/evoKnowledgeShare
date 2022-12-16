using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

namespace evoKnowledgeShare.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService myUserService;
        private Mock<IRepository<User>> myRepositoryMock;
        private List<User> myUsers;

        [SetUp]
        public void Setup()
        {
            myUsers = new List<User>()
            {
                new User(Guid.NewGuid(),"TestUser","User","UserLastName"),
                new User(Guid.NewGuid(),"TestUser2","User2","UserLastName2"),
                new User(Guid.NewGuid(), "Helo", "szia", "")
            };

            myRepositoryMock = new Mock<IRepository<User>>(MockBehavior.Strict);
            myUserService = new UserService(myRepositoryMock.Object);
            myRepositoryMock.Setup(x => x.GetAll()).Returns(myUsers);
            myRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(myUsers[0]);

        }

        #region Get Test Section

        [Test]
        public void UserService_GetAllUsers_ShouldReturnAll()
        {
            var users = myUserService.Get();

            Assert.That(users, Is.EquivalentTo(myUsers));
        }

        [Test]
        public void UserService_GetUserById_ShouldReturnSpecificUserById()
        {
            User user = myUserService.GetUserById(myUsers[0].Id);

            Assert.That(user, Is.EqualTo(myUsers[0]));
        }

        [Test]
        public void UserService_GetUserById_ShouldThrowArgumentException()
        {
            myRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Throws<KeyNotFoundException>();

            Assert.Throws<KeyNotFoundException>(() => myUserService.GetUserById(myUsers[0].Id));
        }

        [Test]
        public void UserService_GetUserByUserName_ShouldReturnSpecificUserByUserName()
        {
            User? user = myUserService.GetUserByUserName(myUsers[0].UserName);

            Assert.That(user, Is.EqualTo(myUsers[0]));
        }

        [Test]
        public void UserService_GetUserByUserName_ShouldThrowArgumentException()
        {
            myRepositoryMock.Setup(x => x.GetAll()).Throws<KeyNotFoundException>();

            Assert.Throws<KeyNotFoundException>(() => myUserService.GetUserByUserName(myUsers[0].UserName));
        }
        #endregion Get Test Section

        #region Add Test Section

        [Test]
        public async Task UserService_CreateUserAsync_CreatesNewUser()
        {
            User user = new User(Guid.NewGuid(), "Lajos", "Lali", "L");
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

            User actualUser = await myUserService.CreateUserAsync(user);

            myRepositoryMock.Verify(x => x.AddAsync(It.Is<User>(y => y.Equals(user))), Times.Once);
            Assert.That(actualUser, Is.EqualTo(user));
        }

        [Test]
        public async Task UserService_CreateUserAsync_ShouldThrowArgumentException()
        {
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).Throws<ArgumentException>();

            Assert.ThrowsAsync<ArgumentException>(async()=>
            {
                await myUserService.CreateUserAsync(myUsers[0]);
            });
        }

        #endregion Add Test Section

        #region Remove Test Section

        [Test]
        public void UserService_Remove_ShouldRemoveSpecificUser()
        {
            myRepositoryMock.Setup(x => x.Remove(It.IsAny<User>()));

            myUserService.Remove(myUsers[0]);

            myRepositoryMock.Verify(x => x.Remove(It.Is<User>(y => y.Equals(myUsers[0]))), Times.Once);
        }

        [Test]
        public void UserService_Remove_ShouldThrowKeyNotFoundException()
        {
            myRepositoryMock.Setup(x => x.Remove(It.IsAny<User>())).Throws<KeyNotFoundException>();

            Assert.Throws<KeyNotFoundException>(() => myUserService.Remove(myUsers[0]));
        }

        [Test]
        public void UserService_RemoveUserById_ShouldRemoveSpecificUserById()
        {
            myRepositoryMock.Setup(x => x.RemoveById(It.IsAny<Guid>()));

            myUserService.RemoveUserById(myUsers[0].Id);

            myRepositoryMock.Verify(x => x.RemoveById(It.Is<Guid>(y => y.Equals(myUsers[0].Id))), Times.Once);
        }
        
        [Test]
        public void UserService_RemoveUserById_ShouldThrowKeyNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            myRepositoryMock.Setup(x => x.RemoveById(It.IsAny<Guid>())).Throws<KeyNotFoundException>();

            Assert.Throws<KeyNotFoundException>(() => myUserService.RemoveUserById(guid));
        }
        
        #endregion Remove Test Section

        #region Update Test Section

        [Test]
        public void UserService_UpdateUser_ShouldUpdate()
        {
            User user = new User(myUsers[0].Id, "Géza", myUsers[0].FirstName, myUsers[0].LastName);
            myRepositoryMock.Setup(x => x.Update(It.IsAny<User>())).Returns(user);

            User actualUser = myUserService.Update(user);

            myRepositoryMock.Verify(x => x.Update(It.Is<User>(y => y.Equals(user))), Times.Once);
            Assert.That(actualUser, Is.EqualTo(user));
        }

        [Test]
        public void UserService_Update_ShouldThrowKeyNotFoundException()
        {
            myRepositoryMock.Setup(x => x.Update(It.IsAny<User>())).Throws<KeyNotFoundException>();

            Assert.Throws<KeyNotFoundException>(() => myUserService.Update(myUsers[0]));
        }

        [Test]
        public void UserService_UpdateRange_ShouldUpdateSpecificUsers()
        {
            IEnumerable<User> users = new List<User> { myUsers[0], myUsers[1] };
            users.ElementAt(0).UserName = "Géza";
            users.ElementAt(1).UserName = "Géza";
            myRepositoryMock.Setup(x => x.UpdateRange(It.IsAny<IEnumerable<User>>())).Returns(users);

            IEnumerable<User> actualUsers = myUserService.UpdateRange(users);

            myRepositoryMock.Verify(x => x.UpdateRange(It.Is<IEnumerable<User>>(y => y.Equals(users))), Times.Once);
            int i = 0;
            foreach(var user in actualUsers)
            {
                Assert.That(user, Is.EqualTo(users.ElementAt(i)));
                i++;
            }
        }

        [Test]
        public void UserService_UpdateRange_ShouldThrowKeyNotFoundException()
        {
            var usersList = new List<User>()
            {
                myUsers[0],
                myUsers[1]
            };
            myRepositoryMock.Setup(x => x.UpdateRange(It.IsAny<List<User>>())).Throws<KeyNotFoundException>();
            Assert.Throws<KeyNotFoundException>(() => myUserService.UpdateRange(usersList));
        }

        #endregion Update Test Section
    }
}