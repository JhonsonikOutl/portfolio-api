using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio específico para usuarios.
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MongoDbContext context)
            : base(context.Users)
        {
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _collection
                .Find(u => u.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _collection
                .Find(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            var count = await _collection.CountDocumentsAsync(filter);
            return count > 0;
        }
    }
}