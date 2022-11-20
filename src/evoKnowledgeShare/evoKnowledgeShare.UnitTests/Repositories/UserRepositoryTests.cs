using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;

namespace evoKnowledgeShare.UnitTests.Repositories
{
    [TestFixture]
    public class UserRepositoryTests : RepositoryTestBase<User>
    {
        private List<User> myUsers;

        [SetUp]
        public void Setup()
        {
            myRepository = new UserRepository(myDbContext);
            myUsers = new List<User>()
            {
                new User(Guid.NewGuid(),"TestUser","User","UserLastName"),
                new User(Guid.NewGuid(),"TestUser2","User2","UserLastName2"),
                new User(Guid.NewGuid(), "Helo", "szia", "")
            };
            myRepository.AddRangeAsync(myUsers);
        }

        #region Add Test Section

        [Test]
        public async Task UserRepository_AddAsync_ShouldAddAUserAsync()
        {
            User user = new User(Guid.NewGuid(), "TestUser3", "User3", "UserLastName3");

            await myRepository.AddAsync(user);

            Assert.That(myDbContext.Users.Count, Is.EqualTo(4));
            Assert.That(myDbContext.Users, Does.Contain(user));
        }

        [Test]
        public async Task UserRepository_AddRangeAsync_ShouldAddARangeOfUsersAsync()
        {
            IEnumerable<User> users = new List<User>() { 
                new User(Guid.NewGuid(), "TestUser3", "User3", "UserLastName3"), 
                new User(Guid.NewGuid(), "TestUser4", "User4", "UserLastName4") };

            await myRepository.AddRangeAsync(users);

            Assert.That(myDbContext.Users.Count, Is.EqualTo(5));
            CollectionAssert.IsSupersetOf(myDbContext.Users, users);
        }

        #endregion Add Test Section

        #region Get Test Section

        [Test]
        public void UserRepository_GetAll_ReturnAllUsers()
        {
            IEnumerable<User> expectedUsers = myRepository.GetAll();

            Assert.That(expectedUsers.Count, Is.EqualTo(3));
            CollectionAssert.AreEquivalent(myDbContext.Users, expectedUsers);
        }

        [Test]
        public void UserRepository_GetById_ReturnSpecificUserById()
        {
            Assert.That(myDbContext.Users, Does.Contain(myRepository.GetById(myUsers[0].Id)));
        }

        [Test]
        public void UserRepository_GetById_ShouldThrowInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => myRepository.GetById(Guid.NewGuid()));
        }
        
        [Test]
        public void UserRepository_GetRangeById_ReturnSpecificUsersByARangeOfId()
        {
            IEnumerable<User> expectedUsers = myRepository.GetRangeById(myUsers.Select(x => x.Id).Take(2));

            Assert.That(expectedUsers.Count, Is.EqualTo(2));
            CollectionAssert.IsSupersetOf(myDbContext.Users, expectedUsers);
            Assert.That(expectedUsers, !Does.Contain(myUsers[2]));
        }

        #endregion Get Test Section

        #region Remove Test Section

        [Test]
        public void UserRepository_Remove_ShouldRemoveSpecificUser()
        {
            myRepository.Remove(myUsers[1]);

            Assert.That(myDbContext.Users.Count, Is.EqualTo(2));
            Assert.That(myDbContext.Users, !Does.Contain(myUsers[1]));
        }
        
        [Test]
        public void UserRepository_RemoveById_ShouldRemoveSpecificUserById()
        {
            myRepository.RemoveById(myUsers[1].Id);

            Assert.That(myDbContext.Users.Count, Is.EqualTo(2));
            Assert.That(myDbContext.Users, !Does.Contain(myUsers[1]));

        }

        [Test]
        public void UserRepository_RemoveById_ShouldThrowKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => myRepository.RemoveById(Guid.NewGuid()));
        }

        [Test]
        public void UserRepository_RemoveRange_ShouldRemoveSpecificUsers()
        {
            IEnumerable<User> users = myUsers.Take(2);
            myRepository.RemoveRange(users);

            Assert.That(myDbContext.Users.Count, Is.EqualTo(1));
            CollectionAssert.IsNotSupersetOf(myDbContext.Users, users);
        }
        
        [Test]
        public void UserRepository_RemoveRangeById_ShouldRemoveSpecificUsers()
        {
            IEnumerable<Guid> ids = myUsers.Select(x => x.Id).Take(2);

            myRepository.RemoveRangeById(ids);

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(1));
            Assert.That(myDbContext.Users, !Does.Contain(myUsers.Where(x => ids.Any(id => x.Id == id))));
        }

        #endregion Remove Test Section

        #region Update Test Section

        [Test]
        public void UserRepository_Update_ShouldUpdateSpecificUser()
        {
            myUsers[0].UserName = "Admin";

            myRepository.Update(myUsers[0]);

            Assert.That(myDbContext.Users, Does.Contain(myUsers[0]));
        }

        [Test]
        public void UserRepository_UpdateRange_ShouldUpdateRange()
        {
            int i = 0;
            foreach(var user in myUsers)
            {
                user.UserName = "Admin" + i;
                i++;
            }

            myRepository.UpdateRange(myUsers);

            CollectionAssert.AreEquivalent(myDbContext.Users, myUsers);
        }

        #endregion Update Test Section
    }
}
