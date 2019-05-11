using Cln.Entities.Projects;
using Microsoft.EntityFrameworkCore;

namespace Cln.Infrastructure.Projects
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public ProjectDbContext()
        {
        }

        public ProjectDbContext(DbContextOptions options) : base(options)
        {
            Database.OpenConnection();
            Database.EnsureCreated();

            Projects.Add(new Project() { Title = "Project 1" });

            Database.CloseConnection();
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
        }
    }
}
