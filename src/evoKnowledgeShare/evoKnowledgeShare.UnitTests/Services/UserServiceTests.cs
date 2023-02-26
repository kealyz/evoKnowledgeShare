using evoKnowledgeShare.Backend.DTO;
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
                new User(Guid.NewGuid(), "Helo", "szia", "A")
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
            var users = myUserService.GetAll();

            Assert.That(users, Is.EquivalentTo(myUsers));
        }

        [Test]
        public void UserService_GetUserById_ShouldReturnSpecificUserById()
        {
            User user = myUserService.GetUserById(myUsers[0].Id);

            Assert.That(user, Is.EqualTo(myUsers[0]));
        }

        [Test]
        public void UserService_GetUserById_ShouldThrowKeyNotFoundException()
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
        public void UserService_GetUserByUserName_ShouldThrowKeyNotFoundException()
        {
            myRepositoryMock.Setup(x => x.GetAll()).Throws<KeyNotFoundException>();

            Assert.Throws<KeyNotFoundException>(() => myUserService.GetUserByUserName(myUsers[0].UserName));
        }
        #endregion Get Test Section

        #region Add Test Section

        [Test]
        public async Task UserService_CreateUserAsync_CreatesNewUser()
        {
            UserDTO userDTOToBeAdded = new UserDTO(myUsers[0].UserName, myUsers[0].FirstName, myUsers[0].LastName);
            User userToBeAdded = new User(userDTOToBeAdded);
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(userToBeAdded);

            User actualUser = await myUserService.CreateUserAsync(userDTOToBeAdded);

            Assert.That(actualUser, Is.EqualTo(userToBeAdded));
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
        public async Task UserService_CreateUserAsync_ShouldThrowArgumentNullException()
        {
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).Throws<ArgumentNullException>();

            Assert.ThrowsAsync<ArgumentNullException>(() => myUserService.CreateUserAsync(new UserDTO("", "", "")));
        }

        #endregion Add Test Section

    }
}