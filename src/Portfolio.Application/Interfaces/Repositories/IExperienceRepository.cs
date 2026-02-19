using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Repositories
{
    public interface IExperienceRepository : IRepository<Experience>
    {
        Task<IEnumerable<Experience>> GetOrderedByDateAsync();
        Task<Experience?> GetCurrentJobAsync();
    }
}