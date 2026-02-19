using Portfolio.Application.DTOs.Skill;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<IEnumerable<SkillDto>> GetAllSkillsAsync()
        {
            var skills = await _skillRepository.GetAllAsync();
            return skills.Select(MapToDto);
        }

        public async Task<SkillDto?> GetSkillByIdAsync(Guid id)
        {
            var skill = await _skillRepository.GetByIdAsync(id);
            return skill != null ? MapToDto(skill) : null;
        }

        public async Task<IEnumerable<SkillDto>> GetSkillsByCategoryAsync(string category)
        {
            var skills = await _skillRepository.GetByCategoryAsync(category);
            return skills.Select(MapToDto);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            return await _skillRepository.GetCategoriesAsync();
        }

        public async Task<SkillDto> CreateSkillAsync(CreateSkillDto createDto)
        {
            var skill = new Skill
            {
                Id = Guid.NewGuid(),
                Name = createDto.Name,
                Category = createDto.Category,
                Level = createDto.Level,
                Icon = createDto.Icon,
                DisplayOrder = createDto.DisplayOrder
            };

            var created = await _skillRepository.CreateAsync(skill);
            return MapToDto(created);
        }

        public async Task<bool> UpdateSkillAsync(Guid id, UpdateSkillDto updateDto)
        {
            var existing = await _skillRepository.GetByIdAsync(id);

            if (existing == null)
            {
                return false;
            }

            existing.Name = updateDto.Name;
            existing.Category = updateDto.Category;
            existing.Level = updateDto.Level;
            existing.Icon = updateDto.Icon;
            existing.DisplayOrder = updateDto.DisplayOrder;

            return await _skillRepository.UpdateAsync(id, existing);
        }

        public async Task<bool> DeleteSkillAsync(Guid id)
        {
            return await _skillRepository.DeleteAsync(id);
        }

        private static SkillDto MapToDto(Skill skill)
        {
            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Category = skill.Category,
                Level = skill.Level,
                Icon = skill.Icon,
                DisplayOrder = skill.DisplayOrder
            };
        }
    }
}
