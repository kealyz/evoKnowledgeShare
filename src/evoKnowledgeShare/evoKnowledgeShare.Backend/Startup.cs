using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using evoKnowledgeShare.Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.Backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (!CurrentEnvironment.IsEnvironment("Testing"))
            {
                Console.WriteLine($"Adding database context type <${nameof(EvoKnowledgeDbContext)}>.");
                services.AddDbContext<EvoKnowledgeDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
            }
            Console.WriteLine("Adding controllers.");
            services.AddControllers();
            Console.WriteLine("Adding services.");
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<History>, HistoryRepository>();
            services.AddScoped<HistoryService>();
            services.AddScoped<UserService>();
            Console.WriteLine("Done.");

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
