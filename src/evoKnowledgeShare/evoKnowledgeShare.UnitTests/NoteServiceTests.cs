using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evoKnowledgeShare.UnitTests
{
    public class NoteServiceTests
    {

        private NoteService myNoteService;
        private Mock<IRepository<Note>> myRepositoryMock;

        [SetUp]
        public void Setup()
        {
            myRepositoryMock = new Mock<IRepository<Note>>(MockBehavior.Strict);
            myNoteService = new NoteService(myRepositoryMock.Object);
            myRepositoryMock.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<Note>()
                {
                    new Note(Guid.Parse("cdf2648a-2a53-4e7a-9d7a-6daae5c3e10c"),"11111111",1,new DateTimeOffset(2022, 10, 23, 12, 0, 0, 10, new TimeSpan(1, 0, 0)),"Leiras1","Title1"),
                    new Note(Guid.Parse("d8592323-486f-480e-bb7b-f58810cdb7e2"),"22222222",2,new DateTimeOffset(2022, 10, 23, 13, 10, 0, 10, new TimeSpan(1, 0, 0)),"Leiras2","Title2"),
                    new Note(Guid.Parse("d9e2aed2-a6b4-48ed-a133-6cc8198b2f24"),"33333333",3,new DateTimeOffset(2022, 10, 23, 14, 20, 0, 10, new TimeSpan(1, 0, 0)),"Leiras3","Title3")
                };
            });
            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(() =>
            {
                return new List<Note>()
                {
                    new Note(Guid.Parse("cdf2648a-2a53-4e7a-9d7a-6daae5c3e10c"),"11111111",1,new DateTimeOffset(2022, 10, 23, 12, 0, 0, 10, new TimeSpan(1, 0, 0)),"Leiras1","Title1"),
                    new Note(Guid.Parse("d8592323-486f-480e-bb7b-f58810cdb7e2"),"22222222",2,new DateTimeOffset(2022, 10, 23, 13, 10, 0, 10, new TimeSpan(1, 0, 0)),"Leiras2","Title2"),
                    new Note(Guid.Parse("d9e2aed2-a6b4-48ed-a133-6cc8198b2f24"),"33333333",3,new DateTimeOffset(2022, 10, 23, 14, 20, 0, 10, new TimeSpan(1, 0, 0)),"Leiras3","Title3")
                };
            });

        }
        //Gets
        [Test]
        public void NoteService_GetAll_ShouldReturnAll()
        {
            var notes = myNoteService.GetAll();

            Assert.That(notes.Count, Is.EqualTo(3));
        }
        [Test]
        public void NoteService_GetNoteById_ShouldReturnASPecificNote()
        {
            Guid guid = Guid.Parse("cdf2648a-2a53-4e7a-9d7a-6daae5c3e10c");
            var note = myNoteService.getNoteById(guid);

            Assert.That(note.NoteId, Is.EqualTo(guid));
        }
        [Test]
        public void NoteService_getNoteByUserId_ShouldReturnSpecificNote()
        {
            string userId = "22222222";
            var note = myNoteService.getNoteByUserId(userId);

            Assert.That(note.UserId, Is.EqualTo(userId));
        }
        [Test]
        public void NoteService_getNoteByTopicId_ShouldReturnSpecificNote()
        {
            int topicId = 2;
            var note = myNoteService.getNoteByTopicId(topicId);

            Assert.That(note.TopicId, Is.EqualTo(topicId));
        }
        [Test]
        public void NoteService_getNoteByCreationTime_ShouldReturnSpecificNote()
        {
            DateTimeOffset date = new DateTimeOffset(2022, 10, 23, 13, 10, 0, 10, new TimeSpan(1, 0, 0));
            var note = myNoteService.getNoteByCreationTime(date);

            Assert.That(note.CreatedAt, Is.EqualTo(date));
        }
        [Test]
        public void NoteService_getNoteByDescription_ShouldReturnSpecificNote()
        {
            string description = "Leiras1";
            Note note = myNoteService.getNoteByDescription(description);

            Assert.That(note.Description, Is.EqualTo(description));
        }
        [Test]
        public void NoteService_getNoteByTitle_ShouldReturnSpecificNote()
        {
            string title = "Title1";
            Note note = myNoteService.getNoteByTitle(title);

            Assert.That(note.Title, Is.EqualTo(title));
        }

        //Gets but async
        [Test]
        public async Task NoteService_GetAllAsync_ShouldReturnAll()
        {
            var notes = await myNoteService.GetAllAsync();


            Assert.That(notes.Count(), Is.EqualTo(3));
        }
        [Test]
        public async Task NoteService_GetNoteByIdAsync_ShouldReturnASPecificNote()
        {
            Guid guid = Guid.Parse("cdf2648a-2a53-4e7a-9d7a-6daae5c3e10c");
            var note = await myNoteService.getNoteByIdAsync(guid);

            Assert.That(note.NoteId, Is.EqualTo(guid));
        }
        [Test]
        public async Task NoteService_getNoteByUserIdAsync_ShouldReturnSpecificNote()
        {
            string userId = "22222222";
            var note = await myNoteService.getNoteByUserIdAsync(userId);

            Assert.That(note.UserId, Is.EqualTo(userId));
        }
        [Test]
        public async Task NoteService_getNoteByTopicIdAsync_ShouldReturnSpecificNote()
        {
            int topicId = 2;
            var note = await myNoteService.getNoteByTopicIdAsync(topicId);

            Assert.That(note.TopicId, Is.EqualTo(topicId));
        }
        [Test]
        public async Task NoteService_getNoteByCreationTimeAsync_ShouldReturnSpecificNote()
        {
            DateTimeOffset date = new DateTimeOffset(2022, 10, 23, 13, 10, 0, 10, new TimeSpan(1, 0, 0));
            var note = await myNoteService.getNoteByCreationTimeAsync(date);

            Assert.That(note.CreatedAt, Is.EqualTo(date));
        }
        [Test]
        public async Task NoteService_getNoteByDescriptionAsync_ShouldReturnSpecificNote()
        {
            string description = "Leiras1";
            var note = await myNoteService.getNoteByDescriptionAsync(description);

            Assert.That(note.Description, Is.EqualTo(description));
        }
        [Test]
        public async Task NoteService_getNoteByTitleAsync_ShouldReturnSpecificNote()
        {
            string title = "Title1";
            var note = await myNoteService.getNoteByTitleAsync(title);

            Assert.That(note.Title, Is.EqualTo(title));
        }
    }
}
