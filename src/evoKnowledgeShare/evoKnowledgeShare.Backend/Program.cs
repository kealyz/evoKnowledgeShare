using evoKnowledgeShare.Backend.DataAccess;
using evoKnowledgeShare.Backend.Interfaces;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using evoKnowledgeShare.Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<EvoKnowledgeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<UserService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}