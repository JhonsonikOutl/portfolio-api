using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Education;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.API.Controllers
{
    /// <summary>
    /// Controller delgado (thin controller) para educación.
    /// Solo maneja HTTP y delega toda la lógica al servicio.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationDto>>> GetAll()
        {
            var educations = await _educationService.GetAllEducationAsync();
            return Ok(educations);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EducationDto>> GetById(Guid id)
        {
            var education = await _educationService.GetEducationByIdAsync(id);

            if (education == null)
            {
                return NotFound(new { message = "Educación no encontrada" });
            }

            return Ok(education);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<EducationDto>> Create([FromBody] CreateEducationDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _educationService.CreateEducationAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEducationDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _educationService.UpdateEducationAsync(id, updateDto);

            if (!success)
            {
                return NotFound(new { message = "Educación no encontrada" });
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _educationService.DeleteEducationAsync(id);

            if (!success)
            {
                return NotFound(new { message = "Educación no encontrada" });
            }

            return NoContent();
        }
    }
}