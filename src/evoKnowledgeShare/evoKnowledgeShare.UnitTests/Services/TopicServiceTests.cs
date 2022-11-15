using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

namespace evoKnowledgeShare.UnitTests.Services
{
    public class TopicServiceTests:ServiceTestBase<Topic> 
    {
        protected TopicService myService = default!;
        List<Topic> myTopics;

        [SetUp]
        public void SetUp() 
        {
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
        public void Topic_GetAll_ShouldReturnAll()
        {
            myRepositoryMock.Setup(x => x.GetAll()).Returns(myTopics);

            IEnumerable<Topic> actualTopics = myService.GetAll();

            foreach (var topic in myTopics)
                Assert.That(actualTopics.Contains(topic));
            Assert.That(actualTopics.Count, Is.EqualTo(myTopics.Count));
        }


        [Test]
        public void Topic_GetById_ShouldReturnTopicWithSpecificId()
        {
            Topic expectedTopic = new Topic(myTopics[2].Id, myTopics[2].Title);
            myRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => expectedTopic);

            Topic actualTopic = myService.GetById(expectedTopic.Id)!;

            Assert.That(Equals(actualTopic, expectedTopic));
        }

        [Test]
        public void Topic_GetByTitle_ShouldReturnAListWhereItMatchesTitle()
        {
            Topic expectedTopic = new Topic(myTopics[3].Id, myTopics[3].Title);
            myRepositoryMock.Setup(x => x.GetAll()).Returns(myTopics);

            IEnumerable<Topic> actualTopics = myService.GetByTitle(expectedTopic.Title);

            Assert.That(actualTopics.Contains(expectedTopic));
        }

        [Test]
        public void Topic_GetRangeById_ShouldReturnARangeOfIds()
        {
            Topic firstExpectedTopic = new Topic(myTopics[2].Id, myTopics[2].Title);
            Topic secondExpectedTopic = new Topic(myTopics[4].Id, myTopics[4].Title);
            myRepositoryMock.Setup(x => x.GetRangeById(It.IsAny<IEnumerable<Guid>>())).Returns(new List<Topic> {
                firstExpectedTopic,
                secondExpectedTopic
            });

            IEnumerable<Topic> actualTopics = myService.GetRangeById(new List<Guid> { firstExpectedTopic.Id, secondExpectedTopic.Id });

            Assert.That(actualTopics.Contains(firstExpectedTopic));
            Assert.That(actualTopics.Contains(secondExpectedTopic));
            Assert.That(actualTopics.Count, Is.EqualTo(2));
        }
        #endregion Get Test Section

        #region Add Test Region
        [Test]
        public async Task Topic_AddAsync_ShouldCallAddAsyncOnce()
        {
            Topic addedTopic = new Topic(myTopics[0].Id, myTopics[0].Title);
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Topic>()));

            await myService.AddAsync(addedTopic);

            myRepositoryMock.Verify(x => x.AddAsync(addedTopic), Times.Once);
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
            List<Topic> removeRangeOfTopics = new List<Topic> {
                new Topic(myTopics[0].Id, myTopics[0].Title),
                new Topic(myTopics[1].Id, myTopics[1].Title),
                new Topic(myTopics[2].Id, myTopics[2].Title),
                new Topic(myTopics[3].Id, myTopics[3].Title)
            };
            myRepositoryMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<Topic>>()));

            myService.RemoveRange(removeRangeOfTopics);

            myRepositoryMock.Verify(x => x.RemoveRange(removeRangeOfTopics), Times.Once);
        }

        [Test]
        public void Topic_RemoveRangeById_ShouldCallRemoveRangeByIdOnce()
        {
            List<Guid> removeRangeOfIds = new List<Guid> { myTopics[0].Id, myTopics[1].Id, myTopics[4].Id };
            myRepositoryMock.Setup(x => x.RemoveRangeById(It.IsAny<IEnumerable<Guid>>()));

            myService.RemoveRangeById(removeRangeOfIds);

            myRepositoryMock.Verify(x => x.RemoveRangeById(removeRangeOfIds), Times.Once);
        }
        #endregion Remove Test Region

        #region Update Test Region
        [Test]
        public void Topic_Update_ShouldCallUpdateOnce()
        {
            Topic updateTopic = new Topic(myTopics[0].Id, myTopics[0].Title);
            myRepositoryMock.Setup(x => x.Update(It.IsAny<Topic>()));

            myService.Update(updateTopic);

            myRepositoryMock.Verify(x => x.Update(updateTopic), Times.Once);
        }

        [Test]
        public void Topic_UpdateRange_ShouldCallUpdateRangeOnce() 
        {
            List<Topic> updateRangeOfTopics = new List<Topic> {
                new Topic(myTopics[0].Id, myTopics[0].Title),
                new Topic(myTopics[1].Id, myTopics[1].Title),
                new Topic(myTopics[2].Id, myTopics[2].Title),
                new Topic(myTopics[3].Id, myTopics[3].Title)
            };
            myRepositoryMock.Setup(x => x.UpdateRange(It.IsAny<IEnumerable<Topic>>()));

            myService.UpdateRange(updateRangeOfTopics);

            myRepositoryMock.Verify(x => x.UpdateRange(updateRangeOfTopics), Times.Once);
        }
        #endregion Update Test Region
    }
}