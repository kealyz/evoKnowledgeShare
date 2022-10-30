using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            myRepositoryMock.Setup(x => x.GetAll()).Returns(() =>
            {
                return new List<Note>()
                {
                    new Note(Guid.NewGuid(),"z004cjm",1,DateTimeOffset.Now,"Description","Title")
                };
            });
        }
        [Test]
        public void NoteService_Get_ShouldReturnAll()
        {

            var notes = myNoteService.Get();

            Assert.That(notes.Count, Is.EqualTo(1));

        }


    }
}
