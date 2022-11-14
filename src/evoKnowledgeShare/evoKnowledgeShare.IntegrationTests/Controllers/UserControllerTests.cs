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
using System.Net.Http.Json;
using System.Text.Json;

namespace evoKnowledgeShare.IntegrationTests.Controllers
{
    public class UserControllerTests
    {
        private EvoKnowledgeDbContext myContext = default!;
        private HttpClient myClient = default!;
        private readonly User[] myUsers = new User[]
        {
            new User(1, "Mika", "Kalman", "Mikorka"),
            new User(2, "Elag", "Agnes", "Elektrom"),
            new User(3, "Csevi", "Virag", "Cserepes"),
            new User(4, "Fuim", "Imre", "Futy"),
            new User(5, "Arak", "Aron", "Akcios")
        };

        [SetUp]
        public void Setup()
        {
            IWebHostBuilder builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .ConfigureTestServices(services => services.AddDbContext<EvoKnowledgeDbContext>(options => options.UseInMemoryDatabase("TestingDB")))
                .UseStartup<Startup>();

            TestServer server = new TestServer(builder);
            myContext = server.Host.Services.GetRequiredService<EvoKnowledgeDbContext>();
            myClient = server.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            myContext.Database.EnsureDeleted();
            myContext.Dispose();
            myClient.Dispose();
        }

        [Test]
        public async Task UserController_GetUsers_ReturnsAllUsers()
        {
            // Arrange
            myContext.Users.AddRange(myUsers);
            myContext.SaveChanges();
            Uri getUri = new Uri("/api/User/Users", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task UserController_GetUsers_NoUsersFound()
        {
            // Arrange
            Uri getUri = new Uri("/api/User/Users", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UserController_GetUserById_ReturnsUserWithId1()
        {
            // Arrange
            myContext.Users.AddRange(myUsers);
            myContext.SaveChanges();
            Uri getUri = new Uri($"/api/User/User/{myUsers[0].Id}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            User? actualUser = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            Assert.That(actualUser, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualUser!.Id, Is.EqualTo(myUsers[0].Id));
            Assert.That(actualUser!.FirstName, Is.EqualTo(myUsers[0].FirstName));
            Assert.That(actualUser!.LastName, Is.EqualTo(myUsers[0].LastName));
        }

        [Test]
        public async Task UserController_CreateUser_UserSuccessfullyCreated()
        {
            // Arrange
            Uri postUri = new Uri("/api/User/Create", UriKind.Relative);
            User user = new User(1, "Mika", "Kalman", "Mikorka");

            // Action
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, user);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            User? actualUser = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            Assert.That(actualUser, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actualUser!.Id, Is.EqualTo(user.Id));
            Assert.That(actualUser!.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(actualUser!.LastName, Is.EqualTo(user.LastName));
        }
    }
}