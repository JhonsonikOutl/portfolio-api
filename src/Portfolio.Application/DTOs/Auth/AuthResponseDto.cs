namespace Portfolio.Application.DTOs.Auth
{
    /// <summary>
    /// DTO de respuesta después de login exitoso.
    /// Contiene el token JWT y datos básicos del usuario.
    /// </summary>
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
