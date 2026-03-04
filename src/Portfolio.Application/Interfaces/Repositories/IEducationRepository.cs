using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Repositories
{
    public interface IEducationRepository : IRepository<Education>
    {
        Task<IEnumerable<Education>> GetAllOrderedAsync();
        Task<IEnumerable<Education>> GetCurrentEducationsAsync();
    }
}
