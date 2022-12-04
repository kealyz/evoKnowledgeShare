using evoKnowledgeShare.Backend.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace evoKnowledgeShare.IntegrationTests.Controllers
{
    internal class NoteControllerTest : ControllerTestBase
    {
        private Note[] myNotes = default!;

        [SetUp]
        public void Setup()
        {
            myNotes = new Note[]
            {
                new Note(Guid.NewGuid(), Guid.NewGuid(), 1, DateTimeOffset.Now, "C# fejlesztes", "Kezdo C#"),
                new Note(Guid.NewGuid(), Guid.NewGuid(), 2, DateTimeOffset.Now, "Java fejlesztes", "Halado java"),
                new Note(Guid.NewGuid(), Guid.NewGuid(), 3, DateTimeOffset.Now, "Assembly fejlesztes", "KYS"),
                new Note(Guid.NewGuid(), Guid.NewGuid(), 4, DateTimeOffset.Now, "Python fejlesztes", "Kigyo vagy"),
                new Note(Guid.NewGuid(), Guid.NewGuid(), 5, DateTimeOffset.Now, "C es C++ fejlesztes", "Kezd az alapoktol")
            };
        }

        #region Get Section
        [Test]
        public async Task NoteController_GetNotes_ShouldReturnWithOk()
        {
            // Arrange
            myContext.Notes.AddRange(myNotes);
            myContext.SaveChanges();

            Uri getUri = new Uri("/api/Note/all", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task NoteController_GetById_ShouldReturnWithOke()
        {
            // Arrange
            myContext.Notes.AddRange(myNotes);
            myContext.SaveChanges();

            Uri getUri = new Uri($"/api/Note/{myNotes[0].NoteId}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task NoteController_GetByUserId_ShouldReturnWithOk()
        {
            // Arrange
            myContext.Notes.AddRange(myNotes);
            myContext.SaveChanges();

            Uri getUri = new Uri($"/api/Note/byUserId/{myNotes[0].UserId}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task NoteController_GetByTopicId_ShouldReturnWithOk()
        {
            // Arrange
            myContext.Notes.AddRange(myNotes);
            myContext.SaveChanges();

            Uri getUri = new Uri($"/api/Note/byTopicId/{myNotes[0].TopicId}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task NoteController_GetByDescription_ShouldReturnWithOk()
        {
            // Arrange
            myContext.Notes.AddRange(myNotes);
            myContext.SaveChanges();

            Uri getUri = new Uri($"/api/Note/byDescription/{myNotes[1].Description}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task NoteController_GetByTitle_ShouldReturnWithOk()
        {
            // Arrange
            myContext.Notes.AddRange(myNotes);
            myContext.SaveChanges();

            Uri getUri = new Uri($"/api/Note/byTitle/{myNotes[1].Title}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        #endregion Get Section

        #region Add Section

        #endregion Add Section

        #region Remove Section

        #endregion Remove Section

        #region Modify Section

        #endregion Modify Section
    }
}
