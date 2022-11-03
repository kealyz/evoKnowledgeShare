using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

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

        }
        //Gets
        [Test]
        public void NoteService_GetAll_ShouldReturnAll()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            myRepositoryMock.Setup(x=>x.GetAll()).Returns(new List<Note>
            {
                expectedNote
            });

            var actualNotes = myNoteService.GetAll();

            Assert.That(actualNotes.Count, Is.EqualTo(1));
            Assert.That(actualNotes.First, Is.EqualTo(expectedNote));
        }
        [Test]
        public void NoteService_GetNoteById_ShouldReturnASpecificNote()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notExpectedNote = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Note>
            {
                expectedNote,
                notExpectedNote
            });

            var note = myNoteService.GetNoteById(expectedNote.NoteId);

            Assert.That(note, Is.EqualTo(expectedNote));
        }
        [Test]
        public void NoteService_getNotesByUserId_ShouldReturnSpecificNote()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notExpectedNote = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Note>
            {
                expectedNote,
                notExpectedNote
            });

            var note = myNoteService.GetNotesByUserId(expectedNote.UserId);

            Assert.That(note.Count, Is.EqualTo(1));
            Assert.That(note.First, Is.EqualTo(expectedNote));
        }
        [Test]
        public void NoteService_getNotesByTopicId_ShouldReturnSpecificNote()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notExpectedNote = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Note>
            {
                expectedNote,
                notExpectedNote
            });

            var note = myNoteService.GetNotesByTopicId(expectedNote.TopicId);

            Assert.That(note.Count, Is.EqualTo(1));
            Assert.That(note.First, Is.EqualTo(expectedNote));
        }
        [Test]
        public void NoteService_getNoteByDescription_ShouldReturnSpecificNote()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notExpectedNote = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Note>
            {
                expectedNote,
                notExpectedNote
            });

            var note = myNoteService.GetNoteByDescription(expectedNote.Description);

            Assert.That(note, Is.EqualTo(expectedNote));
        }
        [Test]
        public void NoteService_getNoteByTitle_ShouldReturnSpecificNote()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notExpectedNote = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Note>
            {
                expectedNote,
                notExpectedNote
            });

            var note = myNoteService.GetNoteByTitle(expectedNote.Title);

            Assert.That(note, Is.EqualTo(expectedNote));
        }

        //Gets but async
        [Test]
        public async Task NoteService_GetAllAsync_ShouldReturnAll()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Note>
            {
                expectedNote
            });

            var actualNotes = await myNoteService.GetAllAsync();

            Assert.That(actualNotes.Count, Is.EqualTo(1));
            Assert.That(actualNotes.First, Is.EqualTo(expectedNote));
        }
        [Test]
        public async Task NoteService_GetNoteByIdAsync_ShouldReturnASpecificNote()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notExpectedNote = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Note>
            {
                expectedNote,
                notExpectedNote
            });

            var note =await myNoteService.GetNoteByIdAsync(expectedNote.NoteId);

            Assert.That(note, Is.EqualTo(expectedNote));
        }
        [Test]
        public async Task NoteService_getNotesByUserIdAsync_ShouldReturnSpecificNote()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notExpectedNote = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Note>
            {
                expectedNote,
                notExpectedNote
            });

            var note = await myNoteService.GetNotesByUserIdAsync(expectedNote.UserId);

            Assert.That(note.Count, Is.EqualTo(1));
            Assert.That(note.First, Is.EqualTo(expectedNote));
        }
        [Test]
        public async Task NoteService_getNotesByTopicIdAsync_ShouldReturnSpecificNote()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notExpectedNote = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Note>
            {
                expectedNote,
                notExpectedNote
            });

            var note =await myNoteService.GetNotesByTopicIdAsync(expectedNote.TopicId);

            Assert.That(note.Count, Is.EqualTo(1));
            Assert.That(note.First, Is.EqualTo(expectedNote));
        }
        [Test]
        public async Task NoteService_getNoteByDescriptionAsync_ShouldReturnSpecificNote()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notExpectedNote = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Note>
            {
                expectedNote,
                notExpectedNote
            });

            var note = await myNoteService.GetNoteByDescriptionAsync(expectedNote.Description);

            Assert.That(note, Is.EqualTo(expectedNote));
        }
        [Test]
        public async Task NoteService_getNoteByTitleAsync_ShouldReturnSpecificNote()
        {
            var expectedNote = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notExpectedNote = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            myRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Note>
            {
                expectedNote,
                notExpectedNote
            });

            var note = await myNoteService.GetNoteByTitleAsync(expectedNote.Title);

            Assert.That(note, Is.EqualTo(expectedNote));
        }

        [Test]
        public void NoteService_AddNote_ShouldAddNoteToRepository()
        {
            var note = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notes= new List<Note>();
            myRepositoryMock.Setup(x => x.Add(It.IsAny<Note>())).Callback<Note>((note =>
            {
                notes.Add(note);
            }));
            myNoteService.AddNote(note);
            myRepositoryMock.Verify(x=>x.Add(note),Times.Once());
            Assert.That(notes.Count == 1);
            Assert.That(notes.ElementAt(0).Equals(note));

        }
        //[Test]
        //public async Task NoteService_AddNoteAsync_ShouldAddNoteToRepository()
        //{
        //    var note = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
        //    var notes = new List<Note>();
        //    myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Note>())).Callback<Note>((note =>
        //    {
        //        notes.Add(note);
        //    }));

        //    await myNoteService.AddNoteAsync(note);
        //    myRepositoryMock.Verify(x => x.AddAsync(note), Times.Once());
        //    Assert.That(notes.Count, Is.EqualTo(1));
        //    Assert.That(notes.ElementAt(0), Is.EqualTo(note));

        //}

    }
}
