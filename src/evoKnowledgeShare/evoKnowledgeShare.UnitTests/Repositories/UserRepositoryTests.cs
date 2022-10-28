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

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            DbContextOptions dbContextOptions = new DbContextOptionsBuilder<EvoKnowledgeDbContext>()
                .UseInMemoryDatabase("InMemoryDB").Options;

            myDbContext = new EvoKnowledgeDbContext(dbContextOptions);
            myRepository = new UserRepository(myDbContext);
        }


        [Test]
        public void Repository_Add_DefaultPositive()
        {
            var user = new User(1, "Lajos", "Lali", "L");

            myRepository.Add(user);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Users.Count(), Is.EqualTo(1));
        }
    }
}
