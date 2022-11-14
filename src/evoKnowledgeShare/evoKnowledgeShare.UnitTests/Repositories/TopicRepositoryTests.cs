using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.UnitTests.Repositories
{
    [TestFixture]
    public class TopicRepositoryTests
    {
        private TopicRepository myRepository = default!;
        private EvoKnowledgeDbContext myDbContext = default!;

        [SetUp]
        public void Setup()
        {
            DbContextOptions dbContextOptions = new DbContextOptionsBuilder<EvoKnowledgeDbContext>()
                .UseInMemoryDatabase("InMemoryDB").Options;

            myDbContext = new EvoKnowledgeDbContext(dbContextOptions);
            myRepository = new TopicRepository(myDbContext);
        }

        [TearDown]
        public void TearDown()
        {
            myDbContext.Database.EnsureDeleted();
            myDbContext.Dispose();
        }

        [Test]
        public async Task Repository_AddAsync_ShouldAddOneNewTopic()
        {
            var expectedTopic = new Topic(1, "Test Topic 1.");

            await myRepository.AddAsync(expectedTopic);
            await myRepository.SaveChangesAsync();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(1));
            Assert.That(myDbContext.Topics.First(), Is.EqualTo(expectedTopic));
        }

        [Test]
        public void Repository_AddRange_ShouldAddARangeOfTopics()
        {
            var expectedTopics = new List<Topic>
            {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(expectedTopics);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(expectedTopics.Count));
            foreach (var expectedTopic in expectedTopics)
            {
                Assert.That(myDbContext.Topics, Does.Contain(expectedTopic));
            }
        }

        [Test]
        public void Repository_GetAll_ShouldGetAllTopics()
        {
            var expectedTopics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };
            
            myDbContext.Topics.AddRange(expectedTopics);
            myDbContext.SaveChanges();

            var actualTopics = myRepository.GetAll().ToArray();

            Assert.That(actualTopics, Has.Length.EqualTo(expectedTopics.Count));
            foreach (var actualTopic in actualTopics)
            {
                Assert.That(myDbContext.Topics, Does.Contain(actualTopic));
            }
        }

        [Test]
        public void Repository_GetById_ShouldReturnATopicWithSpecificId()
        {
            var expectedTopic = new Topic(1, "Test Topic 1.");
            var expectedTopics = new List<Topic> {
                expectedTopic,
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myDbContext.Topics.AddRange(expectedTopics);
            myDbContext.SaveChanges();

            var actualTopic = myRepository.GetById(expectedTopic.Id);

            Assert.That(actualTopic, Is.EqualTo(expectedTopic));
        }

        [Test]
        public void Repository_GetRangeById_ShouldReturnARangeOfTopicsByIds()
        {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myDbContext.Topics.AddRange(topics);
            myDbContext.SaveChanges();

            var actualTopics = myRepository.GetRangeById(topics.Take(2).Select(x => x.Id)).ToArray();

            Assert.That(actualTopics, Does.Contain(topics[0]));
            Assert.That(actualTopics, Does.Contain(topics[1]));
            Assert.That(actualTopics, Has.Length.EqualTo(2));
        }

        [Test]
        public void Repository_Remove_ShouldRemoveOneTopic()
        {
            var topicToRemove = new Topic(1, "Test Topic 1.");
            var topics = new List<Topic> {
                topicToRemove,
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myDbContext.Topics.AddRange(topics);
            myDbContext.SaveChanges();

            myRepository.Remove(topicToRemove);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(2));
            Assert.That(myDbContext.Topics.All(x => !x.Equals(topicToRemove)));
        }

        [Test]
        public void Repository_RemoveById_ShouldRemoveOneTopicWithSpecificId()
        {
            var topicToRemove = new Topic(1, "Test Topic 1.");
            var topics = new List<Topic> {
                topicToRemove,
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myDbContext.Topics.AddRange(topics);
            myDbContext.SaveChanges();

            myRepository.RemoveById(topicToRemove.Id);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(2));
            Assert.That(myDbContext.Topics.All(x => !x.Equals(topicToRemove)));
        }

        [Test]
        public void Repository_RemoveRange_ShouldRemoveARangeOfTopics()
        {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();

            myRepository.RemoveRange(topics.Take(2));
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(1));
            Assert.That(!myDbContext.Topics.Contains(topics[0]));
            Assert.That(!myDbContext.Topics.Contains(topics[1]));
        }

        [Test]
        public void Repository_RemoveRangeById_ShouldRemoveARangeOfTopicsById()
        {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();


            myRepository.RemoveRangeById(topics.Take(2).Select(x => x.Id));
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task Repository_Update_ShouldUpdateOneTopic()
        {
            var topic = new Topic(1, "Test Topic 1.");

            await myRepository.AddAsync(topic);
            myRepository.SaveChanges();

            string newTopicTitle = "Updated Test Topic 1.";

            topic.Title = newTopicTitle;

            myRepository.Update(topic);
            myRepository.SaveChanges();

            var updatedTopic = myRepository.GetById(topic.Id);

            Assert.That(updatedTopic?.Title, Is.EqualTo(newTopicTitle));
        }

        [Test]
        public void Repository_UpdateRange_ShouldUpdateARangeOfTopics()
        {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();

            List<string> newTopicTitles = new List<string> {
                "Updated Test Topic 1.",
                "Updated Test Topic 2.",
                "Updated Test Topic 3."
            };

            topics.ElementAt(0).Title = newTopicTitles.ElementAt(0);
            topics.ElementAt(1).Title = newTopicTitles.ElementAt(1);
            topics.ElementAt(2).Title = newTopicTitles.ElementAt(2);

            myRepository.UpdateRange(topics);
            myRepository.SaveChanges();

            var updatedTopics = myRepository.GetAll();

            Assert.That(updatedTopics.Contains(topics.ElementAt(0)));
            Assert.That(updatedTopics.Contains(topics.ElementAt(1)));
            Assert.That(updatedTopics.Contains(topics.ElementAt(2)));
        }
    }
}