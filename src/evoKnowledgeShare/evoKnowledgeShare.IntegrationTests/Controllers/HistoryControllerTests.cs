using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Models;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace evoKnowledgeShare.IntegrationTests.Controllers
{
    [TestFixture]
    public class HistoryControllerTests : ControllerTestBase
    {
        private History[] myHistories = default!;

        [SetUp]
        public void SetUp()
        {
            myHistories = new History[]
            {
              new History(Guid.NewGuid(), "Activity param", new DateTimeOffset(), "0.1", Guid.NewGuid(), "PK001"),
              new History(Guid.NewGuid(), "Activity param", new DateTimeOffset(), "0.1", Guid.NewGuid(), "PK001"),
              new History(Guid.NewGuid(), "Activity param", new DateTimeOffset(), "0.1", Guid.NewGuid(), "PK001")
            };
            myContext.Histories.AddRangeAsync(myHistories);
            myContext.SaveChangesAsync();
        }

        [Test]
        public async Task HistoryController_BadRequest_ShouldReturnBadRequest()
        {
            Uri getUri = new Uri("/api/History/BadRequest", UriKind.Relative);

            HttpResponseMessage response = await myClient.GetAsync(getUri);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task HistoryController_GetHistories_ShouldReturnOk()
        {
            Uri getUri = new Uri("/api/History", UriKind.Relative);

            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task HistoryController_GetById_ShouldReturnWithOk()
        {
            Uri getUri = new($"/api/History/{myHistories[0].Id}", UriKind.Relative);

            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

         [Test]
       public async Task HistoryController_GetById_ShouldRetrunNoContent()
        {
            Uri getUri = new($"/api/History/{Guid.NewGuid()}", UriKind.Relative);
            
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
   
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task HistoryController_GetById_ShouldReturnBadRequest()
        {
            Uri getUri = new($"/api/History/{"ez tuti nem guid"}", UriKind.Relative);

            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

       [Test]
        public async Task HistoryController_CreateHistory_ShouldReturnCreated()
        {
            Uri postUri = new Uri("/api/History", UriKind.Relative);
            HistoryDTO historyDTO = new HistoryDTO("Activity param", new DateTimeOffset(), "0.1",Guid.NewGuid(), "PK001");

            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, historyDTO)!;
            History history = await response.Content.ReadFromJsonAsync<History>();

            Assert.That(history, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(historyDTO.Activity, Is.EqualTo(history!.Activity));
        }

        [Test]
        public async Task HistoryController_CreateHistory_ShouldReturnArgumentException()
        {
            Uri postUri = new Uri("/api/History", UriKind.Relative);
       
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, myHistories[0].Id);
            History history = await response.Content.ReadFromJsonAsync<History>();
         
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }
    }
}
