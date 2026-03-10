using Portfolio.Application.DTOs.Email;
using Portfolio.Application.Exceptions;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.Application.Services
{
    /// <summary>
    /// Servicio de aplicación mejorado con templates HTML.
    /// </summary>
    public class EmailApplicationService : IEmailApplicationService
    {
        private readonly IEmailService _emailService;
        private readonly IContactService _contactService;

        public EmailApplicationService(
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

            string emailBody = replyDto.Body;

            if (replyDto.IsHtml)
            {
                emailBody = EmailTemplateHelper.GenerateReplyTemplate(
                    recipientName: message.Name,
                    replyBody: replyDto.Body,
                    senderName: "Jonathan Aldana"
                );
            }

            var sent = await _emailService.SendEmailAsync(
                message.Email,
                subject,
                emailBody,
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