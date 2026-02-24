using Portfolio.Domain.ValueObject;

namespace Portfolio.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repositorio para operaciones de seeding
    /// </summary>
    public interface IDatabaseSeedRepository
    {
        Task<SeedDataResult> SeedAllAsync();
        Task ClearAllAsync();
    }

}
