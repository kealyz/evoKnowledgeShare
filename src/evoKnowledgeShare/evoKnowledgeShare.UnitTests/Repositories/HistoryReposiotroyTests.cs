using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evoKnowledgeShare.UnitTests.Repositories
{
    public class HistoryReposiotroyTests
    {
        HistoryRepository myRepository = default!;
        EvoKnowledgeDbContext myDbContext = default!;

        [SetUp]
        public void SetUp()
        {
            DbContextOptions dbContextOptions = new DbContextOptionsBuilder<EvoKnowledgeDbContext>()
               .UseInMemoryDatabase($"InMemoryDB").Options;

            myDbContext = new EvoKnowledgeDbContext(dbContextOptions);
            myRepository = new HistoryRepository(myDbContext);

        }

        [Test]
        public async Task HistoryRepository_GetAll_ShouldReturnAll()
        {
            var entity = new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"), "Activity param", new DateTimeOffset(),
                "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001");
            await myRepository.AddAsync(entity);

            var entities = myRepository.GetAll();

            Assert.That(entities.Contains(entity));
        }

        [Test]
        public async Task HistoryRepository_AddAsync_ShouldAdd()
        {
            var entity = new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"), "Activity param", new DateTimeOffset(),
                "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001");

            var createdEntity = await myRepository.AddAsync(entity);

            Assert.That(myDbContext.Histories.Count(), Is.EqualTo(1));
            Assert.That(Equals(entity.Id, createdEntity.Id));
            Assert.That(myDbContext.Histories, Does.Contain(entity));
        }

        [Test]
        public async Task HistoryRepository_GetById_ShouldReturnEntityById()
        {
            var entity = new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"), "Activity param", new DateTimeOffset(),
                "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001");
            await myRepository.AddAsync(entity);

            var enityById = myRepository.GetById(entity.Id);

            Assert.That(Equals(enityById, entity));
        }
    }
}
