using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.UnitTests.Repositories {
    [TestFixture]
    public class TopicRepositoryTests {
        TopicRepository myRepository;
        EvoKnowledgeDbContext myDbContext;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            DbContextOptions dbContextOptions = new DbContextOptionsBuilder<EvoKnowledgeDbContext>()
                .UseInMemoryDatabase("InMemoryDB").Options;

            myDbContext = new EvoKnowledgeDbContext(dbContextOptions);
            myRepository = new TopicRepository(myDbContext);
        }

        [SetUp]
        public void Setup() {
            foreach (var topic in myDbContext.Topics)
                myDbContext.Topics.Remove(topic);
            myDbContext.SaveChanges();
        }

        [Test]
        public void Repository_Add_DefaultPositive() {
            var topic = new Topic(1, "Test Topic 1.");

            myRepository.Add(topic);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task Repository_AddAsync_ShouldAddOneNewTopic() {
            var topic = new Topic(1, "Test Topic 1.");

            await myRepository.AddAsync(topic);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Repository_AddRange_ShouldAddARangeOfTopics() {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(topics.Count));
        }

        [Test]
        public void Repository_GetAll_ShouldGetAllTopics() {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();

            var actualTopics = myRepository.GetAll();

            Assert.That(Equals(actualTopics.ElementAt(0), topics.ElementAt(0)));
            Assert.That(Equals(actualTopics.ElementAt(1), topics.ElementAt(1)));
            Assert.That(Equals(actualTopics.ElementAt(2), topics.ElementAt(2)));
            Assert.That(actualTopics.Count(), Is.EqualTo(topics.Count));
        }

        [Test]
        public async Task Repository_GetAllAsync_ShouldGetAllAsync() {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();

            var actualTopics = await myRepository.GetAllAsync();

            Assert.That(actualTopics.Contains(topics.ElementAt(0)));
            Assert.That(actualTopics.Contains(topics.ElementAt(1)));
            Assert.That(actualTopics.Contains(topics.ElementAt(2)));
            Assert.That(actualTopics.Count(), Is.EqualTo(topics.Count));
        }

        [Test]
        public void Repository_GetById_ShouldReturnATopicWithSpecificId() {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();

            var actualTopic = myRepository.GetById(topics.ElementAt(1).TopicID);

            Assert.That(Equals(topics.ElementAt(1), actualTopic));
        }

        [Test]
        public void Repository_GetRangeById_ShouldReturnARangeOfTopicsByIds() {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();

            var actualTopics = myRepository.GetRangeById(new List<int> { 1, 3 });

            Assert.That(actualTopics.Contains(topics.ElementAt(0)));
            Assert.That(actualTopics.Contains(topics.ElementAt(2)));
            Assert.That(actualTopics.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Repository_Remove_ShouldRemoveOneTopic() {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();

            myRepository.Remove(topics.ElementAt(1));
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Repository_RemoveById_ShouldRemoveOneTopicWithSpecificId() {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();

            myRepository.RemoveById(2);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Repository_RemoveRange_ShouldRemoveARangeOfTopics() {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();

            myRepository.RemoveRange(new List<Topic> { 
                topics.ElementAt(0),
                topics.ElementAt(2)
            });
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Repository_RemoveRangeById_ShouldRemoveARangeOfTopicsById() {
            var topics = new List<Topic> {
                new Topic(1, "Test Topic 1."),
                new Topic(2, "Test Topic 2."),
                new Topic(3, "Test Topic 3.")
            };

            myRepository.AddRange(topics);
            myRepository.SaveChanges();


            myRepository.RemoveRangeById(new List<int> {
                topics.ElementAt(0).TopicID,
                topics.ElementAt(2).TopicID
            });
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Repository_Update_ShouldUpdateOneTopic() {
            var topic = new Topic(1, "Test Topic 1.");

            myRepository.Add(topic);
            myRepository.SaveChanges();

            string newTopicTitle = "Updated Test Topic 1.";

            topic.Title = newTopicTitle;

            myRepository.Update(topic);
            myRepository.SaveChanges();

            var updatedTopic = myRepository.GetById(topic.TopicID);

            Assert.That(updatedTopic.Title, Is.EqualTo(newTopicTitle));
        }

        [Test]
        public void Repository_UpdateRange_ShouldUpdateARangeOfTopics() {
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

