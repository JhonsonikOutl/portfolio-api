using Portfolio.Application.DTOs.Education;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services
{
    /// <summary>
    /// Servicio de lógica de negocio para educación.
    /// Contiene mapeo y validaciones.
    /// </summary>
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;

        public EducationService(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task<IEnumerable<EducationDto>> GetAllEducationAsync()
        {
            var educations = await _educationRepository.GetAllOrderedAsync();
            return educations.Select(MapToDto);
        }

        public async Task<EducationDto?> GetEducationByIdAsync(Guid id)
        {
            var education = await _educationRepository.GetByIdAsync(id);
            return education != null ? MapToDto(education) : null;
        }

        public async Task<EducationDto> CreateEducationAsync(CreateEducationDto createDto)
        {
            var education = new Education
            {
                Id = Guid.NewGuid(),
                Institution = createDto.Institution,
                Degree = createDto.Degree,
                FieldOfStudy = createDto.FieldOfStudy,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                IsCurrentlyStudying = createDto.IsCurrentlyStudying,
                Description = createDto.Description,
                DisplayOrder = createDto.DisplayOrder
            };

            var created = await _educationRepository.CreateAsync(education);
            return MapToDto(created);
        }

        public async Task<bool> UpdateEducationAsync(Guid id, UpdateEducationDto updateDto)
        {
            var existing = await _educationRepository.GetByIdAsync(id);

            if (existing == null)
            {
                return false;
            }

            existing.Institution = updateDto.Institution;
            existing.Degree = updateDto.Degree;
            existing.FieldOfStudy = updateDto.FieldOfStudy;
            existing.StartDate = updateDto.StartDate;
            existing.EndDate = updateDto.EndDate;
            existing.IsCurrentlyStudying = updateDto.IsCurrentlyStudying;
            existing.Description = updateDto.Description;
            existing.DisplayOrder = updateDto.DisplayOrder;

            return await _educationRepository.UpdateAsync(id, existing);
        }

        public async Task<bool> DeleteEducationAsync(Guid id)
        {
            return await _educationRepository.DeleteAsync(id);
        }

        private static EducationDto MapToDto(Education education)
        {
            return new EducationDto
            {
                Id = education.Id,
                Institution = education.Institution,
                Degree = education.Degree,
                FieldOfStudy = education.FieldOfStudy,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                IsCurrentlyStudying = education.IsCurrentlyStudying,
                Description = education.Description,
                DisplayOrder = education.DisplayOrder
            };
        }
    }
}