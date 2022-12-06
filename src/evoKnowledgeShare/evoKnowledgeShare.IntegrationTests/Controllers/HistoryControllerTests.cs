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
        [SetUp]
        public void SetUp()
        {
          
        }

        [Test]
        public async Task HistoryController_GetHistories_NoHistoriesFound()
        {
            Uri getUri = new Uri("/api/History/Histories", UriKind.Relative);

            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }


        [Test]
        public async Task HistoryController_GetHistories_ReturnAllHistories()
        {
            var entity = new HistoryDTO("Activity param", new DateTimeOffset(), "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001");
            await myContext.Histories.AddAsync(new History(entity));
            myContext.SaveChanges();
            Uri getUri = new Uri("/api/History/Histories", UriKind.Relative);

            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task HistoryController_CreateHistory_HistorySuccessfullyCreated()
        {
            Uri postUri = new Uri("/api/History/Create", UriKind.Relative);
            HistoryDTO historyDTO = new HistoryDTO("Activity param", new DateTimeOffset(), "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001");
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, historyDTO);
            
            //var r = await response.Content.ReadAsStringAsync();
            History history = await response.Content.ReadFromJsonAsync<History>();

            Assert.That(history, Is.Not.Null);
        }
    }
}
