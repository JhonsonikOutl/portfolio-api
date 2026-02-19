using Portfolio.Application.DTOs.Auth;

namespace Portfolio.Application.Interfaces.Services
{
    /// <summary>
    /// Servicio de autenticación.
    /// Maneja login, registro y generación de tokens JWT.
    /// </summary>
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        string GenerateJwtToken(string userId, string username, string role);
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}