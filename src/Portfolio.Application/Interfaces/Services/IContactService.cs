using Portfolio.Application.DTOs.Contact;

namespace Portfolio.Application.Interfaces.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactMessageDto>> GetAllMessagesAsync();
        Task<ContactMessageDto?> GetMessageByIdAsync(Guid id);
        Task<IEnumerable<ContactMessageDto>> GetUnreadMessagesAsync();
        Task<ContactMessageDto> CreateMessageAsync(CreateContactMessageDto createDto);
        Task<bool> MarkAsReadAsync(Guid id);
        Task<bool> DeleteMessageAsync(Guid id);
    }
}
