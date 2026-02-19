using Portfolio.Application.DTOs.Experience;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepository;

        public ExperienceService(IExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }

        public async Task<IEnumerable<ExperienceDto>> GetAllExperiencesAsync()
        {
            var experiences = await _experienceRepository.GetAllAsync();
            return experiences.Select(MapToDto);
        }

        public async Task<ExperienceDto?> GetExperienceByIdAsync(Guid id)
        {
            var experience = await _experienceRepository.GetByIdAsync(id);
            return experience != null ? MapToDto(experience) : null;
        }

        public async Task<IEnumerable<ExperienceDto>> GetOrderedByDateAsync()
        {
            var experiences = await _experienceRepository.GetOrderedByDateAsync();
            return experiences.Select(MapToDto);
        }

        public async Task<ExperienceDto?> GetCurrentJobAsync()
        {
            var experience = await _experienceRepository.GetCurrentJobAsync();
            return experience != null ? MapToDto(experience) : null;
        }

        public async Task<ExperienceDto> CreateExperienceAsync(CreateExperienceDto createDto)
        {
            var experience = new Experience
            {
                Id = Guid.NewGuid(),
                Company = createDto.Company,
                Position = createDto.Position,
                Description = createDto.Description,
                Achievements = createDto.Achievements,
                Technologies = createDto.Technologies,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                IsCurrentJob = createDto.IsCurrentJob,
                DisplayOrder = createDto.DisplayOrder
            };

            var created = await _experienceRepository.CreateAsync(experience);
            return MapToDto(created);
        }

        public async Task<bool> UpdateExperienceAsync(Guid id, UpdateExperienceDto updateDto)
        {
            var existing = await _experienceRepository.GetByIdAsync(id);

            if (existing == null)
            {
                return false;
            }

            existing.Company = updateDto.Company;
            existing.Position = updateDto.Position;
            existing.Description = updateDto.Description;
            existing.Achievements = updateDto.Achievements;
            existing.Technologies = updateDto.Technologies;
            existing.StartDate = updateDto.StartDate;
            existing.EndDate = updateDto.EndDate;
            existing.IsCurrentJob = updateDto.IsCurrentJob;
            existing.DisplayOrder = updateDto.DisplayOrder;

            return await _experienceRepository.UpdateAsync(id, existing);
        }

        public async Task<bool> DeleteExperienceAsync(Guid id)
        {
            return await _experienceRepository.DeleteAsync(id);
        }

        private static ExperienceDto MapToDto(Experience experience)
        {
            return new ExperienceDto
            {
                Id = experience.Id,
                Company = experience.Company,
                Position = experience.Position,
                Description = experience.Description,
                Achievements = experience.Achievements,
                Technologies = experience.Technologies,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                IsCurrentJob = experience.IsCurrentJob,
                DisplayOrder = experience.DisplayOrder
            };
        }
    }
}
