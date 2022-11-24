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
               new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"), "Activity param", new DateTimeOffset(),
               "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001"),
               new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b25"), "Activity param", new DateTimeOffset(),
               "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001")
         };
        }

        [Test]
        public async Task HistoryController_GetHistories_ReturnAllHistories()
        {
            var entity = new History(new Guid("27181d48-4b43-455b-ac50-39ae783a5b24"), "Activity param", new DateTimeOffset(),
               "0.1", new Guid("6b40ce07-e6f3-4a16-a5ae-989cca872a57"), "PK001");
            await myContext.Histories.AddAsync(entity);
            myContext.SaveChanges();
            //myContext.AddRange(myHistories);
            //myContext.SaveChanges();
            Uri getUri = new Uri("/api/History/Histories", UriKind.Relative);

            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /*
        [Test]
        public async Task HistoryController_CreateHistory_HistorySuccessfullyCreated()
        {

        }
        */

    }
}
