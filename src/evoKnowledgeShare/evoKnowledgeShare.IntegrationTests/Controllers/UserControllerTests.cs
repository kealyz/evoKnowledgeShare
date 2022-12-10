using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Models;
using Microsoft.AspNetCore.Http;
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
    public class UserControllerTests : ControllerTestBase
    {
        private User[] myUsers = default!;

        [SetUp]
        public void Setup()
        {
            myUsers = new User[]
            {
                new User(Guid.NewGuid(), "Mika", "Kalman", "Mikorka"),
                new User(Guid.NewGuid(), "Elag", "Agnes", "Elektrom"),
                new User(Guid.NewGuid(), "Csevi", "Virag", "Cserepes"),
                new User(Guid.NewGuid(), "Fuim", "Imre", "Futy"),
                new User(Guid.NewGuid(), "Arak", "Aron", "Akcios")
            };
            myContext.Users.AddRange(myUsers);
            myContext.SaveChanges();
        }

        [Test]
        public async Task UserController_GetUsers_ReturnsAllUsers()
        {
            // Arrange
            Uri getUri = new Uri("/api/User", UriKind.Relative);

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
            myContext.Database.EnsureDeleted();
            Uri getUri = new Uri("/api/User", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UserController_GetUserById_ReturnsUserById()
        {
            // Arrange
            Uri getUri = new Uri($"/api/User/{myUsers[0].Id}", UriKind.Relative);

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
        public async Task UserController_GetUserById_NoUserFound()
        {
            // Arrange
            Uri getUri = new Uri($"/api/User/{Guid.NewGuid()}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UserController_GetUserByUserName_ReturnsUserByUserName()
        {
            // Arrange
            Uri getUri = new Uri($"api/User/ByUserName/{myUsers[0].UserName}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            User? actualUser = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            CollectionAssert.Contains(myContext.Users, actualUser);
        }

        [Test]
        public async Task UserController_GetUserByUserName_NoUserFound()
        {
            // Arrange
            Uri getUri = new Uri($"api/User/ByUserName/Admin", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UserController_GetUserRangeById_ReturnsUserRangeWithIds()
        {
            // Arrange
            Guid[] ids = { myUsers[0].Id, myUsers[1].Id };
            Uri postUri = new Uri($"/api/User/UserRange/{ids}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, ids);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            List<User>? actualUsers = await response.Content.ReadFromJsonAsync<List<User>>();
            //var actualUsers = JsonSerializer.Deserialize<List<User>>(await response.Content.ReadAsStreamAsync());
            //var actualUsers = response.Content.ReadFromJsonAsync<List<User>>().Result;

            // Assert
            CollectionAssert.Contains(myContext.Users, actualUsers![0]);
            CollectionAssert.Contains(myContext.Users, actualUsers![1]);
        }

        [Test]
        public async Task UserController_CreateUser_UserSuccessfullyCreated()
        {
            // Arrange
            Uri postUri = new Uri("/api/User", UriKind.Relative);
            UserDTO userDTO = new UserDTO("Mika", "Kalman", "Mikorka");

            // Action
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, userDTO);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            User? actualUser = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            Assert.That(actualUser, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actualUser!.UserName, Is.EqualTo(userDTO.UserName));
            Assert.That(actualUser!.FirstName, Is.EqualTo(userDTO.FirstName));
            Assert.That(actualUser!.LastName, Is.EqualTo(userDTO.LastName));

        }

        [Test]
        public async Task UserController_DeleteUser_UserSuccessfullyDeleted()
        {
            // Arrange
            Uri deleteUri = new Uri($"/api/User", UriKind.Relative);

            // Action
            HttpRequestMessage requestMessage = new(HttpMethod.Delete, deleteUri);
            requestMessage.Content = JsonContent.Create(myUsers[0]);

            HttpResponseMessage response = await myClient.SendAsync(requestMessage);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            CollectionAssert.DoesNotContain(myContext.Users, myUsers[0]);
        }

        [Test]
        public async Task UserController_DeleteUser_NoUserFound()
        {
            // Arrange
            Uri deleteUri = new Uri($"/api/User", UriKind.Relative);
            myUsers[0].Id = Guid.NewGuid();

            // Action
            HttpRequestMessage requestMessage = new(HttpMethod.Delete, deleteUri);
            requestMessage.Content = JsonContent.Create(myUsers[0]);

            HttpResponseMessage response = await myClient.SendAsync(requestMessage);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UserController_DeleteUserById_UserSuccessfullyDeleted()
        {
            // Arrange
            Uri deleteUri = new Uri($"/api/User/{myUsers[0].Id}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.DeleteAsync(deleteUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            CollectionAssert.DoesNotContain(myContext.Users, myUsers[0]);
        }

        [Test]
        public async Task UserController_DeleteUserById_NoUserFound()
        {
            // Arrange
            Uri deleteUri = new Uri($"/api/User/{Guid.NewGuid()}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.DeleteAsync(deleteUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        /* Pl. környezeti hibákra van fenntartva
        [Test]
        public async Task UserController_DeleteUser_BadRequest()
        {
            // Arrange
            Uri deleteUri = new Uri($"/api/User/Delete/{1}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.DeleteAsync(deleteUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }*/

        [Test]
        public async Task UserController_UpdateUser_UserSuccessfullyUpdated()
        {
            // Arrange
            Uri putUri = new Uri("/api/User", UriKind.Relative);
            myUsers[0].UserName = "Admin";

            // Action
            HttpResponseMessage response = await myClient.PutAsJsonAsync(putUri, myUsers[0]);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            User? actualUser = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            Assert.That(actualUser, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actualUser!.UserName, Is.EqualTo(myUsers[0].UserName));
            Assert.That(actualUser!.FirstName, Is.EqualTo(myUsers[0].FirstName));
            Assert.That(actualUser!.LastName, Is.EqualTo(myUsers[0].LastName));
            CollectionAssert.Contains(myContext.Users, myUsers[0]);
        }

        [Test]
        public async Task UserController_UpdateUser_CreatedButDoesNotUpdate()
        {
            // Arrange
            Uri putUri = new Uri("/api/User", UriKind.Relative);
            User putUser = new User(Guid.NewGuid(), "TestUser", "Test", "User");

            // Action
            HttpResponseMessage response = await myClient.PutAsJsonAsync(putUri, putUser);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(myContext.Users.Count, Is.EqualTo(myUsers.Count()));
        }

        [Test]
        public async Task UserController_UpdateRange_UsersSuccessfullyUpdated()
        {
            // Arrange
            Uri putUri = new Uri("/api/User/UpdateRange", UriKind.Relative);
            myUsers[0].UserName = "Admin0";
            myUsers[1].UserName = "Admin1";
            User[] putUsers = new User[] { myUsers[0], myUsers[1] };

            // Action
            HttpResponseMessage response = await myClient.PutAsJsonAsync(putUri, putUsers);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            CollectionAssert.Contains(myContext.Users, myUsers[0]);
            CollectionAssert.Contains(myContext.Users, myUsers[1]);
        }
    }
}