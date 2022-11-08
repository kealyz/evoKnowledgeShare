using evoKnowledgeShare.Backend;
using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;

using NUnit.Framework;

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace evoKnowledgeShare.IntegrationTests.Controllers
{
    public class UserControllerTests
    {
        private readonly EvoKnowledgeDbContext myContext;
        private readonly HttpClient myClient;
        private readonly User[] myUsers = new User[]
        {
            new User(1, "Mika", "Kalman", "Mikorka"),
            new User(2, "Elag", "Agnes", "Elektrom"),
            new User(3, "Csevi", "Virag", "Cserepes"),
            new User(4, "Fuim", "Imre", "Futy"),
            new User(5, "Arak", "Aron", "Akcios")
        };

        public UserControllerTests()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .ConfigureTestServices(services => services.AddDbContext<EvoKnowledgeDbContext>(options => options.UseInMemoryDatabase("TestingDB")))
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            myContext = server.Host.Services.GetRequiredService<EvoKnowledgeDbContext>();
            myClient = server.CreateClient();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            myClient.Dispose();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UserController_GetUsers_ReturnsAllUsers()
        {
            // Arrange
            myContext.Users.AddRange(myUsers);
            myContext.SaveChanges();
            Uri getUri = new Uri("/api/User/Users", UriKind.Relative);

            // Action
            Task<HttpResponseMessage> response = myClient.GetAsync(getUri);
            HttpResponseMessage responseMessage = response.Result;
            Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.That(HttpStatusCode.OK, Is.EqualTo(responseMessage.StatusCode));
        }
    }
}