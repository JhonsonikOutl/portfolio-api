namespace Portfolio.Application.DTOs.Auth
{
    /// <summary>
    /// DTO para registro de usuarios (solo en desarrollo).
    /// </summary>
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
