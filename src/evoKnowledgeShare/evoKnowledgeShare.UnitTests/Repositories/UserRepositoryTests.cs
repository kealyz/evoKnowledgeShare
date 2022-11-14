using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.UnitTests.Repositories
{
    [TestFixture]
    public class UserRepositoryTests
    {
        UserRepository myRepository;
        EvoKnowledgeDbContext myDbContext;

        [SetUp]
        public void Setup()
        {
            DbContextOptions dbContextOptions = new DbContextOptionsBuilder<EvoKnowledgeDbContext>()
                .UseInMemoryDatabase("InMemoryDB").Options;

            myDbContext = new EvoKnowledgeDbContext(dbContextOptions);
            myRepository = new UserRepository(myDbContext);
        }

        [TearDown]
        public void TearDown()
        {
            myDbContext.Database.EnsureDeleted();
            myDbContext.Dispose();
        }

        [Test]
        public async Task UserRepository_AddAsync_DefaultPositive()
        {
            var user = new User(1, "Lajos", "Lali", "L");

            await myRepository.AddAsync(user);
            await myRepository.SaveChangesAsync();
            Assert.That(myDbContext.Users.Count(), Is.EqualTo(1));

        }

        [Test]
        public void UserRepository_AddRange_DefaultPositives()
        {
            var users = new List<User> { new User(1, "Lajos", "Lali", "l"), new User(2, "Steven", "Steve", "S") };

            myRepository.AddRange(users);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(2));

        }

        [Test]
        public async Task UserRepository_AddRangeAsync_DefaultPositives()
        {
            var users = new List<User> { new User(1, "Lajos", "Lali", "l"), new User(2, "Steven", "Steve", "S") };

            await myRepository.AddRangeAsync(users);
            await myRepository.SaveChangesAsync();

            Assert.That(myDbContext.Users.Count, Is.EqualTo(2));
        }

        [Test]
        public void UserRepository_GetAll_ReturnAllUsers()
        {
            var users = new List<User> { new User(1, "Lajos", "Lali", "l"), new User(2, "Steven", "Steve", "S") };
            myRepository.AddRange(users);
            myRepository.SaveChanges();

            var expectedUsers = myRepository.GetAll();

            Assert.That(expectedUsers.Count(), Is.EqualTo(2));
            for (int i = 0; i < users.Count(); i++)
            {
                Assert.That(myDbContext.Users, Does.Contain(users[i]));
            }
        }

        [Test]
        public async Task UserRepository_GetById_ReturnSpecificUser()
        {
            int id = 1;
            var user = new User(id, "Lajos", "Lali", "l");
            await myRepository.AddAsync(user);
            myRepository.SaveChanges();

            var expectedUser = myRepository.GetById(id);

            Assert.That(myDbContext.Users.First(), Is.EqualTo(expectedUser));
        }

        [Test]
        public void UserRepository_GetRangeById_ReturnSpecificUsers()
        {
            List<int> ids = new List<int>() { 1, 2 };
            var users = new List<User> { new User(1, "Lajos", "Lali", "l"), new User(2, "Steven", "Steve", "S") };
            myRepository.AddRange(users);
            myRepository.SaveChanges();

            var expectedUsers = myRepository.GetRangeById(ids);

            Assert.That(expectedUsers, Does.Contain(users[0]));
            Assert.That(expectedUsers, Does.Contain(users[1]));
        }

        /*[Test]
        public async Task UserRepository_GetRangeByIdAsync_ReturnSpecificUsers()
        {
            List<int> ids = new List<int>() { 1, 2 };
            var users = new List<User> { new User(1, "Lajos", "Lali", "l"), new User(2, "Steven", "Steve", "S") };
            myRepository.AddRange(users);
            myRepository.SaveChanges();

            var expectedUsers = await myRepository.GetRangeByIdAsync(ids);

            Assert.That(expectedUsers, Does.Contain(users[0]));
            Assert.That(expectedUsers, Does.Contain(users[1]));
        }*/

        [Test]
        public async Task UserRepository_Remove_ShouldRemoveSpecificUser()
        {
            var user = new User(1, "Lajos", "Lali", "l");
            await myRepository.AddAsync(user);
            myRepository.SaveChanges();

            myRepository.Remove(user);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task UserRepository_RemoveById_ShouldRemoveSpecificUserById()
        {
            int id = 1;
            var user = new User(id, "Lajos", "Lali", "l");
            await myRepository.AddAsync(user);
            myRepository.SaveChanges();

            myRepository.RemoveById(id);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(0));
        }

        [Test]
        public void UserRepository_RemoveRange_ShouldRemoveSpecificUsers()
        {
            var users = new List<User> { new User(1, "Lajos", "Lali", "l"), new User(2, "Steven", "Steve", "S") };
            myRepository.AddRange(users);
            myRepository.SaveChanges();

            myRepository.RemoveRange(users);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(0));
        }

        [Test]
        public void UserRepository_RemoveRangeById_ShouldRemoveSpecificUsers()
        {
            List<int> ids = new List<int>() { 1, 2 }; 
            var users = new List<User> { new User(1, "Lajos", "Lali", "l"), new User(2, "Steven", "Steve", "S") };
            myRepository.AddRange(users);
            myRepository.SaveChanges();

            myRepository.RemoveRangeById(ids);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task UserRepository_Update_ShouldUpdateSpecificUser()
        {
            var user = new User(1, "Lajos", "Lali", "l");
            await myRepository.AddAsync(user);
            myRepository.SaveChanges();
            string newFirstName = "Lewis";
            user.FirstName = newFirstName;

            myRepository.Update(user);
            myRepository.SaveChanges();
            var updatedUser = myRepository.GetById(1);

            Assert.That(updatedUser.FirstName, Is.EqualTo(newFirstName));
        }

        [Test]
        public void UserRepository_UpdateRange_ShouldUpdateRange()
        {
            var users = new List<User> { new User(1, "Lajos", "Lali", "l"), new User(2, "Steven", "Steve", "S") };
            myRepository.AddRange(users);
            myRepository.SaveChanges();
            string newLastName = "W";

            for(int i = 0; i < users.Count; i++)
            {
                users[i].LastName = newLastName;
            }
            myRepository.UpdateRange(users);
            myRepository.SaveChanges();

            for (int i = 0; i < users.Count; i++)
            {
                Assert.That(myDbContext.Users, Does.Contain(users[i]));
            }
        }
    }
}
