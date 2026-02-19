using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interfaz específica para repositorio de proyectos.
    /// Hereda todas las operaciones CRUD de IRepository<Project>.
    /// </summary>
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetFeaturedProjectsAsync();
        Task<IEnumerable<Project>> GetByTechnologyAsync(string technology);
    }
}