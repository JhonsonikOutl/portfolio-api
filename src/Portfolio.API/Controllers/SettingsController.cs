using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.SessionSettings;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.API.Controllers
{
    /// <summary>
    /// Controller para gestión de configuraciones del sistema
    /// Thin controller: solo maneja HTTP, delega lógica al servicio
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ISessionSettingsService _sessionSettingsService;

        public SettingsController(ISessionSettingsService sessionSettingsService)
        {
            _sessionSettingsService = sessionSettingsService;
        }

        /// <summary>
        /// Obtiene la configuración de sesión (público - usado en login)
        /// </summary>
        [HttpGet("session")]
        [AllowAnonymous]
        public async Task<ActionResult<SessionSettingsDto>> GetSessionSettings()
        {
            var settings = await _sessionSettingsService.GetSessionSettingsAsync();

            if (settings == null)
            {
                return NotFound();
            }

            return Ok(settings);
        }

        /// <summary>
        /// Actualiza la configuración de sesión (solo admin)
        /// </summary>
        [HttpPut("session")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SessionSettingsDto>> UpdateSessionSettings(
            [FromBody] UpdateSessionSettingsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _sessionSettingsService.UpdateSessionSettingsAsync(dto);
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
