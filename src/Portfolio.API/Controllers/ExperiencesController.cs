using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Experience;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperiencesController : ControllerBase
    {
        private readonly IExperienceService _experienceService;

        public ExperiencesController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var experiences = await _experienceService.GetOrderedByDateAsync();
            return Ok(experiences);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var experience = await _experienceService.GetExperienceByIdAsync(id);

            if (experience == null)
            {
                return NotFound(new { message = "Experiencia no encontrada" });
            }

            return Ok(experience);
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent()
        {
            var experience = await _experienceService.GetCurrentJobAsync();

            if (experience == null)
            {
                return NotFound(new { message = "No hay trabajo actual" });
            }

            return Ok(experience);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateExperienceDto createDto)
        {
            var experience = await _experienceService.CreateExperienceAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = experience.Id }, experience);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExperienceDto updateDto)
        {
            var result = await _experienceService.UpdateExperienceAsync(id, updateDto);

            if (!result)
            {
                return NotFound(new { message = "Experiencia no encontrada" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _experienceService.DeleteExperienceAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Experiencia no encontrada" });
            }

            return NoContent();
        }
    }
}