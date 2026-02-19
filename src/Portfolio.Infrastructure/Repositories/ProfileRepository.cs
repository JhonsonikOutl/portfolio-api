using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio para gestionar el perfil profesional.
    /// </summary>
    public class ProfileRepository : IProfileRepository
    {
        private readonly IMongoCollection<Profile> _collection;

        public ProfileRepository(MongoDbContext context)
        {
            _collection = context.Profiles;
        }

        public async Task<Profile?> GetProfileAsync()
        {
            return await _collection.Find(_ => true).FirstOrDefaultAsync();
        }

        public async Task<Profile> CreateOrUpdateProfileAsync(Profile profile)
        {
            var existing = await GetProfileAsync();

            if (existing != null)
            {
                profile.Id = existing.Id;
                profile.CreatedAt = existing.CreatedAt;
                profile.UpdatedAt = DateTime.UtcNow;

                var filter = Builders<Profile>.Filter.Eq("Id", existing.Id);
                await _collection.ReplaceOneAsync(filter, profile);
            }
            else
            {
                profile.Id = Guid.NewGuid();
                profile.CreatedAt = DateTime.UtcNow;
                await _collection.InsertOneAsync(profile);
            }

            return profile;
        }
    }
}
