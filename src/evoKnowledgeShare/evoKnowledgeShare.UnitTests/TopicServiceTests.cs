using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            myRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => {
                return new Topic(2, "Topic Test Title");
            });
        }

        [Test]
        public void Topic_GetAll_ShouldReturnAll() {
            var topics = myTopicService.GetAll();
            Assert.That(topics.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task Topic_GetAllAsync_ShouldReturnAll() {
            var topics = await myTopicService.GetAllAsync();
            Assert.That(topics.Count, Is.EqualTo(2));
        }

        [Test]
        public void Topic_GetById_ShouldReturnTopicWithSpecificId() {
            var expectedTopic = new Topic(2, "Topic Test Title");
            var topic = myTopicService.GetById(2);
            Assert.That(topic.TopicID, Is.EqualTo(expectedTopic.TopicID));
        }

        [Test]
        public void Topic_GetByTitle_ShouldReturnAListWhereItMatchesTitle() {
            var expectedTopic = new Topic(1, "Title1");
            var notExpectedTopic = new Topic(2, "Title2");

            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Topic> {
                expectedTopic,
                notExpectedTopic
            });

            var topic = myTopicService.GetByTitle("Title1");
            Assert.That(topic.Count, Is.EqualTo(1));
            Assert.That(topic.First().Title, Is.EqualTo(expectedTopic.Title));
        }
        /*[Test]
        public void Topic_Add_ShouldAddATopic() {
            var topicToBeAdded = new Topic(1, "Test Topic Title");
            myTopicService.Add(topicToBeAdded);
            Console.WriteLine(myTopicService.GetById(1).TopicID);
        }*/
    }
}
