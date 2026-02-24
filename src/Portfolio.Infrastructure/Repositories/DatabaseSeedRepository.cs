using MongoDB.Driver;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Domain.ValueObject;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Infrastructure.Repositories
{
    /// <summary>
    /// Repositorio para operaciones de seeding
    /// Encapsula el acceso al DbContext
    /// </summary>
    public class DatabaseSeedRepository : IDatabaseSeedRepository
    {
        private readonly MongoDbContext _context;

        public DatabaseSeedRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<SeedDataResult> SeedAllAsync()
        {
            var seeder = new DataSeeder(_context);
            var result = await seeder.SeedAllAsync();

            return new SeedDataResult
            {
                TotalCreated = result.TotalCreated,
                ProfileCreated = result.ProfileCreated,
                SkillsCreated = result.SkillsCreated,
                ExperiencesCreated = result.ExperiencesCreated,
                ProjectsCreated = result.ProjectsCreated,
                SessionSettingsCreated = result.SessionSettingsCreated
            };
        }

        public async Task ClearAllAsync()
        {
            var emptyFilter = Builders<Profile>.Filter.Empty;

            var deleteTasks = new[]
            {
                _context.Profiles.DeleteManyAsync(Builders<Profile>.Filter.Empty),
                _context.Skills.DeleteManyAsync(Builders<Skill>.Filter.Empty),
                _context.Experiences.DeleteManyAsync(Builders<Experience>.Filter.Empty),
                _context.Projects.DeleteManyAsync(Builders<Project>.Filter.Empty),
                _context.ContactMessages.DeleteManyAsync(Builders<ContactMessage>.Filter.Empty),
                _context.SessionSettings.DeleteManyAsync(Builders<SessionSettings>.Filter.Empty)
            };

            await Task.WhenAll(deleteTasks);
        }
    }
}
