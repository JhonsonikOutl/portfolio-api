using Portfolio.Application.DTOs.Education;

namespace Portfolio.Application.Interfaces.Services
{
    /// <summary>
    /// Servicio para lógica de negocio de educación.
    /// </summary>
    public interface IEducationService
    {
        Task<IEnumerable<EducationDto>> GetAllEducationAsync();
        Task<EducationDto?> GetEducationByIdAsync(Guid id);
        Task<EducationDto> CreateEducationAsync(CreateEducationDto createDto);
        Task<bool> UpdateEducationAsync(Guid id, UpdateEducationDto updateDto);
        Task<bool> DeleteEducationAsync(Guid id);
    }
}
