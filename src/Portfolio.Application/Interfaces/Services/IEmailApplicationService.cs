using Portfolio.Application.DTOs.Email;

namespace Portfolio.Application.Interfaces.Services
{
    /// <summary>
    /// Usa DTOs y orquesta IEmailService + IContactService.
    /// </summary>
    public interface IEmailApplicationService
    {
        Task SendEmailAsync(SendEmailDto emailDto);

        Task<EmailResponseDto> ReplyToContactMessageAsync(Guid messageId, ReplyEmailDto replyDto);
    }
}
