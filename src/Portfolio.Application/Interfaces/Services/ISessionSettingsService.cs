using Portfolio.Application.DTOs.SessionSettings;

namespace Portfolio.Application.Interfaces.Services
{
    /// <summary>
    /// Servicio de negocio para gestión de configuración de sesión
    /// </summary>
    public interface ISessionSettingsService
    {
        Task<SessionSettingsDto?> GetSessionSettingsAsync();

        Task<SessionSettingsDto> UpdateSessionSettingsAsync(UpdateSessionSettingsDto dto);
    }
}
