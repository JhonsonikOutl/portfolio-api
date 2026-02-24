using Portfolio.Application.DTOs.SessionSettings;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services
{
    /// <summary>
    /// Servicio de negocio para gestión de configuración de sesión
    /// Contiene toda la lógica de validación y transformación
    /// </summary>
    public class SessionSettingsService : ISessionSettingsService
    {
        private readonly ISessionSettingsRepository _repository;

        public SessionSettingsService(ISessionSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<SessionSettingsDto?> GetSessionSettingsAsync()
        {
            var settings = await _repository.GetAsync();

            if (settings == null)
            {
                return null;
            }

            return MapToDto(settings);
        }

        public async Task<SessionSettingsDto> UpdateSessionSettingsAsync(UpdateSessionSettingsDto dto)
        {
            ValidateUpdateDto(dto);

            var settings = await _repository.GetAsync();

            if (settings == null)
            {
                throw new InvalidOperationException("No existe configuración de sesión");
            }

            settings.InactivityTimeoutMinutes = dto.InactivityTimeoutMinutes;
            settings.WarningBeforeTimeoutMinutes = dto.WarningBeforeTimeoutMinutes;
            settings.IsEnabled = dto.IsEnabled;

            if (!settings.IsValid())
            {
                throw new InvalidOperationException("La configuración de sesión no es válida");
            }

            var updated = await _repository.UpdateAsync(settings);

            return MapToDto(updated);
        }

        private void ValidateUpdateDto(UpdateSessionSettingsDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.InactivityTimeoutMinutes < 1)
            {
                throw new ArgumentException(
                    "El timeout de inactividad debe ser al menos 1 minuto",
                    nameof(dto.InactivityTimeoutMinutes));
            }

            if (dto.InactivityTimeoutMinutes > 1440) // 24 horas max
            {
                throw new ArgumentException(
                    "El timeout de inactividad no puede exceder 24 horas",
                    nameof(dto.InactivityTimeoutMinutes));
            }

            if (dto.WarningBeforeTimeoutMinutes < 0)
            {
                throw new ArgumentException(
                    "El tiempo de advertencia no puede ser negativo",
                    nameof(dto.WarningBeforeTimeoutMinutes));
            }

            if (dto.WarningBeforeTimeoutMinutes >= dto.InactivityTimeoutMinutes)
            {
                throw new ArgumentException(
                    "El tiempo de advertencia debe ser menor al timeout total",
                    nameof(dto.WarningBeforeTimeoutMinutes));
            }
        }

        private SessionSettingsDto MapToDto(SessionSettings entity)
        {
            return new SessionSettingsDto
            {
                InactivityTimeoutMinutes = entity.InactivityTimeoutMinutes,
                WarningBeforeTimeoutMinutes = entity.WarningBeforeTimeoutMinutes,
                IsEnabled = entity.IsEnabled
            };
        }
    }
}
