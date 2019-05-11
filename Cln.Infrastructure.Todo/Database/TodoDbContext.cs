using Cln.Entities.Todo;
using Microsoft.EntityFrameworkCore;

namespace Cln.Infrastructure.Todo.Database
{
    public class TodoDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        public TodoDbContext()
        {
        }

        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // Check if it is already configured. This would happen in unit testing.
            {
                optionsBuilder.UseSqlite("Data Source=Project.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoList>().ToTable("TodoLists").HasMany(e => e.Items).WithOne(e => e.List);
            
            modelBuilder.Entity<TodoItem>()
                .ToTable("TodoItems")
                .HasOne(i => i.List)
                .WithMany(l => l.Items)
                .HasForeignKey(p => p.ListId);
        }
    }
}