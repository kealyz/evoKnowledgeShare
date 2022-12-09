using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using System.Collections;

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

            Assert.That(myDbContext.Users.Count, Is.EqualTo(myUsers.Count()+1));
            Assert.That(myDbContext.Users, Does.Contain(user));
        }

        [Test]
        public async Task UserRepository_AddRangeAsync_ShouldAddARangeOfUsersAsync()
        {
            IEnumerable<User> users = new List<User>() { 
                new User(Guid.NewGuid(), "TestUser3", "User3", "UserLastName3"), 
                new User(Guid.NewGuid(), "TestUser4", "User4", "UserLastName4") };

            await myRepository.AddRangeAsync(users);

            Assert.That(myDbContext.Users.Count, Is.EqualTo(myUsers.Count()+2));
            CollectionAssert.IsSupersetOf(myDbContext.Users, users);
        }

        #endregion Add Test Section

        #region Get Test Section

        [Test]
        public void UserRepository_GetAll_ReturnAllUsers()
        {
            IEnumerable<User> actualUsers = myRepository.GetAll();

            Assert.That(actualUsers.Count, Is.EqualTo(myDbContext.Users.Count()));
            CollectionAssert.AreEquivalent(myDbContext.Users, actualUsers);
        }

        [Test]
        public void UserRepository_GetById_ReturnSpecificUserById()
        {
            User user = myRepository.GetById(myUsers[0].Id);

            Assert.That(myDbContext.Users, Does.Contain(user));
        }

        [Test]
        public void UserRepository_GetById_ShouldThrowInvalidOperationException()
        {
            Assert.Throws<KeyNotFoundException>(() => myRepository.GetById(Guid.NewGuid()));
        }
        
        [Test]
        public void UserRepository_GetRangeById_ReturnSpecificUsersByARangeOfId()
        {
            IEnumerable<User> actualUsers = myRepository.GetRangeById(myUsers.Select(x => x.Id).Take(2));

            Assert.That(actualUsers.Count, Is.EqualTo(myDbContext.Users.Count()-1));
            CollectionAssert.IsSupersetOf(myDbContext.Users, actualUsers);
        }

        #endregion Get Test Section

        #region Remove Test Section

        [Test]
        public void UserRepository_Remove_ShouldRemoveSpecificUser()
        {
            myRepository.Remove(myUsers[1]);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count, Is.EqualTo(myUsers.Count()-1));
            CollectionAssert.DoesNotContain(myDbContext.Users, myUsers[1]);
        }
        
        [Test]
        public void UserRepository_RemoveById_ShouldRemoveSpecificUserById()
        {
            myRepository.RemoveById(myUsers[1].Id);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count, Is.EqualTo(myUsers.Count()-1));
            CollectionAssert.DoesNotContain(myDbContext.Users, myUsers[1]);

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
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count, Is.EqualTo(myUsers.Count()-2));
            CollectionAssert.IsNotSupersetOf(myDbContext.Users, users);
        }
        
        [Test]
        public void UserRepository_RemoveRangeById_ShouldRemoveSpecificUsers()
        {
            IEnumerable<Guid> ids = myUsers.Select(x => x.Id).Take(2);

            myRepository.RemoveRangeById(ids);
            myDbContext.SaveChanges();

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(myUsers.Count()-2));
            CollectionAssert.DoesNotContain(myDbContext.Users, myUsers.Take(2));
        }

        [Test]
        public void UserRepository_RemoveRangeById_ShouldThrowKeyNotFoundException()
        {
            Guid[] ids = { Guid.NewGuid(), Guid.NewGuid() };
            Assert.Throws<KeyNotFoundException>(() => myRepository.RemoveRangeById(ids));
        }

        #endregion Remove Test Section

        #region Update Test Section

        [Test]
        public void UserRepository_Update_ShouldUpdateSpecificUser()
        {
            myUsers[0].UserName = "Admin";

            myRepository.Update(myUsers[0]);
            myRepository.SaveChanges();

            CollectionAssert.Contains(myDbContext.Users, myUsers[0]);
        }

        [Test]
        public void UserRepository_UpdateRange_ShouldUpdateRange()
        {
            for (int i = 0; i < myUsers.Count; i++)
            {
                myUsers[i].UserName = $"Admin{i}";
            }

            myRepository.UpdateRange(myUsers);
            myRepository.SaveChanges();

            CollectionAssert.AreEquivalent(myDbContext.Users, myUsers);
        }

        #endregion Update Test Section
    }
}
