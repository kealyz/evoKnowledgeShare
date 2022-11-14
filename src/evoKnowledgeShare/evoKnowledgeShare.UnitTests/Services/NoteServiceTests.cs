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

            var note = myNoteService.GetByUserId(expectedNote.UserId);

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

            var note = myNoteService.GetByTopicId(expectedNote.TopicId);

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

            var note = myNoteService.GetByDescription(expectedNote.Description);

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

            var note = myNoteService.GetByTitle(expectedNote.Title);

            Assert.That(note, Is.EqualTo(expectedNote));
        }

        [Test]
        public void NoteService_AddNotes_ShouldAddNotesToRepository()
        {
            var note1 = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var note2 = new Note(Guid.NewGuid(), "22222222", 2, DateTimeOffset.Now, "Leiras2", "Title2");
            var noteList = new List<Note>();
            noteList.Add(note1);
            noteList.Add(note2);
            var actualNoteList = new List<Note>();

            myRepositoryMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<Note>>())).Callback<IEnumerable<Note>>((notes =>
            {
                foreach (var item in notes)
                {
                    actualNoteList.Add(item);
                }
            }));

            myNoteService.Add(noteList);
            myRepositoryMock.Verify(x => x.AddRange(noteList), Times.Once());
            Assert.That(actualNoteList.Count == 2);
            Assert.That(actualNoteList.ElementAt(0).Equals(note1));
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

        //[Test]
        //public void NoteService_AddNotesAsync_ShouldAddNotesToRepository()
        //{

        //}

        [Test]
        public void NoteService_RemoveNote_ShouldRemoveNoteFromRepository()
        {
            var note = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var notes = new List<Note>();
            notes.Add(note);
            myRepositoryMock.Setup(x => x.Remove(It.IsAny<Note>())).Callback<Note>((note =>
            {
                notes.Remove(note);
            }));

            myNoteService.Remove(note);
            myRepositoryMock.Verify(x => x.Remove(note), Times.Once());
            Assert.That(notes.Count == 0);
        }
        //[Test]
        //public void NoteService_RemoveNoteById_ShouldRemoveNoteFromRepository()
        //{
        //    var note = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
        //    var notes = new List<Note>();
        //    notes.Add(note);
        //    myRepositoryMock.Setup(x => x.Remove(It.IsAny<Note>())).Callback<Guid>((note =>
        //    {
        //        notes.Remove();
        //    }));

        //    myNoteService.RemoveNoteById(note.NoteId);
        //    myRepositoryMock.Verify(x => x.Remove(note), Times.Once());
        //    Assert.That(notes.Count == 0);
        //}
        //[Test]
        //public void NoteService_RemoveNotesByAuthor_ShouldRemoveNotesFromRepository()
        //{

        //}
        //[Test]
        //public async Task NoteService_RemoveNoteAsync_ShouldRemoveNoteFromRepository()
        //{
        //    var note = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
        //    var notes = new List<Note>();
        //    notes.Add(note);

        //    myRepositoryMock.Setup(x => x.RemoveAsync(It.IsAny<Note>())).Callback<Note>((note =>
        //    {
        //        notes.Remove(note);
        //    }));

        //    await myNoteService.RemoveNoteAsync(note);
        //    myRepositoryMock.Verify(x => x.RemoveAsync(note), Times.Once());
        //    Assert.That(notes.Count == 0);
        //}
        //[Test]
        //public void NoteService_RemoveNoteByIdAsync_ShouldRemoveNoteFromRepository()
        //{

        //}
        //[Test]
        //public void NoteService_RemoveNotesByAuthorAsync_ShouldRemoveNotesFromRepository()
        //{


        [Test]
        public void NoteService_ModifyNote_ShouldChangeTheValuesOfOldNoteToNew()
        {
            var note = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
            var note2 = new Note(note.NoteId, "a", 2, note.CreatedAt, "leiras2", "title2");
            var notes = new List<Note>();
            notes.Add(note);
            myRepositoryMock.Setup(x => x.Update(It.IsAny<Note>())).Callback<Note>((note =>
            {
                notes=new List<Note> { note };
            }));

            myNoteService.Update(note);
            myRepositoryMock.Verify(x => x.Update(note), Times.Once());
            Assert.That(notes.Count == 1);
            Assert.That(notes.Contains(note), Is.True);
        }
        //[Test]
        //public async Task NoteService_ModifyNoteAsync_ShouldChangeTheValuesOfOldNoteToNew()
        //{
        //    var note = new Note(Guid.NewGuid(), "11111111", 1, DateTimeOffset.Now, "Leiras1", "Title1");
        //    var note2 = new Note(note.NoteId, "a", 2, note.CreatedAt, "leiras2", "title2");
        //    var notes = new List<Note>();
        //    notes.Add(note);
        //    myRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Note>())).Callback<Note>((note =>
        //    {
        //        notes = new List<Note> { note };
        //    }));

        //    await myNoteService.ModifyNoteAsync(note);
        //    myRepositoryMock.Verify(x => x.UpdateAsync(note), Times.Once());
        //    Assert.That(notes.Count == 1);
        //    Assert.That(notes.Contains(note), Is.True);
        //}

    }
}
