using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace evoKnowledgeShare.IntegrationTests.Controllers
{
    [TestFixture]
    public class TopicControllerTests : ControllerTestBase
    {
        private Topic[] myTopics = default!;

        [SetUp]
        public void Setup()
        {
            myTopics = new Topic[]
            {
                new Topic(Guid.NewGuid(), "Test Topic Title 1."),
                new Topic(Guid.NewGuid(), "Test Topic Title 2."),
                new Topic(Guid.NewGuid(), "Test Topic Title 3."),
                new Topic(Guid.NewGuid(), "Test Topic Title 4."),
                new Topic(Guid.NewGuid(), "Test Topic Title 5."),
            };
        }

        #region Get Test Section
        [Test]
        public async Task TopicController_GetTopics_ReturnsAllTopics()
        {
            // Arrange
            myContext.Topics.AddRange(myTopics);
            myContext.SaveChanges();
            Uri getUri = new Uri("/api/Topic/All", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task TopicController_GetTopics_NoTopicsFound()
        {
            // Arrange
            Uri getUri = new Uri("/api/Topic/All", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task TopicController_GetTopicById_ReturnsTopicWithId1()
        {
            // Arrange
            myContext.Topics.AddRange(myTopics);
            myContext.SaveChanges();
            Uri getUri = new Uri($"/api/Topic/{myTopics[0].Id}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Topic? actualTopic = await response.Content.ReadFromJsonAsync<Topic>(new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, default);

            // Assert
            Assert.That(actualTopic, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualTopic!.Id, Is.EqualTo(myTopics[0].Id));
            Assert.That(actualTopic!.Title, Is.EqualTo(myTopics[0].Title));
        }

        [Test]
        public async Task TopicController_GetTopicById_NoTopicWithIdFound()
        {
            // Arrange
            Uri getUri = new Uri($"/api/Topic/{myTopics[0].Id}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task TopicController_GetTopicsByTitle_ReturnsTopicsWithSpecificTitle()
        {
            // Arrange
            myContext.Topics.AddRange(myTopics);
            myContext.SaveChanges();
            Uri getUri = new Uri($"/api/Topic/ByTitle/{myTopics[0].Title}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            IEnumerable<Topic>? actualTopics = await response.Content.ReadFromJsonAsync<IEnumerable<Topic>>();

            // Assert
            Assert.That(actualTopics, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            CollectionAssert.AreEqual(actualTopics, myContext.Topics.Where(t => t.Title == myTopics[0].Title));
        }

        [Test]
        public async Task TopicController_GetTopicsByTitle_NoTopicWithTitleFound()
        {
            // Arrange
            Uri getUri = new Uri($"/api/Topic/TopicTitle/{myTopics[0].Title}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
        #endregion Get Test Section

        #region Add Test Section
        [Test]
        public async Task TopicController_CreateTopic_TopicSuccessfullyCreated()
        {
            // Arrange
            Uri postUri = new Uri("/api/Topic/Create", UriKind.Relative);
            TopicDTO topicDTO = new TopicDTO(myTopics[0].Title);

            // Action
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, topicDTO);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Topic? actualTopic = await response.Content.ReadFromJsonAsync<Topic>();

            // Assert
            Assert.That(actualTopic, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actualTopic!.Title, Is.EqualTo(topicDTO.Title));
        }
        #endregion Add Test Section

        #region Remove Test Section

        #endregion Remove Test Section

        #region Update Test Section
        [Test]
        public async Task TopicController_UpdateTopic_TopicSuccessfullyUpdated()
        {
            // Arrange
            Uri postUri = new Uri("/api/Topic/Create", UriKind.Relative);
            Topic topic = new Topic(myTopics[0].Id, myTopics[0].Title);

            // Action
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, topic);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            Uri updateUri = new Uri("/api/Topic/Update", UriKind.Relative);
            topic.Title = "Updated Title";

            HttpResponseMessage updateResponse = await myClient.PostAsJsonAsync(postUri, topic);
            Console.WriteLine(await updateResponse.Content.ReadAsStringAsync());
            Topic? actualTopic = await updateResponse.Content.ReadFromJsonAsync<Topic>();

            // Assert
            Assert.That(actualTopic!.Title, Is.EqualTo(topic.Title));
        }
        #endregion Update Test Section
    }
}