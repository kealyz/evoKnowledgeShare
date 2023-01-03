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
            myContext.Notes.AddRange(myNotes);
            myContext.SaveChanges();
        }

        #region Get Section
        [Test]
        public async Task NoteController_GetNotes_ShouldReturnWithOk()
        {
            Uri getUri = new("/api/Note/", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task NoteController_GetNotes_ShouldReturnWithNoContent()
        {
            myContext.Notes.RemoveRange(myNotes);
            myContext.SaveChanges();

            Uri getUri = new("/api/Note/", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }
        [Test]
        public async Task NoteController_GetById_ShouldReturnWithOk()
        {
            Uri getUri = new($"/api/Note/{myNotes[0].NoteId}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task NoteController_GetById_ShouldReturnWithNotFound()
        {
            Uri getUri = new($"/api/Note/{Guid.NewGuid()}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
        [Test]
        public async Task NoteController_GetByUserId_ShouldReturnWithOk()
        {
            Uri getUri = new($"/api/Note/byUserId/{myNotes[0].UserId}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task NoteController_GetByUserId_ShouldReturnWithNotFound()
        {
            Uri getUri = new($"/api/Note/byUserId/{Guid.NewGuid()}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
        [Test]
        public async Task NoteController_GetByTopicId_ShouldReturnWithOk()
        {
            Uri getUri = new($"/api/Note/byTopicId/{myNotes[0].TopicId}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task NoteController_GetByTopicId_ShouldReturnWitNotFound()
        {
            Uri getUri = new($"/api/Note/byTopicId/{8}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
        [Test]
        public async Task NoteController_GetByDescription_ShouldReturnWithOk()
        {
            Uri getUri = new($"/api/Note/byDescription/{myNotes[1].Description}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task NoteController_GetByDescription_ShouldReturnWitNotFound()
        {
            Uri getUri = new($"/api/Note/byDescription/Description_Test", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
        [Test]
        public async Task NoteController_GetByTitle_ShouldReturnWithOk()
        {
            Uri getUri = new($"/api/Note/byTitle/{myNotes[1].Title}", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task NoteController_GetByTitle_ShouldReturnWithNotFound()
        {
            Uri getUri = new($"/api/Note/byTitle/Title_Test", UriKind.Relative);

            // Action
            HttpResponseMessage response = await myClient.GetAsync(getUri);
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
        #endregion Get Section

        #region Add Section
        [Test]
        public async Task NoteController_AddAsync_ShouldAddNoteAndReturnWithCreated()
        {
            // Arrange
            Uri postUri = new("/api/Note/", UriKind.Relative);
            Note note = new(Guid.NewGuid(), Guid.NewGuid(), 6, DateTimeOffset.Now, "Paint tovabbkepzes", "Photoshop helyett ingyenes paint");

            // Action
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, note);
            Note? actualNote = await response.Content.ReadFromJsonAsync<Note>();

            // Assert
            Assert.That(actualNote, Is.Not.Null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actualNote!.NoteId, Is.EqualTo(note.NoteId));
            Assert.That(actualNote!.Title, Is.EqualTo(note.Title));
        }
        [Test]
        public async Task NoteController_AddAsync_ShouldReturnWithBadRequest()
        {
            // Arrange
            Uri postUri = new("/api/Note/", UriKind.Relative);
            Guid guid = myNotes[2].NoteId;
            Note note = new(guid, Guid.NewGuid(), 6, DateTimeOffset.Now, "Paint tovabbkepzes", "Photoshop helyett ingyenes paint");

            // Action
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, note);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task NoteController_AddRangeAsync_ShouldAddNotesAndReturnWithCreated()
        {
            // Arrange
            Uri postUri = new("/api/Note/createRange", UriKind.Relative);
            Note note = new(Guid.NewGuid(), Guid.NewGuid(), 6, DateTimeOffset.Now, "Paint tovabbkepzes", "Photoshop helyett ingyenes paint");
            Note note2 = new(Guid.NewGuid(), Guid.NewGuid(), 7, DateTimeOffset.Now, "Word szovegfejlesztes", "Word");

            IEnumerable<Note> notes= new[] { note, note2 };

            // Action
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, notes);
            IEnumerable<Note>? actualNotes = await response.Content.ReadFromJsonAsync<IEnumerable<Note>>();

            // Assert
            Assert.That(actualNotes, Is.Not.Empty);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        [Test]
        public async Task NoteController_AddRangeAsync_ShouldReturnWithBadRequest()
        {
            // Arrange
            Uri postUri = new("/api/Note/createRange", UriKind.Relative);
            IEnumerable<Note> notes = new[] { myNotes[0], myNotes[1] };

            // Action
            HttpResponseMessage response = await myClient.PostAsJsonAsync(postUri, notes);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }
        #endregion Add Section

        #region Remove Section
        [Test]
        public async Task NoteController_Remove_ShouldRemoveNoteAndReturnWithNoContent()
        {
            // Arrange
            Uri removeUri = new($"/api/Note/", UriKind.Relative);
            // Action
            HttpRequestMessage requestMessage = new(HttpMethod.Delete, removeUri);
            requestMessage.Content = JsonContent.Create(myNotes[0]);
            
            HttpResponseMessage response = await myClient.SendAsync(requestMessage);

            // Assert
            Assert.IsTrue(myContext.Notes.FirstOrDefault(x => x.NoteId == myNotes[0].NoteId) == null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }
        [Test]
        public async Task NoteController_Remove_ShouldReturnWithNotFound()
        {
            // Arrange
            Uri removeUri = new($"/api/Note/", UriKind.Relative);
            myNotes[0].NoteId = Guid.NewGuid();
            // Action
            HttpRequestMessage requestMessage = new(HttpMethod.Delete, removeUri);
            requestMessage.Content = JsonContent.Create(myNotes[0]);

            HttpResponseMessage response = await myClient.SendAsync(requestMessage);

            // Assert
            Assert.IsTrue(myContext.Notes.FirstOrDefault(x => x.NoteId == myNotes[0].NoteId) == null);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
        [Test]
        public async Task NoteController_ById_ShouldRemoveNoteAndReturnWithNoContent()
        {
            // Arrange
            Uri removeUri = new($"/api/Note/byId/{myNotes[0].NoteId}", UriKind.Relative);
            Guid guid = myNotes[0].NoteId;
            // Action
            HttpResponseMessage response = await myClient.DeleteAsync(removeUri);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.IsTrue(myContext.Notes.FirstOrDefault(x => x.NoteId == guid) == null);
        }
        [Test]
        public async Task NoteController_ById_ShouldReturnWithNotFound()
        {
            // Arrange
            Uri removeUri = new($"/api/Note/byId/{Guid.NewGuid()}", UriKind.Relative);
            Guid guid = myNotes[0].NoteId;
            // Action
            HttpResponseMessage response = await myClient.DeleteAsync(removeUri);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.IsFalse(myContext.Notes.FirstOrDefault(x => x.NoteId == guid) == null);
        }
        #endregion Remove Section

        #region Modify Section

        [Test]
        public async Task NoteController_Update_ShouldReturnWithOkAndWithTheUpdatedNote()
        {

            // Action
            Uri updateUri = new("/api/Note/", UriKind.Relative);
            myNotes[0].Title = "Updated Title";

            HttpResponseMessage updateResponse = await myClient.PutAsJsonAsync(updateUri, myNotes[0]);
            Note? actualNote = await updateResponse.Content.ReadFromJsonAsync<Note>();

            // Assert
            Assert.That(actualNote, Is.Not.Null);
            Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualNote!.NoteId, Is.EqualTo(myNotes[0].NoteId));
            Assert.That(actualNote!.Title, Is.EqualTo(myNotes[0].Title));
            Assert.That(actualNote!.Title, Is.EqualTo("Updated Title"));
            Assert.That(myNotes[0].Title, Is.EqualTo("Updated Title"));

        }
        [Test]
        public async Task NoteController_Update_ShouldReturnWithNotFound()
        {

            // Action
            Uri updateUri = new("/api/Note/", UriKind.Relative);
            Note note=new(Guid.NewGuid(), Guid.NewGuid(), 1, DateTimeOffset.Now, "Paint ismeretek megszerzese", "Paint>Photoshop");

            HttpResponseMessage updateResponse = await myClient.PutAsJsonAsync(updateUri, note);
            Note? actualNote = await updateResponse.Content.ReadFromJsonAsync<Note>();

            // Assert
            Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
        #endregion Modify Section

    }
}
