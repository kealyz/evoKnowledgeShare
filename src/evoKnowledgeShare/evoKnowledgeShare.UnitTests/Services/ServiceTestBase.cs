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
    [TestFixture]
    public abstract class ServiceTestBase<T> where T : class
    {
        protected Mock<IRepository<T>> myRepositoryMock = default!;

        [SetUp]
        public void Setup() 
        {
            myRepositoryMock = new Mock<IRepository<T>>(MockBehavior.Strict);
        }
    }
}
