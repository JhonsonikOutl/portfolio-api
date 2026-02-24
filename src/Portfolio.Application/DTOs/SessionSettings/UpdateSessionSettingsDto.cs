namespace Portfolio.Application.DTOs.SessionSettings
{
    /// <summary>
    /// DTO para actualizar configuración de sesión
    /// </summary>
    public class UpdateSessionSettingsDto
    {
        public int InactivityTimeoutMinutes { get; set; }
        public int WarningBeforeTimeoutMinutes { get; set; }
        public bool IsEnabled { get; set; }
    }
}