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
                    new Note(new Guid(),"z004cj8m",1,DateTimeOffset.Now,"Description","Title")
                };
            });
            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(() =>
            {
                return new List<Note>()
                {
                    new Note(new Guid(),"z004cj8m",1,DateTimeOffset.Now,"Description","Title")
                };
            });
            myRepositoryMock.Setup(x => x.GetByGuid(Guid.Parse("cdf2648a-2a53-4e7a-9d7a-6daae5c3e10c"))).Returns(() =>
            {
                return new Note(Guid.Parse("cdf2648a-2a53-4e7a-9d7a-6daae5c3e10c"), "z004cj8m", 1, DateTimeOffset.Now, "Description", "Title");
            });
            myRepositoryMock.Setup(x => x.GetByUserId("z004cj8m")).Returns(() =>
            {
                return new Note(new Guid(), "z004cj8m", 1, DateTimeOffset.Now, "Description", "Title");
            });
            myRepositoryMock.Setup(x => x.GetByTopicId(2)).Returns(() =>
            {
                return new Note(new Guid(), "alma", 2, DateTimeOffset.Now, "Description", "Title");
            });
            myRepositoryMock.Setup(x => x.GetByCreationTime(new DateTimeOffset(2008, 5, 1, 8, 6, 32, 545, new TimeSpan(1, 0, 0)))).Returns(() =>
            {
                return new Note(new Guid(), "korte", 1, new DateTimeOffset(2008, 5, 1, 8, 6, 32, 545,new TimeSpan(1, 0, 0)), "Description", "Title");
            });
            myRepositoryMock.Setup(x => x.GetByDescription("Leiras")).Returns(() =>
            {
                return new Note(new Guid(), "barack", 1, DateTimeOffset.Now, "Leiras", "Title");
            });
            myRepositoryMock.Setup(x => x.GetByTitle("Cim")).Returns(() =>
            {
                return new Note(new Guid(), "uborka", 1, DateTimeOffset.Now, "Description", "Cim");
            });

        }
        [Test]
        public void NoteService_GetAll_ShouldReturnAll()
        {
            var notes = myNoteService.GetAll();

            Assert.That(notes.Count, Is.EqualTo(1));
        }
        [Test]
        public async Task NoteService_GetAllAsync_ShouldReturnAll()
        {
            var notes = await myNoteService.GetAllAsync();


            Assert.That(notes.Count(), Is.EqualTo(1));
        }
        [Test]
        public void NoteService_GetNoteByGuid_ShouldReturnASPecificNote()
        {
            var note = myNoteService.getNoteByGuid(Guid.Parse("cdf2648a-2a53-4e7a-9d7a-6daae5c3e10c"));

            Assert.That(note.UserId, Is.EqualTo("z004cj8m"));
        }
        [Test]
        public void NoteService_getNoteByUserId_ShouldReturnSpecificNote()
        {
            Note note = myNoteService.getNoteByUserId("z004cj8m");

            Assert.That(note.Description, Is.EqualTo("Description"));
        }
        [Test]
        public void NoteService_getNoteByTopicId_ShouldReturnSpecificNote()
        {
            Note note = myNoteService.getNoteByTopicId(2);

            Assert.That(note.UserId, Is.EqualTo("alma"));
        }
        [Test]
        public void NoteService_getNoteByCreationTime_ShouldReturnSpecificNote()
        {
            Note note = myNoteService.getNoteByCreationTime(new DateTimeOffset(2008, 5, 1, 8, 6, 32, 545, new TimeSpan(1, 0, 0)));

            Assert.That(note.UserId, Is.EqualTo("korte"));
        }
        [Test]
        public void NoteService_getNoteByDescription_ShouldReturnSpecificNote()
        {
            Note note = myNoteService.getNoteByDescription("Leiras");

            Assert.That(note.UserId, Is.EqualTo("barack"));
        }
        [Test]
        public void NoteService_getNoteByTitle_ShouldReturnSpecificNote()
        {
            Note note = myNoteService.getNoteByTitle("Cim");

            Assert.That(note.UserId, Is.EqualTo("uborka"));
        }
    }
}
