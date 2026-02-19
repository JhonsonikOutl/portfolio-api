using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;


namespace Portfolio.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio específico para mensajes de contacto.
    /// </summary>
    public class ContactRepository : Repository<ContactMessage>, IContactRepository
    {
        public ContactRepository(MongoDbContext context)
            : base(context.ContactMessages)
        {
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync()
        {
            return await _collection
                .Find(m => m.IsRead == false)
                .SortByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> MarkAsReadAsync(Guid id)
        {
            var filter = Builders<ContactMessage>.Filter.Eq("Id", id);
            var update = Builders<ContactMessage>.Update
                .Set(m => m.IsRead, true)
                .Set(m => m.UpdatedAt, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}
