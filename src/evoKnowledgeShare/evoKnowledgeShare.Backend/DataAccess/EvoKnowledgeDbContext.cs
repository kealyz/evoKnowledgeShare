using evoKnowledgeShare.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.Backend.DataAccess
{
    public class EvoKnowledgeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Topic> Topics { get; set; }

        public EvoKnowledgeDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
