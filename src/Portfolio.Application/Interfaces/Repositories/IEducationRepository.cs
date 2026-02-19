using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Repositories
{
    public interface IEducationRepository
    {
        Task<IEnumerable<Education>> GetAllAsync();
        Task<Education?> GetByIdAsync(string id);
        Task<Education> CreateAsync(Education education);
        Task<Education?> UpdateAsync(string id, Education education);
        Task<bool> DeleteAsync(string id);
    }
}
