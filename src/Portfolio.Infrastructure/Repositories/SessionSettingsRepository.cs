using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Data;
using Portfolio.Infrastructure.Mappers;

namespace Portfolio.Infrastructure.Repositories
{
    public class SessionSettingsRepository : ISessionSettingsRepository
    {
        private readonly IMongoCollection<SessionSettings> _collection;

        public SessionSettingsRepository(MongoDbContext context)
        {
            _collection = context.SessionSettings;
        }

        public async Task<SessionSettings?> GetAsync()
        {
            var model = await _collection.Find(_ => true).FirstOrDefaultAsync();

            if (model == null)
            {
                var defaultEntity = new SessionSettings
                {
                    InactivityTimeoutMinutes = 15,
                    WarningBeforeTimeoutMinutes = 1,
                    IsEnabled = true,
                    UpdatedAt = DateTime.UtcNow
                };

                var defaultModel = SessionSettingsMapper.ToModel(defaultEntity);
                await _collection.InsertOneAsync(defaultModel);

                return defaultEntity;
            }

            return SessionSettingsMapper.ToEntity(model);
        }

        public async Task<SessionSettings> UpdateAsync(SessionSettings settings)
        {
            if (!settings.IsValid())
            {
                throw new InvalidOperationException("Configuración de sesión inválida");
            }

            settings.UpdatedAt = DateTime.UtcNow;

            var model = SessionSettingsMapper.ToModel(settings);
            var filter = Builders<SessionSettings>.Filter.Eq(s => s.Id, model.Id);

            await _collection.ReplaceOneAsync(filter, model);

            return settings;
        }
    }
}
