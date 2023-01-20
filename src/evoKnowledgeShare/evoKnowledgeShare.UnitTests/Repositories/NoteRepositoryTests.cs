using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

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
                new Note(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, "C# fejlesztes", "Kezdo C#"),
                new Note(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, "Java fejlesztes", "Halado java"),
                new Note(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, "Assembly fejlesztes", "KYS"),
                new Note(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, "Python fejlesztes", "Kigyo vagy"),
                new Note(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, "C es C++ fejlesztes", "Kezd az alapoktol")
            };
            myDbContext.Notes.Add(myNotes[0]);
            myDbContext.Notes.Add(myNotes[1]);
            myDbContext.SaveChanges();
        }

        #region Get Section
        [Test]
        public void NoteRepository_GetAll_ShouldReturnAllNotes()
        {
            var actual = myRepository.GetAll();

            
            Assert.Multiple(() =>
            {
                Assert.That(actual.Count, Is.EqualTo(2));
                Assert.That(actual, Does.Contain(myNotes[0]));
                Assert.That(actual, Does.Contain(myNotes[1]));
            });
        }
        [Test]
        public void NoteRepository_GetAll_ShouldReturnAnEmptyList()
        {
            myDbContext.Notes.RemoveRange(new List<Note> { myNotes[0], myNotes[1] });
            myDbContext.SaveChanges();
            var actual = myRepository.GetAll();

            Assert.That(actual.Count, Is.EqualTo(0));
        }
        [Test]
        public void NoteRepository_GetById_ShouldReturnSpecificNote()
        {
            var actual = myRepository.GetById(myNotes[0].NoteId);

            Assert.That(actual, Is.EqualTo(myNotes[0]));
        }
        [Test]
        public void NoteRepository_GetById_ShouldReturnKeyNotFoundException()
        {
            myDbContext.Notes.RemoveRange(new List<Note> { myNotes[0], myNotes[1] });
            myDbContext.SaveChanges();
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myRepository.GetById(myNotes[0].NoteId);
            });
        }
        [Test]
        public void NoteRepository_GetRangeById_ShouldReturnSpecificNotes()
        {
            List<Guid> guids = new() { myNotes[0].NoteId, myNotes[1].NoteId };
            var actual = myRepository.GetRangeById(guids);

            Assert.Multiple(() =>
            {
                Assert.That(actual, Does.Contain(myNotes[0]));
                Assert.That(actual, Does.Contain(myNotes[1]));
                Assert.That(actual.Count(), Is.EqualTo(2));
            });
        }
        [Test]
        public void NoteRepository_GetRangeById_ShouldReturnAnEmptyList()
        {
            myDbContext.Notes.RemoveRange(new List<Note> { myNotes[0], myNotes[1] });
            myDbContext.SaveChanges();
            List<Guid> guids = new() { myNotes[0].NoteId, myNotes[1].NoteId };

            var actual = myRepository.GetRangeById(guids);

            Assert.That(actual.Count(), Is.EqualTo(0));
        }

        #endregion Get Section
        #region Add Section
        [Test]
        public async Task NoteRepository_AddAsync_ShouldAddNoteToRepository()
        {
            var actualNote = await myRepository.AddAsync(myNotes[3]);
            myDbContext.SaveChanges();

            Assert.Multiple(() =>
            {
                Assert.That(myDbContext.Notes.Count(), Is.EqualTo(3));
                Assert.That(actualNote, Is.EqualTo(myNotes[3]));
                Assert.That(myDbContext.Notes, Does.Contain(myNotes[3]));
            });
        }
        [Test]
        public void NoteRepository_AddAsync_ShouldReturnWithArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await myRepository.AddAsync(myNotes[0]);
            });
        }
        [Test]
        public async Task NoteRepository_AddRangeAsync_ShouldAddNotesToRepository()
        {
            IEnumerable<Note> expectedNotes = new[] { myNotes[2], myNotes[3] };

            var actualNotes = await myRepository.AddRangeAsync(expectedNotes);
            myDbContext.SaveChanges();

            Assert.Multiple(() =>
            {
                Assert.That(actualNotes, Does.Contain(myNotes[2]));
                Assert.That(actualNotes, Does.Contain(myNotes[3]));
            });
        }
        [Test]
        public void NoteRepository_AddRangeAsync_ShouldReturnWithArgumentException()
        {
            IEnumerable<Note> expectedNotes = new[] { myNotes[1], myNotes[2] };
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await myRepository.AddRangeAsync(expectedNotes);
            });
        }
        #endregion Add Section
        #region Remove Section
        [Test]
        public void NoteRepository_Remove_ShouldRemoveOneNote()
        {
            int noteCount = myDbContext.Notes.Count();

            myRepository.Remove(myNotes[0]);
            myDbContext.SaveChanges();
            int expectedNoteCount = myDbContext.Notes.Count();

            Assert.Multiple(() =>
            {
                Assert.That(myDbContext.Notes.Count(), Is.EqualTo(1));
                Assert.That(myDbContext.Notes.Contains(myNotes[0]), Is.False);
                Assert.That(noteCount - 1, Is.EqualTo(expectedNoteCount));
            });
        }
        [Test]
        public void NoteRepository_Remove_ShouldReturnWithKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myRepository.Remove(myNotes[4]);
            });
        }
        [Test]
        public void NoteRepository_RemoveById_ShouldRemoveOneNote()
        {
            int noteCount = myDbContext.Notes.Count();

            myRepository.RemoveById(myNotes[0].NoteId);
            myDbContext.SaveChanges();
            int expectedNoteCount = myDbContext.Notes.Count();
            
            Assert.Multiple(() =>
            {
                Assert.That(myDbContext.Notes.Count(), Is.EqualTo(1));
                Assert.That(myDbContext.Notes.Contains(myNotes[0]), Is.False);
                Assert.That(noteCount - 1, Is.EqualTo(expectedNoteCount));
            });
        }
        [Test]
        public void NoteRepository_RemoveById_ShouldReturnWithKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myRepository.RemoveById(myNotes[4].NoteId);
            });
        }

        [Test]
        public void NoteRepository_RemoveRange_ShouldRemoveNotes()
        {
            IEnumerable<Note> notes = new[] { myNotes[0], myNotes[1] };

            int noteCount = myDbContext.Notes.Count();

            myRepository.RemoveRange(notes);
            myDbContext.SaveChanges();
            int expectedNoteCount = myDbContext.Notes.Count();

            Assert.Multiple(() =>
            {
                Assert.That(myDbContext.Notes.Count(), Is.EqualTo(0));
                Assert.That(myDbContext.Notes.Contains(myNotes[0]), Is.False);
                Assert.That(myDbContext.Notes.Contains(myNotes[1]), Is.False);
                Assert.That(noteCount - notes.Count(), Is.EqualTo(expectedNoteCount));
            });
        }
        [Test]
        public void NoteRepository_RemoveRange_ShouldReturnWithKeyNotFoundException()
        {
            IEnumerable<Note> notes = new[] { myNotes[2], myNotes[3] };
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myRepository.RemoveRange(notes);
            });
        }

        [Test]
        public void NoteRepository_RemoveRangeById_ShouldRemoveNotes()
        {
            IEnumerable<Guid> notes = new[] { myNotes[0].NoteId, myNotes[1].NoteId };

            int noteCount = myDbContext.Notes.Count();

            myRepository.RemoveRangeById(notes);
            myDbContext.SaveChanges();
            int expectedNoteCount = myDbContext.Notes.Count();

            Assert.Multiple(() =>
            {
                Assert.That(myDbContext.Notes.Count(), Is.EqualTo(0));
                Assert.That(myDbContext.Notes.Contains(myNotes[0]), Is.False);
                Assert.That(myDbContext.Notes.Contains(myNotes[1]), Is.False);
                Assert.That(noteCount - notes.Count(), Is.EqualTo(expectedNoteCount));
            });
        }
        [Test]
        public void NoteRepository_RemoveRangeById_ShouldRemoveWithKeyNotFoundException()
        {
            List<Guid> noteGuids = new() { myNotes[2].NoteId, myNotes[3].NoteId };
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myRepository.RemoveRangeById(noteGuids);
            });
        }
        #endregion Remove Section
        #region Modify Section
        [Test]
        public void NoteRepository_Update_ShouldUpdateNoteInRepository()
        {
            string newNoteDescription = "C sharp fejlesztes";

            myNotes[0].Title = newNoteDescription;

            myRepository.Update(myNotes[0]);
            myDbContext.SaveChanges();

            var updatedNote = myDbContext.Notes.First(x => x.NoteId == myNotes[0].NoteId);

            Assert.That(updatedNote.Title, Is.EqualTo(newNoteDescription));

        }
        [Test]
        public void NoteRepository_Update_ShouldReturnWithKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myRepository.Update(myNotes[2]);
            });
        }
        [Test]
        public void NoteRepository_UpdateRange_ShouldUpdateNoteInRepository()
        {
            string newNoteDescriptiontoNote1 = "C sharp fejlesztes";
            string newNoteDescriptiontoNote2 = "Fejlesztes Java-ban";

            myNotes[0].Title = newNoteDescriptiontoNote1;
            myNotes[1].Title = newNoteDescriptiontoNote2;

            List<Note> notes = new() { myNotes[0], myNotes[1] };
            myRepository.UpdateRange(notes);
            myDbContext.SaveChanges();

            var updatedNote = myDbContext.Notes.First(x => x.NoteId == myNotes[0].NoteId);
            var updatedNote2 = myDbContext.Notes.First(x => x.NoteId == myNotes[1].NoteId);

            Assert.Multiple(() =>
            {
                Assert.That(updatedNote.Title, Is.EqualTo(newNoteDescriptiontoNote1));
                Assert.That(updatedNote2.Title, Is.EqualTo(newNoteDescriptiontoNote2));
            });
        }
        [Test]
        public void NoteRepository_UpdateRange_ShouldReturnWithKeyNotFoundException()
        {
            List<Note> notes = new() { myNotes[1], myNotes[2] };
            Assert.Throws<KeyNotFoundException>(() =>
            {
                myRepository.UpdateRange(notes);
            });
        }
        #endregion Modify Section
    }
}