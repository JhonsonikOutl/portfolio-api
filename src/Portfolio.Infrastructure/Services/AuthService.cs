using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.DTOs.Auth;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Portfolio.Infrastructure.Services
{
    /// <summary>
    /// Servicio de autenticación.
    /// Maneja login, registro y generación de tokens JWT.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                return null;
            }

            var token = GenerateJwtToken(user.Id.ToString(), user.Username, user.Role);

            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(registerDto.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("El usuario ya existe");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password),
                Role = "Admin"
            };

            await _userRepository.CreateAsync(user);

            var token = GenerateJwtToken(user.Id.ToString(), user.Username, user.Role);

            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username,
                Email = user.Email
            };
        }

        public string GenerateJwtToken(string userId, string username, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var secretKey = _configuration["JwtSettings__Secret"]
                ?? _configuration["JwtSettings:Secret"]
                ?? jwtSettings["SecretKey"]
                ?? jwtSettings["Secret"]
                ?? throw new InvalidOperationException("JWT Secret no configurada");

            if (secretKey.Length < 32)
            {
                throw new InvalidOperationException(
                    $"JWT Secret debe tener al menos 32 caracteres. Actual: {secretKey.Length}");
            }

            var issuer = _configuration["JwtSettings__Issuer"]
                ?? jwtSettings["Issuer"]
                ?? "PortfolioAPI";

            var audience = _configuration["JwtSettings__Audience"]
                ?? jwtSettings["Audience"]
                ?? "PortfolioWeb";

            var expirationMinutes = int.Parse(
                _configuration["JwtSettings__ExpirationMinutes"]
                ?? jwtSettings["ExpirationMinutes"]
                ?? "60");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}