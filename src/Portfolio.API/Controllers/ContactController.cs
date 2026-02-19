using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Contact;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _contactService.GetAllMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var message = await _contactService.GetMessageByIdAsync(id);

            if (message == null)
            {
                return NotFound(new { message = "Mensaje no encontrado" });
            }

            return Ok(message);
        }

        [HttpGet("unread")]
        [Authorize]
        public async Task<IActionResult> GetUnread()
        {
            var messages = await _contactService.GetUnreadMessagesAsync();
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactMessageDto createDto)
        {
            var message = await _contactService.CreateMessageAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
        }

        [HttpPatch("{id}/read")]
        [Authorize]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            var result = await _contactService.MarkAsReadAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Mensaje no encontrado" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _contactService.DeleteMessageAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Mensaje no encontrado" });
            }

            return NoContent();
        }
    }
}