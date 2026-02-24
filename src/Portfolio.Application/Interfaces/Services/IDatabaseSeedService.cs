using Portfolio.Application.DTOs.Seed;

namespace Portfolio.Application.Interfaces.Services
{
    /// <summary>
    /// Servicio para operaciones de seeding y limpieza de base de datos
    /// SOLO PARA DESARROLLO
    /// </summary>
    public interface IDatabaseSeedService
    {
        Task<SeedResultDto> SeedDatabaseAsync();
        Task ClearDatabaseAsync();
    }
}
