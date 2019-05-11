using Cln.Application.Interfaces.Projects;
using Cln.Entities.Projects;

namespace Cln.Infrastructure.Projects
{
    public class ProjectDbRepository : DbContextRepository<Project, int>, IProjectRepository
    {
        private readonly ProjectDbContext _dbContext;

        public ProjectDbRepository(ProjectDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}