using Portfolio.Application.DTOs.Experience;

namespace Portfolio.Application.Interfaces.Services
{
    public interface IExperienceService
    {
        Task<IEnumerable<ExperienceDto>> GetAllExperiencesAsync();
        Task<ExperienceDto?> GetExperienceByIdAsync(Guid id);
        Task<IEnumerable<ExperienceDto>> GetOrderedByDateAsync();
        Task<ExperienceDto?> GetCurrentJobAsync();
        Task<ExperienceDto> CreateExperienceAsync(CreateExperienceDto createDto);
        Task<bool> UpdateExperienceAsync(Guid id, UpdateExperienceDto updateDto);
        Task<bool> DeleteExperienceAsync(Guid id);
    }
}