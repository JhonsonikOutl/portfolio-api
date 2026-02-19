using Portfolio.Application.DTOs.Contact;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
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
                IsRead = false
            };

            var created = await _contactRepository.CreateAsync(message);
            return MapToDto(created);
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