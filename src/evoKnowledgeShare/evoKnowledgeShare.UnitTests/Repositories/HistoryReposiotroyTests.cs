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
    public class HistoryReposiotroyTests: RepositoryTestBase<History>
    {
        private History[] myHistories = default!;

        [SetUp]
        public void SetUp()
        {
            myRepository = new HistoryRepository(myDbContext);

            myHistories = new History[]
            {
                new History(Guid.NewGuid(), "Activity param", new DateTimeOffset(),"0.1", Guid.NewGuid(), "PK001"),
                new History(Guid.NewGuid(), "Activity param", new DateTimeOffset(),"0.1", Guid.NewGuid(), "PK001"),
                new History(Guid.NewGuid(), "Activity param", new DateTimeOffset(),"0.1", Guid.NewGuid(), "PK001"),
                new History(Guid.NewGuid(), "Activity param", new DateTimeOffset(),"0.1", Guid.NewGuid(), "PK001")
            };

            myDbContext.Histories.Add(myHistories[0]);
            myDbContext.Histories.Add(myHistories[1]);
            myDbContext.SaveChanges();
        }

        [Test]
        public async Task HistoryRepository_GetAll_ShouldReturnAll()
        {
            var entities = myRepository.GetAll();


            Assert.That(entities.Contains(myHistories[0]));
            Assert.That(entities.Contains(myHistories[1]));
            Assert.That(!entities.Contains(myHistories[2]));
        }

        [Test]
        public async Task HistoryRepository_GetById_ShouldReturnEntityById()
        {
            var enityById = myRepository.GetById(myHistories[0].Id);

            Assert.That(Equals(enityById, myHistories[0]));
        }

        [Test]
        public async Task HistoryRepository_AddAsync_ShouldAdd()
        {
            var entity = myHistories[2];

            var createdEntity = await myRepository.AddAsync(entity);

            Assert.That(Equals(entity.Id, createdEntity.Id));
            Assert.That(myDbContext.Histories, Does.Contain(entity));
        }
        
        [Test]
        public void HistoryRepository_AddAsync_ShouldReturnWithArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await myRepository.AddAsync(myHistories[0]);
            });
        }
        
        [Test]
        public async Task HistoryRepository_AddRangeAsync_ShouldAddNotesToRepository()
        {
            IEnumerable<History> expectedNotes = new[] { myHistories[2], myHistories[3] };

            var actualNotes = await myRepository.AddRangeAsync(expectedNotes);
            myDbContext.SaveChanges();

            Assert.Multiple(() =>
            {
                Assert.That(actualNotes, Does.Contain(myHistories[2]));
                Assert.That(actualNotes, Does.Contain(myHistories[3]));
            });
        }
        
        [Test]
        public void HistoryRepository_AddRangeAsync_ShouldReturnWithArgumentException()
        {
            IEnumerable<History> expectedHistories = new[] { myHistories[0], myHistories[1] };
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await myRepository.AddRangeAsync(expectedHistories);
            });
        } 
    }
}
