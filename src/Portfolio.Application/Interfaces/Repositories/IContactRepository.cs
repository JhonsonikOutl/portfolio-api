using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Repositories
{
    public interface IContactRepository : IRepository<ContactMessage>
    {
        Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync();
        Task<bool> MarkAsReadAsync(Guid id);
    }
}