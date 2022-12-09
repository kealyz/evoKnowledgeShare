using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;

namespace evoKnowledgeShare.UnitTests.Services
{
    public class NoteServiceTests
    {

        private NoteService myNoteService;
        private Mock<IRepository<Note>> myRepositoryMock;
        private Note[] myNotes = default!;

        [SetUp]
        public void Setup()
        {
            myRepositoryMock = new Mock<IRepository<Note>>(MockBehavior.Strict);
            myNoteService = new NoteService(myRepositoryMock.Object);
            myNotes = new Note[]
           {
                new Note(Guid.NewGuid(), Guid.NewGuid(), 1, DateTimeOffset.Now, "C# fejlesztes", "Kezdo C#"),
                new Note(Guid.NewGuid(), Guid.NewGuid(), 2, DateTimeOffset.Now, "Java fejlesztes", "Halado java")
           };
        }
        #region Get Section
        [Test]
        public void NoteService_GetAll_ShouldReturnAll()
        {
            var expectedNote = myNotes[0];
            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Note>
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
            var expectedNote = myNotes[0];
            var notExpectedNote = myNotes[1];
            myRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(expectedNote);
            var note = myNoteService.GetById(expectedNote.NoteId);

            myRepositoryMock.Verify(x => x.GetById(It.Is<Guid>(y => y.Equals(expectedNote.NoteId))), Times.Once);
            Assert.That(note, Is.EqualTo(expectedNote));
        }
        [Test]
        public void NoteService_getByUserId_ShouldReturnSpecificNote()
        {
            var expectedNote = myNotes[0];
            var notExpectedNote = myNotes[1];
            var listOfNotes = new List<Note>
            {
                expectedNote,
                notExpectedNote
            };
            myRepositoryMock.Setup(x => x.GetAll()).Returns(listOfNotes);

            var note = myNoteService.GetRangeByUserId(expectedNote.UserId);

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(note.Count, Is.EqualTo(1));
            Assert.That(note.First, Is.EqualTo(expectedNote));
        }
        [Test]
        public void NoteService_getByTopicId_ShouldReturnSpecificNote()
        {
            var expectedNote = myNotes[0];
            var notExpectedNote = myNotes[1];
            var listOfNotes = new List<Note>
            {
                expectedNote,
                notExpectedNote
            };
            myRepositoryMock.Setup(x => x.GetAll()).Returns(listOfNotes);

            var note = myNoteService.GetByTopicId(expectedNote.TopicId);

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(note.Count, Is.EqualTo(1));
            Assert.That(note.First, Is.EqualTo(expectedNote));
        }
        [Test]
        public void NoteService_getByDescription_ShouldReturnSpecificNote()
        {
            var expectedNote = myNotes[0];
            var notExpectedNote = myNotes[1];
            var listOfNotes = new List<Note>
            {
                expectedNote,
                notExpectedNote
            };
            myRepositoryMock.Setup(x => x.GetAll()).Returns(listOfNotes);

            var note = myNoteService.GetByDescription(expectedNote.Description);

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(note, Is.EqualTo(expectedNote));
        }
        [Test]
        public void NoteService_getByTitle_ShouldReturnSpecificNote()
        {
            var expectedNote = myNotes[0];
            var notExpectedNote = myNotes[1];
            var listOfNotes = new List<Note>
            {
                expectedNote,
                notExpectedNote
            };
            myRepositoryMock.Setup(x => x.GetAll()).Returns(listOfNotes);

            var note = myNoteService.GetByTitle(expectedNote.Title);

            Assert.That(note, Is.EqualTo(expectedNote));
        }
        #endregion Get Section
        #region Add Section
        [Test]
        public async Task NoteService_AddAsync_CreatesNote()
        {
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Note>())).ReturnsAsync(myNotes[0]);

            Note actualNote = await myNoteService.AddAsync(myNotes[0]);

            myRepositoryMock.Verify(x => x.AddAsync(It.Is<Note>(y => y.Equals(myNotes[0]))), Times.Once);
            Assert.That(actualNote, Is.EqualTo(myNotes[0]));
        }
        [Test]
        public async Task NoteService_AddRangeAsync_CreatesNotes()
        {
            var note = myNotes[0];
            var note2 = myNotes[1];
            var notesList = new List<Note>()
            {
                note,
                note2
            };
            myRepositoryMock.Setup(x => x.AddRangeAsync(It.IsAny<List<Note>>())).ReturnsAsync(notesList);

            var actualNotes = await myNoteService.AddRangeAsync(notesList);

            myRepositoryMock.Verify(x => x.AddRangeAsync(It.Is<List<Note>>(y => y.Equals(notesList))), Times.Once);
            Assert.That(actualNotes, Is.EqualTo(notesList));
        }
        #endregion Add Section
        #region Remove Section
        [Test]
        public void NoteService_Remove_ShouldRemoveNoteFromRepository()
        {
            var note = myNotes[0];
            var notes = new List<Note>();
            notes.Add(note);
            myRepositoryMock.Setup(x => x.Remove(It.IsAny<Note>())).Callback<Note>((note =>
            {
                notes.Remove(note);
            }));

            myNoteService.Remove(note);
            myRepositoryMock.Verify(x => x.Remove(note), Times.Once());
            Assert.That(notes, Is.Empty);
        }
        [Test]
        public void NoteService_RemoveById_ShouldRemoveNoteFromRepository()
        {
            var note = myNotes[0];
            var notes = new List<Note>
            {
                note
            };
            var noteId = note.NoteId;

            myRepositoryMock.Setup(x => x.RemoveById(It.IsAny<Guid>()));

            myNoteService.RemoveById(noteId);

            myRepositoryMock.Verify(x => x.RemoveById(noteId), Times.Once);

        }
        #endregion Remove Section
        #region Modify Section
        [Test]
        public void NoteService_Update_ShouldChangeTheValuesOfOldNoteToNew()
        {
            myRepositoryMock.Setup(x => x.Update(It.IsAny<Note>())).Returns(myNotes[0]);

            Note actualNote = myNoteService.Update(myNotes[0]);

            myRepositoryMock.Verify(x => x.Update(It.Is<Note>(y => y.Equals(myNotes[0]))), Times.Once);
            Assert.That(actualNote, Is.EqualTo(myNotes[0]));

        }
        #endregion Modify Section
    }
}
