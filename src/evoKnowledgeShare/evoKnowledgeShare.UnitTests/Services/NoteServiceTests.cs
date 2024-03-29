﻿using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Microsoft.EntityFrameworkCore;
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
                new Note(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, "C# fejlesztes", "Kezdo C#"),
                new Note(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, "Java fejlesztes", "Halado java")
           };
        }
        #region Get Section
        [Test]
        public void NoteService_GetAll_ShouldReturnAll()
        {
            myRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Note>
            {
                myNotes[0]
            });

            var actualNotes = myNoteService.GetAll();

            Assert.That(actualNotes.Count, Is.EqualTo(1));
            Assert.That(actualNotes.First, Is.EqualTo(myNotes[0]));
        }
        [Test]
        public void NoteService_GetAll_ShouldReturnEmptyList()
        {
            myRepositoryMock.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Note>);
            var actualNotes = myNoteService.GetAll();

            Assert.That(actualNotes.Count, Is.EqualTo(0));
            Assert.That(actualNotes,Is.Empty);
        }
        [Test]
        public void NoteService_GetNoteById_ShouldReturnASpecificNote()
        {
            myRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(myNotes[0]);
            var note = myNoteService.GetById(myNotes[0].NoteId);

            myRepositoryMock.Verify(x => x.GetById(It.Is<Guid>(y => y.Equals(myNotes[0].NoteId))), Times.Once);
            Assert.That(note, Is.EqualTo(myNotes[0]));
        }
        [Test]
        public void NoteService_GetNoteById_ShouldReturnKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Throws(new KeyNotFoundException());
                var note = myNoteService.GetById(myNotes[0].NoteId);
            });
        }
        [Test]
        public void NoteService_getRangeByUserId_ShouldReturnSpecificNote()
        {

            myRepositoryMock.Setup(x => x.GetAll()).Returns(myNotes);

            var note = myNoteService.GetRangeByUserId(myNotes[0].UserId);

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(note.Count, Is.EqualTo(1));
            Assert.That(note.First, Is.EqualTo(myNotes[0]));
        }
        [Test]
        public void NoteService_getRangeByUserId_ShouldReturnEmptyList()
        {
            myRepositoryMock.Setup(x => x.GetAll()).Returns(myNotes);
            var actualNotes = myNoteService.GetRangeByUserId(Guid.NewGuid());
            Assert.That(actualNotes.Count, Is.EqualTo(0));
            Assert.That(actualNotes, Is.Empty);
        }
        [Test]
        public void NoteService_getRangeByTopicId_ShouldReturnSpecificNote()
        {
            var listOfNotes = new List<Note>
            {
                myNotes[0],
                myNotes[1]
            };
            myRepositoryMock.Setup(x => x.GetAll()).Returns(listOfNotes);

            var note = myNoteService.GetRangeBytTopicId(myNotes[0].TopicId);

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(note.Count, Is.EqualTo(1));
            Assert.That(note.First, Is.EqualTo(myNotes[0]));
        }
        [Test]
        public void NoteService_getRangeByTopicId_ShouldReturnEmptyList()
        {
            myRepositoryMock.Setup(x => x.GetAll()).Returns(myNotes);
            var actualNotes = myNoteService.GetRangeBytTopicId(Guid.NewGuid());

            Assert.That(actualNotes.Count, Is.EqualTo(0));
            Assert.That(actualNotes, Is.Empty);
        }
        [Test]
        public void NoteService_getByDescription_ShouldReturnSpecificNote()
        {
            var listOfNotes = new List<Note>
            {
                myNotes[0],
                myNotes[1]
            };
            myRepositoryMock.Setup(x => x.GetAll()).Returns(listOfNotes);

            var note = myNoteService.GetByDescription(myNotes[0].Description);

            myRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            Assert.That(note, Is.EqualTo(myNotes[0]));
        }
        [Test]
        public void NoteService_GetByDescription_ShouldReturnKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myRepositoryMock.Setup(x => x.GetAll()).Returns(myNotes);
                var note = myNoteService.GetByDescription("Not existing test description");
            });
        }
        [Test]
        public void NoteService_getByTitle_ShouldReturnSpecificNote()
        {
            var listOfNotes = new List<Note>
            {
                myNotes[0],
                myNotes[1]
            };
            myRepositoryMock.Setup(x => x.GetAll()).Returns(listOfNotes);

            var note = myNoteService.GetByTitle(myNotes[0].Title);

            Assert.That(note, Is.EqualTo(myNotes[0]));
        }
        [Test]
        public void NoteService_getByTitle_ShouldReturnKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myRepositoryMock.Setup(x => x.GetAll()).Returns(myNotes);
                var note = myNoteService.GetByTitle("Not existing test title");
            });
        }
        [Test]
        public void NoteService_GetLatestVersion_ShouldReturnKeyNotFoundException() {
            Assert.Throws<KeyNotFoundException>(() => {
                var latestVersion = myNoteService.GetLatestVersion(Guid.Empty);
            });
        }
        #endregion Get Section
        #region Add Section
        [Test]
        public async Task NoteService_AddAsync_CreatesNote()
        {

            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Note>())).ReturnsAsync(myNotes[0]);

            Note actualNote = await myNoteService.AddAsync(myNotes[0], "");

            myRepositoryMock.Verify(x => x.AddAsync(It.Is<Note>(y => y.Equals(myNotes[0]))), Times.Once);
            Assert.That(actualNote, Is.EqualTo(myNotes[0]));
        }
        [Test]
        public async Task NoteService_AddAsync_ThrowsArgumentException()
        {
            myRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Note>())).Throws(new ArgumentException());
            Assert.ThrowsAsync<ArgumentException>(async() =>
            {
                await myNoteService.AddAsync(myNotes[0], "");
            });
        }
        [Test]
        public async Task NoteService_AddRangeAsync_CreatesNotes()
        {
            var notesList = new List<Note>()
            {
                myNotes[0],
                myNotes[1]
            };
            myRepositoryMock.Setup(x => x.AddRangeAsync(It.IsAny<List<Note>>())).ReturnsAsync(notesList);

            var actualNotes = await myNoteService.AddRangeAsync(notesList);

            myRepositoryMock.Verify(x => x.AddRangeAsync(It.Is<List<Note>>(y => y.Equals(notesList))), Times.Once);
            Assert.That(actualNotes, Is.EqualTo(notesList));
        }
        [Test]
        public async Task NoteService_AddRangeAsync_ThrowsArgumentException()
        {
            var notesList = new List<Note>()
            {
                myNotes[0],
                myNotes[1]
            };
            myRepositoryMock.Setup(x => x.AddRangeAsync(It.IsAny<List<Note>>())).Throws(new ArgumentException());
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await myNoteService.AddRangeAsync(notesList);
            });
        }
        #endregion Add Section
        #region Remove Section
        [Test]
        public void NoteService_Remove_ShouldRemoveNoteFromRepository()
        {
            var note = myNotes[0];
            var notes = new List<Note>
            {
                myNotes[0]
            };
            myRepositoryMock.Setup(x => x.Remove(It.IsAny<Note>())).Callback<Note>((note =>
            {
                notes.Remove(note);
            }));

            myNoteService.Remove(note);
            myRepositoryMock.Verify(x => x.Remove(note), Times.Once());
            Assert.That(notes, Is.Empty);
        }
        [Test]
        public void NoteService_Remove_ThrowsKeyNotFoundException()
        {
            myRepositoryMock.Setup(x=>x.Remove(It.IsAny<Note>())).Throws<KeyNotFoundException>();
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myNoteService.Remove(new Note());
            });
        }
        [Test]
        public void NoteService_RemoveById_ShouldRemoveNoteFromRepository()
        {
            var notes = new List<Note>
            {
                myNotes[0]
            };
            var noteId = myNotes[0].NoteId;

            myRepositoryMock.Setup(x => x.RemoveById(It.IsAny<Guid>()));

            myNoteService.RemoveById(noteId);

            myRepositoryMock.Verify(x => x.RemoveById(noteId), Times.Once);

        }
        [Test]
        public void NoteService_RemoveById_ThrowsKeyNotFoundException()
        {
            myRepositoryMock.Setup(x => x.RemoveById(It.IsAny<Guid>())).Throws<KeyNotFoundException>();
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myNoteService.RemoveById(Guid.NewGuid());
            });
        }
        #endregion Remove Section
        #region Modify Section
        [Test]
        public void NoteService_Update_ShouldChangeTheValuesOfOldNoteToNew()
        {
            myRepositoryMock.Setup(x => x.Update(It.IsAny<Note>())).Returns(myNotes[0]);

            Note actualNote = myNoteService.Update(myNotes[0], "", 1);

            myRepositoryMock.Verify(x => x.Update(It.Is<Note>(y => y.Equals(myNotes[0]))), Times.Once);
            Assert.That(actualNote, Is.EqualTo(myNotes[0]));

        }
        [Test]
        public void NoteService_Update_ShouldThrowKeyNotFoundException()
        {
            myRepositoryMock.Setup(x=>x.Update(It.IsAny<Note>())).Throws<KeyNotFoundException>();

            Assert.Throws<KeyNotFoundException>(() =>
            {
                myNoteService.Update(new Note(), "", 1);
            });
        }
        #endregion Modify Section
    }
}
