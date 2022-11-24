using evoKnowledgeShare.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.Backend.DataAccess
{
    public class EvoKnowledgeDbContext : DbContext
    {
        public EvoKnowledgeDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<History> Histories { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;

        public DbSet<Topic> Topics { get; set; } = default!;

        public DbSet<Note> Notes { get; set; } = default!;
    }
}
