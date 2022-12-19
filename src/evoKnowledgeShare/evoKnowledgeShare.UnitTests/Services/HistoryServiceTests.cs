using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

namespace evoKnowledgeShare.UnitTests.Services
{
    public class HistoryServiceTests
    {
        private HistoryService myHistoryService;
        private Mock<IRepository<History>> myRepositoryMock;

        [SetUp]
        public void Setup()
        {
            myRepositoryMock = new Mock<IRepository<History>>(MockBehavior.Strict);
            myHistoryService = new HistoryService(myRepositoryMock.Object);
        }

        [Test]
        public void HistoryService_GetAll_ShouldReturnAll()
        {
            var entity = new History(Guid.NewGuid(), "Activity param", new DateTimeOffset(),
                "0.1", Guid.NewGuid(), "PK001");
            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<History> { entity });

            var historyEntities = myHistoryService.GetAll();

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(historyEntities.Count, Is.EqualTo(1));
            Assert.That(historyEntities.First, Is.EqualTo(entity));
        }

        [Test]
        public void HistoryService_GetById_ShouldReturnSpecificHistory()
        {
            var entity = new History(Guid.NewGuid(), "Activity param", new DateTimeOffset(),
                "0.1", Guid.NewGuid(), "PK001");
            myRepositoryMock.Setup(x => x.GetById(entity.Id)).Returns(entity);

            var historyEntityById = myHistoryService.GetById(entity.Id);

            myRepositoryMock.Verify(x => x.GetById(entity.Id), Times.Once);
            Assert.That(historyEntityById, Is.Not.Null);
            Assert.That(historyEntityById.Id.Equals(entity.Id));
        }

        [Test]
        public async Task HistoryService_Create_ShouldCreateHistory()
        {
            var historyEntities = new List<History>();
            var entity = new History(Guid.NewGuid(), "Activity param", new DateTimeOffset(), "0.1", Guid.NewGuid(), "PK001");
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<History>())).ReturnsAsync(new History(entity.Id, entity.Activity, entity.ChangeDate,
                entity.Version, entity.NoteId, entity.UserId));

            var insertedEntity = await myHistoryService.CreateHistory(entity);

            myRepositoryMock.Verify(x => x.AddAsync(entity), Times.Once);
            Assert.That(insertedEntity.Id.Equals(entity.Id));
        }
    }
}