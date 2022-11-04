using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.UnitTests.Repositories
{
    [TestFixture]
    public class NoteRepositoryTests
    {
        NoteRepository myRepository;
        EvoKnowledgeDbContext myDbContext;

        private void SetUp(int id)
        {
            DbContextOptions dbContextOptions = new DbContextOptionsBuilder<EvoKnowledgeDbContext>()
                .UseInMemoryDatabase($"InMemoryDB{id}").Options;

            myDbContext = new EvoKnowledgeDbContext(dbContextOptions);
            myRepository = new NoteRepository(myDbContext);
        }


        [Test]
        public void NoteRepository_Add_ShouldAddNoteToRepository()
        {
            SetUp(1);
            var note = new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim");

            myRepository.Add(note);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Notes.Count(), Is.EqualTo(1));
            Assert.That(myDbContext.Notes.Last().Equals(note));
        }
        [Test]
        public async Task NoteRepository_AddAsync_ShouldAddNoteToRepository()
        {
            SetUp(2);
            var note = new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim");

            await myRepository.AddAsync(note);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Notes.Count(), Is.EqualTo(1));
            Assert.That(myDbContext.Notes.Last().Equals(note));
        }
        [Test]
        public void NoteRepository_AddRange_ShouldAddNotesToRepository()
        {
            SetUp(3);
            var notes = new List<Note>
            {
            new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim"),
            new Note(new Guid(), "b", 2, new DateTimeOffset(), "leiras2", "cim2")
            };

            myRepository.AddRange(notes);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Notes.Count(), Is.EqualTo(2));
            Assert.That(myDbContext.Notes.Contains(notes.First()));
            Assert.That(myDbContext.Notes.Contains(notes.Last()));
        }

        [Test]
        public async Task NoteRepository_AddRangeAsync_ShouldAddNotesToRepository()
        {
            SetUp(11);
            var notes = new List<Note>
            {
            new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim"),
            new Note(new Guid(), "b", 2, new DateTimeOffset(), "leiras2", "cim2")
            };

            myRepository.AddRangeAsync(notes);
            myRepository.SaveChanges();

            Assert.That(myDbContext.Notes.Count(), Is.EqualTo(2));
            Assert.That(myDbContext.Notes.Contains(notes.First()));
            Assert.That(myDbContext.Notes.Contains(notes.Last()));
        }
        [Test]
        public void NoteRepository_GetAll_ShouldReturnAllNotes()
        {
            SetUp(4);
            var note1 = new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim");
            var note2 = new Note(new Guid(), "b", 2, new DateTimeOffset(), "leiras2", "cim2");
            myDbContext.Notes.Add(note1);
            myDbContext.Notes.Add(note2);
            myDbContext.SaveChanges();

            var actual = myRepository.GetAll();

            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual.Contains(note1));
            Assert.That(actual.Contains(note2));
        }
        [Test]
        public async Task NoteRepository_GetAllAsync_ShouldReturnAllNotes()
        {
            SetUp(5);
            var note1 = new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim");
            var note2 = new Note(new Guid(), "b", 2, new DateTimeOffset(), "leiras2", "cim2");
            myDbContext.Notes.Add(note1);
            myDbContext.Notes.Add(note2);
            myDbContext.SaveChanges();

            var actual = await myRepository.GetAllAsync();

            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual.Contains(note1));
            Assert.That(actual.Contains(note2));
        }
        [Test]
        public void NoteRepository_Remove_ShouldRemoveNoteFromRepository()
        {
            SetUp(6);
            var note1 = new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim");
            var note2 = new Note(new Guid(), "b", 2, new DateTimeOffset(), "leiras2", "cim2");
            myDbContext.Notes.Add(note1);
            myDbContext.Notes.Add(note2);
            myDbContext.SaveChanges();

            myRepository.Remove(note1);
            myDbContext.SaveChanges();

            Assert.That(myDbContext.Notes.Count, Is.EqualTo(1));
            Assert.That(myDbContext.Notes.First, Is.EqualTo(note2));
        }
        [Test]
        public void NoteRepository_RemoveRange_ShouldRemoveNotesFromRepository()
        {
            SetUp(7);
            var note1 = new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim");
            var note2 = new Note(new Guid(), "b", 2, new DateTimeOffset(), "leiras2", "cim2");
            var note3 = new Note(new Guid(), "c", 3, new DateTimeOffset(), "leiras3", "cim3");
            myDbContext.Notes.Add(note1);
            myDbContext.Notes.Add(note2);
            myDbContext.Notes.Add(note3);
            myDbContext.SaveChanges();

            var notes = new List<Note>
            {
                note1,
                note2
            };

            myRepository.RemoveRange(notes);
            myDbContext.SaveChanges();

            Assert.That(myDbContext.Notes.Count, Is.EqualTo(1));
            Assert.That(myDbContext.Notes.First, Is.EqualTo(note3));
        }
        [Test]
        public async Task NoteRepository_SaveChangeAsync_ShouldSaveDb()
        {
            SetUp(8);
            var notes = new List<Note>
            {
            new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim"),
            new Note(new Guid(), "b", 2, new DateTimeOffset(), "leiras2", "cim2")
            };

            myRepository.AddRange(notes);

            Assert.IsFalse(myDbContext.Notes.Count() == 2);
            Assert.IsFalse(myDbContext.Notes.Contains(notes.First()));
            Assert.IsFalse(myDbContext.Notes.Contains(notes.Last()));

            await myRepository.SaveChangesAsync();

            Assert.IsTrue(myDbContext.Notes.Count() == 2);
            Assert.IsTrue(myDbContext.Notes.Contains(notes.First()));
            Assert.IsTrue(myDbContext.Notes.Contains(notes.Last()));
        }
        [Test]
        public void NoteRepository_Update_ShouldUpdateNoteInRepository()
        {
            SetUp(9);
            var note = new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim");
            myDbContext.Notes.Add(note);
            myDbContext.SaveChanges();
            var note2 = new Note(note.NoteId, "b", 2, note.CreatedAt, "Alma", "Title");
            Assert.IsTrue(myDbContext.Notes.Last().Description == "leiras");

            myRepository.Update(note2);

            Assert.That(myDbContext.Notes.Last().Description == "Alma");
            Assert.IsTrue(myDbContext.Notes.Last().Equals(note));
        }
        [Test]
        public void NoteRepository_UpdateRange_ShouldUpdateNotesInRepository()
        {
            SetUp(10);
            var note = new Note(new Guid(), "a", 1, new DateTimeOffset(), "leiras", "cim");
            var note2 = new Note(new Guid(), "b", 2, new DateTimeOffset(), "leiras2", "cim2");
            var note3 = new Note(new Guid(), "c", 3, new DateTimeOffset(), "leiras3", "cim3");
            myDbContext.Notes.Add(note);
            myDbContext.Notes.Add(note2);
            myDbContext.Notes.Add(note3);
            myDbContext.SaveChanges();

            var note4 = new Note(note.NoteId, "aa", 11, note.CreatedAt, "Alma", "Title");
            var note5 = new Note(note2.NoteId, "bb", 22, note.CreatedAt, "Barack", "Korte");
            var notes = new List<Note>
            {
                note4,
                note5
            };

            Assert.IsTrue(myDbContext.Notes.First().Description == "leiras");
            Assert.That(note, Is.EqualTo(myDbContext.Notes.First()));
            Assert.IsTrue(myDbContext.Notes.Last().Description == "leiras3");
            Assert.That(note3, Is.EqualTo(myDbContext.Notes.Last()));

            myRepository.UpdateRange(notes);
            myDbContext.SaveChanges();

            Assert.IsTrue(myDbContext.Notes.First().Description == "Alma");
            Assert.That(myDbContext.Notes.Contains(note4));
            Assert.That(myDbContext.Notes.Contains(note5));
            Assert.IsTrue(myDbContext.Notes.Last().Description == "leiras3");
            Assert.That(note3, Is.EqualTo(myDbContext.Notes.Last()));
        }

    }
}