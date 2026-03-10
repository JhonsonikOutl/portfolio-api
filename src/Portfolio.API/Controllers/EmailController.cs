using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Email;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.API.Controllers
{
    /// <summary>
    /// Solo recibe requests y retorna responses.
    /// NO maneja excepciones (lo hace el middleware global).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailApplicationService _emailService;

        public EmailController(IEmailApplicationService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SendEmailDto emailDto)
        {
            await _emailService.SendEmailAsync(emailDto);

            return Ok(new { message = "Email enviado exitosamente" });
        }

        [HttpPost("reply/{messageId:guid}")]
        public async Task<IActionResult> ReplyToContact(Guid messageId, [FromBody] ReplyEmailDto replyDto)
        {
            var result = await _emailService.ReplyToContactMessageAsync(messageId, replyDto);

            return Ok(result);
        }
    }
}
