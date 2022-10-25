using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

namespace evoKnowledgeShare.UnitTests {
    public class TopicServiceTests {
        private TopicService myTopicService;
        private Mock<IRepository<Topic>> myRepositoryMock;

        [SetUp]
        public void Setup() {
            myRepositoryMock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            myTopicService = new TopicService(myRepositoryMock.Object);

            myRepositoryMock.Setup(x => x.GetAll()).Returns(() => {
                return new List<Topic> {
                    new Topic(1, "Topic Test Title 1."),
                    new Topic(2, "Topic Test Title 2.")
                };
            });

            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(() => {
                return new List<Topic> {
                    new Topic(1, "Topic Test Title 1."),
                    new Topic(2, "Topic Test Title 2.")
                };
            });

            myRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => new Topic(1, "Topic Test Title 1."));

            myRepositoryMock.Setup(x => x.GetRangeById(It.IsAny<IEnumerable<int>>())).Returns(new List<Topic> {
                new Topic(1, "Topic Test Title 1."),
                new Topic(3, "Topic Test Title 3.")
            });
        }

        //GETS TESTS
        [Test]
        public void Topic_GetAll_ShouldReturnAll() {
            var actualTopics = myTopicService.GetAll();

            Assert.That(actualTopics.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task Topic_GetAllAsync_ShouldReturnAll() {
            var actualTopics = await myTopicService.GetAllAsync();

            Assert.That(actualTopics.Count, Is.EqualTo(2));
        }

        [Test]
        public void Topic_GetById_ShouldReturnTopicWithSpecificId() {
            var expectedTopic = new Topic(1, "Topic Test Title 1.");

            var actualTopic = myTopicService.GetById(expectedTopic.TopicID);
            Assert.That(actualTopic.TopicID, Is.EqualTo(expectedTopic.TopicID));
            Assert.That(actualTopic.Title, Is.EqualTo(expectedTopic.Title));
        }

        [Test]
        public void Topic_GetByTitle_ShouldReturnAListWhereItMatchesTitle() {
            var expectedTopic = new Topic(1, "Topic Test Title 1.");

            var actualTopics = myTopicService.GetByTitle(expectedTopic.Title);
            Assert.That(actualTopics.First().Title, Is.EqualTo(expectedTopic.Title));
        }

        [Test]
        public void Topic_GetRangeById_ShouldReturnARangeOfIds() {
            var actualTopics = myTopicService.GetRangeById(new List<int> { 1, 3 });

            Assert.That(actualTopics.Count, Is.EqualTo(2));
        }
    }
}
