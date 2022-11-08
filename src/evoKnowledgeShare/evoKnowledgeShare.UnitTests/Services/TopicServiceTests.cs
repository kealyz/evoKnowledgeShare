using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

namespace evoKnowledgeShare.UnitTests.Services
{
    public class TopicServiceTests
    {
        private TopicService myTopicService;
        private Mock<IRepository<Topic>> myRepositoryMock;

        [SetUp]
        public void Setup()
        {
            myRepositoryMock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            myTopicService = new TopicService(myRepositoryMock.Object);
        }

        //GET TESTS
        [Test]
        public void Topic_GetAll_ShouldReturnAll()
        {
            var shouldReturnFirst = new Topic(1, "Topic Test Title 1.");
            var shouldReturnSecond = new Topic(2, "Topic Test Title 2.");

            myRepositoryMock.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<Topic> {
                    shouldReturnFirst,
                    shouldReturnSecond
                };
            });

            var actualTopics = myTopicService.GetAll();

            Assert.That(Equals(actualTopics.ElementAt(0), shouldReturnFirst));
            Assert.That(Equals(actualTopics.ElementAt(1), shouldReturnSecond));
            Assert.That(actualTopics.Count, Is.EqualTo(2));
        }


        [Test]
        public void Topic_GetById_ShouldReturnTopicWithSpecificId()
        {
            var expectedTopic = new Topic(1, "Topic Test Title 1.");

            myRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => expectedTopic);

            var actualTopic = myTopicService.GetById(expectedTopic.Id);

            Assert.That(Equals(actualTopic, expectedTopic));
            Assert.That(actualTopic.Title, Is.EqualTo(expectedTopic.Title));
        }

        [Test]
        public void Topic_GetByTitle_ShouldReturnAListWhereItMatchesTitle()
        {
            var expectedTopic = new Topic(1, "Topic Test Title 1.");
            var notExpectedTopic = new Topic(2, "Topic Test Title 2.");

            myRepositoryMock.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<Topic> {
                    expectedTopic,
                    notExpectedTopic
                };
            });

            var actualTopics = myTopicService.GetByTitle(expectedTopic.Title);

            Assert.That(Equals(actualTopics.ElementAt(0), expectedTopic));
        }

        [Test]
        public void Topic_GetRangeById_ShouldReturnARangeOfIds()
        {
            var shouldReturnFirst = new Topic(1, "Topic Test Title 1.");
            var shouldReturnSecond = new Topic(2, "Topic Test Title 2.");

            myRepositoryMock.Setup(x => x.GetRangeById(It.IsAny<IEnumerable<int>>())).Returns(new List<Topic> {
                shouldReturnFirst,
                shouldReturnSecond
            });

            var actualTopics = myTopicService.GetRangeById(new List<int> { 1, 3 });

            Assert.That(Equals(actualTopics.ElementAt(0), shouldReturnFirst));
            Assert.That(Equals(actualTopics.ElementAt(1), shouldReturnSecond));
            Assert.That(actualTopics.Count, Is.EqualTo(2));
        }

        //ADD TESTS
        [Test]
        public void Topic_Add_ShouldCallAddOnce()
        {
            var addedTopic = new Topic(1, "Topic Test Title 1.");

            myRepositoryMock.Setup(x => x.Add(It.IsAny<Topic>()));

            myTopicService.Add(addedTopic);

            myRepositoryMock.Verify(x => x.Add(addedTopic), Times.Once);
        }

        [Test]
        public async Task Topic_Add_ShouldCallAddOnceAsync()
        {
            var addedTopic = new Topic(1, "Topic Test Title 1.");

            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Topic>()));

            myTopicService.AddAsync(addedTopic);

            myRepositoryMock.Verify(x => x.AddAsync(addedTopic), Times.Once);
        }

        [Test]
        public void Topic_AddRange_ShouldCallAddRangeOnce()
        {
            List<Topic> rangeOfTopics = new List<Topic> {
                new Topic(1, "First Topic"),
                new Topic(2, "Second Topic"),
                new Topic(3, "Third Topic"),
                new Topic(4, "Fourth Topic")
            };

            myRepositoryMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<Topic>>()));

            myTopicService.AddRange(rangeOfTopics);

            myRepositoryMock.Verify(x => x.AddRange(rangeOfTopics), Times.Once);
        }

        //Remove Tests
        [Test]
        public void Topic_Remove_ShouldCallRemoveOnce()
        {
            var removeTopic = new Topic(1, "Topic Test Title 1.");

            myRepositoryMock.Setup(x => x.Remove(It.IsAny<Topic>()));

            myTopicService.Remove(removeTopic);

            myRepositoryMock.Verify(x => x.Remove(removeTopic), Times.Once);
        }

        [Test]
        public void Topic_RemoveById_ShouldCallRemoveByIdOnce()
        {
            var removeId = 1;

            myRepositoryMock.Setup(x => x.RemoveById(It.IsAny<int>()));

            myTopicService.RemoveById(removeId);

            myRepositoryMock.Verify(x => x.RemoveById(removeId), Times.Once);
        }

        [Test]
        public void Topic_RemoveRange_ShouldCallRemoveRangeOnce()
        {
            List<Topic> removeRangeOfTopics = new List<Topic> {
                new Topic(1, "First Topic"),
                new Topic(2, "Second Topic"),
                new Topic(3, "Third Topic"),
                new Topic(4, "Fourth Topic")
            };

            myRepositoryMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<Topic>>()));

            myTopicService.RemoveRange(removeRangeOfTopics);

            myRepositoryMock.Verify(x => x.RemoveRange(removeRangeOfTopics), Times.Once);
        }

        [Test]
        public void Topic_RemoveRangeById_ShouldCallRemoveRangeByIdOnce()
        {
            List<int> removeRangeOfIds = new List<int> { 1, 2, 5, 6 };

            myRepositoryMock.Setup(x => x.RemoveRangeById(It.IsAny<IEnumerable<int>>()));

            myTopicService.RemoveRangeById(removeRangeOfIds);

            myRepositoryMock.Verify(x => x.RemoveRangeById(removeRangeOfIds), Times.Once);
        }

        //Update Tests
        [Test]
        public void Topic_Update_ShouldCallUpdateOnce()
        {
            var updateTopic = new Topic(1, "Topic Test Title 1.");

            myRepositoryMock.Setup(x => x.Update(It.IsAny<Topic>()));

            myTopicService.Update(updateTopic);

            myRepositoryMock.Verify(x => x.Update(updateTopic), Times.Once);
        }

        [Test]
        public void Topic_UpdateRange_ShouldCallUpdateRangeOnce()
        {
            List<Topic> updateRangeOfTopics = new List<Topic> {
                new Topic(1, "First Topic"),
                new Topic(2, "Second Topic"),
                new Topic(3, "Third Topic"),
                new Topic(4, "Fourth Topic")
            };

            myRepositoryMock.Setup(x => x.UpdateRange(It.IsAny<IEnumerable<Topic>>()));

            myTopicService.UpdateRange(updateRangeOfTopics);

            myRepositoryMock.Verify(x => x.UpdateRange(updateRangeOfTopics), Times.Once);
        }
    }
}