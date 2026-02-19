using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Skill;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var skill = await _skillService.GetSkillByIdAsync(id);

            if (skill == null)
            {
                return NotFound(new { message = "Habilidad no encontrada" });
            }

            return Ok(skill);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var skills = await _skillService.GetSkillsByCategoryAsync(category);
            return Ok(skills);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _skillService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateSkillDto createDto)
        {
            var skill = await _skillService.CreateSkillAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSkillDto updateDto)
        {
            var result = await _skillService.UpdateSkillAsync(id, updateDto);

            if (!result)
            {
                return NotFound(new { message = "Habilidad no encontrada" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _skillService.DeleteSkillAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Habilidad no encontrada" });
            }

            return NoContent();
        }
    }
}
