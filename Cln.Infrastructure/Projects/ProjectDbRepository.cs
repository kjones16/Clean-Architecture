using AutoMapper;
using Cln.Application.Interfaces.Projects;
using Cln.Entities.Projects;

namespace Cln.Infrastructure.Projects
{
    public class ProjectDbRepository : DbContextRepository<Project, int>, IProjectRepository
    {
        public ProjectDbRepository(ProjectDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}