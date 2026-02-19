using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Project;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.API.Controllers
{
    /// <summary>
    /// Controller delgado para proyectos.
    /// Toda la lógica está en ProjectService.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            if (project == null)
            {
                return NotFound(new { message = "Proyecto no encontrado" });
            }

            return Ok(project);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeatured()
        {
            var projects = await _projectService.GetFeaturedProjectsAsync();
            return Ok(projects);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto createDto)
        {
            var project = await _projectService.CreateProjectAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectDto updateDto)
        {
            var result = await _projectService.UpdateProjectAsync(id, updateDto);

            if (!result)
            {
                return NotFound(new { message = "Proyecto no encontrado" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _projectService.DeleteProjectAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Proyecto no encontrado" });
            }

            return NoContent();
        }
    }
}