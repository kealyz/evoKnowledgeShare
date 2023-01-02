using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

namespace evoKnowledgeShare.UnitTests.Services
{
    public class TopicServiceTests
    {
        protected Mock<IRepository<Topic>> myRepositoryMock = default!;
        protected TopicService myService = default!;
        List<Topic> myTopics;

        [SetUp]
        public void SetUp()
        {
            myRepositoryMock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            myService = new TopicService(myRepositoryMock.Object);
            myTopics = new List<Topic> {
                new Topic(Guid.NewGuid(), "Topic Test Title 1."),
                new Topic(Guid.NewGuid(), "Topic Test Title 2."),
                new Topic(Guid.NewGuid(), "Topic Test Title 3."),
                new Topic(Guid.NewGuid(), "Topic Test Title 4."),
                new Topic(Guid.NewGuid(), "Topic Test Title 5."),
            };
        }

        #region Get Test Section

        [Test]
        public void TopicService_GetAll_ShouldReturnAll()
        {
            myRepositoryMock.Setup(x => x.GetAll()).Returns(myTopics);

            IEnumerable<Topic> actualTopics = myService.GetAll();

            foreach (var topic in myTopics)
                Assert.That(actualTopics.Contains(topic));
            Assert.That(actualTopics.Count, Is.EqualTo(myTopics.Count));
        }

        [Test]
        public void TopicService_GetById_ShouldReturnTopicWithSpecificId()
        {
            Topic expectedTopic = new Topic(myTopics[2].Id, myTopics[2].Title);
            myRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => expectedTopic);

            Topic actualTopic = myService.GetById(expectedTopic.Id)!;

            Assert.That(Equals(actualTopic, expectedTopic));
        }

        [Test]
        public void TopicService_GetById_ShouldThrowKeyNotFoundException()
        {
            Topic notExistingTopic = new Topic(Guid.NewGuid(), "Randomtitle");
            myRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Throws<KeyNotFoundException>();

            Assert.Throws<KeyNotFoundException>(() =>
            {
                Topic actualTopic = myService.GetById(notExistingTopic.Id)!;
            });
        }

        [Test]
        public void TopicService_GetByTitle_ShouldReturnAListWhereItMatchesTitle()
        {
            Topic expectedTopic = new Topic(myTopics[3].Id, myTopics[3].Title);
            myRepositoryMock.Setup(x => x.GetAll()).Returns(myTopics);

            IEnumerable<Topic> actualTopics = myService.GetByTitle(expectedTopic.Title);

            Assert.That(actualTopics.Contains(expectedTopic));
        }

        [Test]
        public void TopicService_GetByTitle_ShouldThrowKeyNotFoundException()
        {
            myRepositoryMock.Setup(x => x.GetAll()).Throws<KeyNotFoundException>();


            Assert.Throws<KeyNotFoundException>(() =>
            {
                IEnumerable<Topic> actualTopics = myService.GetByTitle("Nagyonrandomtitle");
            });
        }

        [Test]
        public void TopicService_GetRangeById_ShouldReturnARangeOfIds()
        {
            List<Topic> topicsToBeAdded = new List<Topic>() { myTopics[0], myTopics[1] };
            myRepositoryMock.Setup(x => x.GetRangeById(It.IsAny<IEnumerable<Guid>>())).Returns(topicsToBeAdded);

            IEnumerable<Topic> actualTopics = myService.GetRangeById(new List<Guid>() { topicsToBeAdded[0].Id, topicsToBeAdded[1].Id });

            CollectionAssert.AreEqual(actualTopics, topicsToBeAdded);
        }

        [Test]
        public void TopicService_GetRangeById_ShouldThrowKeyNotFoundException()
        {
            myRepositoryMock.Setup(x => x.GetRangeById(It.IsAny<IEnumerable<Guid>>())).Throws<KeyNotFoundException>();

            Assert.Throws<KeyNotFoundException>(() =>
            {
                IEnumerable<Topic> actualTopics = myService.GetRangeById(new List<Guid> { myTopics[0].Id, myTopics[1].Id });
            });
        }

