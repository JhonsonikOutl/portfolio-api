using Portfolio.Application.DTOs.Contact;
using Portfolio.Application.Helpers;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services
{
    /// <summary>
    /// Servicio de lógica de negocio para mensajes de contacto.
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IEmailService _emailService;
        private readonly IContactAuditService _contactAuditService;

        public ContactService(
            IContactRepository contactRepository,
            IEmailService emailService,
            IContactAuditService contactAuditService)
        {
            _contactRepository = contactRepository;
            _emailService = emailService;
            _contactAuditService = contactAuditService;
        }

        public async Task<IEnumerable<ContactMessageDto>> GetAllMessagesAsync()
        {
            var messages = await _contactRepository.GetAllAsync();
            return messages.Select(MapToDto);
        }

        public async Task<ContactMessageDto?> GetMessageByIdAsync(Guid id)
        {
            var message = await _contactRepository.GetByIdAsync(id);
            return message != null ? MapToDto(message) : null;
        }

        public async Task<IEnumerable<ContactMessageDto>> GetUnreadMessagesAsync()
        {
            var messages = await _contactRepository.GetUnreadMessagesAsync();
            return messages.Select(MapToDto);
        }

        public async Task<ContactMessageDto> CreateMessageAsync(CreateContactMessageDto createDto)
        {
            var message = new ContactMessage
            {
                Id = Guid.NewGuid(),
                Name = createDto.Name,
                Email = createDto.Email,
                Subject = createDto.Subject,
                Message = createDto.Message,
                RadicateNumber = createDto.RadicateNumber,
                IsRead = false
            };

            var created = await _contactRepository.CreateAsync(message);

            var radicateNumber = await _contactAuditService.GenerateRadicateAsync(created.Id);

            var confirmationBody = EmailTemplateHelper.GenerateConfirmationTemplate(
                recipientName: created.Name,
                subject: created.Subject,
                radicate : radicateNumber
            );

            await _emailService.SendEmailAsync(
                to: created.Email,
                subject: "He recibido tu mensaje",
                body: confirmationBody,
                isHtml: true
            );

            var dto = MapToDto(created);
            dto.RadicateNumber = radicateNumber;

            return dto;
        }

        public async Task<bool> MarkAsReadAsync(Guid id)
        {
            return await _contactRepository.MarkAsReadAsync(id);
        }

        public async Task<bool> DeleteMessageAsync(Guid id)
        {
            return await _contactRepository.DeleteAsync(id);
        }

        private static ContactMessageDto MapToDto(ContactMessage message)
        {
            return new ContactMessageDto
            {
                Id = message.Id,
                Name = message.Name,
                Email = message.Email,
                Subject = message.Subject,
                Message = message.Message,
                IsRead = message.IsRead,
                CreatedAt = message.CreatedAt
            };
        }
    }
}