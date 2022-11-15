using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.UnitTests.Repositories
{
    [TestFixture]
    public class TopicRepositoryTests:RepositoryTestBase<Topic>
    {
        List<Topic> myTopics;

        [SetUp]
        public void Setup()
        {
            myRepository = new TopicRepository(myDbContext);
            myTopics = new List<Topic>() { 
                new Topic(Guid.NewGuid(), "Test Topic Title 1."),
                new Topic(Guid.NewGuid(), "Test Topic Title 2."),
                new Topic(Guid.NewGuid(), "Test Topic Title 3."),
                new Topic(Guid.NewGuid(), "Test Topic Title 4."),
                new Topic(Guid.NewGuid(), "Test Topic Title 5."),
            };
        }

        #region Add Test Section
        [Test]
        public async Task Repository_AddAsync_ShouldAddOneNewTopic()
        {
            Topic expectedTopic = new Topic(myTopics[1].Id, myTopics[1].Title);

            Topic actualTopic = await myRepository.AddAsync(expectedTopic);
            await myRepository.SaveChangesAsync();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(1));
            Assert.That(Equals(actualTopic, expectedTopic));
            Assert.That(myDbContext.Topics, Does.Contain(expectedTopic));
        }

        [Test]
        public async Task Repository_AddRangeAsync_ShouldAddARangeOfTopicsAsync() {
            List<Topic> expectedTopics = new List<Topic>() {
                new Topic(myTopics[1].Id, myTopics[1].Title),
                new Topic(myTopics[2].Id, myTopics[2].Title),
                new Topic(myTopics[3].Id, myTopics[3].Title),
            };

            IEnumerable<Topic> actualTopics = await myRepository.AddRangeAsync(expectedTopics);

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(expectedTopics.Count));
            foreach (var topic in myTopics) {
                Assert.That(actualTopics.Contains(topic));
                Assert.That(myDbContext.Topics, Does.Contain(topic));
            }
            
        }
        #endregion Add Test Section

        #region Get Test Section
        [Test]
        public void Repository_GetAll_ShouldGetAllTopics()
        {   
            myDbContext.Topics.AddRange(myTopics);
            myDbContext.SaveChanges();

            IEnumerable<Topic> actualTopics = myRepository.GetAll().ToList();

            Assert.That(actualTopics.Count, Is.EqualTo(myTopics.Count));
            foreach (var actualTopic in actualTopics)
                Assert.That(myDbContext.Topics, Does.Contain(actualTopic));
        }

        [Test]
        public void Repository_GetById_ShouldReturnATopicWithSpecificId()
        {
            Topic expectedTopic = new Topic(myTopics[2].Id, myTopics[2].Title);
            myDbContext.Topics.AddRange(myTopics);
            myDbContext.SaveChanges();

            Topic actualTopic = myRepository.GetById(expectedTopic.Id);

            Assert.That(Equals(actualTopic, expectedTopic));
        }

        [Test]
        public void Repository_GetRangeById_ShouldReturnARangeOfTopicsByIds()
        {
            List<Topic> expectedTopics = new List<Topic> {
                new Topic(myTopics[1].Id, myTopics[1].Title),
                new Topic(myTopics[3].Id, myTopics[3].Title),
                new Topic(myTopics[4].Id, myTopics[4].Title)
            };

            myDbContext.Topics.AddRange(myTopics);
            myDbContext.SaveChanges();

            IEnumerable<Topic> actualTopics = myRepository.GetRangeById(expectedTopics.Take(3).Select(x => x.Id)).ToArray();

            foreach (var expectedTopic in expectedTopics)
                Assert.That(myDbContext.Topics, Does.Contain(expectedTopic));
            Assert.That(actualTopics.Count, Is.EqualTo(expectedTopics.Count));
        }
        #endregion Get Test Section

        #region Remove Test Section
        [Test]
        public void Repository_Remove_ShouldRemoveOneTopic()
        {
            Topic topicToRemove = new Topic(myTopics[0].Id, myTopics[0].Title);

            myDbContext.Topics.AddRange(myTopics);
            myDbContext.SaveChanges();

            myRepository.Remove(topicToRemove);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(myTopics.Count - 1));
            Assert.That(myDbContext.Topics.All(x => !x.Equals(topicToRemove)));
        }

        [Test]
        public void Repository_RemoveById_ShouldRemoveOneTopicWithSpecificId() 
        {
            Topic topicToRemove = new Topic(myTopics[0].Id, myTopics[0].Title);

            myDbContext.Topics.AddRange(myTopics);
            myDbContext.SaveChanges();

            myRepository.RemoveById(topicToRemove.Id);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(myTopics.Count - 1));
            Assert.That(myDbContext.Topics.All(x => !x.Equals(topicToRemove)));
        }

        [Test]
        public void Repository_RemoveRange_ShouldRemoveARangeOfTopics()
        {
            List<Topic> topicsToRemove = new List<Topic>() {
                new Topic(myTopics[0].Id, myTopics[0].Title),
                new Topic(myTopics[3].Id, myTopics[3].Title),
            };

            myRepository.AddRangeAsync(myTopics);
            myRepository.SaveChanges();

            myRepository.RemoveRange(topicsToRemove.Take(2));
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(myTopics.Count - 2));
            Assert.That(!myDbContext.Topics.Contains(topicsToRemove[0]));
            Assert.That(!myDbContext.Topics.Contains(topicsToRemove[1]));
        }

        [Test]
        public void Repository_RemoveRangeById_ShouldRemoveARangeOfTopicsById() 
        {
            List<Topic> topicsToRemove = new List<Topic>() {
                new Topic(myTopics[0].Id, myTopics[0].Title),
                new Topic(myTopics[3].Id, myTopics[3].Title),
            };

            myRepository.AddRangeAsync(myTopics);
            myRepository.SaveChanges();

            myRepository.RemoveRangeById(topicsToRemove.Take(2).Select(x => x.Id));
            myRepository.SaveChanges();

            Assert.That(myDbContext.Topics.Count(), Is.EqualTo(myTopics.Count - 2));
        }
        #endregion Remove Test Section

        #region Update Test Section
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
        #endregion Update Test Section
    }
}