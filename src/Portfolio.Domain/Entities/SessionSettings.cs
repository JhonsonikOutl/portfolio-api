namespace Portfolio.Domain.Entities
{
    /// <summary>
    /// Configuración de la sesión de usuario
    /// </summary>
    public class SessionSettings
    {
        public string? Id { get; set; }

        public int InactivityTimeoutMinutes { get; set; } = 15;

        public int WarningBeforeTimeoutMinutes { get; set; } = 1;

        public bool IsEnabled { get; set; } = true;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsValid()
        {
            return InactivityTimeoutMinutes > 0 &&
                   WarningBeforeTimeoutMinutes >= 0 &&
                   WarningBeforeTimeoutMinutes < InactivityTimeoutMinutes;
        }
    }
}
