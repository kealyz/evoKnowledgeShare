using evoKnowledgeShare.Backend.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        public async Task UserController_GetUserById_ReturnsUserWithId()
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
        public async Task UserController_GetUserById_UserNotFound_NoUserFound()
        {
            // Arrange
            Uri getUri = new Uri($"/api/User/Users/{Guid.NewGuid()}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UserController_CreateUser_UserSuccessfullyCreated()
        {
            // Arrange
            Uri postUri = new Uri("/api/User/Create", UriKind.Relative);
            User user = new User(Guid.NewGuid(), "Mika", "Kalman", "Mikorka");

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

        [Test]
        public async Task UserController_DeleteUser_UserSuccessfullyDeleted()
        {
            // Arrange
            myContext.Users.AddRange(myUsers);
            myContext.SaveChanges();
            Uri deleteUri = new Uri($"/api/User/Delete/{myUsers[0].Id}", UriKind.Relative);
            Uri getUri = new Uri($"/api/User/Users", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.DeleteAsync(deleteUri);
            HttpResponseMessage getRemaining = await myClient.GetAsync(getUri);
            List<User>? remainingUsers = await getRemaining.Content.ReadFromJsonAsync<List<User>>();

            // Assert
            Assert.That(remainingUsers, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(remainingUsers, !Does.Contain(myUsers[0]));
        }

        [Test]
        public async Task UserController_DeleteUser_NoUsersFound()
        {
            // Arrange
            Uri deleteUri = new Uri($"/api/User/Delete/{Guid.NewGuid()}", UriKind.Relative);

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
            myContext.AddRange(myUsers);
            myContext.SaveChanges();
            Uri putUri = new Uri("/api/User/Update", UriKind.Relative);
            User putUser = new User(myUsers[0].Id, "TestUser", "Test", "User");

            // Action
            HttpResponseMessage response = await myClient.PutAsJsonAsync(putUri, putUser);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            User? actualUser = await response.Content.ReadFromJsonAsync<User>();

            // Assert
            Assert.That(actualUser, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actualUser!.Id, Is.EqualTo(putUser.Id));
            Assert.That(actualUser!.UserName, Is.EqualTo(putUser.UserName));
            Assert.That(actualUser!.FirstName, Is.EqualTo(putUser.FirstName));
            Assert.That(actualUser!.LastName, Is.EqualTo(putUser.LastName));
        }

        [Test]
        public async Task UserController_UpdateUser_NoUsersFound()
        {
            // Arrange
            Uri putUri = new Uri("/api/Update", UriKind.Relative);
            User putUser = new User(Guid.NewGuid(), "TestUser", "Test", "User");

            // Action
            HttpResponseMessage response = await myClient.PutAsJsonAsync(putUri, putUser);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task UserController_UpdateRange_UsersSuccessfullyUpdated()
        {
            // Arrange
            myContext.Users.AddRange(myUsers);
            myContext.SaveChanges();
            Uri putUri = new Uri("/api/User/UpdateRange", UriKind.Relative);
            Uri getUri = new Uri($"/api/User/Users", UriKind.Relative);
            User[] putUsers = new User[]
            {
                new User(myUsers[1].Id, "TestUser1", "Test", "User"),
                new User(myUsers[2].Id, "TestUser2", "Test", "User")
            };

            // Action
            HttpResponseMessage response = await myClient.PutAsJsonAsync(putUri, putUsers);
            HttpResponseMessage getUpdatedUsers = await myClient.GetAsync(getUri);
            Console.WriteLine(await getUpdatedUsers.Content.ReadAsStringAsync());
            List<User>? updatedUsers = await getUpdatedUsers.Content.ReadFromJsonAsync<List<User>>();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(updatedUsers![1].Id, Is.EqualTo(putUsers[0].Id));
            Assert.That(updatedUsers![1].FirstName, Is.EqualTo(putUsers[0].FirstName));
            Assert.That(updatedUsers![1].LastName, Is.EqualTo(putUsers[0].LastName));
            Assert.That(updatedUsers![2].Id, Is.EqualTo(putUsers[1].Id));
            Assert.That(updatedUsers![2].FirstName, Is.EqualTo(putUsers[1].FirstName));
            Assert.That(updatedUsers![2].LastName, Is.EqualTo(putUsers[1].LastName));
        }
    }
}