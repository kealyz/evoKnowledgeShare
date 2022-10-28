using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

namespace evoKnowledgeShare.UnitTests
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
        public void GetAllHistories_ShouldReturnAll()
        {
            var entity = new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"), "Activity param", new DateTimeOffset(),
                "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001");
            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<History> { entity });
            
            var historyEntities = myHistoryService.GetAll();

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(historyEntities.Count, Is.EqualTo(1));
            Assert.That(historyEntities.First, Is.EqualTo(entity));
        }

        [Test]
        public async Task GetAllHistoriesAsync_ShouldReturnAll()
        {
            var entity = new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"), "Activity param", new DateTimeOffset(),
                "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001");
            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<History> { entity });
            
            var historyEntities = await myHistoryService.GetAllAsync();

            myRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.That(historyEntities.Count, Is.EqualTo(1));
            Assert.That(historyEntities.First, Is.EqualTo(entity));
        }

        [Test]
        public void GetHistoryById_ShouldReturnSpecificHistory()
        {
            var entity = new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"), "Activity param", new DateTimeOffset(),
                "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001");
            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<History> { entity });
            
            var historyEntityById = myHistoryService.GetById(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"));

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(historyEntityById, Is.Not.Null);
            Assert.That(historyEntityById.Id.Equals( new Guid("27181d48-4b43-455b-ac50-39ae783a5b24")));
        }

        [Test]
        public async Task CreateHistory_ShouldCreateHistory()
        {
            var historyEntities = new List<History>();
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<History>())).Callback<History>((newHistoryEntity =>
            {
                historyEntities.Add(newHistoryEntity);
            })).Returns(Task.CompletedTask);
            var entity = new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"), "Activity param", new DateTimeOffset(),
                "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001");

            await myHistoryService.CreateHistory(entity);

            myRepositoryMock.Verify(x => x.AddAsync(entity), Times.Once);
            Assert.That(historyEntities.Count().Equals(1));
            Assert.That(historyEntities.First(), Is.EqualTo(entity));
        }
    }
}