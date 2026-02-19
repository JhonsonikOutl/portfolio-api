using Portfolio.Domain.Entities;

namespace Portfolio.Application.Interfaces.Repositories
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<IEnumerable<Skill>> GetByCategoryAsync(string category);
        Task<IEnumerable<string>> GetCategoriesAsync();
    }
}