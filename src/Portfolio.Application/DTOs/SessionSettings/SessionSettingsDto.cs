namespace Portfolio.Application.DTOs.SessionSettings
{
    /// <summary>
    /// DTO para configuración de sesión
    /// </summary>
    public class SessionSettingsDto
    {
        public int InactivityTimeoutMinutes { get; set; }

        public int WarningBeforeTimeoutMinutes { get; set; }

        public bool IsEnabled { get; set; }
    }
}
