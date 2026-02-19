using Portfolio.Application.DTOs.Skill;

namespace Portfolio.Application.Interfaces.Services
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillDto>> GetAllSkillsAsync();
        Task<SkillDto?> GetSkillByIdAsync(Guid id);
        Task<IEnumerable<SkillDto>> GetSkillsByCategoryAsync(string category);
        Task<IEnumerable<string>> GetCategoriesAsync();
        Task<SkillDto> CreateSkillAsync(CreateSkillDto createDto);
        Task<bool> UpdateSkillAsync(Guid id, UpdateSkillDto updateDto);
        Task<bool> DeleteSkillAsync(Guid id);
    }
}
