using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;
using Newtonsoft.Json.Linq;
using System.Linq;

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

            List<History> historiesList = new List<History>
            {
                new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"), "Activity param", new DateTimeOffset(), "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001"),
                new History(new Guid("6eaf2c30-8276-499a-804b-630fe8e79722"), "Activity param", new DateTimeOffset(), "0.1", new Guid("9b042f7b-3c56-4d1c-a4fd-63f500c4cd92"), "PK002")
            };

            myRepositoryMock.Setup(x => x.GetAll()).Returns(() => historiesList);

            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync((() => historiesList));

            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<History>())).Callback<History>((history =>
            {
                historiesList.Add(history);
            }));

            myRepositoryMock.Setup(x => x.Remove(It.IsAny<History>())).Callback<History>((history =>
            {
                historiesList.Remove(history);
            }));

            myRepositoryMock.Setup(x => x.Update(It.IsAny<History>())).Callback<History>((history =>
            {
                foreach (History e in historiesList)
                {
                    if (e.Id == history.Id)
                    {
                        e.Activity = history.Activity;
                        e.ChangeDate = history.ChangeDate;
                        e.Version = history.Version;
                        e.NoteId = history.NoteId;
                        e.PKKey = history.PKKey;
                    }
                }
            }));
        }

        [Test]
        public void GetAllHistories_ShouldReturnAll()
        {
            var histories = myHistoryService.GetAll();

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(histories.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllHistoriesAsync_ShouldReturnAll()
        {
            var histories = await myHistoryService.GetAllAsync();

            myRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.That(histories.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetHistoryById_ShouldReturnSpecificHistory()
        {
            var history = myHistoryService.GetById(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"));

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(history, Is.Not.Null);
            Assert.That(history.Id.Equals( new Guid("27181d48-4b43-455b-ac50-39ae783a5b24")));
        }

        [Test]
        public async Task CreateHistory_ShouldCreateHistory()
        {
            var histories = await myHistoryService.GetAllAsync();
            var history = new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b25"), "Activity param",
                new DateTimeOffset(), "0.4", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK004");

            myHistoryService.CreateHistory(history);

            myRepositoryMock.Verify(x => x.AddAsync(history), Times.Once);
            Assert.That(histories.Count().Equals(3));
        }

        [Test]
        public void RemoveHistoryById_ShouldDeleteSpecificHistory()
        {
            var removeEntityId = new Guid("27181d48-4b43-455b-ac50-39ae783a5b24");
            var histories = myHistoryService.GetAll();
            var history = myHistoryService.GetById(removeEntityId);

            myHistoryService.RemoveHistoryById(removeEntityId);

            myRepositoryMock.Verify(x => x.GetAll(), Times.AtLeastOnce);
            myRepositoryMock.Verify(x => x.Remove(history), Times.Once);
            Assert.That(histories.Count().Equals(1));
        }

        [Test]
        public void UpdateHistory_ShouldUpdateSpecificHistory()
        {
            var updateEntityId = new Guid("6eaf2c30-8276-499a-804b-630fe8e79722");
            var newHistory = new History(updateEntityId, "Activity param", new DateTimeOffset(), "0.1",
                new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "NEWPKKEY");

            myHistoryService.UpdateHistory(newHistory);
            var historyEntityById = myHistoryService.GetById(updateEntityId);

            myRepositoryMock.Verify(x => x.Update(newHistory), Times.Once);
            myRepositoryMock.Verify(x=> x.GetAll(), Times.AtLeastOnce);
            Assert.That(historyEntityById.Id, Is.EqualTo(newHistory.Id));
            Assert.That(historyEntityById.Activity, Is.EqualTo(newHistory.Activity));
            Assert.That(historyEntityById.ChangeDate, Is.EqualTo(newHistory.ChangeDate));
            Assert.That(historyEntityById.Version, Is.EqualTo(newHistory.Version));
            Assert.That(historyEntityById.NoteId, Is.EqualTo(newHistory.NoteId));
            Assert.That(historyEntityById.PKKey, Is.EqualTo(newHistory.PKKey));
        }
    }
}