using evoKnowledgeShare.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace evoKnowledgeShare.Backend.DataAccess
{
    public class EvoKnowledgeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Note> Notes { get; set; } = default!;

        public EvoKnowledgeDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
