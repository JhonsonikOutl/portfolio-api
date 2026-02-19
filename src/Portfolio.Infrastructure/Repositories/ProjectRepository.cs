using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio específico para proyectos.
    /// Hereda de Repository<Project> (tiene CRUD básico).
    /// Implementa IProjectRepository (métodos específicos).
    /// </summary>
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(MongoDbContext context)
            : base(context.Projects)
        {
        }

        public async Task<IEnumerable<Project>> GetFeaturedProjectsAsync()
        {
            return await _collection
                .Find(p => p.IsFeatured == true)
                .SortBy(p => p.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetByTechnologyAsync(string technology)
        {
            return await _collection
                .Find(p => p.Technologies.Contains(technology))
                .SortBy(p => p.DisplayOrder)
                .ToListAsync();
        }
    }
}