        #endregion Get Test Section

        #region Add Test Region

        [Test]
        public async Task Topic_AddAsync_ShouldCallAddAsyncOnce()
        {
            TopicDTO topicDTOToBeAdded = new TopicDTO(myTopics[0].Title);
            Topic topicToBeAdded = new Topic(topicDTOToBeAdded);
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Topic>())).ReturnsAsync(topicToBeAdded);

            Topic addedTopic = await myService.AddAsync(topicDTOToBeAdded);

            //myRepositoryMock.Verify(x => x.AddAsync(asd), Times.Once);
            Assert.That(addedTopic, Is.EqualTo(topicToBeAdded));
        }

        #endregion Add Test Region

        #region Remove Test Region

        [Test]
        public void Topic_Remove_ShouldCallRemoveOnce()
        {
            Topic removeTopic = new Topic(myTopics[0].Id, myTopics[0].Title);
            myRepositoryMock.Setup(x => x.Remove(It.IsAny<Topic>()));

            myService.Remove(removeTopic);

            myRepositoryMock.Verify(x => x.Remove(removeTopic), Times.Once);
        }

        [Test]
        public void Topic_RemoveById_ShouldCallRemoveByIdOnce()
        {
            Guid removeId = myTopics[2].Id;
            myRepositoryMock.Setup(x => x.RemoveById(It.IsAny<Guid>()));

            myService.RemoveById(removeId);

            myRepositoryMock.Verify(x => x.RemoveById(removeId), Times.Once);
        }

        [Test]
        public void Topic_RemoveRange_ShouldCallRemoveRangeOnce()
        {
            myRepositoryMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<Topic>>()));

            myService.RemoveRange(myTopics);

            myRepositoryMock.Verify(x => x.RemoveRange(myTopics), Times.Once);
        }

        [Test]
        public void TopicService_RemoveRangeById_ShouldCallRemoveRangeByIdOnce()
        {
            List<Guid> removeRangeOfIds = new List<Guid> { myTopics[0].Id, myTopics[1].Id, myTopics[4].Id };
            myRepositoryMock.Setup(x => x.RemoveRangeById(It.IsAny<IEnumerable<Guid>>()));

            myService.RemoveRangeById(removeRangeOfIds);

            myRepositoryMock.Verify(x => x.RemoveRangeById(removeRangeOfIds), Times.Once);
        }

        #endregion Remove Test Region

        #region Update Test Region

        [Test]
        public void TopicService_Update_ShouldCallUpdateOnce()
        {
            Topic topicToBeUpdated = new Topic(myTopics[0].Id, "Ezegytitle");
            myRepositoryMock.Setup(x => x.Update(It.IsAny<Topic>())).Returns(topicToBeUpdated);

            Topic updatedTopic = myService.Update(topicToBeUpdated);

            myRepositoryMock.Verify(x => x.Update(topicToBeUpdated), Times.Once);
            Assert.That(updatedTopic, Is.EqualTo(topicToBeUpdated));
        }

        [Test]
        public void TopicService_UpdateRange_ShouldCallUpdateRangeOnce()
        {
            List<Topic> updateRangeOfTopics = new List<Topic> {
                new Topic(myTopics[0].Id, myTopics[1].Title),
                new Topic(myTopics[1].Id, myTopics[0].Title)
            };
            myRepositoryMock.Setup(x => x.UpdateRange(It.IsAny<IEnumerable<Topic>>())).Returns(updateRangeOfTopics);

            IEnumerable<Topic> updatedTopics = myService.UpdateRange(updateRangeOfTopics);

            myRepositoryMock.Verify(x => x.UpdateRange(updateRangeOfTopics), Times.Once);
            Assert.That(updatedTopics, Is.EqualTo(updateRangeOfTopics));
        }

        #endregion Update Test Region
    }
}