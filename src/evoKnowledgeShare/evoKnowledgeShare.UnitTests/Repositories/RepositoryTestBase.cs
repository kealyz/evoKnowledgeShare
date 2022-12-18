using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.UnitTests.Repositories
{
    [TestFixture]
    public abstract class RepositoryTestBase<TEntity> where TEntity : class
    {
        protected EvoKnowledgeDbContext myDbContext = default!;

        protected Repository<TEntity> myRepository = default!;

        private readonly DbContextOptions myDbContextOptions = new DbContextOptionsBuilder<EvoKnowledgeDbContext>()
            .UseInMemoryDatabase("InMemoryDB").Options;

        [SetUp]
        public void SetUp()
        {
            myDbContext = new EvoKnowledgeDbContext(myDbContextOptions);
        }

        [TearDown]
        public void TearDown()
        {
            myDbContext.Database.EnsureDeleted();
            myDbContext.Dispose();
        }
    }
}