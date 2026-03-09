using Portfolio.Application.DTOs.Email;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para lógica de negocio de emails.
    /// Lanza excepciones específicas en lugar de retornar Success/Failure.
    /// </summary>
    public class EmailService : IEmailApplicationService
    {
        private readonly IEmailService _emailService;
        private readonly IContactService _contactService;

        public EmailService(
            IEmailService emailService,
            IContactService contactService)
        {
            _emailService = emailService;
            _contactService = contactService;
        }

        public async Task SendEmailAsync(SendEmailDto emailDto)
        {
            var sent = await _emailService.SendEmailAsync(
                emailDto.To,
                emailDto.Subject,
                emailDto.Body,
                emailDto.IsHtml
            );

            if (!sent)
            {
                throw new EmailSendException("Error al enviar el email");
            }
        }

        public async Task<EmailResponseDto> ReplyToContactMessageAsync(Guid messageId, ReplyEmailDto replyDto)
        {
            var message = await _contactService.GetMessageByIdAsync(messageId);

            if (message == null)
            {
                throw new NotFoundException("Mensaje no encontrado");
            }

            var subject = $"Re: {message.Subject}";

            var sent = await _emailService.SendEmailAsync(
                message.Email,
                subject,
                replyDto.Body,
                replyDto.IsHtml
            );

            if (!sent)
            {
                throw new EmailSendException("Error al enviar la respuesta");
            }

            await _contactService.MarkAsReadAsync(messageId);

            return new EmailResponseDto
            {
                Message = "Respuesta enviada exitosamente",
                SentTo = message.Email
            };
        }
    }
}