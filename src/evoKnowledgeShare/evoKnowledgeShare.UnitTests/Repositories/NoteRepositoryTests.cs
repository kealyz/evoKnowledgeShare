using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.UnitTests.Repositories
{
    [TestFixture]
    public class NoteRepositoryTests : RepositoryTestBase<Note>
    {
        private Note[] myNotes = default!;
        [SetUp]
        public void SetUp()
        {
            myRepository = new NoteRepository(myDbContext);
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
        public void NoteRepository_GetAll_ShouldReturnAllNotes()
        {
            var note1 = myNotes[0];
            var note2 = myNotes[1];
            myDbContext.Notes.Add(note1);
            myDbContext.Notes.Add(note2);
            myDbContext.SaveChanges();

            var actual = myRepository.GetAll();

            
            Assert.Multiple(() =>
            {
                Assert.That(actual.Count, Is.EqualTo(2));
                Assert.That(actual, Does.Contain(note1));
                Assert.That(actual, Does.Contain(note2));
            });
        }

        [Test]
        public void NoteRepository_GetById_ShouldReturnSpecificNote()
        {
            var note1 = myNotes[0];
            myDbContext.Notes.Add(note1);
            myDbContext.SaveChanges();

            var actual = myRepository.GetById(note1.NoteId);

            Assert.That(actual, Is.EqualTo(note1));
        }
        [Test]
        public void NoteRepository_GetByIdRange_ShouldReturnSpecificNotes()
        {
            var note1 = myNotes[0];
            var note2 = myNotes[1];
            myDbContext.Notes.Add(note1);
            myDbContext.Notes.Add(note2);
            myDbContext.SaveChanges();

            List<Guid> guids = new() { note1.NoteId, note2.NoteId };
            var actual = myRepository.GetRangeById(guids);

            Assert.Multiple(() =>
            {
                Assert.That(actual, Does.Contain(note1));
                Assert.That(actual, Does.Contain(note2));
                Assert.That(actual.Count(), Is.EqualTo(2));
            });
        }

        #endregion Get Section
        #region Add Section
        [Test]
        public async Task NoteRepository_AddAsync_ShouldAddNoteToRepository()
        {
            Note expectedNote = myNotes[0];

            var actualNote = await myRepository.AddAsync(expectedNote);
            myDbContext.SaveChanges();

            Assert.Multiple(() =>
            {
                Assert.That(myDbContext.Notes.Count(), Is.EqualTo(1));
                Assert.That(actualNote, Is.EqualTo(expectedNote));
                Assert.That(myDbContext.Notes, Does.Contain(expectedNote));
            });
        }
        [Test]
        public async Task NoteRepository_AddRangeAsync_ShouldAddNotesToRepository()
        {
            Note expectedNote1 = myNotes[0];
            Note expectedNote2 = myNotes[1];
            IEnumerable<Note> expectedNotes = new[] { expectedNote1, expectedNote2 };

            var actualNotes = await myRepository.AddRangeAsync(expectedNotes);
            myDbContext.SaveChanges();

            Assert.Multiple(() =>
            {
                Assert.That(actualNotes, Does.Contain(expectedNote1));
                Assert.That(actualNotes, Does.Contain(expectedNote2));
            });
        }
        #endregion Add Section
        #region Remove Section
        [Test]
        public void NoteRepository_Remove_ShouldRemoveOneNote()
        {
            Note note = myNotes[0];
            myDbContext.Notes.Add(note);
            myDbContext.SaveChanges();

            int noteCount = myDbContext.Notes.Count();

            myRepository.Remove(note);
            myDbContext.SaveChanges();
            int expectedNoteCount = myDbContext.Notes.Count();

            Assert.Multiple(() =>
            {
                Assert.That(myDbContext.Notes.Count(), Is.EqualTo(0));
                Assert.That(myDbContext.Notes.Contains(note), Is.False);
                Assert.That(noteCount - 1, Is.EqualTo(expectedNoteCount));
            });
        }

        [Test]
        public void NoteRepository_RemoveById_ShouldRemoveOneNote()
        {
            Note note = myNotes[0];
            myDbContext.Notes.Add(note);
            myDbContext.SaveChanges();

            int noteCount = myDbContext.Notes.Count();

            myRepository.RemoveById(note.NoteId);
            myDbContext.SaveChanges();
            int expectedNoteCount = myDbContext.Notes.Count();
            
            Assert.Multiple(() =>
            {
                Assert.That(myDbContext.Notes.Count(), Is.EqualTo(0));
                Assert.That(myDbContext.Notes.Contains(note), Is.False);
                Assert.That(noteCount - 1, Is.EqualTo(expectedNoteCount));
            });
        }

        [Test]
        public void NoteRepository_RemoveRange_ShouldRemoveOneNote()
        {
            Note note = myNotes[0];
            Note note2 = myNotes[1];
            IEnumerable<Note> notes = new[] { note, note2 };
            myDbContext.Notes.Add(note);
            myDbContext.Notes.Add(note2);
            myDbContext.SaveChanges();

            int noteCount = myDbContext.Notes.Count();

            myRepository.RemoveRange(notes);
            myDbContext.SaveChanges();
            int expectedNoteCount = myDbContext.Notes.Count();

            Assert.Multiple(() =>
            {
                Assert.That(myDbContext.Notes.Count(), Is.EqualTo(0));
                Assert.That(myDbContext.Notes.Contains(note), Is.False);
                Assert.That(myDbContext.Notes.Contains(note2), Is.False);
                Assert.That(noteCount - notes.Count(), Is.EqualTo(expectedNoteCount));
            });
        }

        [Test]
        public void NoteRepository_RemoveRangeById_ShouldRemoveOneNote()
        {
            Note note = myNotes[0];
            Note note2 = myNotes[1];
            IEnumerable<Guid> notes = new[] { note.NoteId, note2.NoteId };
            myDbContext.Notes.Add(note);
            myDbContext.Notes.Add(note2);
            myDbContext.SaveChanges();

            int noteCount = myDbContext.Notes.Count();

            myRepository.RemoveRangeById(notes);
            myDbContext.SaveChanges();
            int expectedNoteCount = myDbContext.Notes.Count();

            Assert.Multiple(() =>
            {
                Assert.That(myDbContext.Notes.Count(), Is.EqualTo(0));
                Assert.That(myDbContext.Notes.Contains(note), Is.False);
                Assert.That(myDbContext.Notes.Contains(note2), Is.False);
                Assert.That(noteCount - notes.Count(), Is.EqualTo(expectedNoteCount));
            });
        }
        #endregion Remove Section
        #region Modify Section
        [Test]
        public void NoteRepository_Update_ShouldUpdateNoteInRepository()
        {
            var note = myNotes[0];
            myDbContext.Notes.Add(note);
            myDbContext.SaveChanges();

            string newNoteDescription = "C sharp fejlesztes";

            note.Title = newNoteDescription;

            myRepository.Update(note);
            myDbContext.SaveChanges();

            var updatedNote = myDbContext.Notes.First(x => x.NoteId == note.NoteId);

            Assert.That(updatedNote.Title, Is.EqualTo(newNoteDescription));

        }
        [Test]
        public void NoteRepository_UpdateRange_ShouldUpdateNoteInRepository()
        {
            var note = myNotes[0];
            var note2 = myNotes[1];
            myDbContext.Notes.Add(note);
            myDbContext.Notes.Add(note2);
            myDbContext.SaveChanges();

            string newNoteDescriptiontoNote1 = "C sharp fejlesztes";
            string newNoteDescriptiontoNote2 = "Fejlesztes Java-ban";

            note.Title = newNoteDescriptiontoNote1;
            note2.Title = newNoteDescriptiontoNote2;

            List<Note> notes = new() { note, note2 };
            myRepository.UpdateRange(notes);
            myDbContext.SaveChanges();

            var updatedNote = myDbContext.Notes.First(x => x.NoteId == note.NoteId);
            var updatedNote2 = myDbContext.Notes.First(x => x.NoteId == note2.NoteId);

            Assert.Multiple(() =>
            {
                Assert.That(updatedNote.Title, Is.EqualTo(newNoteDescriptiontoNote1));
                Assert.That(updatedNote2.Title, Is.EqualTo(newNoteDescriptiontoNote2));
            });
        }
        #endregion Modify Section
    }
}