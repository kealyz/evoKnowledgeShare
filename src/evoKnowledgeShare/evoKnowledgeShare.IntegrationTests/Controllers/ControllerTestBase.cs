using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using evoKnowledgeShare.Backend;
using evoKnowledgeShare.Backend.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace evoKnowledgeShare.IntegrationTests.Controllers
{
    [TestFixture]
    public abstract class ControllerTestBase
    {
        protected IWebHostBuilder myBuilder = default!;
        protected TestServer myTestServer = default!;
        protected EvoKnowledgeDbContext myContext = default!;
        protected HttpClient myClient = default!;

        [SetUp]
        public void SetUp()
        {
            myBuilder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .ConfigureTestServices(services => services.AddDbContext<EvoKnowledgeDbContext>(options => options.UseInMemoryDatabase("TestingDB")))
                .UseStartup<Startup>();

            myTestServer = new TestServer(myBuilder);
            myContext = myTestServer.Host.Services.GetRequiredService<EvoKnowledgeDbContext>();
            myClient = myTestServer.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            myClient.Dispose();

            myTestServer.Dispose();
            myContext.Database.EnsureDeleted();

            myContext.Dispose();
        }
    }
}
