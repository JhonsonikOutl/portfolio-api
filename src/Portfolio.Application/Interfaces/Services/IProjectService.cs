using Portfolio.Application.DTOs.Project;

namespace Portfolio.Application.Interfaces.Services
{
    /// <summary>
    /// Servicio para lógica de negocio de proyectos.
    /// </summary>
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectByIdAsync(Guid id);
        Task<IEnumerable<ProjectDto>> GetFeaturedProjectsAsync();
        Task<IEnumerable<ProjectDto>> GetProjectsByTechnologyAsync(string technology);
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto createDto);
        Task<bool> UpdateProjectAsync(Guid id, UpdateProjectDto updateDto);
        Task<bool> DeleteProjectAsync(Guid id);
    }
}
