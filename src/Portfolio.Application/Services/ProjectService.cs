using Portfolio.Application.DTOs.Project;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services
{
    /// <summary>
    /// Servicio de lógica de negocio para proyectos.
    /// Contiene mapeo y validaciones.
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(MapToDto);
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return project != null ? MapToDto(project) : null;
        }

        public async Task<IEnumerable<ProjectDto>> GetFeaturedProjectsAsync()
        {
            var projects = await _projectRepository.GetFeaturedProjectsAsync();
            return projects.Select(MapToDto);
        }

        public async Task<IEnumerable<ProjectDto>> GetProjectsByTechnologyAsync(string technology)
        {
            var projects = await _projectRepository.GetByTechnologyAsync(technology);
            return projects.Select(MapToDto);
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto createDto)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = createDto.Title,
                Description = createDto.Description,
                Technologies = createDto.Technologies,
                ImageUrl = createDto.ImageUrl,
                GithubUrl = createDto.GithubUrl,
                LiveUrl = createDto.LiveUrl,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                IsFeatured = createDto.IsFeatured,
                DisplayOrder = createDto.DisplayOrder
            };

            var created = await _projectRepository.CreateAsync(project);
            return MapToDto(created);
        }

        public async Task<bool> UpdateProjectAsync(Guid id, UpdateProjectDto updateDto)
        {
            var existing = await _projectRepository.GetByIdAsync(id);

            if (existing == null)
            {
                return false;
            }

            existing.Title = updateDto.Title;
            existing.Description = updateDto.Description;
            existing.Technologies = updateDto.Technologies;
            existing.ImageUrl = updateDto.ImageUrl;
            existing.GithubUrl = updateDto.GithubUrl;
            existing.LiveUrl = updateDto.LiveUrl;
            existing.StartDate = updateDto.StartDate;
            existing.EndDate = updateDto.EndDate;
            existing.IsFeatured = updateDto.IsFeatured;
            existing.DisplayOrder = updateDto.DisplayOrder;

            return await _projectRepository.UpdateAsync(id, existing);
        }

        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            return await _projectRepository.DeleteAsync(id);
        }

        private static ProjectDto MapToDto(Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Technologies = project.Technologies,
                ImageUrl = project.ImageUrl,
                GithubUrl = project.GithubUrl,
                LiveUrl = project.LiveUrl,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                IsFeatured = project.IsFeatured,
                DisplayOrder = project.DisplayOrder
            };
        }
    }
}