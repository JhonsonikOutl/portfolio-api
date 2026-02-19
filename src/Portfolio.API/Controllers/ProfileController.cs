using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Profile;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.API.Controllers
{
    /// <summary>
    /// Controller para gestionar el perfil profesional público.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _profileService.GetProfileAsync();

            if (profile == null)
            {
                return NotFound(new { message = "Perfil no encontrado" });
            }

            return Ok(profile);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateDto)
        {
            var profile = await _profileService.UpdateProfileAsync(updateDto);
            return Ok(profile);
        }

        [HttpGet("resume")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadCv()
        {
            try
            {
                var pdfBytes = await _profileService.GenerateCvAsync();
                return File(pdfBytes, "application/pdf", "CV-Jonathan-Aldana.pdf");
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}