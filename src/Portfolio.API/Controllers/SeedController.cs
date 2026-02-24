using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Seed;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.API.Controllers
{
    /// <summary>
    /// Controller para operaciones de seeding de base de datos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly IDatabaseSeedService _seedService;

        public SeedController(IDatabaseSeedService seedService)
        {
            _seedService = seedService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SeedDatabase()
        {
            var result = await _seedService.SeedDatabaseAsync();
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ClearDatabase()
        {
            await _seedService.ClearDatabaseAsync();
            return NoContent();
        }
    }
}