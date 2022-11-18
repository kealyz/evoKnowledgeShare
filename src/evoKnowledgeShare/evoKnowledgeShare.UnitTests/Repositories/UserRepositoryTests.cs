using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using evoKnowledgeShare.Backend.Services;
using System.Collections.Generic;

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
        }

        #region Add Test Section

        [Test]
        public async Task UserRepository_AddAsync_ShouldAddAUserAsync()
        {
            await myRepository.AddAsync(myUsers[0]);
            await myRepository.SaveChangesAsync();

            var expectedUser = myRepository.GetById(myUsers[0].Id);
            Assert.That(myDbContext.Users.Count(), Is.EqualTo(1));
            Assert.That(myDbContext.Users, Does.Contain(expectedUser));
            Assert.That(Equals(myUsers[0], expectedUser));
        }

        [Test]
        public async Task UserRepository_AddRangeAsync_ShouldAddARangeOfUsersAsync()
        {
            await myRepository.AddRangeAsync(myUsers);
            await myRepository.SaveChangesAsync();

            var expectedUsers = myRepository.GetAll();
            Assert.That(myDbContext.Users.Count(), Is.EqualTo(3));
            for (int i = 0; i < myUsers.Count(); i++)
            {
                Assert.That(myDbContext.Users, Does.Contain(expectedUsers.ElementAt(i)));
                Assert.That(Equals(myUsers[i], expectedUsers.ElementAt(i)));
            }
        }

        #endregion Add Test Section

        #region Get Test Section

        [Test]
        public void UserRepository_GetAll_ReturnAllUsers()
        {
            myRepository.AddRangeAsync(myUsers);
            myRepository.SaveChanges();

            var expectedUsers = myRepository.GetAll();

            Assert.That(expectedUsers.Count(), Is.EqualTo(3));
            for (int i = 0; i < 3; i++)
            {
                Assert.That(myDbContext.Users, Does.Contain(expectedUsers.ElementAt(i)));
            }
        }

        [Test]
        public void UserRepository_GetById_ReturnSpecificUserById()
        {
            myRepository.AddAsync(myUsers[0]);
            myRepository.SaveChanges();

            Guid expectedId = myUsers[0].Id;
            var expectedUser = myRepository.GetById(expectedId);

            Assert.That(myDbContext.Users, Does.Contain(expectedUser));
        }
        
        [Test]
        public void UserRepository_GetRangeById_ReturnSpecificUsersByARangeOfId()
        {
            myRepository.AddRangeAsync(myUsers);
            myRepository.SaveChanges();

            IEnumerable<Guid> ids = new List<Guid>() { myUsers[0].Id, myUsers[1].Id, myUsers[2].Id }; 

            var expectedUsers = myRepository.GetRangeById(ids);

            Assert.That(expectedUsers.Count(), Is.EqualTo(3));
            foreach(User user in expectedUsers)
            {
                Assert.That(myDbContext.Users, Does.Contain(user));
            }
        }

        #endregion Get Test Section

        #region Remove Test Section

        [Test]
        public void UserRepository_Remove_ShouldRemoveSpecificUser()
        {
            myRepository.AddAsync(myUsers[0]);
            myRepository.SaveChanges();

            myRepository.Remove(myUsers[0]);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(0));
        }
        
        [Test]
        public void UserRepository_RemoveById_ShouldRemoveSpecificUserById()
        {
            Guid guid = myUsers[0].Id;
            myRepository.AddRangeAsync(myUsers);
            myRepository.SaveChanges();

            myRepository.RemoveById(guid);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users, !Does.Contain(myUsers[0]));
            Assert.That(myDbContext.Users.Count, Is.EqualTo(2));
        }

        [Test]
        public void UserRepository_RemoveById_ShouldThrowKeyNotFoundException()
        {
            Guid guid = Guid.NewGuid();
            myRepository.AddRangeAsync(myUsers);
            myRepository.SaveChanges();

            Assert.Throws<KeyNotFoundException>(() => myRepository.RemoveById(guid));
        }

        [Test]
        public void UserRepository_RemoveRange_ShouldRemoveSpecificUsers()
        {
            myRepository.AddRangeAsync(myUsers);
            myRepository.SaveChanges();

            myRepository.RemoveRange(myUsers);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(0));
        }
        
        [Test]
        public void UserRepository_RemoveRangeById_ShouldRemoveSpecificUsers()
        {
            IEnumerable<Guid> ids = new List<Guid>() { myUsers[0].Id, myUsers[1].Id };
            myRepository.AddRangeAsync(myUsers);
            myRepository.SaveChanges();

            myRepository.RemoveRangeById(ids);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(1));
            Assert.That(myDbContext.Users, Does.Contain(myUsers[2]));
        }

        #endregion Remove Test Section

        #region Update Test Section

        [Test]
        public void UserRepository_Update_ShouldUpdateSpecificUser()
        {
            myRepository.AddAsync(myUsers[0]);
            myRepository.SaveChanges();
            string newUserName = "UserTest";
            myUsers[0].UserName = newUserName;

            myRepository.Update(myUsers[0]);
            myRepository.SaveChanges();
            var updatedUser = myRepository.GetById(myUsers[0].Id);

            Assert.That(updatedUser.UserName, Is.EqualTo(newUserName));
        }

        [Test]
        public void UserRepository_UpdateRange_ShouldUpdateRange()
        {
            myRepository.AddRangeAsync(myUsers);
            myRepository.SaveChanges();
            string newUserName = "UserTest";

            foreach(var user in myUsers)
            {
                user.UserName = newUserName;
            }
            myRepository.UpdateRange(myUsers);
            myRepository.SaveChanges();

            foreach (var user in myUsers)
            {
                Assert.That(myDbContext.Users, Does.Contain(user));
            }
        }

        #endregion Update Test Section
    }
}
