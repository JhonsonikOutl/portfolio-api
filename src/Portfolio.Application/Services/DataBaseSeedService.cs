using Portfolio.Application.DTOs.Seed;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;

namespace Portfolio.Application.Services
{
    /// <summary>
    /// Servicio para operaciones de seeding y limpieza de base de datos
    /// Encapsula toda la lógica de seeding fuera del controller
    /// </summary>
    public class DatabaseSeedService : IDatabaseSeedService
    {
        private readonly IDatabaseSeedRepository _seedRepository;

        public DatabaseSeedService(IDatabaseSeedRepository seedRepository)
        {
            _seedRepository = seedRepository;
        }

        public async Task<SeedResultDto> SeedDatabaseAsync()
        {
            var result = await _seedRepository.SeedAllAsync();

            return new SeedResultDto
            {
                ProfileCreated = result.ProfileCreated,
                SkillsCreated = result.SkillsCreated,
                ExperiencesCreated = result.ExperiencesCreated,
                ProjectsCreated = result.ProjectsCreated,
                SessionSettingsCreated = result.SessionSettingsCreated
            };
        }

        public async Task ClearDatabaseAsync()
        {
            await _seedRepository.ClearAllAsync();
        }
    }
}
